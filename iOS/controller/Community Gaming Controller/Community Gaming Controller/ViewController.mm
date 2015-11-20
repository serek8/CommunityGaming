//
//  ViewController.m
//  Community Gaming Controller
//
//  Created by Wojtek Frątczak on 09/11/15.
//  Copyright © 2015 iLabs. All rights reserved.
//

#import "ViewController.h"
#import "WarriorsSerializer.h"

#define ShootAction 1

@interface ViewController () 

@property (weak, nonatomic) IBOutlet ContainerView *movementContainerView;
@property (weak, nonatomic) IBOutlet ContainerView *rotationContainerView;

@property (strong, nonatomic) WarriorsSerializer *war;

@property NSTimeInterval tempTimestampForDubleTouch;

@property BOOL dataDidSend;

@end

@implementation ViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    self.war = [[WarriorsSerializer alloc] init];
    
    [self addGestureRecognizerForShoot];
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

#pragma mark - UIResponder

- (void)touchesBegan:(NSSet<UITouch *> *)touches withEvent:(UIEvent *)event{
    [self handleWithEvent:(UIEvent*)event];
}

-(void)touchesMoved:(NSSet<UITouch *> *)touches withEvent:(UIEvent *)event{
    [self handleWithEvent:event];
}

- (void)touchesEnded:(NSSet<UITouch *> *)touches withEvent:(UIEvent *)event{
    NSLog(@"Touches Ended");
}

- (void)touchesCancelled:(NSSet<UITouch *> *)touches withEvent:(UIEvent *)event{
    NSLog(@"Touches Cancelled");
}

#pragma mark - Helpers

- (void)addGestureRecognizerForShoot{
    UITapGestureRecognizer *tapGesture = [[UITapGestureRecognizer alloc] initWithTarget:self action:@selector(handleShootGesture:)];
    tapGesture.numberOfTapsRequired = 2;
    [self.rotationContainerView addGestureRecognizer:tapGesture];
}

- (void)handleShootGesture:(UITapGestureRecognizer *)sender {
    CGPoint rotationPoint = [sender locationInView:self.rotationContainerView];
    self.rotationContainerView.pointFollower.center = rotationPoint;
    int angle = [self.rotationContainerView angleToPoint:rotationPoint];
    [self rotationDidChange:angle withAction: ShootAction];
}

- (void)rotationDidChange:(int)rotation withAction:(int)action{
    [self sendObjectWithMovement:-1 rotation:rotation speed:3 action:action];
}

- (void)rotationDidChange:(int)rotation{
    [self sendObjectWithMovement:-1 rotation:rotation speed:3 action:0];
}

- (void)movementDidChange:(int)movement{
    [self sendObjectWithMovement:movement rotation:-1 speed:3 action:0];
}

- (void)movementDidChange:(int)movement andRotationDidChange:(int)rotation {
    [self sendObjectWithMovement:movement rotation:rotation speed:3 action:0];
}

- (void)sendObjectWithMovement:(int)movement rotation:(int)rotation speed:(int)speed action:(int)action{
    self.war.warriorRoatation = rotation;
    self.war.warriorSpeed = speed;
    self.war.warriorMovement = movement;
    self.war.warriorAction = action;
    [self.socketClient sendObject:self.war];
}

- (void)handleWithEvent:(UIEvent*)event{
    NSInteger numberOfTouches = [event.allTouches count];
    switch (numberOfTouches) {
        case 1:
        {
            [self handleWithSingleTouchForEvent:event];
        }
            break;
        case 2:
        {
            [self handleWithDoubleTouchForEvent:event];
        }
            break;
        default:
            break;
    }
}

- (void)handleWithSingleTouchForEvent:(UIEvent*)event{
    
    if ([[event touchesForView:self.movementContainerView] count] > 0) {
        UITouch *movementTouch = [[event touchesForView:self.movementContainerView] anyObject];
        CGPoint movementPoint = [movementTouch locationInView:self.movementContainerView];
        self.movementContainerView.pointFollower.center = movementPoint;
        int angle = [self.movementContainerView angleToPoint:movementPoint];
        [self movementDidChange:angle];
    }
    
    if ([[event touchesForView:self.rotationContainerView] count] > 0) {
        UITouch *rotationTouch = [[event touchesForView:self.rotationContainerView] anyObject];
        CGPoint rotationPoint = [rotationTouch locationInView:self.rotationContainerView];
        self.rotationContainerView.pointFollower.center = rotationPoint;
        int angle = [self.rotationContainerView angleToPoint:rotationPoint];
        [self rotationDidChange:angle];
    }
}

- (void)handleWithDoubleTouchForEvent:(UIEvent*)event{
    if (([[event touchesForView:self.movementContainerView] count] > 0 ) && ([[event touchesForView:self.rotationContainerView] count] > 0)) {
        
        UITouch *movementTouch = [[event touchesForView:self.movementContainerView] anyObject];
        CGPoint movementPoint = [movementTouch locationInView:self.movementContainerView];
        self.movementContainerView.pointFollower.center = movementPoint;
        int movAngle = [self.movementContainerView angleToPoint:movementPoint];
        
        UITouch *rotationTouch = [[event touchesForView:self.rotationContainerView] anyObject];
        CGPoint rotationPoint = [rotationTouch locationInView:self.rotationContainerView];
        self.rotationContainerView.pointFollower.center = rotationPoint;
        int rotAngle = [self.rotationContainerView angleToPoint:rotationPoint];
        
        if (!self.dataDidSend) {
            [self movementDidChange:movAngle andRotationDidChange:rotAngle];
        }
        self.dataDidSend = !self.dataDidSend;
    }
    
    else if ([[event touchesForView:self.rotationContainerView] count] > 0) {
        UITouch *rotationTouch = [[event touchesForView:self.rotationContainerView] anyObject];
        CGPoint rotationPoint = [rotationTouch locationInView:self.rotationContainerView];
        self.rotationContainerView.pointFollower.center = rotationPoint;
        int angle = [self.rotationContainerView angleToPoint:rotationPoint];
        [self rotationDidChange:angle];
    }
    
    else if ([[event touchesForView:self.movementContainerView] count] > 0) {
        UITouch *movementTouch = [[event touchesForView:self.movementContainerView] anyObject];
        CGPoint movementPoint = [movementTouch locationInView:self.movementContainerView];
        self.movementContainerView.pointFollower.center = movementPoint;
        int angle = [self.movementContainerView angleToPoint:movementPoint];
        [self movementDidChange:angle];
    }
}

@end
