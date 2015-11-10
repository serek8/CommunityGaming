//
//  ContainerView.h
//  Community Gaming Controller
//
//  Created by Wojtek Frątczak on 09/11/15.
//  Copyright © 2015 iLabs. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "WarriorsSerializer.h"

enum {
    ContainerViewTypeMovement = 1,
    ContainerViewTypeRotation = 2
}; typedef NSUInteger ContainerViewType;

@protocol ContainerViewDelegate <NSObject>

- (void)rotationDidChange:(int)rotation;
- (void)movementDidChange:(int)movement;

@end

@interface ContainerView : UIView

@property ContainerViewType containerViewType;
@property (nonatomic, weak) id <ContainerViewDelegate> delegate;

@end
