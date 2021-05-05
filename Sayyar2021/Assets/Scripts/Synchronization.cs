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
using UnityEngine.Video;


public class Synchronization : MonoBehaviourPunCallbacks
{
    // [SerializeField]
    // private TMPro.TMP_Text numOfJoinedPlayersText;

    [SerializeField]
    private TMPro.TMP_Text roomCodeText;

    private List<MyPlayer> playerList = new List<MyPlayer>();
    [SerializeField]
    private MyPlayer myPlayer;

    private List<bool> arePlayersReady;

    private bool isPlayerReady;

    private int count;

    [SerializeField]
    private GameObject start;
    [SerializeField]
    private GameObject readyButton;
    private PhotonView photonView;
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
    public VideoPlayer videoPlayer;
    public GameObject playIcon;

    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public DatabaseReference reference;
    private FirebaseUser user;
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        addNewPlayer(newPlayer);
        updatePosition();

    }
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("SolarSystemGame");
    }
    private void Awake()
    {
        videoPlayer.Pause();

        count = 0;
        PhotonNetwork.AutomaticallySyncScene = true;
        roomCodeText.text = PhotonNetwork.CurrentRoom.Name;
        //storeData();

        isPlayerReady = false;
        getRoomPlayers();
    }

    public void IamReady()
    {
        Debug.Log("inside stop");
        videoPlayer.Pause();
        readyButton.GetComponent<Button>().interactable = false;
        if (!PhotonNetwork.IsMasterClient && !isPlayerReady)
        {
            Debug.Log("not master client");
            photonView.RPC("updateReady", RpcTarget.MasterClient, 0);
            isPlayerReady = true;
        }
        else if (!isPlayerReady)
        {
            Debug.Log("is master client");
            updateReady(0);
            isPlayerReady = true;
        }
    }

    [PunRPC]
    private void updateReady(int a)
    {
        Debug.Log("inside update ready");
        if (arePlayersReady == null)
        {
            arePlayersReady = new List<bool>(PhotonNetwork.CurrentRoom.PlayerCount);

        }
        arePlayersReady.Add(true);
        bool flag = true;
        for (int i = 0; i < arePlayersReady.Count; i++)
        {
            if (!arePlayersReady[i])
            {
                flag = false;
            }
        }
        if (flag)
        {
            if (PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount && (arePlayersReady.Count == arePlayersReady.Capacity))
            {
                start.GetComponent<Button>().interactable = true;
            }
        }
    }
    public void PlayButton()
    {
        playIcon.SetActive(false);
        videoPlayer.Play();
    }
    private async void storeData()
    {

        FirebaseStorageAfterGame.InitializeFirebase();
        await FirebaseStorageAfterGame.storeGameData();
    }
    private void Start()
    {
        Debug.Log("room code in 85 " + roomCodeText.text);
        photonView = this.gameObject.GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient){
            readyButton.SetActive(false);
            updateReady(0);
            isPlayerReady = true;
            }else{
               start.SetActive(false);  
            }
    }

    public void addNewPlayer(Player newPlayer)
    {

        MyPlayer player = Instantiate(myPlayer, tr);
        if (player != null)
        {
            player.setPlayerName(newPlayer);
            playerList.Add(player);
        }
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
            i -= 180;
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("on player left");
        if (PhotonNetwork.IsMasterClient)
        {
            arePlayersReady.RemoveAt(arePlayersReady.Count - 1);
            start.SetActive(true);
        }
        int index = -1;
        foreach (MyPlayer playerInfo in playerList)
        {
            if (playerInfo.Player.Equals(otherPlayer))
            {
                index = playerList.IndexOf(playerInfo);
                Debug.Log("Index is " + index);
            }
        }
        if (index != -1)
        {
            Debug.Log("Before ddestroy !");
            Destroy(playerList[index].gameObject);
            Debug.Log("After destroy ");
            playerList.RemoveAt(index);
        }
        updatePosition();
        start.GetComponent<Button>().interactable = false;
    }
    public void onClickExitGameButton()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("HomeScene");//change it later to HomeScene 
    }

}

