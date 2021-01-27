using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
namespace com.cactusteam.Sayyar{
    public class PlayerConnectionScript: MonoBehaviourPunCallbacks{
        [Tooltip("Maximum number of players in each room. When a room is full, new players can't join in. Therefore a new room will be created for the new player.")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;


        public override void OnConnectedToMaster(){
            Debug.Log("connected to master");
            PhotonNetwork.JoinRandomRoom();
        }
        public override void OnDisconnected(DisconnectCause cause){
            Debug.Log("disconnected" + cause);
        }       
          string gameVersion = "1";
          void Awake() {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        public override void OnJoinRandomFailed(short returnCode, string message){
            Debug.Log("join room failed" + message);
            PhotonNetwork.CreateRoom(null, new RoomOptions{MaxPlayers = maxPlayersPerRoom});
        }
        public override void OnJoinedRoom(){
            Debug.Log("Success! joined room");
        }
        

        public void Connect(){
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