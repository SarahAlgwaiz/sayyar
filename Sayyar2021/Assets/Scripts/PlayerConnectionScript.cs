using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace com.cactusteam.Sayyar{
    public class PlayerConnectionScript: MonoBehaviourPunCallbacks{
        [Tooltip("Maximum number of players in each room. When a room is full, new players can't join in. Therefore a new room will be created for the new player.")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;
    [SerializeField]
private GameObject playButton;
[SerializeField]
private GameObject connectingText;
private InputField roomNumField;

private int roomNumber;

        public override void OnConnectedToMaster(){
            Debug.Log("connected to master");
        }
        public void createRoom(){
             roomNumber=  UnityEngine.Random.Range(0, 100000);
            PhotonNetwork.CreateRoom(""+roomNumber, new RoomOptions{IsVisible = false, IsOpen = true, MaxPlayers = maxPlayersPerRoom});
        }
        public override void OnCreatedRoom(){
            GUI.Label(new Rect(),""+roomNumber);

        }
        public void JoinRoom(){
            string roomCode = roomNumField.text;
            bool flag = false;
            foreach (char element in roomCode)
            {
                if(!Char.IsDigit(element)){
                    flag = true;
                }
            }
            if(roomCode.Length != 5){
                Debug.Log("Room code should be 5 characters long");
            }
            else if(flag){
                Debug.Log("Room code should contain numbers only");
            }
            else{
            PhotonNetwork.JoinRoom(roomCode);
            }
        }
        public override void OnDisconnected(DisconnectCause cause){
                    playButton.SetActive(true);
            connectingText.SetActive(false);
            Debug.Log("disconnected" + cause);
        }       
          string gameVersion = "1";
          void Awake() {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        public override void OnJoinRoomFailed(short returnCode, string message){
            Debug.Log("join room failed" + message);
        }
        public override void OnJoinedRoom(){
            Debug.Log("Success! joined room");
        }
         void Start() {
            playButton.SetActive(true);
            connectingText.SetActive(false);
        }
       public override void OnCreateRoomFailed(short returnCode, string message){
           Debug.Log("failed to create room");
           createRoom();
            
        }
        public void Connect(){
               playButton.SetActive(false);
            connectingText.SetActive(true);
            if(PhotonNetwork.IsConnected){
                PhotonNetwork.JoinRandomRoom();
                Debug.Log("Success");
            }
            else{
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
                Debug.Log("Success");

            }
        }
    }
}