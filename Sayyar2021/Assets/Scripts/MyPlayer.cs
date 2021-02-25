using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using ArabicSupport;
using Firebase;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using TMPro;



public class MyPlayer : MonoBehaviour
{
public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;
        public DependencyStatus dependencyStatus;


   [Header("avatars")]
        public Sprite avaterA ;
        public Sprite avaterB ; 
        public Sprite avaterC ;
        public Sprite avaterD ;
        public Sprite avaterE ;
        public Sprite avaterF ;

        [Header("playersAvatar")]
        public Image PlayerAvatar ;

        [Header("playersName")]
        public TMPro.TMP_Text PlayerName;

   public Player Player;

   void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth inside show Badges");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;

    }

     public async void toCall()
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


    public async void setPlayerName(Player newPlayer){
        await Task.Run(() => toCall());
      Debug.Log("in set player name");
   var userID = newPlayer.NickName;

    var playerAvatarDB = await Task.Run(() => DBreference.Child("playerInfo").Child(userID).Child("Avatar").GetValueAsync());

    switch (playerAvatarDB)
    {
        case "avaterA":
        PlayerAvatar.sprite = avaterA;
        break;
        case "avaterB":
        PlayerAvatar.sprite = avaterB;
        break;
        case "avaterC":
        PlayerAvatar.sprite = avaterC;
        break;
        case "avaterD":
        PlayerAvatar.sprite = avaterD;
        break;
        case "avaterE":
        PlayerAvatar.sprite = avaterE;
        break;
        case "avaterF":
        PlayerAvatar.sprite = avaterF;
        break;

    }

var playerNameDB = await Task.Run(() => DBreference.Child("playerInfo").Child(userID).Child("Username").GetValueAsync());

PlayerName.text = ArabicFixer.Fix(playerNameDB);

   }
}
