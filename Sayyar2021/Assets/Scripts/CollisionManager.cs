using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CollisionManager : MonoBehaviour, IPunObservable
{
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo messageInfo){
       if(stream.IsWriting){
    stream.SendNext(PlanetsOnPlane.isPlanetInserted);
    }
    else
    {
      bool[] planetsArr =  (bool[]) stream.ReceiveNext();
      for(int i =0; i<planetsArr.Length; i++){
        if(planetsArr[i])
        PlanetsOnPlane.isPlanetInserted[i] = planetsArr[i];
      }
  }
    }
     private void OnTriggerEnter(Collider other) {
       Debug.Log("collided outside");
       Debug.Log("other " + other.gameObject.name);
       Debug.Log("name " + name);
        if((other.gameObject.name + "(Clone)") == name){
         other.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
            Debug.Log("collided inside");
            this.gameObject.SetActive(false);
            Debug.Log("after destroy");
            other.GetComponent<PhotonView>().RequestOwnership();
            switch(name){
                 case "Mercury(Clone)":
                 PlanetsOnPlane.isPlanetInserted[0] = true;
                 this.gameObject.SetActive(false);
                 Debug.Log("planet 0 done");
                 break;
                 case "Venus(Clone)":
                 PlanetsOnPlane.isPlanetInserted[1] = true;
                 this.gameObject.SetActive(false);
                 Debug.Log("planet 1 done");
                 break;
                 case "Earth(Clone)":
                 PlanetsOnPlane.isPlanetInserted[2] = true;
                 this.gameObject.SetActive(false);
                Debug.Log("planet 2 done");
                 break;
                  case "Mars(Clone)":
                 PlanetsOnPlane.isPlanetInserted[3] = true;
                 this.gameObject.SetActive(false);
                 Debug.Log("planet 3 done");
                  break;
                  case "Jupiter(Clone)":
                  PlanetsOnPlane.isPlanetInserted[4] = true;
                  this.gameObject.SetActive(false);
                  Debug.Log("planet 4 done");
                  break;
                   case "Saturn(Clone)":
                 PlanetsOnPlane.isPlanetInserted[5] = true;
                 this.gameObject.SetActive(false);
                 Debug.Log("planet 5 done");
                   break;
                   case "Uranus(Clone)":
                  PlanetsOnPlane.isPlanetInserted[6] = true;
                  this.gameObject.SetActive(false);
                  Debug.Log("planet 6 done");
                   break;
                    case "Neptune(Clone)":
                  PlanetsOnPlane.isPlanetInserted[7] = true;
                  this.gameObject.SetActive(false);
                   Debug.Log("planet 7 done");
                    break;
                    default: break;
            }
    }
        }
}