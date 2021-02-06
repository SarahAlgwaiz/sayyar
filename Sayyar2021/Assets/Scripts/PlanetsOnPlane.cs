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
static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
private void Awake() {
    raycastManager = GetComponent<ARRaycastManager>();
    AR_Plane_Manager = GetComponent<ARPlaneManager>();
    
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
    if(raycastManager.Raycast(touchPosition,s_Hits,TrackableType.PlaneWithinPolygon)){
        var hitPose = s_Hits[0].pose;
        if(spawnedObject==null){
            spawnedObject = Instantiate(placablePrefab, hitPose.position, hitPose.rotation);
            Debug.Log("IF");
         disablePlane();

        }
        else{
            spawnedObject.transform.position = hitPose.position;
            spawnedObject.transform.rotation = hitPose.rotation;
                        Debug.Log("ELSE");

        }}
    }
}
}
