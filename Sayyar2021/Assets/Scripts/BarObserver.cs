
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BarObserver: MonoBehaviour, IPunObservable
{
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo messageInfo){
        if(stream.IsWriting){
            Debug.Log("writing");
            stream.SendNext(this.gameObject.GetComponent<RectTransform>().localScale);
        }
        else{
          Debug.Log("reading");
            Vector3 timerScale = (Vector3) stream.ReceiveNext();
           this.gameObject.GetComponent<RectTransform>().localScale.Set(timerScale.x,timerScale.y,timerScale.z);
        }
    }
}
