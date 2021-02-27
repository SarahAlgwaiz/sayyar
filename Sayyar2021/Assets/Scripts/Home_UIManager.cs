using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using ArabicSupport;
using UnityEngine.UI;

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
  //Variable to prepare Badges After login
   // public GameObject holdedScript;
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

    void start(){
        //holdedScript.SetActive(true);
    }
//  public void HomeScreen() //Back button  
//     {

//         homeScreen.SetActive(true);
        
//         //SceneManager.LoadScene("HomeScene");

//     }

    public void MenuScreen() //Back button  
    {

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
}
