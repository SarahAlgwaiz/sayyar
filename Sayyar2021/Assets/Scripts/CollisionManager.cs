using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
     private void OnTriggerEnter(Collider other) {
       Debug.Log("collided outside");
       Debug.Log("other " + other.gameObject.name);
       Debug.Log("name " + name);
        if((other.gameObject.name + "(Clone)") == name){
         other.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
            Debug.Log("collided inside");
            this.gameObject.SetActive(false);
            Debug.Log("after destroy");
            switch(name){
                 case "Mercury(Clone)":
                 PlanetsOnPlane.isPlanetInserted[0] = true;
                 Debug.Log("planet 0 done");
                 break;
                 case "Venus(Clone)":
                 PlanetsOnPlane.isPlanetInserted[1] = true;
                 Debug.Log("planet 1 done");
                 break;
                 case "Earth(Clone)":
                 PlanetsOnPlane.isPlanetInserted[2] = true;
                Debug.Log("planet 2 done");
                 break;
                  case "Mars(Clone)":
                 PlanetsOnPlane.isPlanetInserted[3] = true;
                 Debug.Log("planet 3 done");
                  break;
                  case "Jupiter(Clone)":
                  PlanetsOnPlane.isPlanetInserted[4] = true;
                  Debug.Log("planet 4 done");
                  break;
                   case "Saturn(Clone)":
                 PlanetsOnPlane.isPlanetInserted[5] = true;
                 Debug.Log("planet 5 done");
                   break;
                   case "Uranus(Clone)":
                  PlanetsOnPlane.isPlanetInserted[6] = true;
                  Debug.Log("planet 6 done");
                   break;
                    case "Neptune(Clone)":
                  PlanetsOnPlane.isPlanetInserted[7] = true;
                   Debug.Log("planet 7 done");
                    break;
                    default: break;
            }
    }
        }
}