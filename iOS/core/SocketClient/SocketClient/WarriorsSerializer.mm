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
//    (*[socketClient getOutputSerializer])<<_warriorMovement;
//    (*[socketClient getOutputSerializer])<<_warriorRoatation;
//    (*[socketClient getOutputSerializer])<<_warriorAction;
    
    [self sendAsUdpToServer:socketClient];
}
//
-(void)sendAsUdpToServer:(SocketClient*)socketClient
{

    static char tab[12];
    
    for(int i=0; i<4; i++) tab[i]=((char*)(&(_warriorMovement)))[i];
    for(int i=0; i<4; i++) tab[i+4]=((char*)(&(_warriorRoatation)))[i];
    for(int i=0; i<4; i++) tab[i+8]=((char*)(&(_warriorAction)))[i];
    [socketClient sendUdpObject:[NSData dataWithBytes:tab length:12]];
}


-(id)receiveFromStream:(SocketClient*)socketClient{return nil;}




@end
