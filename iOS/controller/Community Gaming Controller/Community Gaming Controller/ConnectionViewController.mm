//
//  ConnectionViewController.m
//  Community Gaming Controller
//
//  Created by Wojtek Frątczak on 10/11/15.
//  Copyright © 2015 iLabs. All rights reserved.
//

#import "ConnectionViewController.h"
#import "ViewController.h"

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
    [self performSegueWithIdentifier:@"controller_segue" sender:self];
}

-(void)clientSocketDidDisconnectFromServer{
    NSLog(@"Disconnected");
    self.connectionStatusLabel.text = @"Connetion status: Disconnected";
}

-(void)clientSocketEncounteredErrorConnectingToServer{
    NSLog(@"Error");
    self.connectionStatusLabel.text = @"Connetion status: Error";
}

- (void)clientSocketDidReceivedData:(uint8_t *)data numberOfReadBytes:(int)dataSize{
    
}

#pragma mark - Segue

- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender{
    ViewController *vc = (ViewController*)[segue destinationViewController];
    vc.socketClient = self.socketClient;
}

@end
