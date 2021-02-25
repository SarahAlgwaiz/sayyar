using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using ArabicSupport;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //Screen object variables
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject mainScreen;
    public GameObject homeScreen;
    public GameObject menuScreen;
    public GameObject Panel_signUp;
    public GameObject PanelLoginEmail;
    public GameObject Panel_ShowProfile;
    public GameObject Panel_EditProfile;
    public GameObject Panel_Badges;
    public GameObject parentPanel;
    public GameObject videoPanel;
    public GameObject FrogetPasswordPanel;
    public GameObject JoinRoomPanel;
    public InputField Num1;
    public GameObject CreateRoomPanel;



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

    //Functions to change the login screen UI
    public void LoginScreen() //Login button
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);
        mainScreen.SetActive(false);
    }
    public void RegisterScreen() // Regester button
    {
        registerUI.SetActive(true);
        Panel_signUp.SetActive(true);
    }

    public void MainScreen() //Back button  
    {

        mainScreen.SetActive(true);
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        Panel_signUp.SetActive(false);
        homeScreen.SetActive(false);

    }

    public void HomeScreen() //Back button  
    {

        homeScreen.SetActive(true);
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        mainScreen.SetActive(false);
        PanelLoginEmail.SetActive(false);
        //SceneManager.LoadScene("HomeScene");

    }

    public void MenuScreen() //Back button  
    {

        menuScreen.SetActive(true);
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        mainScreen.SetActive(false);
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

    public void ClosePanelـsignUp()
    {
        Panel_signUp.SetActive(false);

    }

    public void ClosePanel_ShowProfile()
    {
        Panel_ShowProfile.SetActive(false);
    }

    public void ClosePanel_EditProfile()
    {
        Panel_EditProfile.SetActive(false);
    }


    public void OpenPanel()
    {
        if (PanelLoginEmail != null)
        {
            PanelLoginEmail.SetActive(true);

        }
    }

    public void ClosePanel()
    {

        PanelLoginEmail.SetActive(false);

    }

    public void ClosePanel_menuScreen()
    {
        menuScreen.SetActive(false);
        HomeScreen();

    }

    public void showBadgesButton()
    {

        Panel_Badges.SetActive(true);
        parentPanel.SetActive(true);
        videoPanel.SetActive(false);

    }

    public void ResetPasswordButton()
    {
        FrogetPasswordPanel.SetActive(true);
    }

    public void CloseResetPasswordPanel()
    {
        FrogetPasswordPanel.SetActive(false);
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