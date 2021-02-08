using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlanetsOnPlane : MonoBehaviour
{
 private ARRaycastManager raycastManager;
 private GameObject spawnedObject;

 [SerializeField]
 private ARSessionOrigin session ;
 private ARPlaneManager AR_Plane_Manager;

[SerializeField] 
private GameObject placablePrefab;

[SerializeField] 
private GameObject Mercury;

[SerializeField] 
private GameObject Venus;

[SerializeField] 
private GameObject Earth;

[SerializeField] 
private GameObject Mars;

[SerializeField] 
private GameObject Jupiter;

[SerializeField] 
private GameObject Saturn;

[SerializeField] 
private GameObject Uranus;

[SerializeField] 
private GameObject Neptune;
[SerializeField] 
private ARPlane myPlane;

private Vector3 planeSize;

private Vector3 solarSystemSize;

private LineRenderer meshRenderer;
private ARRaycastHit lastHit;
static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
private void Awake() {
    raycastManager = GetComponent<ARRaycastManager>();
    AR_Plane_Manager = GetComponent<ARPlaneManager>();
    meshRenderer = Neptune.GetComponent<LineRenderer>();
}
public void disablePlane(){
AR_Plane_Manager.enabled = false;
}

// bool tryGetTouchPosition(out Vector2 touchPosition){
//     if(Input.touchCount > 0){
//         touchPosition=Input.GetTouch(0).position;
//             disablePlane();

//         return true;
//     }
//     touchPosition = default;
//     return false;
// }
private void Update() {
    // if(!tryGetTouchPosition(out Vector2 touchPosition))
    // return;
    Vector2 touchPosition = default;
    if(AR_Plane_Manager.enabled){
    if(raycastManager.Raycast(touchPosition,s_Hits,TrackableType.PlaneWithinBounds)){
        var hitPose = s_Hits[0].pose;
        if(spawnedObject==null){
            spawnedObject = Instantiate(placablePrefab, hitPose.position, hitPose.rotation);
            Debug.Log("IF");
            disablePlane();
           planeSize = s_Hits[s_Hits.Count-1].pose.position;
        }
        else{
            spawnedObject.transform.position = hitPose.position;
            spawnedObject.transform.rotation = hitPose.rotation;
                        Debug.Log("ELSE");
        }}
    }
   
            Debug.Log("PLANE SIZE:" +planeSize);
        solarSystemSize = meshRenderer.bounds.size;
            Debug.Log("SOLAR SYSTEM SIZE:" + solarSystemSize);

}
    private void setPosition(){
        
//         if(Random.value < GetRatio(planeSize.-xminRed),xmaxRed-xmaxBlue){
//         x= Random.Range(xminBlue,xminRed);
//     }else
//     {
//         x= Random.Range(xmaxRed,xmaxBlue);
//     }
// float GetRatio (float distance_1,float distance_2){
//     return distance_1 / distance_1 + distance_2;

// } 
}
    }

