// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Photon.Pun;
// using Photon;
// public class RPCsScript : MonoBehaviour
// {
//     public bool[] isTaken;
    
//     private void Awake() {
//        for(int i =0; i<PlanetsOnPlane.planets.Length; i++){
//            isTaken[i]= false;
//        }
//     }
//     public void planetTakenRPC(int planetNumber){
//         GetComponent<PhotonView>().RPC(
//             "PlanetTakenOthers",
//             PhotonTargets.Others,
//             new object[] {planetNumber}
//         );
//         PlanetTakenSelf(planetNumber);
//     }

//     public void planetReleasedRPC(int planetNumber){
//         GetComponent<PhotonView>().RPC(
//             "PlanetReleasedOthers",
//             PhotonTargets.Others,
//             new object[] {planetNumber}
//         );
//         PlanetReleasedSelf(planetNumber);
//     }
//     [PunRPC]
//     public void PlanetTakenOthers(int planetNumber){
//         if (Input.touchposition &&  PlanetsOnPlane.planets[planetNumber] != null)
//          {
//              if ( PlanetsOnPlane.planets[planetNumber].GetComponent<PhotonView>().ownerId == PhotonNetwork.LocalPlayer.ID) // I own this object already
//              {
//                  PlanetsOnPlane.planets[planetNumber].GetComponent<Material>().material.color = new Color (0.8f,0.8f,0.8f);

//              }
//              else
//              {
//                  PlanetsOnPlane.planets[planetNumber].GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer.ID); //someone else owns this object, and I will take it
//                  PlanetsOnPlane.planets[planetNumber].GetComponent<Material>().material.color = new Color (0.8f,0.8f,0.8f);
//                  PlanetsOnPlane.planets[planetNumber].GetComponent<Rigidbody>.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

//              }
//          }
//     }

//     public void PlanetTakenSelf(int planetNumber){
//         if (Input.touchposition &&  PlanetsOnPlane.planets[planetNumber] != null)
//          {
//              if ( PlanetsOnPlane.planets[planetNumber].GetComponent<PhotonView>().ownerId == PhotonNetwork.LocalPlayer.ID) // I own this object already
//              {
                
//              }
//              else
//              {
//                  PlanetsOnPlane.planets[planetNumber].GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer.ID); //someone else owns this object, and I will take it

//              }
//          }
//     }

//     public void PlanetReleasedSelf(){

//     }
 
    
// }
