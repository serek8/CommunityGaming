//
//  ViewController.m
//  SocketClient
//
//  Created by Jan Seredynski on 04/10/15.
//  Copyright © 2015 Jan Seredynski. All rights reserved.
//

#import "ViewController.h"
#import "OutputSerializer.h"
#import "WarriorsSerializer.h"

@interface ViewController () 
@property (weak, nonatomic) IBOutlet UITextField *textFieldRead;
@property (weak, nonatomic) IBOutlet UITextField *textField;
@property (strong, nonatomic) SocketClient *socketClient;
@property (weak, nonatomic) IBOutlet UILabel *statusLabel;

@end

@implementation ViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    
    // Do any additional setup after loading the view, typically from a nib.
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}


-(void)viewDidAppear:(BOOL)animated
{
    [super viewDidAppear:animated];
}

- (IBAction)sendButton:(UIButton *)sender
{
    
}

- (IBAction)readButton:(UIButton *)sender
{
    //[self.textField setText:[self.socketClient readText]];
}

-(void)clientSocketDidConnectToServer
{
    [self.statusLabel setText:@"Connected"];
}
-(void)clientSocketDidDisconnectFromServer
{
    [self.statusLabel setText:@"Disonnected"];
}
-(void)clientSocketEncounteredErrorConnectingToServer
{
    [self.statusLabel setText:@"Error"];
}
-(void)clientSocketDidReceivedData:(uint8_t *)data numberOfReadBytes:(int)dataSize;
{
    [self.textFieldRead setText:[NSString stringWithUTF8String:(const char*)data]];
    NSLog(@"Otrzymuje:%s", (const char*)data);
    [self.textField resignFirstResponder];
}
- (IBAction)connectButton:(UIButton *)sender
{
    //localhost
    //192.168.0.104
    self.socketClient = [[SocketClient alloc] initWithHost:@"localhost" port:5555 delegate:self];
    //[sender setUserInteractionEnabled:NO];
}

- (IBAction)sendWithSerializer:(UIButton *)sender
{
    WarriorsSerializer *war = [[WarriorsSerializer alloc] init];
    war.warriorMovement = 1;
    war.warriorRoatation = 2;
    war.warriorAction = 3;
    [self.socketClient sendObject:war];

}
@end





