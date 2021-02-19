using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;


public class showBadges : MonoBehaviour
{

    //DB variables definitions 
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

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

    //Planet Buttons definition
    public Button sunButton, earthButton, jupiterButton, marsButton, mercuryButton, neptuneButton, saturnButton, uransButton, venusButton;


    // Start is called before the first frame update
    void Start()
    {
        parentPanel.SetActive(false);
        VideosPanel.SetActive(false);

        //initially make all planet buttons unclickable
        sunButton.interactable = false;
        earthButton.interactable = false;
        jupiterButton.interactable = false;
        marsButton.interactable = false;
        mercuryButton.interactable = false;
        neptuneButton.interactable = false;
        saturnButton.interactable = false;
        uransButton.interactable = false;
        venusButton.interactable = false;



        getOwnedBadges(); //محتارة وين المفروض اناديها 


    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;



    }

    public async Task getOwnedBadges()
    {

        InitializeFirebase();
        DBreference = DBreference.Root;
        var OwnedBadges = await Task.Run(() => DBreference.Child("playerInfo").Child("" + User.UserId).Child("Badges").Child("EARTH_BAD").GetValueAsync().Result);
        if (OwnedBadges.Exists)
        {
            sunButton.interactable = true;

        }
    }

    public void popUp_Cancel_Button() // Badges pop up screen
    {
        parentPanel.SetActive(false);
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
