//
//  ContainerView.h
//  Community Gaming Controller
//
//  Created by Wojtek Frątczak on 09/11/15.
//  Copyright © 2015 iLabs. All rights reserved.
//

#import <UIKit/UIKit.h>

enum {
    ContainerViewTypeMovement = 1,
    ContainerViewTypeRotation = 2
}; typedef NSUInteger ContainerViewType;

@interface ContainerView : UIView

@property ContainerViewType containerViewType;

@end
