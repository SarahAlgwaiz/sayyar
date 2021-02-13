using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameSynchronization : MonoBehaviour , IPunObservable
{

Rigidbody rb; 
PhotonView photonView;

Vector3 networkedPosition; 
Quaternion networkedRotaion;


private void Awake() {
    
    rb = GetComponent<Rigidbody>();
    photonView = GetComponent<PhotonView>();

    networkedPosition = new Vector3();
    networkedRotaion = new Quaternion();
}

    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    private void FixedUpdate() {
      
            rb.position = Vector3.MoveTowards(rb.position, networkedPosition, Time.fixedDeltaTime);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, networkedRotaion, Time.fixedDeltaTime*100);
       
    }

    public void OnPhotonSerializeView(PhotonStream stream , PhotonMessageInfo Info) 
    {


if(stream.IsWriting){

    //Then, PhotonView is mine and I am the one who controls this player. 
    //should send position, velocity etc. data to the other players
stream.SendNext(rb.position);
stream.SendNext(rb.rotation);
//stream.SendNext(rb.scale);

}else
{
    //called on my player gameobject that existis in remote player's game
    networkedPosition = (Vector3)stream.ReceiveNext();
   networkedRotaion = (Quaternion)stream.ReceiveNext();


}
    }
}
 