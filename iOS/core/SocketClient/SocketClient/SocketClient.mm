//
//  SocketClient.m
//  SocketClient
//
//  Created by Jan Seredynski on 04/10/15.
//  Copyright © 2015 Jan Seredynski. All rights reserved.
//

#import "SocketClient.h"
#import "BaseSerializable.h"
#import "UDPEcho.h"

@interface SocketClient()
{
    OutputSerializer *_outputSerializer;
}

@property (nonatomic, strong) NSInputStream *inputStream;
@property (nonatomic, strong) NSOutputStream *outputStream;
@property (nonatomic, strong) UDPEcho *udpObj;
@property (nonatomic, weak) id<SocketClientDelegate> delegate;

@end


@implementation SocketClient

-(void)dealloc
{
    [self.inputStream close];
    [self.outputStream close];
}

-(id)initWithHost:(NSString*)hostName port:(int)port delegate:(id<SocketClientDelegate>)delegate {
    self = [super init];
    self.delegate = delegate;
    CFReadStreamRef readStream;
    CFWriteStreamRef writeStream;
    
    CFStreamCreatePairWithSocketToHost(NULL, (__bridge CFStringRef)hostName, port, &readStream, &writeStream);
    
    self.inputStream = (__bridge_transfer NSInputStream *)readStream;
    self.outputStream = (__bridge_transfer NSOutputStream *)writeStream;
    [self.inputStream setDelegate:self];
    [self.outputStream setDelegate:self];
    [self.inputStream  scheduleInRunLoop:[NSRunLoop currentRunLoop] forMode:NSDefaultRunLoopMode];
    [self.outputStream scheduleInRunLoop:[NSRunLoop currentRunLoop] forMode:NSDefaultRunLoopMode];
    [self.inputStream open];
    [self.outputStream open];
    _outputSerializer=new OutputSerializer(self.outputStream);
    
    // Create UDP client
    self.udpObj = [[UDPEcho alloc] init];
    [self.udpObj startConnectedToHostName:hostName port:5557];
    
    return self;
}


-(void)sendObject:(id<BaseSerializable>)serializer
{
    [serializer writeToStream:self];
}

-(void)sendUdpObject:(NSData*)data
{
    [self.udpObj sendData:data];
}


- (void)stream:(NSStream *)theStream handleEvent:(NSStreamEvent)streamEvent
{
    if(theStream == self.inputStream)
    switch (streamEvent) {
            
        case NSStreamEventOpenCompleted:
            NSLog(@"\nStream opened");
            [self.delegate clientSocketDidConnectToServer];
            break;
            
        case NSStreamEventHasBytesAvailable:
            
            if (theStream == self.inputStream) {
                
                static uint8_t buffer[4];
                static int len;
                while ([self.inputStream hasBytesAvailable]) {
                    len += [self.inputStream read:buffer maxLength:sizeof(buffer)];
                    if (len == 4) {
                        [self.delegate clientSocketDidReceivedCode:(int)(buffer)];
                        len = 0;
                        NSLog(@"\nServer received: %d code", (int)(buffer));
                       
                        
                        
                    }
                }
            }
            break;
            
            
        case NSStreamEventErrorOccurred:
            
            NSLog(@"\nCannot connect to the host!");
            [self.delegate clientSocketEncounteredErrorConnectingToServer];
            break;
            
        case NSStreamEventEndEncountered:
            
            [theStream close];
            [theStream removeFromRunLoop:[NSRunLoop currentRunLoop] forMode:NSDefaultRunLoopMode];
            theStream = nil;
            NSLog(@"\nServer disconnected !");
            [self.delegate clientSocketDidDisconnectFromServer];
            
            break;
        default:
            NSLog(@"\nUnknown event");
    }
    
}

-(OutputSerializer*)getOutputSerializer
{
    return _outputSerializer;
}






@end
