//
//  WarriorsSerializer.m
//  SocketClient
//
//  Created by Jan Seredynski on 28/10/15.
//  Copyright © 2015 Jan Seredynski. All rights reserved.
//

#import "WarriorsSerializer.h"
#import "SocketClient.h"



@implementation WarriorsSerializer


-(void)writeToStream:(SocketClient*)socketClient
{
    (*[socketClient getOutputSerializer])<<_warriorMovement;
    (*[socketClient getOutputSerializer])<<_warriorRoatation;
    (*[socketClient getOutputSerializer])<<_warriorAction;
}
-(id)receiveFromStream:(SocketClient*)socketClient{return nil;}




@end
