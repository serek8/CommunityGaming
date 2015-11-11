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

@property (strong, nonatomic) ContainerView *leftContainerView;
@property (strong, nonatomic) ContainerView *rightContainerView;
@property (strong, nonatomic) WarriorsSerializer *war;

@end

@implementation ViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    //self.socketClient = [[SocketClient alloc] initWithHost:@"localhost" port:5555 delegate:self];
    [self configureContainerViews];
    self.war = [[WarriorsSerializer alloc] init];
}

- (void)configureContainerViews{
    self.leftContainerView = [[[NSBundle mainBundle] loadNibNamed:@"ContainerView" owner:self options:nil] lastObject];
    self.leftContainerView.containerViewType = ContainerViewTypeMovement;
    self.leftContainerView.delegate = self;
    self.leftContainerView.frame = CGRectMake(0, 0, self.view.frame.size.width / 2 - 1, self.view.frame.size.height);
    [self.view addSubview:self.leftContainerView];

    self.rightContainerView = [[[NSBundle mainBundle] loadNibNamed:@"ContainerView" owner:self options:nil] lastObject];
    self.rightContainerView.containerViewType = ContainerViewTypeRotation;
    self.rightContainerView.delegate = self;
    self.rightContainerView.frame = CGRectMake(self.view.frame.size.width / 2 + 1, 0, self.view.frame.size.width/2 - 1, self.view.frame.size.height);
    [self.view addSubview:self.rightContainerView];
}

#pragma mark - ContainerViewDelegate

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
