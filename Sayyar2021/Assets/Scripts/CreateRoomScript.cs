using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using System.Threading.Tasks;
public class CreateRoomScript: MonoBehaviourPunCallbacks{


         [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public DatabaseReference reference;
    private FirebaseUser user;
        
    
    [Tooltip("Maximum number of players in each room. When a room is full, new players can't join in. Therefore a new room will be created for the new player.")]
    [SerializeField]
    private byte maxPlayersPerRoom = 2;
    
[SerializeField]
private GameObject createRoomView;
[SerializeField]
private TMPro.TMP_Text roomCodeCreateField;

public static string virtualPlayroomKey;

 private int roomNumber;



        public void createRoom(){
             Debug.Log("Connected create room");
             roomNumber=  UnityEngine.Random.Range(10000, 100000);
             PhotonNetwork.CreateRoom(roomNumber.ToString("00000"), new RoomOptions{IsVisible = false, IsOpen = true, MaxPlayers = maxPlayersPerRoom, PublishUserId=true});
        }
        public override void OnCreatedRoom(){
            Debug.Log("Room Created"); //popup pleeeease
            roomCodeCreateField.text = "Room Number: "+roomNumber;
             PlayerPrefs.SetString("RoomCode",""+roomNumber);
                FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are available Initialize Firebase
                InitializeFirebase();
            }
            else 
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
        }
         private void Start() {
            createRoom();
        }
        public override void OnDisconnected(DisconnectCause cause){
            Debug.Log("disconnected from server"); //popup pleeeease
        }       
          void Awake() {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        public override void OnJoinRoomFailed(short returnCode, string message){
            Debug.Log("join room failed" + message);
        }
        public override void OnJoinedRoom(){
            Debug.Log("Success! joined room");
         SceneManager.LoadSceneAsync("WaitingRoomScene");
        }
       public override void OnCreateRoomFailed(short returnCode, string message){
           Debug.Log("failed to create room"); //popup pleeeease
           createRoom();
        }
    
    async void InitializeFirebase(){
        reference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;
         await writeVirtualPlayroomData();
            }

        public async Task<string> writeVirtualPlayroomData(){
        reference = reference.Child("VirtualPlayrooms").Push();
        var key = reference.Key;
        reference = reference.Root;
        await Task.Run(() =>reference.Child("VirtualPlayrooms").Child(key).Child("NumOfPlayers").SetValueAsync(maxPlayersPerRoom));// Num of player may change
        await Task.Run(() =>reference.Child("VirtualPlayrooms").Child(key).Child("VirtualPlayroomID").SetValueAsync(key));
       //the following code should have a valid value (not null) otherwise the subsequent async calls will fail
        //await Task.Run(() =>reference.Child("VirtualPlayrooms").Child(key).Child("Game").SetValueAsync(key));
        return key;
    }   
   
       
    }

   

