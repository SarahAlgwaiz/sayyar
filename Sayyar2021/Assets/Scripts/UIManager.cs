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
    public GameObject Panel_signUp;
    public GameObject PanelLoginEmail;
    public GameObject FrogetPasswordPanel;
    


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

    }


    

    public void ClosePanelـsignUp()
    {
        Panel_signUp.SetActive(false);

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

   

    public void ResetPasswordButton()
    {
        FrogetPasswordPanel.SetActive(true);
    }

    public void CloseResetPasswordPanel()
    {
        FrogetPasswordPanel.SetActive(false);
    }

    //////////////All Work Below is fingerprint code 
    [DllImport("__Internal")]
    private static extern int _CMethod_FP();

public void fingerprintButton(){ 
_CMethod_FP();
}



}