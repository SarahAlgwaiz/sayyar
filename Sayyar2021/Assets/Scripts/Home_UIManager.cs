using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using ArabicSupport;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using ArabicSupport;
using TMPro;
using System.Threading.Tasks;
using System.Runtime.InteropServices;





public class Home_UIManager : MonoBehaviour
{
    public static Home_UIManager instance;
    public GameObject menuScreen;
    public GameObject Panel_ShowProfile;
    public GameObject Panel_EditProfile;
    public GameObject Panel_Badges;
    public GameObject parentPanel;
    public GameObject videoPanel;
    public GameObject JoinRoomPanel;
    public InputField Num1;
    public GameObject CreateRoomPanel;

    public FirebaseAuth auth;
    public DatabaseReference DBreference;
    public TextMeshProUGUI userName;

    //Variable to prepare Badges After login
    // public GameObject holdedScript;

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;

    }

    [DllImport("__Internal")]
    private static extern void _storeDeviceToken(string userID);

    async void CheckFP()
    {

        var isON = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("isFPallowed").GetValueAsync().Result.Value);
        string isOn = await Task.Run(() => isON.ToString());

        if (isOn == "1")
        {

            _storeDeviceToken(auth.CurrentUser.UserId);
            DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("isFPallowed").SetValueAsync("1");

        }
    }


    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    void start()
    {
        CheckFP();
        //holdedScript.SetActive(true);
    }
    //  public void HomeScreen() //Back button  
    //     {

    //         homeScreen.SetActive(true);

    //         //SceneManager.LoadScene("HomeScene");

    //     }

    public void MenuScreen() //Back button  
    {

        getUserName();
        menuScreen.SetActive(true);
        //homeScreen.SetActive(false);
        Panel_ShowProfile.SetActive(false);
        Panel_EditProfile.SetActive(false);
    }
    public void openPanel_ShowProfile()
    {
        if (Panel_ShowProfile != null)
            Panel_ShowProfile.SetActive(true);
    }

    public void openPanel_EditProfile()
    {
        if (Panel_EditProfile != null)
            Panel_EditProfile.SetActive(true);
    }

    public void ClosePanel_ShowProfile()
    {
        Panel_ShowProfile.SetActive(false);
    }

    public void ClosePanel_EditProfile()
    {
        Panel_EditProfile.SetActive(false);
    }
    public void ClosePanel_menuScreen()
    {
        menuScreen.SetActive(false);
        //HomeScreen();

    }

    public void showBadgesButton()
    {

        Panel_Badges.SetActive(true);
        parentPanel.SetActive(true);
        videoPanel.SetActive(false);

    }
    public void JoinRoomButton()
    {
        JoinRoomPanel.SetActive(true);
        Num1.ActivateInputField();
    }

    public void CloseJoinRoomPanelButton()
    {
        JoinRoomPanel.SetActive(false);
    }

    public void CreateRoomButton()
    {
        CreateRoomPanel.SetActive(true);
    }

    public void CloseCreateRoomPanelButton()
    {
        CreateRoomPanel.SetActive(false);
    }

    public async void getUserName()
    {
        InitializeFirebase();
        var Name = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Username").GetValueAsync().Result.Value) as string;
        userName.text = Name + ArabicFixer.Fix("رائد/ة الفضاء: ");
    }
}
