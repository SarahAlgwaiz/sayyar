
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



public class EditAvatar : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    public GameObject Panel_EditAvatar;
     string ChoosenAvatar;
     public GameObject AvatarA, AvatarB, AvatarC, AvatarD, AvatarE, AvatarF, DAvatarA, DAvatarB, DAvatarC, DAvatarD, DAvatarE, DAvatarF;

//___________________________________________________________________________________InitializeFirebase Function
    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;

    }

//___________________________________________________________________________________to get the avatar that was choosen in edit avatar popup, and then save the last choosen one

public void AvatarAChoosen(){
       AvatarA.SetActive(true);
       AvatarB.SetActive(false);
       AvatarC.SetActive(false);
       AvatarD.SetActive(false);
       AvatarE.SetActive(false);
       AvatarF.SetActive(false);

       DAvatarA.SetActive(true);
       DAvatarB.SetActive(false);
       DAvatarC.SetActive(false);
       DAvatarD.SetActive(false);
       DAvatarE.SetActive(false);
       DAvatarF.SetActive(false);
       ChoosenAvatar = "AvatarA";
     }

     public void AvatarBChoosen(){
       AvatarA.SetActive(false);
       AvatarB.SetActive(true);
       AvatarC.SetActive(false);
       AvatarD.SetActive(false);
       AvatarE.SetActive(false);
       AvatarF.SetActive(false);

       DAvatarA.SetActive(false);
       DAvatarB.SetActive(true);
       DAvatarC.SetActive(false);
       DAvatarD.SetActive(false);
       DAvatarE.SetActive(false);
       DAvatarF.SetActive(false);
       ChoosenAvatar = "AvatarB";
     }

     public void AvatarCChoosen(){
       AvatarA.SetActive(false);
       AvatarB.SetActive(false);
       AvatarC.SetActive(true);
       AvatarD.SetActive(false);
       AvatarE.SetActive(false);
       AvatarF.SetActive(false);

       DAvatarA.SetActive(false);
       DAvatarB.SetActive(false);
       DAvatarC.SetActive(true);
       DAvatarD.SetActive(false);
       DAvatarE.SetActive(false);
       DAvatarF.SetActive(false);
       ChoosenAvatar = "AvatarC";
     }

         public void AvatarDChoosen(){
       AvatarA.SetActive(false);
       AvatarB.SetActive(false);
       AvatarC.SetActive(false);
       AvatarD.SetActive(true);
       AvatarE.SetActive(false);
       AvatarF.SetActive(false);

       DAvatarA.SetActive(false);
       DAvatarB.SetActive(false);
       DAvatarC.SetActive(false);
       DAvatarD.SetActive(true);
       DAvatarE.SetActive(false);
       DAvatarF.SetActive(false);
       ChoosenAvatar = "AvatarD";
     }

     public void AvatarEChoosen(){
       AvatarA.SetActive(false);
       AvatarB.SetActive(false);
       AvatarC.SetActive(false);
       AvatarD.SetActive(false);
       AvatarE.SetActive(true);
       AvatarF.SetActive(false);

       DAvatarA.SetActive(false);
       DAvatarB.SetActive(false);
       DAvatarC.SetActive(false);
       DAvatarD.SetActive(false);
       DAvatarE.SetActive(true);
       DAvatarF.SetActive(false);
       ChoosenAvatar = "AvatarE";
     }

     public void AvatarFChoosen(){
       AvatarA.SetActive(false);
       AvatarB.SetActive(false);
       AvatarC.SetActive(false);
       AvatarD.SetActive(false);
       AvatarE.SetActive(false);
       AvatarF.SetActive(true);

       DAvatarA.SetActive(false);
       DAvatarB.SetActive(false);
       DAvatarC.SetActive(false);
       DAvatarD.SetActive(false);
       DAvatarE.SetActive(false);
       DAvatarF.SetActive(true);
       ChoosenAvatar = "AvatarF";
     }


 public void SaveAvatarButton()
    { 
        StartCoroutine(SaveChoosenAvatar(ChoosenAvatar));
    }
//___________________________________________________________________________________To save the choosen avatar in the firebase

    private IEnumerator SaveChoosenAvatar(string ChoosenAvatar)
    {
        if(ChoosenAvatar != null){ 
    
       var DBTask = DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Avatar").SetValueAsync(ChoosenAvatar);
              
  
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
         Debug.Log("Database ChoosenAvatar is now updated");
        }
        }
        else
        {
          Debug.Log("ChoosenAvatar is null");
        }
    }
     


      public void Open_Panel_EditAvatar(){
        if (Panel_EditAvatar != null)
        Panel_EditAvatar.SetActive(true);
    }

      public void Close_Panel_EditAvatar(){
        Panel_EditAvatar.SetActive(false);
        Debug.Log(ChoosenAvatar);
    }

       public void Display()
    { 
        StartCoroutine(DisplayDB());
    }

     private void Awake()
    { 
      InitializeFirebase();
        StartCoroutine(DisplayDB());
    }

//___________________________________________________________________________________When logged in, this function will take the stored avatar from the firebase and then display it
        private IEnumerator DisplayDB()
    {
        

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
            AvatarAChoosen();
           }
           else
          if(av=="AvatarB"){
           AvatarBChoosen();
          }
            else
          if(av=="AvatarC"){
           AvatarCChoosen();
          }
            else
          if(av=="AvatarD"){
           AvatarDChoosen();
          }
            else
          if(av=="AvatarE"){
           AvatarEChoosen();
          }
            else
          if(av=="AvatarF"){
           AvatarFChoosen();
          }

         
        }
    }


}