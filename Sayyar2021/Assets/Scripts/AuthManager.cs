//___________________________________________________________________________________Using packeges

using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using TMPro;
using ArabicSupport;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using System.Threading.Tasks;


//___________________________________________________________________________________ beging of the class

public class AuthManager : MonoBehaviour
{
    //___________________________________________________________________________________variables
    [SerializeField]
    bool useTashkeel; // لعرض الاحرف العربية بالتشكيل
    [SerializeField]
    bool useArabicNumbers; // لاستخدام الارقام العربية

    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    //Login variables
    [Header("Login")]
    public InputField lemail;
    public InputField lpassword;
    public TextMeshProUGUI ErrorMsgL;

    //Register variables
    [Header("Register")]
    public InputField email;
    public InputField password;
    public TMP_InputField username;
    public TextMeshProUGUI ErrorMsgR;


    //Show Profile Info variables
    [Header("Show Profile Info")]
    public TextMeshProUGUI P_username;
    public Text P_email;

    //Show Profile Info variables
    [Header("Edit Profile Info")]
    public TMP_InputField E_username;
    public InputField E_email;
    public InputField E_password;
    public InputField E_ConfirmPass;
    public TextMeshProUGUI UpdatedMsg;

    [Header("Reset Password Info")]
    public InputField E_ResetPass;
    public TextMeshProUGUI ResetErrorMsg;

    [Header("fingerPrint")]
    public Toggle signUpToggle;

  


    //___________________________________________________________________________________Awake Function
    void Awake()
    {

        
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    //___________________________________________________________________________________InitializeFirebase Function
    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;

    }

    //___________________________________________________________________________________Functions to clearFields
    public void ClearLoginFeilds()
    {
        lemail.text = "";
        lpassword.text = "";
        ErrorMsgL.text = ArabicFixer.Fix("");
    }
    public void ClearResetPasswordFeilds()
    {
        E_ResetPass.text = "";
        ResetErrorMsg.text = ArabicFixer.Fix("");
    }
    public void ClearRegisterFeilds()
    {
        username.text = ArabicFixer.Fix("");
        email.text = "";
        password.text = "";
        ErrorMsgR.text = ArabicFixer.Fix("");

    }
    public void ClearEditFeilds()
    {
        E_username.text = ArabicFixer.Fix("");
        E_email.text = "";
        E_password.text = "";
        E_ConfirmPass.text = "";
        UpdatedMsg.text = ArabicFixer.Fix("");
    }

    //___________________________________________________________________________________LoginButton Function
    public void LoginButton()
    {
        
        //Call the login coroutine passing the email and password
        ErrorMsgL.text = "";
        StartCoroutine(Login(lemail.text, lpassword.text));
    }

    //___________________________________________________________________________________RegisterButton Function
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and Username
        ErrorMsgR.text = "";
        StartCoroutine(Register(email.text, password.text, ArabicFixer.Fix(username.text)));

    }

    //___________________________________________________________________________________SignOutButton Function
    public void SignOutButton()
    {
        auth.SignOut();
        //UIManager.instance.MainScreen(); //  move to MainScreen after SignOut
        SceneManager.LoadScene("MainScene");
       
    }

    //___________________________________________________________________________________SaveDataButton Function
    public void SaveDataButton()
    {
        if (E_username.text.Equals("") && E_email.text.Equals("") && E_password.text.Equals("") && E_ConfirmPass.text.Equals(""))
        {
            UpdatedMsg.text = ArabicFixer.Fix("لا توجد بيانات مدخلة");
        }
        else
        {
            StartCoroutine(UpdateUsername(ArabicFixer.Fix(E_username.text)));
            StartCoroutine(UpdateEmail(E_email.text));
           // StartCoroutine(UpdatePassword(E_password.text, E_ConfirmPass.text));
            ClearEditFeilds();
            Home_UIManager.instance.ClosePanel_EditProfile();
        }
    }

    //___________________________________________________________________________________MyProfileButton Function
    public void MyProfileButton()
    {
        StartCoroutine(MyProfile());
    }
     //___________________________________________________________________________________MyProfileButton Function
    public void EditProfileButton()
    {
        StartCoroutine(EditProfile());
    }

    //___________________________________________________________________________________ResetPassButton Function
    public void ResetPassButton()
    {
        StartCoroutine(ResetPass());
    }

    //___________________________________________________________________________________MyProfile Function
    public IEnumerator MyProfile()
    {
        Debug.Log("inside profile button and the id is :" + auth.CurrentUser.UserId);

        var DBTask = DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            Debug.Log("No data exists yet or the object is null");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            P_username.text = snapshot.Child("Username").Value.ToString();
            P_email.text = snapshot.Child("Email").Value.ToString();
        }
    }

     public IEnumerator EditProfile()
    {
        Debug.Log("inside profile button and the id is :" + auth.CurrentUser.UserId);

        var DBTask = DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            Debug.Log("No data exists yet or the object is null");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            E_username.text = snapshot.Child("Username").Value.ToString();
            E_email.text = snapshot.Child("Email").Value.ToString();
        }
    }



    //___________________________________________________________________________________Login Function
    private IEnumerator Login(string _email, string _password)
    {
        InitializeFirebase();
        //Call the Firebase auth signin function passing the email and password
        if (_email.Equals("") || _password.Equals(""))
        {
            ErrorMsgL.text = ArabicFixer.Fix("لطفاً قم بإدخال بياناتك", useTashkeel, useArabicNumbers);
        }
        else
        {

            var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

            if (LoginTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
                FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "فشلت عملية تسجيل الدخرل ";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "يرجى إدخال البريد الالكتروني";
                        break;
                    case AuthError.MissingPassword:
                        message = "يرجى إدخال كلمة المرور";
                        break;
                    case AuthError.WrongPassword:
                        message = "كلمة المرور غير صحيحة";
                        break;
                    case AuthError.InvalidEmail:
                        message = "البريد الالكتروني غير صحيح";
                        break;
                    case AuthError.UserNotFound:
                        message = "البريد الالكتروني غير مسجل مسبقاً";
                        break;
                }
                ErrorMsgL.text = ArabicFixer.Fix(message);
            }
            else
            {
                // if (!Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.IsEmailVerified)
                // {
                //     ErrorMsgL.text = ArabicFixer.Fix("لطفاً تحقق من بريدك الالكتروني");
                //     Debug.Log("verify your email");
                //     auth.SignOut();
                // }
                // else
                // {
                    //User is now logged in
                    
                    //Now get the result
                    User = LoginTask.Result;
                    Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);

                    //  yield return new WaitForSeconds(2);
        
                SceneManager.LoadScene("HomeScene");


                    // Home_UIManager.instance.HomeScreen();

                    ClearLoginFeilds();
                    ClearRegisterFeilds();
                // }
            }
        }
    }

    //___________________________________________________________________________________Register Function
    [DllImport("__Internal")]
    private static extern void _storeDeviceToken(string userID);
    private IEnumerator Register(string _email, string _password, string _username)
    {
        InitializeFirebase();

        if (_username.Equals("") || _email.Equals("") || _password.Equals(""))
        {

            ErrorMsgR.text = ArabicFixer.Fix("لطفاً قم بتعبئة بياناتك");
        }
        else
        {

            string arabicCheck = "([\u0600-\u06ff]|[\u0750-\u077f]|[\ufb50-\ufbc1]|[\ufbd3-\ufd3f]|[\ufd50-\ufd8f]|[\ufd92-\ufdc7]|[\ufe70-\ufefc]|[\ufdf0-\ufdfd])"; //check whether string contains arabic characters
                                                                                                                                                                      // Regex arabicRegex = new Regex(arabicCheck);
            bool result = Regex.IsMatch(_password, arabicCheck);

            if (Regex.IsMatch(_email, arabicCheck))
            {
                Debug.Log("* Invalid Email");
                ErrorMsgR.text = ArabicFixer.Fix("البريد الالكتروني خاطئ");
            }

            else if (result)
            {

                Debug.Log("* Password should contain only English characters");
                ErrorMsgR.text = ArabicFixer.Fix("كلمة المرور يجب ان تحتوي على الاحرف الانجليزية فقط");
            }

            else if (_password.Length != 6)
            {
                ErrorMsgR.text = ArabicFixer.Fix("كلمة المرور يجب ان تكون ٦ رموز");
                Debug.Log("Password should have length of six characters");
            }
            else
            {

                //Call the Firebase auth signin function passing the email and password
                var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password).ContinueWith(task =>
                {
                   // auth.CurrentUser.SendEmailVerificationAsync();
                    // Firebase user has been created.
                    Firebase.Auth.FirebaseUser newUser = task.Result;
                    Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);

                    DBreference.Child("playerInfo").Child(newUser.UserId).Child("Email").SetValueAsync(_email); // newUser.UserId is samiller to auth.CurrentUser.UserId
                    DBreference.Child("playerInfo").Child(newUser.UserId).Child("Username").SetValueAsync(_username);
                    DBreference.Child("playerInfo").Child(newUser.UserId).Child("Avatar").SetValueAsync("AvatarA");
                    DBreference.Child("playerInfo").Child(newUser.UserId).Child("password").SetValueAsync(_password);
                   
                   if (signUpToggle.isOn){
                   _storeDeviceToken(newUser.UserId);
                   }
                    


                });

                //Wait until the task completes
                yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

                if (RegisterTask.Exception != null)
                {
                    //If there are errors handle them
                    Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                    FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;

                    AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                    string message = "لم يتم إنشاء حساب جديد";

                    switch (errorCode)
                    {
                        case AuthError.MissingEmail:
                            message = "يرجى إدخال البريد الالكتروني";
                            break;
                        case AuthError.MissingPassword:
                            message = "يرجى إدخال كلمة المرور";
                            break;
                        case AuthError.WeakPassword:
                            message = "كلمة السر ضعيفة، يرجى إدخال كلمة سر أقوى";
                            break;
                        case AuthError.EmailAlreadyInUse:
                            message = "البريد الالكتروني مسجل لحساب آخر";
                            break;
                        case AuthError.InvalidEmail:
                            message = "البريد الالكتروني غير صحيح";
                            break;
                    }
                    Debug.Log(message);
                    ErrorMsgR.text = ArabicFixer.Fix(message);
                }
                else
                {
                    //ErrorMsgR.text = ArabicFixer.Fix("تم انشاء حساب بنجاح ، قم بتأكيد حسابك ");


                    ClearLoginFeilds();
                    ClearRegisterFeilds();
                }

            }
            if (ErrorMsgR.text.Equals(""))
              ErrorMsgR.text = ArabicFixer.Fix("تم انشاء حساب بنجاح ");
                UIManager.instance.LoginScreen();

        }

    }

    //___________________________________________________________________________________Update the profile's info Functions
    private IEnumerator UpdateUsername(string UpdatedName)
    {
        if (!(UpdatedName.Equals("")))
        { //في حال كانت الخانة متعبية باسم جديد

            var DBTask = DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Username").SetValueAsync(UpdatedName);

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
            else
            {
                Debug.Log("Database username is now updated");
                //UpdatedMsg.text = ArabicFixer.Fix("تم التعديل بنجاح");

            }
        }
        else
        {
            Debug.Log(" Name is null ");
        }
        //  UpdatedMsg.text = "* Kindly enter a name if you want to update it";

    }


    private IEnumerator UpdateEmail(string UpdatedEmail)
    {
        string arabicCheck = "([\u0600-\u06ff]|[\u0750-\u077f]|[\ufb50-\ufbc1]|[\ufbd3-\ufd3f]|[\ufd50-\ufd8f]|[\ufd92-\ufdc7]|[\ufe70-\ufefc]|[\ufdf0-\ufdfd])"; //check whether string contains arabic characters
                                                                                                                                                                  // Regex arabicRegex = new Regex(arabicCheck);
        if (!(UpdatedEmail.Equals("")))
        {

            var DBTask1 = auth.CurrentUser.UpdateEmailAsync(UpdatedEmail);


            yield return new WaitUntil(predicate: () => DBTask1.IsCompleted);

            if (DBTask1.Exception != null)
            {
                FirebaseException firebaseEx = DBTask1.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                if (AuthError.EmailAlreadyInUse.Equals(errorCode))
                    UpdatedMsg.text = ArabicFixer.Fix("البريد الالكتروني مستخدم مسبقاً");

                else if (AuthError.InvalidEmail.Equals(errorCode) || Regex.IsMatch(UpdatedEmail, arabicCheck))
                    UpdatedMsg.text = ArabicFixer.Fix("البريد الالكتروني غير صحيح");
            }
            else
            {
                var DBTask = DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Email").SetValueAsync(UpdatedEmail);
                Debug.Log("Database Email is now updated");
              //  UpdatedMsg.text = ArabicFixer.Fix("تم التعديل بنجاح");

            }

        }
        else Debug.Log(" email is null ");





    }
    private IEnumerator UpdatePassword(string UpdatedPass, string UpdatedPassConfirm)
    {
        string arabicCheck = "([\u0600-\u06ff]|[\u0750-\u077f]|[\ufb50-\ufbc1]|[\ufbd3-\ufd3f]|[\ufd50-\ufd8f]|[\ufd92-\ufdc7]|[\ufe70-\ufefc]|[\ufdf0-\ufdfd])"; //check whether string contains arabic characters
                                                                                                                                                                  // Regex arabicRegex = new Regex(arabicCheck);

        if (!(UpdatedPass.Equals("")) && !(UpdatedPassConfirm.Equals("")))
        {
            Firebase.Auth.FirebaseUser user = auth.CurrentUser;
            if (user != null)
            {
                var DBTask = user.UpdatePasswordAsync(UpdatedPass);

                yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

                if (DBTask.Exception != null)
                {
                    if (Regex.IsMatch(UpdatedPass, arabicCheck) || Regex.IsMatch(UpdatedPassConfirm, arabicCheck))
                        UpdatedMsg.text = ArabicFixer.Fix("كلمة المرور يجب ان تحتوي على الاحرف الانجليزية فقط");

                    else if (!UpdatedPass.Equals(UpdatedPassConfirm))
                    {
                        Debug.Log("the passwords are not the same");
                        UpdatedMsg.text = ArabicFixer.Fix("كلمة المرور غير متطابقة");
                    }
                    else if (UpdatedPass.Length != 6 || UpdatedPassConfirm.Length != 6)
                        UpdatedMsg.text = ArabicFixer.Fix("كلمة المرور يجب ان تكون ٦ رموز");
                }

                else
                {
                    Debug.Log("the passwords are the same and updated successfully.");
                   // UpdatedMsg.text = ArabicFixer.Fix("تم التعديل بنجاح");
                }
            }


        }
        else
            Debug.Log(" Password is null ");
        //  UpdatedMsg.text = "* Kindly enter a Password if you want to update it";

    }


    //___________________________________________________________________________________Toggle_Change Function
    
    // public void Toggle_Change(bool vlaue)
    // {
    //    _getDeviceToken();
    // }

    //___________________________________________________________________________________ResetPass Function
    private IEnumerator ResetPass()
    {
        string emailAddress = E_ResetPass.text;
        //(DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Email").GetValueAsync()).ToString();
        if (emailAddress != "")
        {
            var Task = auth.SendPasswordResetEmailAsync(emailAddress);

            yield return new WaitUntil(predicate: () => Task.IsCompleted);

            if (Task.Exception != null)
            {
                //HERE Email verfication
                Debug.LogWarning("SendPasswordResetEmailAsync was canceled.");
            }
            else
                Debug.Log("Password reset email sent successfully.");
            ResetErrorMsg.text = ArabicFixer.Fix("لطفاً تحقق من بريدك الالكتروني");

        }
        else
        {
            ResetErrorMsg.text = ArabicFixer.Fix("يرجى إدخال البريد الإلكتروني");
        }
    }

    //////////////All Work Below is fingerprint code 
    [DllImport("__Internal")]
    private static extern void _getDeviceToken();

    public async void fingerprintButton()
    { 
      await Task.Run(() =>  _getDeviceToken());
       InitializeFirebase();

       string DT = await Task.Run(() => DBreference.Child("tmpDT").Child("DT").GetValueAsync().Result.Value) as string;
     
     if(DT != "NONE"){
      DBreference.Child("tmpDT").Child("DT").SetValueAsync("NONE"); 
      var UID = await Task.Run(() => DBreference.Child("FingerpintInfo").Child(DT).Child("UID").GetValueAsync().Result.Value);
      string userID = UID.ToString();
      string _password = await Task.Run(() => DBreference.Child("playerInfo").Child(userID).Child("Password").GetValueAsync().Result.Value) as string;
      string _email = await Task.Run(() => DBreference.Child("playerInfo").Child(userID).Child("Email").GetValueAsync().Result.Value) as string;
      Debug.Log("Email is       #@#@#nn  "+_email);
      Debug.Log("Password is       #@#@#nn  "+_password);
      auth.SignInWithEmailAndPasswordAsync(_email, _password);
      SceneManager.LoadScene("HomeScene");
     }
     else{
         Debug.Log("NONE !!!");
     }

    }

}