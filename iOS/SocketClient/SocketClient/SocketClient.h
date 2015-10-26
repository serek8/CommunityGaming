//
//  SocketClient.h
//  SocketClient
//
//  Created by Jan Seredynski on 04/10/15.
//  Copyright © 2015 Jan Seredynski. All rights reserved.
//

#import <Foundation/Foundation.h>

#define BUFFER_SIZE 128

@protocol SocketClientDelegate <NSObject>

@required

-(void)clientSocketDidConnectToServer;
-(void)clientSocketEncounteredErrorConnectingToServer;
-(void)clientSocketDidDisconnectFromServer;
-(void)clientSocketDidReceivedData:(uint8_t *)data numberOfReadBytes:(int)dataSize;



@end


@interface SocketClient : NSObject <NSStreamDelegate>

@property (nonatomic, strong) NSInputStream *inputStream;
@property (nonatomic, strong) NSOutputStream *outputStream;


-(id)initWithHost:(NSString*)hostName port:(int)port delegate:(id<SocketClientDelegate>)delegate;
-(void)sendText:(NSString*)text; // It automatically ads new line sign at the end of the string


@end
