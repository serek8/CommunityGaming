//
//  ContainerView.m
//  Community Gaming Controller
//
//  Created by Wojtek Frątczak on 09/11/15.
//  Copyright © 2015 iLabs. All rights reserved.
//

#import "ContainerView.h"
#import "../../../core/SocketClient/SocketClient/SocketClient.h"

@interface ContainerView () <SocketClientDelegate>

@property (strong, nonatomic) SocketClient *socketClient;
@property (weak, nonatomic) IBOutlet UIImageView *pointFollower;
@property (weak, nonatomic) IBOutlet UIView *centerPoint;

@end


@implementation ContainerView

//- (void)configurePointFollower{
//    UIImageView *pointFollower = [[UIImageView alloc] initWithImage:[UIImage imageNamed:@"ball"]];
//    pointFollower.center = self.center;
//    [self addSubview:pointFollower];
//}

- (void)awakeFromNib{
    self.socketClient = [[SocketClient alloc] initWithHost:@"localhost" port:5555 delegate:self];
}

- (void)touchesBegan:(NSSet<UITouch *> *)touches withEvent:(UIEvent *)event{


    UITouch *touch = [[event touchesForView:self] anyObject];
    CGPoint point = [touch locationInView:self];
    self.pointFollower.center = point;
    CGFloat angle = [self angleToPoint:point];
    if (self.containerViewType == ContainerViewTypeMovement) {
        NSLog([NSString stringWithFormat:@"Movement touch began: %f",angle]);
    }
    else if (self.containerViewType == ContainerViewTypeRotation){
        NSLog([NSString stringWithFormat:@"Rotation touch began: %f",angle]);
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
    CGFloat angle = [self angleToPoint:point];
    if (self.containerViewType == ContainerViewTypeMovement) {
        NSLog([NSString stringWithFormat:@"Movement: %f",angle]);
    }
    else if (self.containerViewType == ContainerViewTypeRotation){
        NSLog([NSString stringWithFormat:@"Rotation: %f",angle]);
    }
    //self.imageView.center = point;
}

- (float)angleToPoint:(CGPoint)tapPoint{
    int x = self.centerPoint.center.x;
    int y = self.centerPoint.center.y;
    float dx = tapPoint.x - x;
    float dy = tapPoint.y - y;
    CGFloat radians = atan2(dy,dx); // in radians
    CGFloat degrees = radians * 180 / M_PI; // in degrees
    
    if (degrees < 0) return fabsf(degrees);
    else return 360 - degrees;
}

#pragma mark - SocketClientDelegate

-(void)clientSocketDidConnectToServer{
    NSLog(@"Connected");
}

-(void)clientSocketDidDisconnectFromServer{
    NSLog(@"Disconnected");
}

-(void)clientSocketEncounteredErrorConnectingToServer{
    NSLog(@"Error");
}

-(void)clientSocketDidReceivedData:(uint8_t *)data numberOfReadBytes:(int)dataSize;{
//    [self.textFieldRead setText:[NSString stringWithUTF8String:(const char*)data]];
//    NSLog(@"Otrzymuje:%s", (const char*)data);
}

@end
