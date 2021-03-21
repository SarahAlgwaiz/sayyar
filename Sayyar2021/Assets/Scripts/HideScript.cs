using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class HideScript : MonoBehaviour
{

   public bool isAvailable = true;
    // Update is called once per frame
    public void Update()
    {
        Debug.Log("Inside HideScript");
        if(!this.gameObject.GetComponent<PhotonView>().IsMine){
           if(this.gameObject.GetComponent<PlacementObject>().Moving){
               this.gameObject.GetComponent<Renderer>().material.color = new Color (0.8f,0.8f,0.8f);
                isAvailable = false;
           }
           else {
               this.gameObject.GetComponent<Renderer>().material.color = new Color (1f,1f,1f);
               isAvailable = true;
           }
        }
        else {
        this.gameObject.GetComponent<Renderer>().material.color = new Color (1f,1f,1f);
           if(this.gameObject.GetComponent<PlacementObject>().Moving){
                isAvailable = false;

        }
        else     isAvailable = true;

        }
    }
}
