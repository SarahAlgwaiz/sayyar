using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
namespace com.cactusteam.Sayyar{

public class HomeSceneScript: MonoBehaviourPunCallbacks{
    
    [SerializeField]
private Button joinButton;
 [SerializeField]
private Button createButton;
[SerializeField]
private GameObject connectingText;

string gameVersion = "1";

    public void OnClickJoinButton(){
       SceneManager.LoadScene("JoinGameScene", LoadSceneMode.Single);
          }
    public void OnClickCreateButton(){
        SceneManager.LoadScene("CreateGameScene", LoadSceneMode.Single);
    }
        public override void OnConnectedToMaster(){
            Debug.Log("connected to master");
            createButton.gameObject.SetActive(true);
            joinButton.gameObject.SetActive(true);
            connectingText.SetActive(false);
        }
 
        public override void OnDisconnected(DisconnectCause cause){

            Debug.Log("disconnected" + cause);
        }       
          void Awake() {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
         void Start() {
               createButton.gameObject.SetActive(false);
               joinButton.gameObject.SetActive(false);
               connectingText.SetActive(true);
            Connect();
        }
          public void Connect(){
            if(PhotonNetwork.IsConnected){
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