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
    private byte maxPlayersPerRoom = 4;
    
[SerializeField]
private GameObject createRoomView;
[SerializeField]
private TMPro.TMP_Text roomCodeCreateField;
 

[SerializeField]
private TMPro.TMP_Text player1;
[SerializeField]
private TMPro.TMP_Text player2;
[SerializeField]
private TMPro.TMP_Text player3;
[SerializeField]
private TMPro.TMP_Text player4;
[SerializeField]
private TMPro.TMP_Text numOfJoinedPlayersText;

private int roomNumber;
private int numOfJoinedPlayers=0;


        public void createRoom(){
            Debug.Log("Connected create room");
             roomNumber=  UnityEngine.Random.Range(0, 100000);
            PhotonNetwork.CreateRoom(roomNumber.ToString("00000"), new RoomOptions{IsVisible = false, IsOpen = true, MaxPlayers = maxPlayersPerRoom});
            Debug.Log("Room Created");
        }
        public override void OnCreatedRoom(){
            roomCodeCreateField.text = "Room Number: "+roomNumber;
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
            //PlayerPrefs.setString("Name","Hadeel Alhajri") ;// current user's name from database in enterRoomScrip---> delete it from here
            PhotonNetwork.NickName = "Hadeel Alhajri";
           // SceneManager.LoadSceneAsync("SolarSystemGame");
            numOfJoinedPlayers= PhotonNetwork.CurrentRoom.PlayerCount;
            numOfJoinedPlayersText.text=numOfJoinedPlayers.ToString();
           if(PhotonNetwork.CurrentRoom.PlayerCount==1){
                           Debug.Log("ONLY the host, the host is "+PhotonNetwork.NickName);

                player1.text = PhotonNetwork.NickName;
           }
        }
       public override void OnCreateRoomFailed(short returnCode, string message){
           Debug.Log("failed to create room");
           createRoom();
        }
<<<<<<< HEAD
    
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
=======
        public override void OnPlayerEnteredRoom(Player newPlayer)
   {
       numOfJoinedPlayers= PhotonNetwork.CurrentRoom.PlayerCount;
      numOfJoinedPlayersText.text=numOfJoinedPlayers.ToString();
if(numOfJoinedPlayers==2){
    player2.text= newPlayer.NickName;
}
else if(numOfJoinedPlayers==3){
    player3.text= newPlayer.NickName;
}else if(numOfJoinedPlayers==4){
    player4.text= newPlayer.NickName;
}
       Debug.Log("player "+newPlayer.NickName+" joined to room "+PhotonNetwork.CurrentRoom.Name+" , we have "+numOfJoinedPlayers+" players");
   }
>>>>>>> 25f13038f2a9b6110aec22e96543c123265bd61d
    }
}
