using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace com.cactusteam.Sayyar{
    public class JoinRoomScript: MonoBehaviourPunCallbacks{
        [Tooltip("Maximum number of players in each room. When a room is full, new players can't join in. Therefore a new room will be created for the new player.")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;
    
[SerializeField]
private GameObject joinRoomView;
[SerializeField]
private Button roomCodeConfirmButton;
[SerializeField]
private TMPro.TMP_InputField roomNumField;
 
 [SerializeField]
private GameObject GameView;
 
 [SerializeField]
private Button ExitGameButton;
 
private int roomNumber;

string gameVersion = "1";


    public void OnClickRoomCodeConfirmButton(){
        joinRoomView.SetActive(false);
        JoinRoom();
    }
        public override void OnConnectedToMaster(){
            Debug.Log("connected to master");
            connectingText.SetActive(false);
        }
        
        public void JoinRoom(){
            while(!PhotonNetwork.IsConnected){
                Connect();
            }         
               string roomCode = roomNumField.text;
               if(roomCode.Length!=5){
                   Debug.Log("Room Code length should be 5");
               }
          
            PhotonNetwork.JoinRoom(roomCode);
            
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
        }
        public override void OnJoinedRoom(){
            Debug.Log("Success! joined room");
            GameView.gameObject.SetActive(true);
            joinRoomView.SetActive(false);
        }

    }
}