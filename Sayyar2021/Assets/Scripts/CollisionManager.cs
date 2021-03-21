using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CollisionManager : MonoBehaviour
{
     private void OnTriggerEnter(Collider other) {
       Debug.Log("collided outside");
       Debug.Log("other " + other.gameObject.name);
       Debug.Log("name " + name);
        if((other.gameObject.name + "(Clone)") == name){
        // other.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
            Debug.Log("collided inside");
            Debug.Log("after destroy");
            switch(name){
                 case "Mercury(Clone)":
                 this.gameObject.GetComponent<PhotonView>().RPC("updatePlanetInsertion",RpcTarget.All,new object[]{0,other});
                 Debug.Log("planet 0 done");
                 break;
                 case "Venus(Clone)":
                 this.gameObject.GetComponent<PhotonView>().RPC("updatePlanetInsertion",RpcTarget.All,new object[]{1,other});

                 Debug.Log("planet 1 done");
                 break;
                 case "Earth(Clone)":
                 this.gameObject.GetComponent<PhotonView>().RPC("updatePlanetInsertion",RpcTarget.All,new object[]{2,other});

                Debug.Log("planet 2 done");
                 break;
                  case "Mars(Clone)":
                 this.gameObject.GetComponent<PhotonView>().RPC("updatePlanetInsertion",RpcTarget.All,new object[]{3,other});

                 Debug.Log("planet 3 done");
                  break;
                  case "Jupiter(Clone)":
                 this.gameObject.GetComponent<PhotonView>().RPC("updatePlanetInsertion",RpcTarget.All,new object[]{4,other});

                  Debug.Log("planet 4 done");
                  break;
                   case "Saturn(Clone)":
                 this.gameObject.GetComponent<PhotonView>().RPC("updatePlanetInsertion",RpcTarget.All,new object[]{5,other});

                 Debug.Log("planet 5 done");
                   break;
                   case "Uranus(Clone)":
                 this.gameObject.GetComponent<PhotonView>().RPC("updatePlanetInsertion",RpcTarget.All,new object[]{6,other});

                  Debug.Log("planet 6 done");
                   break;
                    case "Neptune(Clone)":
                 this.gameObject.GetComponent<PhotonView>().RPC("updatePlanetInsertion",RpcTarget.All,new object[]{7,other});

                   Debug.Log("planet 7 done");
                    break;
                    default: break;
            }
    }
        }


        [PunRPC]
        public void updatePlanetInsertion(int planetNumber, Collider other){
        Debug.Log("inside RPC");
        other.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
        this.gameObject.SetActive(false);
        switch(planetNumber){
          case 0:
          PlanetsOnPlane.isPlanetInserted[0] = true;
          break;
          case 1:
          PlanetsOnPlane.isPlanetInserted[1] = true;
          break;
          case 2:
          PlanetsOnPlane.isPlanetInserted[2] = true;
          break;
          case 3:
          PlanetsOnPlane.isPlanetInserted[3] = true;
          break;
          case 4:
          PlanetsOnPlane.isPlanetInserted[4] = true;
          break;
          case 5:
          PlanetsOnPlane.isPlanetInserted[5] = true;
          break;
          case 6:
          PlanetsOnPlane.isPlanetInserted[6] = true;
          break;
          case 7:
          PlanetsOnPlane.isPlanetInserted[7] = true;
          break;
          default:
          break;
        }
        }
}