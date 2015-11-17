//
//  ContainerView.m
//  Community Gaming Controller
//
//  Created by Wojtek Frątczak on 09/11/15.
//  Copyright © 2015 iLabs. All rights reserved.
//

#import "ContainerView.h"

@implementation ContainerView

- (void)awakeFromNib{
    //self.pointFollower.center = self.center;
}

- (int)angleToPoint:(CGPoint)tapPoint{
    int x = self.centerPoint.center.x;
    int y = self.centerPoint.center.y;
    int dx = tapPoint.x - x;
    int dy = tapPoint.y - y;
    float radians = atan2(dy,dx); // in radians
    float degrees = radians * 180 / M_PI; // in degrees

    if (degrees < 0) return (int)fabsf(degrees);
    else return (int)(360 - degrees);
}

@end
