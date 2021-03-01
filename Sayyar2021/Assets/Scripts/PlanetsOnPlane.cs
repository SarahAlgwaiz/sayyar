using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Firebase.Auth;
using Firebase.Database;
using Firebase;
using System.Threading.Tasks;
[RequireComponent(typeof(ARRaycastManager))]
public class PlanetsOnPlane : MonoBehaviour
{

 [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public DatabaseReference reference;
    private FirebaseUser user;
    private ARRaycastManager raycastManager;
 private GameObject spawnedObject;

 [SerializeField]
 private ARSessionOrigin session ;
 private ARPlaneManager AR_Plane_Manager;

[SerializeField] 
private GameObject placablePrefab;

[SerializeField]
public  GameObject[] planets;

[SerializeField]
private Camera ARCamera;

private PlacementObject selectedObject;

[SerializeField] 
private ARPlane plane;
private Vector3 solarSystemSize;

public static bool[] isPlanetInserted;

public static string status; 
static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
private void Awake() {
    PhotonNetwork.AutomaticallySyncScene = true;
    status = "Ongoing";
    storeDataBeforeGame();
    raycastManager = GetComponent<ARRaycastManager>();
    AR_Plane_Manager = GetComponent<ARPlaneManager>();
    isPlanetInserted = new bool[8];
    for(int i=0; i<planets.Length; i++){
        isPlanetInserted[i] = false;
    }
}

public void disablePlane(){
      setPosition();
   AR_Plane_Manager.enabled = false;
}
   public void onClickExitGameButton(){
                   Debug.Log("inside onClick");
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("HomeScene");
        }
  private void Update() {
    Vector2 touchPosition = default;
    if(AR_Plane_Manager.enabled){
    if(raycastManager.Raycast(touchPosition,s_Hits,TrackableType.PlaneWithinBounds)){
        var hitPose = s_Hits[0].pose;
        if(spawnedObject==null){
      //   if(PhotonNetwork.IsMasterClient){
       spawnedObject =  RPCsScript.initializeSolarSystem(placablePrefab,touchPosition);
       //  }
       //spawnedObject= Instantiate(placablePrefab,hitPose.position,Quaternion.identity);
            disablePlane();
        }
        else{
            spawnedObject.transform.position = hitPose.position;
            spawnedObject.transform.rotation = hitPose.rotation;
            Debug.Log("ELSE");
        } 
        }
    }
        if(Input.touchCount>0){
            Touch touch = Input.GetTouch(0);
            Vector2 tp = touch.position;
            if(touch.phase == TouchPhase.Began){
               Debug.Log("touch begin");
                //selectedObject.transform.localScale = new Vector3 (0.125f,0.125f,0.125f);
                Ray ray = ARCamera.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit)){
                     selectedObject = hit.transform.GetComponent<PlacementObject>();
                     if(selectedObject != null){
                         PlacementObject[] otherObj = (PlacementObject[]) FindObjectsOfType(typeof(PlacementObject));
                         foreach(PlacementObject obj in otherObj){
                             obj.Selected = selectedObject == obj;
                         }
                     }
                }
            }
              
        if(raycastManager.Raycast(tp, s_Hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinInfinity))
        {
            Debug.Log("during touch");
            Pose hitPose = s_Hits[0].pose;
                if(selectedObject.Selected)
                {
                    selectedObject.transform.position = hitPose.position;
                    Debug.Log("selected position" + selectedObject.transform.position);
                }
            }
            if(TouchPhase.Ended == touch.phase){
                selectedObject.Selected = false;
                  Debug.Log("touch end");
            }
            
            }
            checkPlanets();
        }       
            private void checkPlanets(){        
            if(isPlanetInserted[0] && isPlanetInserted[1] && isPlanetInserted[2] && isPlanetInserted[3] && isPlanetInserted[4] && isPlanetInserted[5] && isPlanetInserted[6] && isPlanetInserted[7]){
                status= "Won";
                finishGame();
            }
        }

  public void finishGame(){
      //storeDataAfterGame();
     SceneManager.LoadScene("HomeScene");
  }

public async void storeDataBeforeGame(){
     FirebaseStorageAfterGame.InitializeFirebase();
      await FirebaseStorageAfterGame.storeGameData();
}

  public async void storeDataAfterGame(){
    //await FirebaseStorageAfterGame.storeVirtualPlayroomData();
    //await FirebaseStorageAfterGame.storeBadgeData();
  }
    public void setPosition(){
    // if(PhotonNetwork.IsMasterClient){
    RPCsScript.initializePlanetsRPC(planets);
    //Instantiate(planets[i],randomPosition,Quaternion.identity);
    //planets[i].GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
    }
      //  }
}

