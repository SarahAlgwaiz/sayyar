using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using TMPro;
using System.Linq;



public class DisplayedAvaterInHome : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    //public GameObject Panel_EditAvatar;
     //string ChoosenAvatar;
     public GameObject AvatarA, AvatarB, AvatarC, AvatarD, AvatarE, AvatarF;

//___________________________________________________________________________________InitializeFirebase Function
    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;

    }


private void Awake() {
      
        StartCoroutine(DisplayDB());
    }
    
// //___________________________________________________________________________________When logged in, this function will take the stored avatar from the firebase and then display it
        private IEnumerator DisplayDB()
    {
        InitializeFirebase();

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
           string av = snapshot.Child("Avatar").Value.ToString();

           if (av=="AvatarA"){
            AvatarA.SetActive(true);
           }
           else
          if(av=="AvatarB"){
           AvatarB.SetActive(true);
          }
            else
          if(av=="AvatarC"){
           AvatarC.SetActive(true);
          }
            else
          if(av=="AvatarD"){
           AvatarD.SetActive(true);
          }
            else
          if(av=="AvatarE"){
           AvatarE.SetActive(true);
          }
            else
          if(av=="AvatarF"){
           AvatarF.SetActive(true);
          }

         
        }
    }


}