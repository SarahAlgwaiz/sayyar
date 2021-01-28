using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

namespace com.cactusteam.Sayyar{
[RequireComponent(typeof(InputField))] //this script requires a component of type inputfield, which, in this case, is the player's name
public class EnterPlayerNameScript : MonoBehaviour
{

    const string namePrefKey = "Name";

    // Start is called before the first frame update
    void Start()
    {
        string name = "Sarah"; //replace this with the current user's name stored in DB
        if(PlayerPrefs.HasKey(namePrefKey)){
            name = PlayerPrefs.GetString(namePrefKey);
        }
        PhotonNetwork.NickName = name;

    }

    public void setName(string name){
        PhotonNetwork.NickName = name;
        PlayerPrefs.SetString(namePrefKey,name);
    }
}

}
