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
               //this.gameObject.GetComponent<Renderer>().material.color = new Color (0.4f,0.4f,0.4f); //dark
               this.gameObject.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineAll;
               this.gameObject.GetComponent<Outline>().OutlineColor = new Color(1f,0.9373403f,0f,1f);
               this.gameObject.GetComponent<Outline>().OutlineWidth = 10f;
            }
           else {
          Debug.Log("Inside  Available");
               //this.gameObject.GetComponent<Renderer>().material.color = new Color (1f,1f,1f); //normal
               this.gameObject.GetComponent<Outline>().OutlineColor = new Color(1f,0.9373403f,0f,0f);

           }
        }
        else {
         Debug.Log("Inside Mine");
        //this.gameObject.GetComponent<Renderer>().material.color = new Color (1f,1f,1f);
        this.gameObject.GetComponent<Outline>().OutlineColor = new Color(1f,0.9373403f,0f,0f);
        if(this.gameObject.GetComponent<PlacementObject>().Moving){
          Debug.Log("Inside Moving");

            isAvailable = false;
        }
        else{
            Debug.Log("Inside Not Moving");
            isAvailable = true;

        }
        }
        Debug.Log("Available: " + isAvailable);

    //     if(!isAvailable && this.gameObject.GetComponent<PlacementObject>().Selected && !this.gameObject.GetComponent<PhotonView>().IsMine){

    //      Toast.Instance.Show(ArabicFixer.Fix("الكوكب مع زميلك حاليًا"),2f,Toast.ToastType.Warning);
    //       AudioManager.playSound("holdedPlanet");
        
    // }
    }

    
}
