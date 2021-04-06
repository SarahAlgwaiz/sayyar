using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ArabicSupport;


public class CollisionManager : MonoBehaviour
{

     [SerializeField]

     public Material[] materials;
     private void OnTriggerEnter(Collider other) {
     PhotonView photonView = this.GetComponent<PhotonView>();
       Debug.Log("collided outside");
       Debug.Log("other " + other.gameObject.name);
       Debug.Log("name " + name);
        if(other.gameObject.name == (name + "(Clone)")){
          photonView.TransferOwnership(PhotonNetwork.LocalPlayer);        //take control of hidden object (this)
            Debug.Log("collided inside");
            Debug.Log("after destroy");
            PhotonView otherView = other.GetComponent<PhotonView>();
           // Toast.Instance.Show(ArabicFixer.Fix("أحسنت"),2f,Toast.ToastType.Check);//Toast
           // AudioManager.playSound("ahsant");
             otherView.TransferOwnership(PhotonNetwork.LocalPlayer);  //take control of original object (this)
            switch(name){
                 case "Mercury":
                 photonView.RPC("updatePlanetInsertion",RpcTarget.All,0);
                PhotonNetwork.Destroy(other.gameObject.GetComponent<PhotonView>()); //destroy other object (other photonview)
                 Debug.Log("planet 0 done");
                 break;
                 case "Venus":
                 photonView.RPC("updatePlanetInsertion",RpcTarget.All,1);
                PhotonNetwork.Destroy(other.gameObject.GetComponent<PhotonView>());

                 Debug.Log("planet 1 done");
                 break;
                 case "Earth":
                 photonView.RPC("updatePlanetInsertion",RpcTarget.All,2);
                PhotonNetwork.Destroy(other.gameObject.GetComponent<PhotonView>());

                Debug.Log("planet 2 done");
                 break;
                  case "Mars":
                 photonView.RPC("updatePlanetInsertion",RpcTarget.All,3);
                PhotonNetwork.Destroy(other.gameObject.GetComponent<PhotonView>());

                 Debug.Log("planet 3 done");
                  break;
                  case "Jupiter":
               photonView.RPC("updatePlanetInsertion",RpcTarget.All,4);
                PhotonNetwork.Destroy(other.gameObject.GetComponent<PhotonView>());

                  Debug.Log("planet 4 done");
                  break;
                   case "Saturn":
              photonView.RPC("updatePlanetInsertion",RpcTarget.All,5);
                PhotonNetwork.Destroy(other.gameObject.GetComponent<PhotonView>());

                 Debug.Log("planet 5 done");
                   break;
                   case "Uranus":
              photonView.RPC("updatePlanetInsertion",RpcTarget.All,6);
                PhotonNetwork.Destroy(other.gameObject.GetComponent<PhotonView>());

                  Debug.Log("planet 6 done");
                   break;
                    case "Neptune":
               photonView.RPC("updatePlanetInsertion",RpcTarget.All,7);
                PhotonNetwork.Destroy(other.gameObject.GetComponent<PhotonView>());

                   Debug.Log("planet 7 done");
                    break;
                    default: break;
            }
    }else{
            //popup  "غير صحيح، حاول مرة أخرى" Mismatching planets (INCORRECT)  
           // Toast.Instance.Show(ArabicFixer.Fix("حاول مرة أخرى"),2f,Toast.ToastType.Warning);
           // AudioManager.playSound("tryAgain");
            other.gameObject.transform.SetPositionAndRotation(new Vector3(other.gameObject.transform.position.x+ 0.5f,other.gameObject.transform.position.y,other.gameObject.transform.position.z + 0.5f), Quaternion.identity);
        }

     }
        [PunRPC]
        public void updatePlanetInsertion(int planetNumber){
        Debug.Log("inside RPC");
        switch(planetNumber){
          case 0:
                  this.GetComponent<Renderer>().material = materials[0];

          PlanetsOnPlane.isPlanetInserted[0] = true;
          break;
          case 1:
          PlanetsOnPlane.isPlanetInserted[1] = true;
                  this.GetComponent<Renderer>().material = materials[1];

          break;
          case 2:
          PlanetsOnPlane.isPlanetInserted[2] = true;
                  this.GetComponent<Renderer>().material = materials[2];

          break;
          case 3:
          PlanetsOnPlane.isPlanetInserted[3] = true;
                  this.GetComponent<Renderer>().material = materials[3];

          break;
          case 4:
          PlanetsOnPlane.isPlanetInserted[4] = true;
                  this.GetComponent<Renderer>().material = materials[4];

          break;
          case 5:
          PlanetsOnPlane.isPlanetInserted[5] = true;
                  this.GetComponent<Renderer>().material = materials[5];

          break;
          case 6:
          PlanetsOnPlane.isPlanetInserted[6] = true;
                  this.GetComponent<Renderer>().material = materials[6];
          break;
          case 7:
          PlanetsOnPlane.isPlanetInserted[7] = true;
                  this.GetComponent<Renderer>().material = materials[7];

          break;
          default:
          break;
        }
        }

       
}