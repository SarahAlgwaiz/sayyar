using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using TMPro;

public class showBadges : MonoBehaviour
{

    //DB variables definitions 
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;
    public DependencyStatus dependencyStatus;
    public DatabaseReference reference;

    // Videos definitions
    public GameObject EARTH_VID;
    public GameObject JUPITER_VID;
    public GameObject MARS_VID;
    public GameObject MERCURY_VID;
    public GameObject NEPTUNE_VID;
    public GameObject SATURN_VID;
    public GameObject SUN_VID;
    public GameObject URANUS_VID;
    public GameObject VENUS_VID;

    //Other components definition
    public GameObject parentPanel;
    public GameObject VideosPanel;
    public GameObject showBadgesPanel;

    //Planet Buttons definition
    public Button sunButton, earthButton, jupiterButton, marsButton, mercuryButton, neptuneButton, saturnButton, uransButton, venusButton;
    // Planets Count definition 
    public TextMeshProUGUI sunCount, earthCount, jupiterCount, marsCount, mercuryCount, neptuneCount, saturnCount, uransCount, venusCount;
    // Planets Count BG definition 
    public GameObject sunBG, earthBG, jupiterBG, marsBG, mercuryBG, neptuneBG, saturnBG, uransBG, venusBG;
    void Awake()
    {
     
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
                
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });


    }
    // Start is called before the first frame update

    void Start()
    {
        
getOwnedBadges();
        
    }



    void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth inside show Badges");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;

    }

    public async void getOwnedBadges()
    {
        ///EARTH 
        Debug.Log("Iam in getOwned ");
        DataSnapshot BadgeID = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Badges").Child("EARTH_BAD").GetValueAsync().Result);
        if (BadgeID.Exists)
        {
            Debug.Log("Earth exists!!!!");
            earthButton.interactable = true;
            Debug.Log("Interactable!!!!!");
            var BadgeCount = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Badges").Child("EARTH_BAD").Child("BadgeCount").GetValueAsync().Result.Value);
            if (BadgeCount != null && !(("" + BadgeCount).Equals("1")))
            {
                earthCount.text = "X" + BadgeCount;
                earthBG.SetActive(true);
            }
        }

        ///SUN 
        DataSnapshot BadgeID2 = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Badges").Child("SUN_BAD").GetValueAsync().Result);
        if (BadgeID2.Exists)
        {
            sunButton.interactable = true;
            var BadgeCount = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Badges").Child("SUN_BAD").Child("BadgeCount").GetValueAsync().Result.Value);
            if (BadgeCount != null && !(("" + BadgeCount).Equals("1")))
            {
                sunCount.text = "X" + BadgeCount;
                sunBG.SetActive(true);
            }
        }

        ///MERCURY_BAD 
        DataSnapshot BadgeID3 = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Badges").Child("MERCURY_BAD").GetValueAsync().Result);
        if (BadgeID3.Exists)
        {
            mercuryButton.interactable = true;
            var BadgeCount = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Badges").Child("MERCURY_BAD").Child("BadgeCount").GetValueAsync().Result.Value);
            if (BadgeCount != null && !(("" + BadgeCount).Equals("1")))
            {
                mercuryCount.text = "X" + BadgeCount;
                mercuryBG.SetActive(true);
            }
        }


        ///VENUS_BAD 
        DataSnapshot BadgeID4 = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Badges").Child("VENUS_BAD").GetValueAsync().Result);
        if (BadgeID4.Exists)
        {
            venusButton.interactable = true;
            var BadgeCount = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Badges").Child("VENUS_BAD").Child("BadgeCount").GetValueAsync().Result.Value);
            if (BadgeCount != null && !(("" + BadgeCount).Equals("1")))
            {
                venusCount.text = "X" + BadgeCount;
                venusBG.SetActive(true);
            }
        }


        ///MARS_BAD 
        DataSnapshot BadgeID5 = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Badges").Child("MARS_BAD").GetValueAsync().Result);
        if (BadgeID5.Exists)
        {
            marsButton.interactable = true;
            var BadgeCount = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Badges").Child("MARS_BAD").Child("BadgeCount").GetValueAsync().Result.Value);
            if (BadgeCount != null && !(("" + BadgeCount).Equals("1")))
            {
                marsCount.text = "X" + BadgeCount;
                marsBG.SetActive(true);
            }
        }

        ///JUPITER_BAD 
        DataSnapshot BadgeID6 = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Badges").Child("JUPITER_BAD").GetValueAsync().Result);
        if (BadgeID6.Exists)
        {
            jupiterButton.interactable = true;
            var BadgeCount = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Badges").Child("JUPITER_BAD").Child("BadgeCount").GetValueAsync().Result.Value);
            if (BadgeCount != null && !(("" + BadgeCount).Equals("1")))
            {
                jupiterCount.text = "X" + BadgeCount;
                jupiterBG.SetActive(true);
            }
        }

        ///SATURN_BAD 
        DataSnapshot BadgeID7 = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Badges").Child("SATURN_BAD").GetValueAsync().Result);
        if (BadgeID7.Exists)
        {
            saturnButton.interactable = true;
            var BadgeCount = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Badges").Child("SATURN_BAD").Child("BadgeCount").GetValueAsync().Result.Value);
            if (BadgeCount != null && !(("" + BadgeCount).Equals("1")))
            {
                saturnCount.text = "X" + BadgeCount;
                saturnBG.SetActive(true);
            }
        }

        ///URANUS_BAD 
        DataSnapshot BadgeID8 = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Badges").Child("URANUS_BAD").GetValueAsync().Result);
        if (BadgeID8.Exists)
        {
            uransButton.interactable = true;
            var BadgeCount = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Badges").Child("URANUS_BAD").Child("BadgeCount").GetValueAsync().Result.Value);
            if (BadgeCount != null && !(("" + BadgeCount).Equals("1")))
            {
                uransCount.text = "X" + BadgeCount;
                uransBG.SetActive(true);
            }
        }

        ///NEPTUNE_BAD 
        DataSnapshot BadgeID9 = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Badges").Child("NEPTUNE_BAD").GetValueAsync().Result);
        if (BadgeID9.Exists)
        {
            neptuneButton.interactable = true;
            var BadgeCount = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("Badges").Child("NEPTUNE_BAD").Child("BadgeCount").GetValueAsync().Result.Value);
            if (BadgeCount != null && !(("" + BadgeCount).Equals("1")))
            {
                neptuneCount.text = "X" + BadgeCount;
                neptuneBG.SetActive(true);
            }
        }



    }

    public void popUp_Cancel_Button() // Badges pop up screen
    {
        parentPanel.SetActive(false);
        showBadgesPanel.SetActive(false);

    }

    public void SUN_Button()
    {

        VideosPanel.SetActive(true);
        EARTH_VID.SetActive(false);
        JUPITER_VID.SetActive(false);
        MARS_VID.SetActive(false);
        MERCURY_VID.SetActive(false);
        NEPTUNE_VID.SetActive(false);
        SATURN_VID.SetActive(false);
        SUN_VID.SetActive(true);
        URANUS_VID.SetActive(false);
        VENUS_VID.SetActive(false);

    }

    public void EARTH_Button()
    {

        VideosPanel.SetActive(true);
        EARTH_VID.SetActive(true);
        JUPITER_VID.SetActive(false);
        MARS_VID.SetActive(false);
        MERCURY_VID.SetActive(false);
        NEPTUNE_VID.SetActive(false);
        SATURN_VID.SetActive(false);
        SUN_VID.SetActive(false);
        URANUS_VID.SetActive(false);
        VENUS_VID.SetActive(false);

    }

    public void JUPITER_Button()
    {

        VideosPanel.SetActive(true);
        EARTH_VID.SetActive(false);
        JUPITER_VID.SetActive(true);
        MARS_VID.SetActive(false);
        MERCURY_VID.SetActive(false);
        NEPTUNE_VID.SetActive(false);
        SATURN_VID.SetActive(false);
        SUN_VID.SetActive(false);
        URANUS_VID.SetActive(false);
        VENUS_VID.SetActive(false);

    }

    public void MARS_Button()
    {

        VideosPanel.SetActive(true);
        EARTH_VID.SetActive(false);
        JUPITER_VID.SetActive(false);
        MARS_VID.SetActive(true);
        MERCURY_VID.SetActive(false);
        NEPTUNE_VID.SetActive(false);
        SATURN_VID.SetActive(false);
        SUN_VID.SetActive(false);
        URANUS_VID.SetActive(false);
        VENUS_VID.SetActive(false);

    }

    public void MERCURY_Button()
    {

        VideosPanel.SetActive(true);
        EARTH_VID.SetActive(false);
        JUPITER_VID.SetActive(false);
        MARS_VID.SetActive(false);
        MERCURY_VID.SetActive(true);
        NEPTUNE_VID.SetActive(false);
        SATURN_VID.SetActive(false);
        SUN_VID.SetActive(false);
        URANUS_VID.SetActive(false);
        VENUS_VID.SetActive(false);

    }

    public void NEPTUNE_Button()
    {

        VideosPanel.SetActive(true);
        EARTH_VID.SetActive(false);
        JUPITER_VID.SetActive(false);
        MARS_VID.SetActive(false);
        MERCURY_VID.SetActive(false);
        NEPTUNE_VID.SetActive(true);
        SATURN_VID.SetActive(false);
        SUN_VID.SetActive(false);
        URANUS_VID.SetActive(false);
        VENUS_VID.SetActive(false);

    }

    public void SATURN_Button()
    {

        VideosPanel.SetActive(true);
        EARTH_VID.SetActive(false);
        JUPITER_VID.SetActive(false);
        MARS_VID.SetActive(false);
        MERCURY_VID.SetActive(false);
        NEPTUNE_VID.SetActive(false);
        SATURN_VID.SetActive(true);
        SUN_VID.SetActive(false);
        URANUS_VID.SetActive(false);
        VENUS_VID.SetActive(false);

    }

    public void URANUS_Button()
    {

        VideosPanel.SetActive(true);
        EARTH_VID.SetActive(false);
        JUPITER_VID.SetActive(false);
        MARS_VID.SetActive(false);
        MERCURY_VID.SetActive(false);
        NEPTUNE_VID.SetActive(false);
        SATURN_VID.SetActive(false);
        SUN_VID.SetActive(false);
        URANUS_VID.SetActive(true);
        VENUS_VID.SetActive(false);

    }

    public void VENUS_Button()
    {

        VideosPanel.SetActive(true);
        EARTH_VID.SetActive(false);
        JUPITER_VID.SetActive(false);
        MARS_VID.SetActive(false);
        MERCURY_VID.SetActive(false);
        NEPTUNE_VID.SetActive(false);
        SATURN_VID.SetActive(false);
        SUN_VID.SetActive(false);
        URANUS_VID.SetActive(false);
        VENUS_VID.SetActive(true);

    }

    public void video_Cancel_Button()
    {
        VideosPanel.SetActive(false);
    }


}
