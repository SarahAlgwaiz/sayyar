//
//  Objective-CFile_FP.m
//  Sayyar
//
//  Created by Shahad Suliman on 06/08/1442 AH.
//

#import <Foundation/Foundation.h>
#include "UnityFramework/UnityFramework-Swift.h"

extern "C" {
    
#pragma mark - Functions
    
 void _storeDeviceToken(const char* UID) {
     
     printf("%s", UID);

     NSString *UID2 = [[NSString alloc] initWithCString:(const char*)UID];

     [[SwiftFile_FP shared] storeDeviceTokenWithUID:(UID2)];


    }

void _getDeviceToken() {
    
    [[SwiftFile_FP shared] getDeviceToken];
      
   }


}

