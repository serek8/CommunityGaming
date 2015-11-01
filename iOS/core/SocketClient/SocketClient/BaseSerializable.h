//
//  BaseSerializer.h
//  SocketClient
//
//  Created by Jan Seredynski on 28/10/15.
//  Copyright © 2015 Jan Seredynski. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "SocketClient.h"

@class SocketClient;
@protocol BaseSerializable <NSObject>

@required

-(void)writeToStream:(SocketClient*)socketClient;
-(id)receiveFromStream:(SocketClient*)socketClient;

@end


