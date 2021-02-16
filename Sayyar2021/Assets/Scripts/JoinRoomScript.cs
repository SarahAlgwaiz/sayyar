using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using System.Threading.Tasks;
namespace com.cactusteam.Sayyar{
    public class JoinRoomScript: MonoBehaviourPunCallbacks{
    
     [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public DatabaseReference reference;
    private FirebaseUser user;

[SerializeField]
private GameObject joinRoomView;
[SerializeField]
private Button roomCodeConfirmButton;
[SerializeField]
private TMPro.TMP_InputField roomNumField;

private bool valid ;

    public void OnClickRoomCodeConfirmButton(){
        JoinRoom();
    }
        
        public void JoinRoom(){    
               string roomCode = roomNumField.text;
               if(roomCode.Length!=5){
                   Debug.Log("Room Code length should be 5");
                   return;
               }
                PhotonNetwork.JoinRoom(roomCode);
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
        public override void OnDisconnected(DisconnectCause cause){
            Debug.Log("disconnected from server"); //popup pleeeease
        }       
          void Awake() {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        public override void OnJoinRoomFailed(short returnCode, string message){
            Debug.Log("join room failed" + message);
            Debug.Log(returnCode);
            switch(returnCode){
                case 32758:
                Debug.Log("Room code does not exist"); //popup pleeeease
                break;
                case 32765:
                Debug.Log("Room already full");//popup pleeeease
                break;
            }
            valid = false;
        }
        public override void OnJoinedRoom(){
            Debug.Log("Success! joined room");

           SceneManager.LoadSceneAsync("WaitingRoomScene");
        }

  async void InitializeFirebase(){
        reference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;
        //valid = await isRoomCodeValid();
        // if(valid){
        //     SceneManager.LoadSceneAsync("WaitingRoomScene"); //has joined the room and will now go to the waiting room
        // }

            }

        async Task<bool> isRoomCodeValid(){

            Query doesRoomCodeExist = reference.Root.Child("WaitingRooms").OrderByChild("RoomCode");
            Query q1 = doesRoomCodeExist.EqualTo(roomNumField.text);
            
            DataSnapshot result = await Task.Run(() => q1.GetValueAsync().Result);
            if(result.Exists){
                //PhotonNetwork.JoinRoom(roomNumField.text);
                if(!PhotonNetwork.InRoom){ // more validation cases (full or code not valid)
                    return false;
                }
                // DatabaseReference parent = q1.Reference.Parent.Child("KindergartnersID").Child(user.UserId);
                // await Task.Run(() => parent.SetValueAsync(user.UserId));
                return true;
            }
            return false;

        }
}}

