using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void LoginScreen() //Back button
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);
        mainScreen.SetActive(false);
    }
    public void RegisterScreen() // Regester button
    {   registerUI.SetActive(true);
        Panel_signUp.SetActive(true);
        //loginUI.SetActive(false);
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

           }

    public void MenuScreen() //Back button  
      {   
               
        menuScreen.SetActive(true);     
        loginUI.SetActive(false);      
        registerUI.SetActive(false);
 mainScreen.SetActive(false);
         homeScreen.SetActive(false);     

           }

   

    // public void openPanelـsignUp(){
    //     if (registerUI != null)
    //     registerUI.SetActive(true);


    // }
    public void ClosePanelـsignUp(){
        Panel_signUp.SetActive(false);


    }
    


    //Login popup varible
   

    public void OpenPanel(){
        if(PanelLoginEmail != null){  
            PanelLoginEmail.SetActive(true); 
        }
    }

    public void ClosePanel(){
     
            PanelLoginEmail.SetActive(false); 
        
    }

}