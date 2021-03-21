using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using System.Threading.Tasks;
using TMPro;
using ArabicSupport;
using UnityEngine.UI;

public class CreateRoomScript : MonoBehaviourPunCallbacks
{


    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public DatabaseReference reference;
    private FirebaseUser user;

    [Header("Front-End")]
    public TextMeshProUGUI ErrorMsg;
    public Button twoPlayerButton;
    public Button threePlayerButton;
    public Button fourPlayerButton;
    public Color SelectedButtonColor;


    [Header("Other")]

    [Tooltip("Maximum number of players in each room. When a room is full, new players can't join in. Therefore a new room will be created for the new player.")]
    [SerializeField]
    private byte maxPlayersPerRoom = 2;

    [SerializeField]
    private GameObject createRoomView;
    [SerializeField]
    private TMPro.TMP_Text roomCodeCreateField;

    public static string virtualPlayroomKey;

    private int roomNumber;




    public void createRoom()
    {
        if (maxPlayersPerRoom == 0)
        {
            ErrorMsg.text = ArabicFixer.Fix("الرجاء اختيار عدد اللاعبين");
            return;
        }
        Debug.Log("Connected create room and max is "+maxPlayersPerRoom);
        roomNumber = UnityEngine.Random.Range(10000, 100000);
        Debug.Log("In create the room num is "+roomNumber);
        PhotonNetwork.CreateRoom(roomNumber.ToString("00000"), new RoomOptions { CleanupCacheOnLeave = false, IsVisible = false, IsOpen = true, MaxPlayers = maxPlayersPerRoom, PublishUserId = true });
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Room Created"); //popup pleeeease >> Where this should apear ? @HadeelHamad
        //roomCodeCreateField.text = "Room Number: " + roomNumber;
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
{
    dependencyStatus = task.Result;
    if (dependencyStatus == DependencyStatus.Available)
    {
        //If they are available Initialize Firebase
        InitializeFirebase();
    }
    else
    {
        Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
    }
});
    }
    // private void Start()
    // {
    //     createRoom();
    // }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("disconnected from server"); //popup pleeeease
    }
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("join room failed" + message);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Success! joined room");
        SceneManager.LoadSceneAsync("WaitingRoomScene");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("failed to create room"); //popup pleeeease
        createRoom();
    }

    async void InitializeFirebase()
    {
        reference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;
        await writeVirtualPlayroomData();
    }

    public async Task<string> writeVirtualPlayroomData()
    {
        reference = reference.Child("VirtualPlayrooms").Push();
        var key = reference.Key;
        reference = reference.Root;
        await Task.Run(() => reference.Child("VirtualPlayrooms").Child(key).Child("NumOfPlayers").SetValueAsync(maxPlayersPerRoom));// Num of player may change
        await Task.Run(() => reference.Child("VirtualPlayrooms").Child(key).Child("VirtualPlayroomID").SetValueAsync(key));
        //the following code should have a valid value (not null) otherwise the subsequent async calls will fail
        //await Task.Run(() =>reference.Child("VirtualPlayrooms").Child(key).Child("Game").SetValueAsync(key));
        return key;
    }

    //These following methods is for Front-End Matters

    public void TwoPlayerButton()
    {
        clearFields();

        ColorBlock tmp = twoPlayerButton.colors;
        tmp.selectedColor = SelectedButtonColor;
        tmp.normalColor = SelectedButtonColor;
        twoPlayerButton.colors = tmp;

        maxPlayersPerRoom = 2;
    }
    public void ThreePlayerButton()
    {
        clearFields();

        ColorBlock tmp = threePlayerButton.colors;
        tmp.selectedColor = SelectedButtonColor;
        tmp.normalColor = SelectedButtonColor;
        threePlayerButton.colors = tmp;

        maxPlayersPerRoom = 3;
    }
    public void FourPlayerButton()
    {
        clearFields();

        ColorBlock tmp = fourPlayerButton.colors;
        tmp.selectedColor = SelectedButtonColor;
        tmp.normalColor = SelectedButtonColor;
        fourPlayerButton.colors = tmp;

        maxPlayersPerRoom = 4;
    }

    public void clearFields()
    {
        ErrorMsg.text = ArabicFixer.Fix("");

        ColorBlock tmp = fourPlayerButton.colors;
        tmp.selectedColor = Color.white;
        tmp.normalColor = Color.white;
        fourPlayerButton.colors = tmp;

        tmp = twoPlayerButton.colors;
        tmp.selectedColor = Color.white;
        tmp.normalColor = Color.white;
        twoPlayerButton.colors = tmp;

        tmp = threePlayerButton.colors;
        tmp.selectedColor = Color.white;
        tmp.normalColor = Color.white;
        threePlayerButton.colors = tmp;

        maxPlayersPerRoom = 0;
    }


}



