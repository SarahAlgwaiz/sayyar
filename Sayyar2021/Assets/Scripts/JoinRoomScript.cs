using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;

namespace com.cactusteam.Sayyar{
    public class JoinRoomScript: MonoBehaviourPunCallbacks{
    
    
[SerializeField]
private GameObject joinRoomView;
[SerializeField]
private Button roomCodeConfirmButton;
[SerializeField]
private TMPro.TMP_InputField roomNumField;



    public void OnClickRoomCodeConfirmButton(){
        JoinRoom();
    }
        public override void OnConnectedToMaster(){
            Debug.Log("connected to master");
        }
        
        public void JoinRoom(){    
               string roomCode = roomNumField.text;
               if(roomCode.Length!=5){
                   Debug.Log("Room Code length should be 5");
               }
          
            PhotonNetwork.JoinRoom(roomCode);
            
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

    }
}