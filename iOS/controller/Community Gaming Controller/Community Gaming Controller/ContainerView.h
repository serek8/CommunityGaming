//
//  ContainerView.h
//  Community Gaming Controller
//
//  Created by Wojtek Frątczak on 09/11/15.
//  Copyright © 2015 iLabs. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "WarriorsSerializer.h"

@interface ContainerView : UIView

@property (weak, nonatomic) IBOutlet UIImageView *pointFollower;
@property (weak, nonatomic) IBOutlet UIView *centerPoint;

- (int)angleToPoint:(CGPoint)tapPoint;

@end
