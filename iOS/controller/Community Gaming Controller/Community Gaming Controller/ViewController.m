//
//  ViewController.m
//  Community Gaming Controller
//
//  Created by Wojtek Frątczak on 09/11/15.
//  Copyright © 2015 iLabs. All rights reserved.
//

#import "ViewController.h"
#import "ContainerView.h"

@interface ViewController ()

@property (strong, nonatomic) ContainerView *leftContainerView;
@property (strong, nonatomic) ContainerView *rightContainerView;

@end

@implementation ViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    [self configureContainerViews];
    
//    self.imageView = [[UIImageView alloc]initWithImage:[UIImage imageNamed:@"ball"]];
//    self.imageView.center = self.view.center;
//    [self.view addSubview:self.imageView];
//    Do any additional setup after loading the view, typically from a nib.
}

- (void)configureContainerViews{
    self.leftContainerView = [[[NSBundle mainBundle] loadNibNamed:@"ContainerView" owner:self options:nil] lastObject];
    self.leftContainerView.containerViewType = ContainerViewTypeMovement;
    self.leftContainerView.frame = CGRectMake(0, 0, self.view.frame.size.width / 2 - 1, self.view.frame.size.height);
    [self.view addSubview:self.leftContainerView];

    self.rightContainerView = [[[NSBundle mainBundle] loadNibNamed:@"ContainerView" owner:self options:nil] lastObject];
    self.rightContainerView.containerViewType = ContainerViewTypeRotation;
    self.rightContainerView.frame = CGRectMake(self.view.frame.size.width / 2 + 1, 0, self.view.frame.size.width/2 - 1, self.view.frame.size.height);
    [self.view addSubview:self.rightContainerView];
}



@end
