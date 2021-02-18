using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        registerUI.SetActive(true);
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


    }

    public void MenuScreen() //Back button  
    {

        menuScreen.SetActive(true);
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        mainScreen.SetActive(false);
        homeScreen.SetActive(false);
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

        SceneManager.LoadSceneAsync("badges");

    }

}