//
//  ContainerView.m
//  Community Gaming Controller
//
//  Created by Wojtek Frątczak on 09/11/15.
//  Copyright © 2015 iLabs. All rights reserved.
//

#import "ContainerView.h"
#import "SocketClient.h"

@interface ContainerView ()

@property (weak, nonatomic) IBOutlet UIImageView *pointFollower;
@property (weak, nonatomic) IBOutlet UIView *centerPoint;

@end


@implementation ContainerView

//- (void)configurePointFollower{
//    UIImageView *pointFollower = [[UIImageView alloc] initWithImage:[UIImage imageNamed:@"ball"]];
//    pointFollower.center = self.center;
//    [self addSubview:pointFollower];
//}

- (void)touchesBegan:(NSSet<UITouch *> *)touches withEvent:(UIEvent *)event{

    UITouch *touch = [[event touchesForView:self] anyObject];
    CGPoint point = [touch locationInView:self];
    self.pointFollower.center = point;
    int angle = [self angleToPoint:point];
    if (self.containerViewType == ContainerViewTypeMovement) {
        NSLog([NSString stringWithFormat:@"Movement touch began: %i",angle]);
        [self.delegate movementDidChange:angle];
    }
    else if (self.containerViewType == ContainerViewTypeRotation){
        NSLog([NSString stringWithFormat:@"Rotation touch began: %i",angle]);
        [self.delegate rotationDidChange:angle];
    }
    
    //self.imageView.center = point;
}

- (void)touchesEnded:(NSSet<UITouch *> *)touches withEvent:(UIEvent *)event{
    NSLog(@"Touch end");
    self.pointFollower.center = self.centerPoint.center;
    //self.imageView.center = point;
}

- (void)touchesCancelled:(NSSet<UITouch *> *)touches withEvent:(UIEvent *)event{
    NSLog(@"Touch cancel");
}

- (void)touchesMoved:(NSSet<UITouch *> *)touches withEvent:(UIEvent *)event{
    UITouch *touch = [[event touchesForView:self] anyObject];
    CGPoint point = [touch locationInView:self];
    self.pointFollower.center = point;
    int angle = [self angleToPoint:point];
    if (self.containerViewType == ContainerViewTypeMovement) {
        NSLog([NSString stringWithFormat:@"Movement: %i",angle]);
        [self.delegate movementDidChange:angle];
    }
    else if (self.containerViewType == ContainerViewTypeRotation){
        NSLog([NSString stringWithFormat:@"Rotation: %i",angle]);
        [self.delegate rotationDidChange:angle];
    }
    //self.imageView.center = point;
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
