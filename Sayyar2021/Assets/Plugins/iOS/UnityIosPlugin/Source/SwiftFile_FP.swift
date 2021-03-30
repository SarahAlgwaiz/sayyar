//
//  SwiftFile_FP.swift
//  Sayyar
//
//  Created by Shahad Suliman on 06/08/1442 AH.
//

import Foundation
import Firebase
import LocalAuthentication;



@objc public class SwiftFile_FP : NSObject {
    @objc public static let shared = SwiftFile_FP()
    var deviceToken:String = "NONE"
    @objc public func storeDeviceToken(UID:String) {
        
        //Checking for Biometric availability
        let context = LAContext()
        var error: NSError?
        if context.canEvaluatePolicy(
            LAPolicy.deviceOwnerAuthenticationWithBiometrics,
            error: &error) {
            
            context.evaluatePolicy(
                LAPolicy.deviceOwnerAuthenticationWithBiometrics,
                localizedReason: "السماح باستخدام البصمة لتطبيق سيّار",
                reply: {(success, error) in
                    // Code to handle reply here
                    DispatchQueue.main.async {
                        
                        if let err = error {
                            switch err._code {
                            case LAError.Code.systemCancel.rawValue:
                                self.notifyUser("فشل السماح لتطبيق سيّار باستخدام بصمتك",
                                                err: err.localizedDescription)
                                
                            case LAError.Code.userCancel.rawValue:
                                self.notifyUser("الرجاء المحاولة مرة أخرى",
                                                err: err.localizedDescription)
                                
                            default:
                                self.notifyUser("حاول مرة أخرى",
                                                err: err.localizedDescription)
                            }
                            
                        } else {
                            self.notifyUser("تم التحقق بنجاح",
                                            err: "يمكنك الآن استخدام خاصية البصمة عند تسجيل الدخول على هذا الجهاز")
                            
                            var deviceToken:String? = ""
                            print("UUUUUUUUIIIIIIIIIDDDDDDDDD")
                            print(UID)
                            
                            //get device token and store as String
                            deviceToken = UIDevice.current.identifierForVendor?.uuidString
                            //Store Device token with user ID in data base
                            print("NNEEEWWW DEVIICEE TOOKKEEENN\(deviceToken)")
                            var ref: DatabaseReference! = Database.database().reference()
                            ref.child("FingerpintInfo").child(deviceToken!).setValue(["UID": UID])
                        }
                    }
                })
        } else {
            // Biometry is not available on the device
            // No hardware support or user has not set up biometric auth
            
            if let err = error {
                switch err.code {
                
                case LAError.Code.biometryNotEnrolled.rawValue:
                    notifyUser("هذا الجهاز لا يعمل بخاصية البصمة",
                               err: err.localizedDescription)
                    
                case LAError.Code.passcodeNotSet.rawValue:
                    notifyUser("هذا الجهاز لا يملك بصمة",
                               err: err.localizedDescription)
                    
                    
                case LAError.Code.biometryNotAvailable.rawValue:
                    notifyUser("هذا الجهاز لا يعمل بخاصية البصمة",
                               err: err.localizedDescription)
                default:
                    notifyUser("خطأ غير معروف",
                               err: err.localizedDescription)
                }
            }
        }
    }
    
    func notifyUser(_ msg: String, err: String?) {
        let alert = UIAlertController(title: msg,
                                      message: err,
                                      preferredStyle: .alert)
        
        let cancelAction = UIAlertAction(title: "OK",
                                         style: .cancel, handler: nil)
        
        alert.addAction(cancelAction)
        
    }
    
    //OTTHHHHEEEEEEERRRRR@#$%^&*()(*&^%$#@|___________________________________________________________________
    
    @objc public func getDeviceToken(){
        //Checking for Biometric availability
        let context = LAContext()
        var error: NSError?
        if context.canEvaluatePolicy(
            LAPolicy.deviceOwnerAuthenticationWithBiometrics,
            error: &error) {
            
            context.evaluatePolicy(
                LAPolicy.deviceOwnerAuthenticationWithBiometrics,
                localizedReason: "السماح باستخدام البصمة لتطبيق سيّار",
                reply: {(success, error) in
                    // Code to handle reply here
                    DispatchQueue.main.async {
                        
                        if let err = error {
                            switch err._code {
                            case LAError.Code.systemCancel.rawValue:
                                self.notifyUser("فشل السماح لتطبيق سيّار باستخدام بصمتك",
                                                err: err.localizedDescription)
                                
                            case LAError.Code.userCancel.rawValue:
                                self.notifyUser("الرجاء المحاولة مرة أخرى",
                                                err: err.localizedDescription)
                                
                            default:
                                self.notifyUser("حاول مرة أخرى",
                                                err: err.localizedDescription)
                            }
                            
                        } else {
                            self.notifyUser("تم التحقق بنجاح",
                                            err: "يمكنك الآن استخدام خاصية البصمة عند تسجيل الدخول على هذا الجهاز")
                            print("SUUUCCEESSSSSS       )()()()()()()")
                            self.devicetoken()
                        }
                    }
                })
        } else {
            // Biometry is not available on the device
            // No hardware support or user has not set up biometric auth
            
            if let err = error {
                switch err.code {
                
                case LAError.Code.biometryNotEnrolled.rawValue:
                    self.notifyUser("هذا الجهاز لا يعمل بخاصية البصمة",
                                    err: err.localizedDescription)
                    
                case LAError.Code.passcodeNotSet.rawValue:
                    self.notifyUser("هذا الجهاز لا يملك بصمة",
                                    err: err.localizedDescription)
                    
                    
                case LAError.Code.biometryNotAvailable.rawValue:
                    self.notifyUser("هذا الجهاز لا يعمل بخاصية البصمة",
                                    err: err.localizedDescription)
                default:
                    self.notifyUser("خطأ غير معروف",
                                    err: err.localizedDescription)
                }
            }
        }
    }
    func devicetoken(){
        let ref: DatabaseReference! = Database.database().reference()
        ref.child("tmpDT").setValue(["DT": UIDevice.current.identifierForVendor!.uuidString])
    }

    }
    
   
