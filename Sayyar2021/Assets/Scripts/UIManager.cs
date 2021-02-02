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
       // Panel_signUp.SetActive(false);
        registerUI.SetActive(false);
        mainScreen.SetActive(false);
         //PanelLoginEmail.SetActive(false); 
    }
    public void RegisterScreen() // Regester button
    {
       // Panel_signUp.SetActive(false);
        loginUI.SetActive(false);
        registerUI.SetActive(true);
        mainScreen.SetActive(false);
       //  PanelLoginEmail.SetActive(false); 
    }

    public void MainScreen() //Back button  
      {   
             //   Panel_signUp.SetActive(false);
               
          mainScreen.SetActive(true);     
         loginUI.SetActive(false);      
           registerUI.SetActive(false);
           // PanelLoginEmail.SetActive(false); 
           }


   

    public void openPanelـsignUp(){
        if (Panel_signUp != null)
        Panel_signUp.SetActive(true);

    }
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