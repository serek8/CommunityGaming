//
//  SocketClient.h
//  SocketClient
//
//  Created by Jan Seredynski on 04/10/15.
//  Copyright © 2015 Jan Seredynski. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "OutputSerializer.h"
#import "BaseSerializable.h"

#define BUFFER_SIZE 128

@protocol SocketClientDelegate <NSObject>

@required

-(void)clientSocketDidConnectToServer;
-(void)clientSocketEncounteredErrorConnectingToServer;
-(void)clientSocketDidDisconnectFromServer;
-(void)clientSocketDidReceivedData:(uint8_t *)data numberOfReadBytes:(int)dataSize;



@end

@protocol BaseSerializable;
@interface SocketClient : NSObject <NSStreamDelegate>




-(id)initWithHost:(NSString*)hostName port:(int)port delegate:(id<SocketClientDelegate>)delegate;
-(void)sendObject:(id<BaseSerializable>)serializer;

-(OutputSerializer*)getOutputSerializer;

@end
