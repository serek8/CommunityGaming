//
//  WarriorsSerializer.h
//  SocketClient
//
//  Created by Jan Seredynski on 28/10/15.
//  Copyright © 2015 Jan Seredynski. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "BaseSerializable.h"

@interface WarriorsSerializer : NSObject<BaseSerializable>

@property (atomic, assign) int warriorMovement;
@property (atomic, assign) int warriorRoatation;
@property (atomic, assign) int warriorAction;

@end
