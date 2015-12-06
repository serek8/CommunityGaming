//
//  ConnectionViewController.m
//  Community Gaming Controller
//
//  Created by Wojtek Frątczak on 10/11/15.
//  Copyright © 2015 iLabs. All rights reserved.
//

#import "ConnectionViewController.h"
#import "WarriorsSerializer.h"


@interface ConnectionViewController ()

@property (strong, nonatomic) SocketClient *socketClient;
@property (weak, nonatomic) IBOutlet UIButton *connectButton;
@property (weak, nonatomic) IBOutlet UITextField *IPTextField;
@property (weak, nonatomic) IBOutlet UILabel *connectionStatusLabel;

@end

@implementation ConnectionViewController

- (void)viewDidLoad {
    [super viewDidLoad];
}

- (IBAction)connectAction:(id)sender {
    NSString *IPAdress = self.IPTextField.text;
    self.socketClient = [[SocketClient alloc] initWithHost:IPAdress port:5555 delegate:self];
}

#pragma mark - SocketClientDelegate

-(void)clientSocketDidConnectToServer{
    NSLog(@"Connected");
    self.connectionStatusLabel.text = @"Connetion status: Connected";
    
}

-(void)clientSocketDidDisconnectFromServer{
    NSLog(@"Disconnected");
    [self dismissViewControllerAnimated:YES completion:nil];
    self.connectionStatusLabel.text = @"Connetion status: Disconnected";
}

-(void)clientSocketEncounteredErrorConnectingToServer{
    NSLog(@"Error");
    self.connectionStatusLabel.text = @"Connetion status: Error";
}

- (void)clientSocketDidReceivedData:(uint8_t *)data numberOfReadBytes:(int)dataSize{
    
}


- (IBAction)sendSAmpleButton:(UIButton *)sender
{
    WarriorsSerializer *war = [[WarriorsSerializer alloc] init];
    
    war.warriorMovement = 5;
    war.warriorRoatation = 6;
    war.warriorAction = 7;

    [war sendAsUdpToServer:self.socketClient];
//    char tab[12];
//    for(int i=0; i<4; i++) tab[i]=((char*)(&(a)))[i];
//    //[self.socketClient sendObject:war];
//    NSData *data = [NSData dataWithBytes:tab length:12];
//    [self.socketClient sendUdpObject:data];
}


#pragma mark - Segue



@end
