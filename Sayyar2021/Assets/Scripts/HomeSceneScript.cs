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
namespace com.cactusteam.Sayyar
{

    public class HomeSceneScript : MonoBehaviourPunCallbacks
    {

        [Header("Firebase")]
        public DependencyStatus dependencyStatus;
        public DatabaseReference reference;
        private FirebaseUser user;
        public FirebaseAuth auth;

        [SerializeField]
        private Button joinButton;
        [SerializeField]
        private Button createButton;
        // [SerializeField]
        private GameObject connectingText;

        //[SerializeField]
        private TMP_Text playerNameText;


        private string nameFromFirebase;
        string gameVersion = "1";

        public void OnClickJoinButton()
        {
            SceneManager.LoadScene("JoinGameScene", LoadSceneMode.Single);
        }
        public void OnClickCreateButton()
        {
            SceneManager.LoadScene("CreateGameScene", LoadSceneMode.Single);
        }

        public void activateButtons(){
            createButton.interactable = true;
            joinButton.interactable = true;
        }
        public override void OnConnectedToMaster()
        {

            Debug.Log("Photon Nickname " + PhotonNetwork.NickName);
            Debug.Log("connected to master");
            activateButtons();
            // connectingText.SetActive(false);
            // playerNameText.text = nameFromFirebase;
        }

        public override void OnDisconnected(DisconnectCause cause)
        {

            Debug.Log("disconnected from server"); //popup pleeeease
        }
        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            Connect();
        }
        void Start()
        {
            // createButton.interactable = false;
            // joinButton.interactable = false;
            //connectingText.SetActive(true);
            if(PhotonNetwork.IsConnectedAndReady){
              activateButtons();
            }
            else if(PhotonNetwork.InRoom){
                PhotonNetwork.LeaveRoom();
            }
            else
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
        public void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                Debug.Log("Success");
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
                Debug.Log("Success");
            }

        }

        public async void InitializeFirebase()
        {
            reference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;
            Debug.Log("initialize");
            auth = FirebaseAuth.DefaultInstance;

            await setName();
        }

        public async Task setName()
        {
            // Debug.Log("set name");
            // DataSnapshot ds = await Task.Run(() => reference.Child("playerInfo").Child("A2KP5BlspjYdfhBc61qm1WnusKZ2").Child("Username").GetValueAsync());//replace this with the current user's name stored in DB
            // Debug.Log("after ds");
            // await Task.Run(() => nameFromFirebase = ds.Value.ToString());
            // Debug.Log("name is" + nameFromFirebase);
            // Debug.Log("after set active");
            var userID = auth.CurrentUser.UserId;
            Debug.Log("user id is " + userID);
            await Task.Run(() => PhotonNetwork.NickName = userID);

            // PlayerPrefs.SetString("UID",user.UserId);
            Debug.Log("after nickname");

        }


    }
}