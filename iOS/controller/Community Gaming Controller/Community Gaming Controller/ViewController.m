//
//  ViewController.m
//  Community Gaming Controller
//
//  Created by Wojtek Frątczak on 09/11/15.
//  Copyright © 2015 iLabs. All rights reserved.
//

#import "ViewController.h"

@interface ViewController ()

@property (strong, nonatomic) UIImageView *imageView;

@end

@implementation ViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    self.imageView = [[UIImageView alloc]initWithImage:[UIImage imageNamed:@"ball"]];
    self.imageView.center = self.view.center;
    [self.view addSubview:self.imageView];
    // Do any additional setup after loading the view, typically from a nib.
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void)touchesBegan:(NSSet<UITouch *> *)touches withEvent:(UIEvent *)event{
    NSLog(@"Touch began");
    
    UITouch *touch = [[event touchesForView:self.view] anyObject];
    CGPoint point = [touch locationInView:self.view];
    self.imageView.center = point;
}

- (void)touchesEnded:(NSSet<UITouch *> *)touches withEvent:(UIEvent *)event{
    NSLog(@"Touch end");
    UITouch *touch = [[event touchesForView:self.view] anyObject];
    CGPoint point = [touch locationInView:self.view];
    self.imageView.center = point;
}

- (void)touchesCancelled:(NSSet<UITouch *> *)touches withEvent:(UIEvent *)event{
    NSLog(@"Touch cancel");
}

- (void)touchesMoved:(NSSet<UITouch *> *)touches withEvent:(UIEvent *)event{
    NSLog(@"Touch moved");
    UITouch *touch = [[event touchesForView:self.view] anyObject];
    CGPoint point = [touch locationInView:self.view];
    self.imageView.center = point;
}

@end
