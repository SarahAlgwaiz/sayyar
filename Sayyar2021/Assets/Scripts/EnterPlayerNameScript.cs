using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.UI;
using System.Threading.Tasks;
namespace com.cactusteam.Sayyar{
public class EnterPlayerNameScript : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Awake() {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
}

}
