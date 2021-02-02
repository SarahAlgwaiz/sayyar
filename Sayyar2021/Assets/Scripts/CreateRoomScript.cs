using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;

namespace com.cactusteam.Sayyar{
    public class CreateRoomScript: MonoBehaviourPunCallbacks{
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
    }
}