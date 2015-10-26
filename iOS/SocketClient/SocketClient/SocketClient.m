//
//  SocketClient.m
//  SocketClient
//
//  Created by Jan Seredynski on 04/10/15.
//  Copyright © 2015 Jan Seredynski. All rights reserved.
//

#import "SocketClient.h"

@interface SocketClient()
{}


@property (nonatomic, weak) id<SocketClientDelegate> delegate;

@end


@implementation SocketClient

-(void)dealloc
{
    [self.inputStream close];
    [self.outputStream close];
}

-(id)initWithHost:(NSString*)hostName port:(int)port delegate:(id<SocketClientDelegate>)delegate
{
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
    //[self.outputStream scheduleInRunLoop:[NSRunLoop currentRunLoop] forMode:NSDefaultRunLoopMode];
    [self.inputStream open];
    [self.outputStream open];
    return self;
}

-(void)sendText:(NSString*)text
{
    NSString *str = [NSString stringWithFormat:@"%@", text];
    NSLog(@"\nSocketClient: Data buffered:%d, | Data sent:%d",[str length], [self.outputStream write:((const uint8_t*)[str UTF8String]) maxLength:[str length]]);
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
                
                uint8_t buffer[BUFFER_SIZE];
                int len;
                while ([self.inputStream hasBytesAvailable]) {
                    len = [self.inputStream read:buffer maxLength:sizeof(buffer)];
                    if (len > 0) {
                        if(len<BUFFER_SIZE) buffer[len]=0x00;
                        NSLog(@"\nServer received: %d bytes", len);
                        //NSLog(@"\nServer said: %@", output);
                        [self.delegate clientSocketDidReceivedData:buffer numberOfReadBytes:len];
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








@end
