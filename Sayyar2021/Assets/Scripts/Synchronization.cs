using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
}
   
