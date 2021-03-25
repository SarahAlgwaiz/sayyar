using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using Firebase;
using TMPro;


public class Synchronization : MonoBehaviourPunCallbacks
{
    // [SerializeField]
    // private TMPro.TMP_Text numOfJoinedPlayersText;

    [SerializeField]
    private TMPro.TMP_Text roomCodeText;

    private List<MyPlayer> playerList = new List<MyPlayer>();
    [SerializeField]
    private MyPlayer myPlayer;

    [SerializeField]
    private Button start;
    [SerializeField]
    private GameObject playerBG;
    private Transform tr;

    // [Header("avatars")]
    // public Sprite avaterA ;
    // public Sprite avaterB ; 
    // public Sprite avaterC ;
    // public Sprite avaterD ;
    // public Sprite avaterE ;
    // public Sprite avaterF ;

    // [Header("playersAvatar")]
    // public Image HostAvatar ;
    // public Image PlayerONEAvatar ;
    // public Image PlayerTWOAvatar ;
    // public Image PlayerTHREEAvatar ;

    // [Header("playersName")]
    // public TMP_Text HostName;
    // public TMP_Text PlayerONEName;
    // public TMP_Text PlayerTWOName;
    // public TMP_Text PlayerTHREEName;


    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public DatabaseReference reference;
    private FirebaseUser user;
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        addNewPlayer(newPlayer);
        updatePosition();
        if (PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount && PhotonNetwork.IsMasterClient)
        {
            start.interactable = true;
        }
    }
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("SolarSystemGame");
    }
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        roomCodeText.text = PhotonNetwork.CurrentRoom.Name;
        //storeData();
        getRoomPlayers();
    }

    private async void storeData()
    {

        FirebaseStorageAfterGame.InitializeFirebase();
        await FirebaseStorageAfterGame.storeGameData();
    }
    private void Start()
    {
        Debug.Log("room code in 85 " + roomCodeText.text);
    }

    public void addNewPlayer(Player newPlayer)
    {

        MyPlayer player = Instantiate(myPlayer, tr);
        if (player != null)
        {
            player.setPlayerName(newPlayer);
            playerList.Add(player);
        }
        // numOfJoinedPlayersText.text = ""+ PhotonNetwork.CurrentRoom.PlayerCount;
    }
    public void getRoomPlayers()
    {
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log(playerInfo.Value);
            addNewPlayer(playerInfo.Value);
        }
        updatePosition();

    }
    public void updatePosition()
    {
        int i = 280;
        foreach (MyPlayer player in playerList)
        {
            player.transform.parent = playerBG.transform;
            Debug.Log("update");
            player.transform.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, i, 0);
            i -= 180;//---------
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {Debug.Log("on player left");
        int index = -1;
        foreach (MyPlayer playerInfo in playerList)
        {
            if(playerInfo.Player.Equals(otherPlayer))
            {
                index = playerList.IndexOf(playerInfo);
                Debug.Log("Index is "+index);
            }
        }
        //int index = playerList.FindIndex(x => x.Player.Equals(otherPlayer));
        if (index != -1)
        {
            Debug.Log("Before ddestroy !");
            Destroy(playerList[index].gameObject);
            Debug.Log("After destroy ");
            playerList.RemoveAt(index);
        }

        //  numOfJoinedPlayersText.text = ""+ PhotonNetwork.CurrentRoom.PlayerCount;
        updatePosition();
            start.interactable = false;

    }
     public void onClickExitGameButton(){
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("HomeScene");//change it later to HomeScene 
        }

}

