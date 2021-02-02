using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using TMPro;
using System.Linq;





public class AuthManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    //Login variables

    //Register variables
    [Header("Register")]
    public InputField email;
    public InputField password;
    public InputField username;

    public GameObject Panel_signUp;

    public void openPanel(){
        if (Panel_signUp != null)
        Panel_signUp.SetActive(true);

    }
    public void ClosePanel(){
        Panel_signUp.SetActive(false);

    }
    

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

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;

    }

    //Function for the login button

    public void RegisterButton()
    {     Debug.LogError("inside RegisterButton" );
        //Call the register coroutine passing the email, password, and name
        StartCoroutine(Register(email.text, password.text,username.text));

    }

    public void Toggle_Change(bool vlaue)
    {
        Debug.Log(vlaue);
    }

    private IEnumerator Register(string _email, string _password,string _username)
    {Debug.LogError("inside IEnumerator Register" );
        string arabicCheck = "([\u0600-\u06ff]|[\u0750-\u077f]|[\ufb50-\ufbc1]|[\ufbd3-\ufd3f]|[\ufd50-\ufd8f]|[\ufd92-\ufdc7]|[\ufe70-\ufefc]|[\ufdf0-\ufdfd])"; //check whether string contains arabic characters
                                                                                                                                                                  // Regex arabicRegex = new Regex(arabicCheck);
        bool result = Regex.IsMatch(_password, arabicCheck);

        if (result)
        {
            Debug.Log("Password should contain only English characters");
        }
        if (_password.Length != 6)
        {
            Debug.Log("Password should have length of six characters");
        }

        //Call the Firebase auth signin function passing the email and password
        var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
        //Call the realtime database to save playerInfo
       DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Email").SetValueAsync(_email);
       DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Username").SetValueAsync(_username);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

        if (RegisterTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
            FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            //string message = "لم يتم إنشاء حساب جديد";
            string message = "Unable to create new account";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    // message = "يرجى إدخال البريد الالكتروني";
                    message = "Kindly enter a valid email";
                    break;
                case AuthError.MissingPassword:
                    //message = "يرجى إدخال كلمة السر";
                    message = "Kindly enter a password";
                    break;
                case AuthError.WeakPassword:
                    //message = "كلمة السر ضعيفة، يرجى إدخال كلمة سر أقوى";
                    message = "Password is weak, kindly enter a stronger password";
                    break;
                case AuthError.EmailAlreadyInUse:
                    //message = "البريد الالكتروني مسجل لحساب آخر";
                    message = "Email is already in use";
                    break;
            }
            Debug.Log(message);
            //  warningRegisterText.text = message;
        }
        // else
        // {
        //     //User has now been created
        //     //Now get the result
        //     User = RegisterTask.Result;

        //     if (User != null)
        //     {
        //         //Create a user profile and set the username
        //        // UserProfile profile = new UserProfile{DisplayName = _username};

        //         //Call the Firebase auth update user profile function passing the profile with the username
        //        // var ProfileTask = User.UpdateUserProfileAsync(profile);
        //         //Wait until the task completes
        //        // yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        //         if (ProfileTask.Exception != null)
        //         {
        //             //If there are errors handle them
        //             Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        //             FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
        //             AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
        //             warningRegisterText.text = "Username Set Failed!";
        //         }
        //         else
        //         {
        //             //Username is now set
        //             //Now return to login screen
        //             UIManager.instance.LoginScreen();
        //             //warningRegisterText.text = "";
        //         }
        //     }
        // }

    }
}