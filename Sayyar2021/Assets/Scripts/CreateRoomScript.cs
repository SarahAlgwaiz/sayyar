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
 
private int roomNumber;


        public void createRoom(){
            Debug.Log("Connected create room");
             roomNumber=  UnityEngine.Random.Range(0, 100000);
            PhotonNetwork.CreateRoom(roomNumber.ToString("00000"), new RoomOptions{IsVisible = false, IsOpen = true, MaxPlayers = maxPlayersPerRoom});
            Debug.Log("Room Created");
        }
        public override void OnCreatedRoom(){
            roomCodeCreateField.text = "Room Number: "+roomNumber;
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
            SceneManager.LoadSceneAsync("SolarSystemGame");
        }
       public override void OnCreateRoomFailed(short returnCode, string message){
           Debug.Log("failed to create room");
           createRoom();
        }
    }
}