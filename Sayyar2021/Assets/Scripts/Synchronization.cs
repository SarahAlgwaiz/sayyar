using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Synchronization : MonoBehaviourPunCallbacks
{
[SerializeField]
private TMPro.TMP_Text numOfJoinedPlayersText;

private List<MyPlayer> playerList = new List<MyPlayer>();
[SerializeField]
private MyPlayer myPlayer;
[SerializeField]
private GameObject myquad;
private Transform tr;
 public override void OnPlayerEnteredRoom(Player newPlayer)
   {
        addNewPlayer(newPlayer);
            updatePosition();

    //   Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
    //   numOfJoinedPlayersText.text= "" + PhotonNetwork.CurrentRoom.PlayerCount;
    //   Player[] playerslist = PhotonNetwork.PlayerList;
    //   for(int i = 0; i<playerslist.Length; i++){
    //       Player player = (Player) playerslist.GetValue(i);
    //       string playerName = player.NickName;
    //       if(playerList(i) != -1){

    //       }
    //   }
    //    Debug.Log("player "+newPlayer.NickName+" joined to room "+PhotonNetwork.CurrentRoom.Name);


   }
   private void Awake() {
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
       int i = 4;
          foreach (MyPlayer player in playerList) {
              player.gameObject.transform.position.Set(2,i--,0);
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
    //    Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
    //   numOfJoinedPlayersText.text= "" + PhotonNetwork.CurrentRoom.PlayerCount;
    //   Player[] playerslist = PhotonNetwork.PlayerList;
    //   for(int i = 0; i<playerslist.Length; i++){
    //       Player player = (Player) playerslist.GetValue(i);
    //       string playerName = player.NickName;
    //   }
    //    Debug.Log("player "+newPlayer.NickName+" joined to room "+PhotonNetwork.CurrentRoom.Name);
   }
}
   
