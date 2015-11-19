//
//  ViewController.m
//  Community Gaming Controller
//
//  Created by Wojtek Frątczak on 09/11/15.
//  Copyright © 2015 iLabs. All rights reserved.
//

#import "ViewController.h"
#import "WarriorsSerializer.h"

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
    NSInteger numberOfTouches = [event.allTouches count];
    NSLog(@"Touches Began");
    
    switch (numberOfTouches) {
        case 1:
        {
            if ([[event touchesForView:self.movementContainerView] count] > 0) {
                UITouch *movementTouch = [[event touchesForView:self.movementContainerView] anyObject];
                CGPoint movementPoint = [movementTouch locationInView:self.movementContainerView];
                self.movementContainerView.pointFollower.center = movementPoint;
                int angle = [self.movementContainerView angleToPoint:movementPoint];
                NSLog([NSString stringWithFormat:@"Movement touch began: %i",angle]);
                [self movementDidChange:angle];
            }
            
            if ([[event touchesForView:self.rotationContainerView] count] > 0) {
                UITouch *rotationTouch = [[event touchesForView:self.rotationContainerView] anyObject];
                CGPoint rotationPoint = [rotationTouch locationInView:self.rotationContainerView];
                self.rotationContainerView.pointFollower.center = rotationPoint;
                int angle = [self.rotationContainerView angleToPoint:rotationPoint];
                NSLog([NSString stringWithFormat:@"Rotation touch began: %i",angle]);
                [self rotationDidChange:angle];
            }
        }
            break;
        case 2:
        {
            if (([[event touchesForView:self.movementContainerView] count] > 0 ) && ([[event touchesForView:self.rotationContainerView] count] > 0)) {
                
                UITouch *movementTouch = [[event touchesForView:self.movementContainerView] anyObject];
                
                //self.tempTimestampForDubleTouch = movementTouch.timestamp;
                
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
                NSLog([NSString stringWithFormat:@"Rotation touch began: %i",angle]);
                [self rotationDidChange:angle];
            }
            
            else if ([[event touchesForView:self.movementContainerView] count] > 0) {
                UITouch *movementTouch = [[event touchesForView:self.movementContainerView] anyObject];
                CGPoint movementPoint = [movementTouch locationInView:self.movementContainerView];
                self.movementContainerView.pointFollower.center = movementPoint;
                int angle = [self.movementContainerView angleToPoint:movementPoint];
                NSLog([NSString stringWithFormat:@"Movement touch began: %i",angle]);
                [self movementDidChange:angle];
            }
            
        }
            break;
        default:
            break;
    }
    
}

-(void)touchesMoved:(NSSet<UITouch *> *)touches withEvent:(UIEvent *)event{
    NSInteger numberOfTouches = [event.allTouches count];
    
    switch (numberOfTouches) {
        case 1:
        {
            if ([[event touchesForView:self.movementContainerView] count] > 0) {
                UITouch *movementTouch = [[event touchesForView:self.movementContainerView] anyObject];
                CGPoint movementPoint = [movementTouch locationInView:self.movementContainerView];
                self.movementContainerView.pointFollower.center = movementPoint;
                int angle = [self.movementContainerView angleToPoint:movementPoint];
                NSLog([NSString stringWithFormat:@"Movement: %i",angle]);
                [self movementDidChange:angle];
            }
            
            if ([[event touchesForView:self.rotationContainerView] count] > 0) {
                UITouch *rotationTouch = [[event touchesForView:self.rotationContainerView] anyObject];
                CGPoint rotationPoint = [rotationTouch locationInView:self.rotationContainerView];
                self.rotationContainerView.pointFollower.center = rotationPoint;
                int angle = [self.rotationContainerView angleToPoint:rotationPoint];
                NSLog([NSString stringWithFormat:@"Rotation: %i",angle]);
                [self rotationDidChange:angle];
            }
        }
            break;
        case 2:
        {
            if (([[event touchesForView:self.movementContainerView] count] > 0 ) && ([[event touchesForView:self.rotationContainerView] count] > 0)) {
                
                UITouch *movementTouch = [[event touchesForView:self.movementContainerView] anyObject];
                
                //self.tempTimestampForDubleTouch = movementTouch.timestamp;
                
                CGPoint movementPoint = [movementTouch locationInView:self.movementContainerView];
                self.movementContainerView.pointFollower.center = movementPoint;
                int movAngle = [self.movementContainerView angleToPoint:movementPoint];
                
                UITouch *rotationTouch = [[event touchesForView:self.rotationContainerView] anyObject];
                CGPoint rotationPoint = [rotationTouch locationInView:self.rotationContainerView];
                self.rotationContainerView.pointFollower.center = rotationPoint;
                int rotAngle = [self.rotationContainerView angleToPoint:rotationPoint];
                
                if (!self.dataDidSend) {
                    [self movementDidChange:movAngle andRotationDidChange:rotAngle];
                    NSLog([NSString stringWithFormat:@"Rotation: %i Movement: %i",rotAngle,movAngle]);
                }
                self.dataDidSend = !self.dataDidSend;
                
                
                
            }
            
            else if ([[event touchesForView:self.rotationContainerView] count] > 0) {
                UITouch *rotationTouch = [[event touchesForView:self.rotationContainerView] anyObject];
                CGPoint rotationPoint = [rotationTouch locationInView:self.rotationContainerView];
                self.rotationContainerView.pointFollower.center = rotationPoint;
                int angle = [self.rotationContainerView angleToPoint:rotationPoint];
                NSLog([NSString stringWithFormat:@"Rotation: %i",angle]);
                [self rotationDidChange:angle];
            }
            
            else if ([[event touchesForView:self.movementContainerView] count] > 0) {
                UITouch *movementTouch = [[event touchesForView:self.movementContainerView] anyObject];
                CGPoint movementPoint = [movementTouch locationInView:self.movementContainerView];
                self.movementContainerView.pointFollower.center = movementPoint;
                int angle = [self.movementContainerView angleToPoint:movementPoint];
                NSLog([NSString stringWithFormat:@"Movement: %i",angle]);
                [self movementDidChange:angle];
            }
            
        }
            break;
        default:
            break;
    }
}

- (void)touchesEnded:(NSSet<UITouch *> *)touches withEvent:(UIEvent *)event{
    NSLog(@"Touches Ended");
}

- (void)touchesCancelled:(NSSet<UITouch *> *)touches withEvent:(UIEvent *)event{
    NSLog(@"Touches Cancelled");
}

#pragma mark - Helpers

- (void)rotationDidChange:(int)rotation{
    self.war.warriorRoatation = rotation;
    self.war.warriorMovement = -1;
    [self.socketClient sendObject:self.war];
}

- (void)movementDidChange:(int)movement{
    self.war.warriorMovement = movement;
    self.war.warriorRoatation = -1;
    [self.socketClient sendObject:self.war];
}

- (void)movementDidChange:(int)movement andRotationDidChange:(int)rotation {
    self.war.warriorMovement = movement;
    self.war.warriorRoatation = rotation;
    [self.socketClient sendObject:self.war];
}

@end
