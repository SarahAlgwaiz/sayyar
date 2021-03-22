//
//  SwiftFile_FP.swift
//  Sayyar
//
//  Created by Shahad Suliman on 06/08/1442 AH.
//

import Foundation
import DeviceCheck

@objc public class SwiftFile_FP : NSObject {
    
    @objc public static let shared = SwiftFile_FP()
    @objc public func fingerprintMethodInSwift(){

        if DCDevice.current.isSupported { // Always test for availability.
            DCDevice.current.generateToken { token, error in
                guard error == nil else { return}
                print("ttttttooooooookkkkkkkeeeeeennnnn!!!!!!!!!!!!@@@@@@@@@@@@@")
                var dt = token?.map{String(format: "%02.2hhx", $0)}.joined()
                print(dt)

                // Send the token to your server.
            }
        }
        
        print("It wooorrrrrrkkkk fiiinnnaaaallllyyyy !!!!!!!!!!!!@@@@@@@@@@@@@")
    }
    

    
}
