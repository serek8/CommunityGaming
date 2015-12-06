//
//  OutputSerializer.h
//  Contacts
//
//  Created by Jan Seredynski on 15/09/15.
//  Copyright (c) 2015 Jan Seredynski. All rights reserved.
//

#ifndef __Contacts__OutputSerializer__
#define __Contacts__OutputSerializer__



#include <iostream>
#include <type_traits>
#import <Foundation/Foundation.h>




class OutputSerializer
{
    template<bool> struct _TPODAdapter {};

public:

    //NSOutputStream *outputStream;
    
    OutputSerializer(NSOutputStream *outputStream);
    
    
    template <typename T>
    void operator << (const T& value)
    {
        save_object(value, _TPODAdapter<std::is_pod<T>::value>());
        //return stream_;
    }
    
private:
    
    template<typename T> void save_object(const T& value, _TPODAdapter<true>)
    {
        [outputStream_ write:(const uint8_t*)&value maxLength:sizeof(value)];
    }
    template<typename T> void save_object(const T& value, _TPODAdapter<false>)
    {
        std::cerr<<("Not Supported object");
    }
    



    NSOutputStream *outputStream_;
    
};



#endif /* defined(__Contacts__OutputSerializer__) */
