using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using System.Threading.Tasks;
using TMPro;
namespace com.cactusteam.Sayyar{

public class HomeSceneScript: MonoBehaviourPunCallbacks{

       [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public DatabaseReference reference;
    private FirebaseUser user;
    
    [SerializeField]
private Button joinButton;
 [SerializeField]
private Button createButton;
[SerializeField]
private GameObject connectingText;

// [SerializeField]
// private TMP_Text playerNameText;

[SerializeField]
private TMP_Text playerNameText;

private string name; 
string gameVersion = "1";

    public void OnClickJoinButton(){
       SceneManager.LoadScene("JoinGameScene", LoadSceneMode.Single);
          }
    public void OnClickCreateButton(){
        SceneManager.LoadScene("CreateGameScene", LoadSceneMode.Single);
    }
        public override void OnConnectedToMaster(){
            Debug.Log("connected to master");
            createButton.gameObject.SetActive(true);
            joinButton.gameObject.SetActive(true);
            connectingText.SetActive(false);
           playerNameText.text = name;
        }
        public override void OnDisconnected(DisconnectCause cause){

            Debug.Log("disconnected" + cause);
        }       
          void Awake() {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
         void Start() {
               createButton.gameObject.SetActive(false);
               joinButton.gameObject.SetActive(false);
               connectingText.SetActive(true);
            Connect();

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
          public void Connect(){
            if(PhotonNetwork.IsConnected){
                Debug.Log("Success");
            }
            else{
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
                Debug.Log("Success");
            }
 
        }

    async void InitializeFirebase(){
        reference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;
      Debug.Log("initialize");
        await setName();

            }


    public async Task setName(){
       Debug.Log("set name");
        DataSnapshot ds =  await Task.Run(() => reference.Child("playerInfo").Child("1x8VnXozbxgTYLRLPrOxfBldhNu1").Child("Username").GetValueAsync());//replace this with the current user's name stored in DB
         Debug.Log("after ds");
         name = ds.Value.ToString();
        Debug.Log("name is" + name);
         Debug.Log("after set active");
          PhotonNetwork.NickName = name;
        Debug.Log("after nickname");
        // playerNameText.SetText(name); 

        
          

    }

    
}
}