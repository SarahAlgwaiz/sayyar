using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

namespace com.cactusteam.Sayyar{
public class EnterPlayerNameScript : MonoBehaviour
{
[SerializeField]
private TMPro.TMP_Text playerNameText;
    const string namePrefKey = "Name";

    // Start is called before the first frame update
    void Start()
    {
        string name = "Sarah"; //replace this with the current user's name stored in DB
        if(PlayerPrefs.HasKey(namePrefKey)){
            name = PlayerPrefs.GetString(namePrefKey);
            Debug.Log("name already exists");
        }
       else{
           Debug.Log("new name");
        PlayerPrefs.SetString(namePrefKey,name);     
          } 
          PhotonNetwork.NickName = name;
          playerNameText.text = PhotonNetwork.NickName;

    }

}

}
