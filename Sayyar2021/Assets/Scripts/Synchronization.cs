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

private Transform tr;
 public override void OnPlayerEnteredRoom(Player newPlayer)
   {
        addNewPlayer(newPlayer);
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
           addNewPlayer(playerInfo.Value);
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
   
