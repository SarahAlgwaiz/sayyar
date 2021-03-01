using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;
public class RPCScript : MonoBehaviour
{
    public bool[] isTaken;


    Rigidbody rb; 
    Vector3 networkedPosition; 
    Quaternion networkedRotaion;


    private static PhotonView photonView;


     //  for(int i =0; i<PlanetsOnPlane.planets.Length; i++){
      //     isTaken[i]= false;
     //  }
    public static void solarSystemCall(PhotonView photonView, GameObject solarSystem, Vector3 placePosition){
        object[] parameters = new object[2];
        parameters[0] = solarSystem;
        parameters[1] = placePosition;
        photonView.RPC("initializeSolarSystem", RpcTarget.All,parameters);
    }
    public static GameObject initializeSolarSystem(GameObject solarSystem, Vector3 placePosition){
                Debug.Log("PhotonView" + photonView != null);
                Debug.Log("Is Mine: " + photonView.IsMine);
                Debug.Log("Is MasterClient: " + PhotonNetwork.IsMasterClient);
                Debug.Log("Solar system name : " + solarSystem.name);
                Debug.Log("Place position : " + placePosition);

          if(photonView.IsMine){
        if(PhotonNetwork.IsMasterClient){
            return PhotonNetwork.Instantiate(solarSystem.name,placePosition,Quaternion.identity,0, null); 
        }
          }
          return null;
    }
    public static void initializePlanetsRPC(GameObject[] planets){
     if(photonView.IsMine){
     if(PhotonNetwork.IsMasterClient){
     for(int i=0; i<planets.Length;i++){
    float randomX = Random.Range(-3, 3);
    //float randomY = Random.Range(-3, 3);
     float randomZ = Random.Range(-3, 3);
    Vector3 randomPosition = new Vector3 (randomX, 0, randomZ);    
    PhotonNetwork.Instantiate(planets[i].name,randomPosition,Quaternion.identity);
        }
        }
    }
    }

private void Awake() {
    
    rb = GetComponent<Rigidbody>();
    photonView = GetComponent<PhotonView>();

    networkedPosition = new Vector3();
    networkedRotaion = new Quaternion();
}
    void Start(){
        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 10;
    }
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

    }
    else
    {
    //called on my player gameobject that existis in remote player's game
    networkedPosition = (Vector3)stream.ReceiveNext();
   networkedRotaion = (Quaternion)stream.ReceiveNext();

}
    }
}
    
    // }
    // public void planetTakenRPC(int planetNumber){
    //     GetComponent<PhotonView>().RPC(
    //         "PlanetTakenOthers",
    //         RpcTarget.Others,
    //         new object[] {planetNumber}
    //     );
    //     PlanetTakenSelf(planetNumber);
    // }

    // public void planetReleasedRPC(int planetNumber){
    //     GetComponent<PhotonView>().RPC(
    //         "PlanetReleasedOthers",
    //         RpcTarget.Others,
    //         new object[] {planetNumber}
    //     );
    //     PlanetReleasedSelf(planetNumber);
    // }
    // [PunRPC]
    // public void PlanetTakenOthers(int planetNumber){
    //     if (Input.touchposition &&  PlanetsOnPlane.planets[planetNumber] != null)
    //      {
    //          if ( PlanetsOnPlane.planets[planetNumber].GetComponent<PhotonView>().ownerId == PhotonNetwork.LocalPlayer.ID) // I own this object already
    //          {
    //              PlanetsOnPlane.planets[planetNumber].GetComponent<Material>().material.color = new Color (0.8f,0.8f,0.8f);
    //          }
    //          else
    //          {
    //              PlanetsOnPlane.planets[planetNumber].GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer.ID); //someone else owns this object, and I will take it
    //              PlanetsOnPlane.planets[planetNumber].GetComponent<Material>().material.color = new Color (0.8f,0.8f,0.8f);
    //              PlanetsOnPlane.planets[planetNumber].GetComponent<Rigidbody>.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

    //          }
    //      }
    // }

    // public void PlanetTakenSelf(int planetNumber){
    //     if (Input.touchposition &&  PlanetsOnPlane.planets[planetNumber] != null)
    //      {
    //          if ( PlanetsOnPlane.planets[planetNumber].GetComponent<PhotonView>().ownerId == PhotonNetwork.LocalPlayer.ID) // I own this object already
    //          {
                
    //          }
    //          else
    //          {
    //              PlanetsOnPlane.planets[planetNumber].GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer.ID); //someone else owns this object, and I will take it

    //          }
    //      }
    // }

    // public void PlanetReleasedSelf(){

    // }
 

