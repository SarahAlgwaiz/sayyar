using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.EventSystems;


public class RequestOwnershipScript : MonoBehaviourPun, IPunOwnershipCallbacks
{ 

    private void Awake() {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDestroy() {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer) {
        // OnOwnershipRequest gets called on every script that implements it every time a request for ownership transfer of any object occurs
        // So, firstly, only continue if this callback is getting called because *this* object is being transferred

        // when someone requests ownership from the targetView.
        if (targetView.gameObject != this.gameObject) {
            return;
        }
        if ((targetView.Owner != requestingPlayer))
        // && !targetView.gameObject.GetComponent<HideScript>().isAvailable)
        {
            Debug.Log("Requesting granted");
            targetView.TransferOwnership(requestingPlayer);
        }  
        if(!targetView.IsMine){
            Debug.Log("NOT MINE"); //replace code here
        }

    }

        public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner){
                //implement avatar/name change in here and on ownership change
                //whenever the owner changes for the targetView.
    }

        public void RequestOwnership(){
            Debug.Log("inside request ownership");
            this.gameObject.GetComponent<PhotonView>().RequestOwnership();
        }

     private void OnMouseDown() {
       RequestOwnership();
     }
    public void OnPointerDown(PointerEventData eventData)
    {
        RequestOwnership();
    }

}






