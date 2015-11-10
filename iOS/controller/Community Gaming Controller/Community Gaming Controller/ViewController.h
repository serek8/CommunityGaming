//
//  ViewController.h
//  Community Gaming Controller
//
//  Created by Wojtek Frątczak on 09/11/15.
//  Copyright © 2015 iLabs. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "ContainerView.h"
#import "SocketClient.h"

@interface ViewController : UIViewController <ContainerViewDelegate, SocketClientDelegate>


@end

