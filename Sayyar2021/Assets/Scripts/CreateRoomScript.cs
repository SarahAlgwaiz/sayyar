using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using System.Threading.Tasks;
namespace com.cactusteam.Sayyar{
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
 

 private int roomNumber;



        public void createRoom(){
            Debug.Log("Connected create room");
             roomNumber=  UnityEngine.Random.Range(0, 100000);
            PhotonNetwork.CreateRoom(roomNumber.ToString("00000"), new RoomOptions{IsVisible = false, IsOpen = true, MaxPlayers = maxPlayersPerRoom,PublishUserId=true});
            Debug.Log("Room Created");
        }
        public override void OnCreatedRoom(){
            roomCodeCreateField.text = "Room Number: "+roomNumber;
             PlayerPrefs.SetString("RoomCode",""+roomNumber);
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
         private void Start() {
            createRoom();
        }
        public override void OnDisconnected(DisconnectCause cause){
            Debug.Log("disconnected" + cause);
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
           Debug.Log("failed to create room");
           createRoom();
        }
    
    async void InitializeFirebase(){
        reference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;
        string key = await writeVirtualPlayroomData();
        await writeWaitingRoomData(key);
            }
        public async Task<string> writeVirtualPlayroomData(){
        reference = reference.Child("VirtualPlayrooms").Push();
        var key = reference.Key;
        reference = reference.Root;
        await Task.Run(() =>reference.Child("VirtualPlayrooms").Child(key).Child("NumOfPlayers").SetValueAsync(maxPlayersPerRoom));
        await Task.Run(() =>reference.Child("VirtualPlayrooms").Child(key).Child("VirtualPlayroomID").SetValueAsync(key));

       //the following code should have a valid value (not null) otherwise the subsequent async calls will fail

        //await Task.Run(() =>reference.Child("VirtualPlayrooms").Child(key).Child("Game").SetValueAsync(key));
        //await Task.Run(() => reference.Child("VirtualPlayrooms").Child(key1).Child("HostID").SetValueAsync(user.UserId));
        return key;
    }   
    public async Task writeWaitingRoomData(string VPkey){
         reference = reference.Child("WaitingRooms").Push();
        var key = reference.Key;
        reference = reference.Root;
       await Task.Run(() =>  reference.Child("WaitingRooms").Child(key).Child("WaitingRoomID").SetValueAsync(key));
       await Task.Run(() =>  reference.Child("WaitingRooms").Child(key).Child("RoomCode").SetValueAsync(roomNumber));
       await Task.Run(() =>  reference.Child("VirtualPlayrooms").Child(VPkey).Child("WaitingRoomID").SetValueAsync(key));
       //the following code should have a valid value (not null) otherwise the subsequent async calls will fail

        //await Task.Run(() =>reference.Child("WaitingRooms").Child(key).Child("HostID").SetValueAsync(user.UserId));
        //await Task.Run(() =reference.Child("WaitingRooms").Child("1").Child("KindergartnerIDs").setValueAsync("something")); 
        //await Task.Run(() =reference.Child("WaitingRooms").Child(""+roomNumber).Child("VideoID").SetValueAsync(videoID));    
    }
       
    }

   
}
