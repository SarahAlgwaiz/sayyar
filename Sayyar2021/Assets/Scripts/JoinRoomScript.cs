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
               }
            //    PhotonNetwork.JoinRoom(roomCode);
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
            Debug.Log("disconnected" + cause);
        }       
          void Awake() {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        public override void OnJoinRoomFailed(short returnCode, string message){
            Debug.Log("join room failed" + message);
            valid = false;
        }
        public override void OnJoinedRoom(){
            Debug.Log("Success! joined room");

            SceneManager.LoadSceneAsync("WaitingRoomScene");
        }

  async void InitializeFirebase(){
        reference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;
        valid = await isRoomCodeValid();
        if(valid){
            SceneManager.LoadSceneAsync("game"); //has joined the room and will now go to the waiting room
        }

            }

        async Task<bool> isRoomCodeValid(){
            Query doesRoomCodeExist = reference.Root.Child("WaitingRooms").OrderByChild("RoomCode").OrderByValue().EqualTo(roomNumField);
            DataSnapshot result = Task.Run(() => doesRoomCodeExist.GetValueAsync()).Result;
            if(result.Exists){
                PhotonNetwork.JoinRoom(roomNumField.text);
                if(!PhotonNetwork.InRoom){
                    return false;
                }
                DatabaseReference parent = doesRoomCodeExist.Reference.Parent.Child("KindergartnersID").Child(user.UserId);
                await Task.Run(() => parent.SetValueAsync(user.UserId));
                return true;
            }
            return false;

        }


 public async Task writeKindergartenerData(){
        string waitingRoomId = reference.Root.Child("WaitingRooms").Child(roomNumField.text).Reference.Parent.Key;
         reference = reference.Root.Child("WaitingRooms").Child(waitingRoomId);
       
  // await Task.Run(() =>  reference.Child("WaitingRooms").Child(waitingRoomId).Child("KindergartnerIDs").SetValueAsync(user.UserId));




    }
}}

