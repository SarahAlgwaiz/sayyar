using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

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
private GameObject GameView;
 
 
private int roomNumber;

string gameVersion = "1";


        public override void OnConnectedToMaster(){
            Debug.Log("connected to master");
            connectingText.SetActive(false);
        }
        public void createRoom(){
            while(!PhotonNetwork.IsConnected){
                Connect();
            }
            Debug.Log("Connected create room");
             roomNumber=  UnityEngine.Random.Range(0, 100000);
            PhotonNetwork.CreateRoom(roomNumber.ToString("00000"), new RoomOptions{IsVisible = false, IsOpen = true, MaxPlayers = maxPlayersPerRoom});
            Debug.Log("Room Created");
        }
        public override void OnCreatedRoom(){
            roomCodeCreateField.text = "Room Number: "+roomNumber;
        }
        
        public override void OnDisconnected(DisconnectCause cause){
            connectingText.SetActive(false);
            Debug.Log("disconnected" + cause);
        }       
          void Awake() {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        public override void OnJoinRoomFailed(short returnCode, string message){
            Debug.Log("join room failed" + message);
            connectingText.SetActive(false);
        }
        public override void OnJoinedRoom(){
            Debug.Log("Success! joined room");
            GameView.gameObject.SetActive(true);
            joinRoomView.SetActive(false);
        }
       public override void OnCreateRoomFailed(short returnCode, string message){
           Debug.Log("failed to create room");
           createRoom();
        }
    }
}