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
private GameObject[] planets;

// [SerializeField] 
// private GameObject Venus;

// [SerializeField] 
// private GameObject Earth;

// [SerializeField] 
// private GameObject Mars;

// [SerializeField] 
// private GameObject Jupiter;

// [SerializeField] 
// private GameObject Saturn;

// [SerializeField] 
// private GameObject Uranus;

[SerializeField] 
private ARPlane plane;

private Vector3 planeSize;

private LineRenderer lineRenderer;
private Vector3 solarSystemSize;

static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
private void Awake() {
    raycastManager = GetComponent<ARRaycastManager>();
    AR_Plane_Manager = GetComponent<ARPlaneManager>();
    lineRenderer = planets[7].GetComponent<LineRenderer>();
}
public void disablePlane(){
      setPosition();
   AR_Plane_Manager.enabled = false;
}

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
            solarSystemSize = new Vector2 (lineRenderer.bounds.size.x, lineRenderer.bounds.size.z);
            Debug.Log("SOLAR SYSTEM SIZE: " + solarSystemSize);
            disablePlane();
        }
        else{
            spawnedObject.transform.position = hitPose.position;
            spawnedObject.transform.rotation = hitPose.rotation;
                        Debug.Log("ELSE");
        }}
    }
}

    private void setPosition(){
        for(int i=0; i<planets.Length;i++){
    float randomX = Random.Range(-3, 3);
    float randomY = Random.Range(-3, 3);
     float randomZ = Random.Range(-3, 3);
    Vector3 randomPosition = new Vector3 (randomX, randomY, randomZ);    
    Debug.Log("RandomPosition" + randomPosition);
    Debug.Log("Plane local scale " + plane.transform.localScale);
    Instantiate(planets[i], randomPosition, Quaternion.identity);
        }
}
    }

