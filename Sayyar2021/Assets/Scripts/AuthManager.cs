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
        StartCoroutine(Register(email.text, password.text, username.text));

    }

    //___________________________________________________________________________________SignOutButton Function
    public void SignOutButton()
    {
        auth.SignOut();
        UIManager.instance.MainScreen(); //  move to MainScreen after SignOut
        ClearRegisterFeilds();
        ClearLoginFeilds();
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
            StartCoroutine(UpdateUsername(E_username.text));
            StartCoroutine(UpdateEmail(E_email.text));
            StartCoroutine(UpdatePassword(E_password.text, E_ConfirmPass.text));
            ClearEditFeilds();
        }
    }

    //___________________________________________________________________________________MyProfileButton Function
    public void MyProfileButton()
    {
        StartCoroutine(MyProfile());
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
            P_username.text = ArabicFixer.Fix(snapshot.Child("Username").Value.ToString());
            P_email.text = snapshot.Child("Email").Value.ToString();
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


            var LoginTask = FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(_email, _password);
            // if (LoginTask.Result.IsEmailVerified)
            // {

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
                //User is now logged in
                //Now get the result
                User = LoginTask.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);

                //  yield return new WaitForSeconds(2);

                UIManager.instance.HomeScreen();

                ClearLoginFeilds();
                ClearRegisterFeilds();
            }
            // }
            // else
            // {
            //     ErrorMsgL.text = ArabicFixer.Fix("لطفاً تحقق من بريدك الالكتروني");
            //     Debug.Log("virfy your email");

            //}

        }
    }

    //___________________________________________________________________________________Register Function
    private IEnumerator Register(string _email, string _password, string _username)
    {
        InitializeFirebase();

        if (_username.Equals("") || _email.Equals("") || _password.Equals(""))
        {

            ErrorMsgR.text = ArabicFixer.Fix("لطفاً قم بتعبئة بياناتك", useTashkeel, useArabicNumbers);
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
                    auth.CurrentUser.SendEmailVerificationAsync();
                    // Firebase user has been created.
                    Firebase.Auth.FirebaseUser newUser = task.Result;
                    Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);

                    DBreference.Child("playerInfo").Child(newUser.UserId).Child("Email").SetValueAsync(_email); // newUser.UserId is samiller to auth.CurrentUser.UserId
                    DBreference.Child("playerInfo").Child(newUser.UserId).Child("Username").SetValueAsync(_username);

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
                else ErrorMsgR.text = ArabicFixer.Fix("تم انشاء حساب بنجاح ، قم بتأكيد حسابك ");

            }
        }
        //UIManager.instance.MainScreen(); 

        ClearRegisterFeilds();
        ClearLoginFeilds();


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
                UpdatedMsg.text = ArabicFixer.Fix("تم التعديل بنجاح");

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
        if (!(UpdatedEmail.Equals("")) && !Regex.IsMatch(UpdatedEmail, arabicCheck))
        {

            var DBTask1 = auth.CurrentUser.UpdateEmailAsync(UpdatedEmail);


            yield return new WaitUntil(predicate: () => DBTask1.IsCompleted);

            if (DBTask1.Exception != null)
            {
                FirebaseException firebaseEx = DBTask1.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                if (AuthError.EmailAlreadyInUse.Equals(errorCode))
                    UpdatedMsg.text = ArabicFixer.Fix("البريد الالكتروني مستخدم مسبقاً");

                if (AuthError.InvalidEmail.Equals(errorCode))
                    UpdatedMsg.text = ArabicFixer.Fix("البريد الالمتروني غير صحيح");
            }
            else
            {
                var DBTask = DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Email").SetValueAsync(UpdatedEmail);
                Debug.Log("Database Email is now updated");
                UpdatedMsg.text = ArabicFixer.Fix("تم التعديل بنجاح");

            }
        }
        else
        {
            if (UpdatedEmail.Equals(""))
            { Debug.Log(" Email is null "); }
            else { UpdatedMsg.text = ArabicFixer.Fix("البريد الالمتروني غير صحيح"); }
        }
        // UpdatedMsg.text = "* Kindly enter an email if you want to update it";


    }
    private IEnumerator UpdatePassword(string UpdatedPass, string UpdatedPassConfirm)
    {
        string arabicCheck = "([\u0600-\u06ff]|[\u0750-\u077f]|[\ufb50-\ufbc1]|[\ufbd3-\ufd3f]|[\ufd50-\ufd8f]|[\ufd92-\ufdc7]|[\ufe70-\ufefc]|[\ufdf0-\ufdfd])"; //check whether string contains arabic characters
                                                                                                                                                                  // Regex arabicRegex = new Regex(arabicCheck);

        if (!(UpdatedPass.Equals("")) && !(UpdatedPassConfirm.Equals("")))
        {
            if (UpdatedPass.Equals(UpdatedPassConfirm) && UpdatedPass.Length == 6)
            {
                if (Regex.IsMatch(UpdatedPass, arabicCheck))
                {
                    UpdatedMsg.text = ArabicFixer.Fix("كلمة المرور يجب ان تحتوي على الاحرف الانجليزية فقط");
                }
                else
                {
                    Firebase.Auth.FirebaseUser user = auth.CurrentUser;
                    if (user != null)
                    {
                        var DBTask = user.UpdatePasswordAsync(UpdatedPass);

                        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

                        if (DBTask.Exception != null)
                        {
                            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
                        }
                        else
                        {
                            Debug.Log("the passwords are the same and updated successfully.");
                            UpdatedMsg.text = ArabicFixer.Fix("تم التعديل بنجاح");
                        }
                    }
                }
            }
            else if (!(UpdatedPass.Equals(UpdatedPassConfirm)))
            { Debug.Log("the passwords are not the same"); UpdatedMsg.text = ArabicFixer.Fix("كلمة المرور غير متطابقة"); }
            else { UpdatedMsg.text = ArabicFixer.Fix("كلمة المرور يجب ان تكون ٦ رموز"); }
        }
        else
            Debug.Log(" Password is null ");
        //  UpdatedMsg.text = "* Kindly enter a Password if you want to update it";

    }


    //___________________________________________________________________________________Toggle_Change Function
    public void Toggle_Change(bool vlaue)
    {
        Debug.Log(vlaue);
    }

    //___________________________________________________________________________________ResetPass Function
    private IEnumerator ResetPass()
    {
        string emailAddress = (DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Email").GetValueAsync()).ToString();

        var Task = auth.SendPasswordResetEmailAsync(emailAddress);

        yield return new WaitUntil(predicate: () => Task.IsCompleted);

        if (Task.Exception != null)
        {
            Debug.LogWarning("SendPasswordResetEmailAsync was canceled.");
        }
        else
            Debug.Log("Password reset email sent successfully.");

    }
}