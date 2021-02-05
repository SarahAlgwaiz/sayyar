using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using Firebase;

public class Synchronization : MonoBehaviourPunCallbacks
{
[SerializeField]
private TMPro.TMP_Text numOfJoinedPlayersText;

private List<MyPlayer> playerList = new List<MyPlayer>();
[SerializeField]
private MyPlayer myPlayer;

[SerializeField]
private Button start;

private Transform tr;

 [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public DatabaseReference reference;
    private FirebaseUser user;
 public override void OnPlayerEnteredRoom(Player newPlayer)
   {
        addNewPlayer(newPlayer);
            updatePosition();
if(PhotonNetwork.CurrentRoom.MaxPlayers==PhotonNetwork.CurrentRoom.PlayerCount&& PhotonNetwork.IsMasterClient){
    start.interactable=true;
}
   }
   public void OnClickStartButton(){
       SceneManager.LoadScene("SolarSystemGame");

   }
    public override void OnLeftRoom()
    {
        removeData();
    }
    // public override void OnMasterClientSwitched(Player newMasterClient)
    // {
    //     if(PhotonNetwork.LocalPlayer.UserId == newMasterClient.UserId){
            
    //     }
    // }

 
 
   private void Awake() {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith( task =>
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
       getRoomPlayers();
   }
   private void Start() {
       Debug.Log(PlayerPrefs.GetString("RoomCode"));
   }
   public void addNewPlayer(Player newPlayer){
       
        MyPlayer player = Instantiate(myPlayer, tr);
        if(player != null){
            player.setPlayerName(newPlayer);
            playerList.Add(player);
        }
        numOfJoinedPlayersText.text = ""+ PhotonNetwork.CurrentRoom.PlayerCount;
   }
   public void getRoomPlayers(){
       foreach (KeyValuePair <int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players){
           Debug.Log(playerInfo.Value);
           addNewPlayer(playerInfo.Value);
       }
           updatePosition();

   }
   public void updatePosition(){
       int i = 100;
          foreach (MyPlayer player in playerList) {
            Debug.Log("update");
         player.transform.GetChild(0).GetChild(0).gameObject.GetComponent<RectTransform>().localPosition = new Vector3(300,i,0);
         i-=20;//---------
               }
   }
 public override void OnPlayerLeftRoom(Player otherPlayer)
   {
        int index = playerList.FindIndex(x => x.Player == otherPlayer);
        if(index!= -1){
            Destroy(playerList[index].gameObject);
            playerList.RemoveAt(index);
        }

        numOfJoinedPlayersText.text = ""+ PhotonNetwork.CurrentRoom.PlayerCount;
    updatePosition();
    
 
   }
   public async Task removeKindergartenerData(){
        string waitingRoomId = reference.Root.Child("WaitingRooms").OrderByChild("RoomCode").OrderByValue().EqualTo(PlayerPrefs.GetString("RoomCode")).Reference.Parent.Key;
         reference = reference.Root.Child("WaitingRooms").Child(waitingRoomId);
       

      
    // await Task.Run(() =>  reference.Child("WaitingRooms").Child(waitingRoomId).Child("KindergartnerIDs").Child(user.UserId).RemoveValueAsync());
       
       
    }
       
    
     void InitializeFirebase(){
        reference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;
            }
    async void removeData(){
      await   removeKindergartenerData();
    }
}
   
