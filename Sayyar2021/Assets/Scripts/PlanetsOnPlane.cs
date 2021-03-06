using System.ComponentModel;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
public class PlanetsOnPlane : MonoBehaviourPunCallbacks
{

    int calls = 0;
    int calls2 = 0;

    [Header("GameLoading")]
    public GameObject BeforeWinnerBoard;

    [Header("planets")]
    public Sprite SUN;
    public Sprite MERCURY;
    public Sprite VENUS;
    public Sprite EARTH;
    public Sprite MARS;
    public Sprite JUPITER;
    public Sprite SATURN;
    public Sprite URANUS;
    public Sprite NEPTUNE;
    public GameObject WinnerBoard;


    Image renderer;

    public GameObject planet;


    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public DatabaseReference reference;
    private FirebaseUser user;
    private ARRaycastManager raycastManager;
    private GameObject spawnedObject;

    [Header("PopUps")]
    public GameObject noOnepopUp;
    public GameObject ExitButtonpopUp;

    [SerializeField]
    private ARSessionOrigin session;
    private ARPlaneManager AR_Plane_Manager;

    [SerializeField]
    private GameObject placablePrefab;

    [SerializeField]
    public GameObject[] planets;

    [SerializeField]
    private Camera ARCamera;

    private PlacementObject selectedObject;

    [SerializeField]
    private PhotonView photonView;

    [SerializeField]
    private ARPlane plane;
    private Vector3 solarSystemSize;

    public static bool[] isPlanetInserted;
    public bool won = false;

    public static GameObject[] planetsObj;
    static bool isSelectedMine;

    public static string status;
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
        private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        status = "Ongoing";
        storeDataBeforeGame();
        raycastManager = GetComponent<ARRaycastManager>();
        AR_Plane_Manager = GetComponent<ARPlaneManager>();
        isPlanetInserted = new bool[8];
        planetsObj = new GameObject[8];
        for (int i = 0; i < planets.Length; i++)
        {
            isPlanetInserted[i] = false;
        }
    }
    public void disablePlane()
    {
        Debug.Log("inside disabled");
        setPosition();
        AR_Plane_Manager.enabled = false;
    }
    public void onClickExitGameButton()
    {
        Debug.Log("inside onClick"); //PPPPPP{{{{{{{{PPPPPPOOOOOOOOOOOOOOOPOOOO}}}}}}}}
        ExitButtonpopUp.SetActive(true);
    }

    public void BackButton()
    {
        ExitButtonpopUp.SetActive(false);
    }

    public void EndGameButtons()
    {

        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("HomeScene");
    }
    private void Update()
    {
        Debug.Log(PhotonNetwork.InRoom);
        if (PhotonNetwork.CurrentRoom == null)
        {
            PhotonNetwork.Disconnect();
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 1 && status == "Ongoing")
        {
            //PhotonNetwork.Disconnect();
            calls++;
            if (calls == 1)
            {
                noOnepopUp.SetActive(true);
                AudioManager.playSound("noOne");
            }

        }
       Vector2 touchPosition = new Vector2(0,0);
        if (AR_Plane_Manager.enabled)
        {
            if (raycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinInfinity))
            {
                var hitPose = s_Hits[0].pose;
                if (spawnedObject == null)
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        spawnedObject = PhotonNetwork.Instantiate(placablePrefab.name, hitPose.position, Quaternion.identity, 0, null);
                    }
                }

                disablePlane();
            }
            //         else       
            //           if(PhotonNetwork.IsMasterClient){
            // spawnedObject = PhotonNetwork.Instantiate(placablePrefab.name,Vector3.zero,Quaternion.identity,0, null);
            //         }
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 tp = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("touch begin");
                //selectedObject.transform.localScale = new Vector3 (0.125f,0.125f,0.125f);
                Ray ray = ARCamera.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    selectedObject = hit.transform.GetComponent<PlacementObject>();
                    //hit.transform.GetComponent<RequestOwnershipScript>().RequestOwnership();     
                    //photonView =  hit.transform.GetComponent<PhotonView>();
                    //if(photonView.IsMine)
                    //  isSelectedMine = true;
                    //  else isSelectedMine = false;
                    //   if(isSelectedMine){
                    if (selectedObject != null)
                    {
                        PlacementObject[] otherObj = (PlacementObject[])FindObjectsOfType(typeof(PlacementObject));
                        selectedObject.Moving = true;
                        foreach (PlacementObject obj in otherObj)
                        {
                            obj.Selected = selectedObject == obj;

                        }
                    }
                    // }
                }
            }
            if (raycastManager.Raycast(tp, s_Hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinInfinity))
            {
                Debug.Log("during touch");
                Pose hitPose = s_Hits[0].pose;
                // if(isSelectedMine){

                if (selectedObject.Selected)
                {
                    selectedObject.transform.position = hitPose.position;
                    Debug.Log("selected position" + selectedObject.transform.position);
                    selectedObject.Moving = true;
                }
                if (TouchPhase.Ended == touch.phase)
                {
                    selectedObject.Selected = false;
                    selectedObject.Moving = false;
                    Debug.Log("touch end");
                }
                // }
            }
        }
        if (!won)
            checkPlanets();
    }
    private void checkPlanets()
    {

        if (isPlanetInserted[0] && isPlanetInserted[1] && isPlanetInserted[2] && isPlanetInserted[3] && isPlanetInserted[4] && isPlanetInserted[5] && isPlanetInserted[6] && isPlanetInserted[7])
        {

            status = "Won";
            won = true;
            BeforeWinnerBoard.SetActive(true);
            finishGame();
        }
    }

    public void Okofwinnerboard()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void finishGame()
    {
        storeDataAfterGame();

    }

    public async void storeDataBeforeGame()
    {
        FirebaseStorageAfterGame.InitializeFirebase();
        if (PhotonNetwork.IsMasterClient)
            await FirebaseStorageAfterGame.storeGameData();
    }

    public async void storeDataAfterGame()
    {
        await FirebaseStorageAfterGame.storeVirtualPlayroomData();
        if (PhotonNetwork.IsMasterClient)
        {
            await FirebaseStorageAfterGame.storeBadgeData();
            Debug.Log("storeBadgeData");

            await FirebaseStorageAfterGame.storeTimeAndStatus();
            Debug.Log("storeTimeAndStatus");

        }
        Debug.Log("Before");
        calls2++;
        if (calls2 == 1)
        {
            if (PhotonNetwork.IsMasterClient)
                photonView.RPC("showBoard", RpcTarget.All);
            Debug.Log("After");
        }

    }
    public void setPosition()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < planets.Length; i++)
            {
                float randomX = Random.Range(-1.5f, 1.5f);
                float randomY = Random.Range(-1.5f, 1.5f);
                float randomZ = Random.Range(-1.5f, 1.5f);
                Vector3 randomPosition = new Vector3(randomX, 0, randomZ);
                PhotonNetwork.Instantiate(planets[i].name, randomPosition, Quaternion.identity, 0, null);

            }
        }
    }

    public void InitializeFirebase()
    {
        reference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;
    }
    [PunRPC]
    public async void showBoard()
    {
        Debug.Log("Inside showBoard");
        InitializeFirebase();
        reference = reference.Root;

        var result = await Task.Run(() => reference.Child("VirtualPlayrooms").OrderByChild("RoomCode").EqualTo(PhotonNetwork.CurrentRoom.Name).GetValueAsync().Result);
        while (result == null)
        {
            result = await Task.Run(() => reference.Child("VirtualPlayrooms").OrderByChild("RoomCode").EqualTo(PhotonNetwork.CurrentRoom.Name).GetValueAsync().Result);
        }
        var result2 = await Task.Run(() => result.Children.ElementAt(0).Child("VirtualPlayroomID").Value);
        while (result2 == null)
        {
            result2 = await Task.Run(() => result.Children.ElementAt(0).Child("VirtualPlayroomID").Value);
        }
        string VID = result2.ToString();
        Debug.Log("VID     " + VID);
        var result3 = await Task.Run(() => reference.Child("VirtualPlayrooms").Child(VID).Child("GameID").GetValueAsync().Result.Value);
        while (result3 == null)
        {
            result3 = await Task.Run(() => reference.Child("VirtualPlayrooms").Child(VID).Child("GameID").GetValueAsync().Result.Value);
        }
        string GID = result3.ToString();
        Debug.Log("GID       " + GID);
        var result4 = await Task.Run(() => reference.Child("Game").Child(GID).Child("Badge").GetValueAsync().Result.Value);
        while (result4 == null)
        {
            result4 = await Task.Run(() => reference.Child("Game").Child(GID).Child("Badge").GetValueAsync().Result.Value);
        }
        string badgeID = result4.ToString();

        renderer = planet.GetComponent<Image>();
        switch (badgeID)
        {
            case "SUN_BAD":
                renderer.sprite = SUN;
                break;
            case "MERCURY_BAD":
                renderer.sprite = MERCURY;
                break;
            case "VENUS_BAD":
                renderer.sprite = VENUS;
                break;
            case "EARTH_BAD":
                renderer.sprite = EARTH;
                break;
            case "MARS_BAD":
                renderer.sprite = MARS;
                break;
            case "JUPITER_BAD":
                renderer.sprite = JUPITER;
                break;
            case "SATURN_BAD":
                renderer.sprite = SATURN;
                break;
            case "URANUS_BAD":
                renderer.sprite = URANUS;
                break;
            case "NEPTUNE_BAD":
                renderer.sprite = NEPTUNE;
                break;

        }
        BeforeWinnerBoard.SetActive(false);
        WinnerBoard.SetActive(true);
        AudioManager.playSound("winner");
    }
}

