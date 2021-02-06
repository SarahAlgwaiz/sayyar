//___________________________________________________________________________________Using packeges

using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using TMPro;
using System.Linq;

//___________________________________________________________________________________ beging of the class

public class AuthManager : MonoBehaviour
{
//___________________________________________________________________________________variables

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
    public Text ErrorMsgL;

    //Register variables
    [Header("Register")]
    public InputField email;
    public InputField password;
    public InputField username;
    public Text ErrorMsgR;


    //Show Profile Info variables
    [Header("Show Profile ")]
    public Text P_username;
    public Text P_email;

    //Show Profile Info variables
    [Header("Edit Profile ")]
    public InputField E_username;
    public InputField E_email;
    public InputField E_password;
    public InputField E_ConfirmPass;


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
    }
     public void ClearRegisterFeilds()
    {
        username.text = "";
        email.text = "";
        password.text = "";
    }

//___________________________________________________________________________________LoginButton Function
        public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(lemail.text, lpassword.text));
    }

//___________________________________________________________________________________RegisterButton Function
public void RegisterButton()
    { 
        //Call the register coroutine passing the email, password, and Username
        StartCoroutine(Register(email.text, password.text,username.text));

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
        StartCoroutine(UpdateUsername(E_username.text));
        StartCoroutine(UpdateEmail(E_email.text));
        StartCoroutine(UpdatePassword(E_password.text,E_ConfirmPass.text));
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
        Debug.Log("inside profile button and the id is :"+auth.CurrentUser.UserId);

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


//___________________________________________________________________________________Login Function
     private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            ErrorMsgL.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            // warningLoginText.text = "";
            // confirmLoginText.text = "Logged In";
          //  yield return new WaitForSeconds(2);

           // usernameField.text = User.DisplayName;
             UIManager.instance.HomeScreen(); 
            // confirmLoginText.text = "";
             ClearLoginFeilds();
             ClearRegisterFeilds();
        }
    }
   
//___________________________________________________________________________________Register Function
    private IEnumerator Register(string _email, string _password,string _username)
    {
        string arabicCheck = "([\u0600-\u06ff]|[\u0750-\u077f]|[\ufb50-\ufbc1]|[\ufbd3-\ufd3f]|[\ufd50-\ufd8f]|[\ufd92-\ufdc7]|[\ufe70-\ufefc]|[\ufdf0-\ufdfd])"; //check whether string contains arabic characters
                                                                                                                                                                  // Regex arabicRegex = new Regex(arabicCheck);
        bool result = Regex.IsMatch(_password, arabicCheck);

        if (result)
        {
            Debug.Log("Password should contain only English characters");
            ErrorMsgR.text ="Password should contain only English characters";
        }
        if (_password.Length != 6)
        {
             ErrorMsgR.text ="Password should have length of six characters";
            Debug.Log("Password should have length of six characters");
        }

        //Call the Firebase auth signin function passing the email and password
        var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password).ContinueWith(task => {
  if (task.IsCanceled) {
    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
    return;
  }
  if (task.IsFaulted) {
    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
    return;
  }

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
            ErrorMsgR.text = message;
        }
       
         ClearRegisterFeilds();
         ClearLoginFeilds();
    }

//___________________________________________________________________________________Update the profile's info Functions
 private IEnumerator UpdateUsername(string UpdatedName)
    {
        if(UpdatedName != null){

       var DBTask = DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Username").SetValueAsync(UpdatedName);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
         Debug.Log("Database username is now updated");
        }
    }
    else 
        Debug.Log(" Name is null ");
        }
    
 
 private IEnumerator UpdateEmail(string UpdatedEmail)
    {
        if(!(UpdatedEmail.Equals(""))){
       
        var DBTask1 = auth.CurrentUser.UpdateEmailAsync(UpdatedEmail);
        var DBTask = DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Email").SetValueAsync(UpdatedEmail);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
         Debug.Log("Database Email is now updated");
        }}
        else 
        Debug.Log(" Email is null ");

    }
 private IEnumerator UpdatePassword(string UpdatedPass,string UpdatedPassConfirm)
    {
        if(UpdatedPass + UpdatedPassConfirm != null){
        if (UpdatedPass.Equals(UpdatedPassConfirm)) {

        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null) {
        var DBTask = user.UpdatePasswordAsync(UpdatedPass);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
         Debug.Log("the passwords are the same and updated successfully.");
        }}
       }else 
       {Debug.Log("the passwords are not the same");} 
    }
     else 
        Debug.Log(" Password is null ");}



//___________________________________________________________________________________Toggle_Change Function
    public void Toggle_Change(bool vlaue)
    {
        Debug.Log(vlaue);
    }

//___________________________________________________________________________________ResetPass Function
 private IEnumerator ResetPass()
    {
    string emailAddress =(DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Email").GetValueAsync()).ToString();

    var Task = auth.SendPasswordResetEmailAsync(emailAddress);
    
    yield return new WaitUntil(predicate: () => Task.IsCompleted);

    if (Task.Exception != null) {
      Debug.LogWarning("SendPasswordResetEmailAsync was canceled.");
    }
   else
    Debug.Log("Password reset email sent successfully.");
  
}
}