using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class HideScript : MonoBehaviour, IPunObservable
{
   public bool isAvailable = true;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo messageInfo){
       if(stream.IsWriting){

    stream.SendNext(isAvailable);

    }
    else
    {
    isAvailable = (bool)stream.ReceiveNext();

}
    }
    // Update is called once per frame
    public void Update()
    {
        Debug.Log("Inside HideScript");
        if(!this.gameObject.GetComponent<PhotonView>().IsMine){
           Debug.Log("Inside Not Mine");
            if(!isAvailable){
        Debug.Log("Inside Not Available");
               this.gameObject.GetComponent<Renderer>().material.color = new Color (0.8f,0.8f,0.8f); //dark
            }
           else {
          Debug.Log("Inside  Available");
               this.gameObject.GetComponent<Renderer>().material.color = new Color (1f,1f,1f); //normal
           }
        }
        else {
         Debug.Log("Inside Mine");
        this.gameObject.GetComponent<Renderer>().material.color = new Color (1f,1f,1f);
        if(this.gameObject.GetComponent<PlacementObject>().Moving){
            isAvailable = false;
        }
        else{
            isAvailable = true;

        }
        }
    }
}
