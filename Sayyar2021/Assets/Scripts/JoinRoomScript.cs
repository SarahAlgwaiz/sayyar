using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using System.Threading.Tasks;
using TMPro;
using ArabicSupport;

namespace com.cactusteam.Sayyar
{
    public class JoinRoomScript : MonoBehaviourPunCallbacks
    {

        [Header("Firebase")]
        public DependencyStatus dependencyStatus;
        public DatabaseReference reference;
        private FirebaseUser user;

        [SerializeField]

        // private GameObject joinRoomView;
        // [SerializeField]
        // private Button roomCodeConfirmButton;
        //[SerializeField]
        //The above lines I don't no what is for and not used in this class @SarahAlgwaiz please deleted if not neccessary كل الحب


        //private TMP_InputField roomNumField; >>> the following line is alternative for Fron-End matters <3  Num1.text + Num2.text + Num3.text + Num4.text + Num5.text @SarahAlgwaiz

        [Header("JoinRoomCode")]
        public InputField Num1, Num2, Num3, Num4, Num5;
        public TextMeshProUGUI ErrorMsg;
        public static int WhereIsTheFoucs;

        private bool valid;

        public void OnClickRoomCodeConfirmButton()
        {

            JoinRoom();

        }

        public void JoinRoom()
        {
            string roomCode = Num1.text + Num2.text + Num3.text + Num4.text + Num5.text;
            if (roomCode.Length != 5)
            {
                ErrorMsg.text = ArabicFixer.Fix("لطفًا أدخل رقم الغرفة كاملًا");
                Debug.Log("Room Code length should be 5");
                return;
            }
            PhotonNetwork.JoinRoom(roomCode);
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
 {
     dependencyStatus = task.Result;
     if (dependencyStatus == DependencyStatus.Available)
     {
         //If they are avalible Initialize Firebase
         InitializeFirebase();
     }
     else
     {
         Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
     }
 });


        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("disconnected from server"); //popup pleeeease, Where this should apear ? @SarahAlgwaiz
        }
        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log("join room failed" + message);
            Debug.Log(returnCode);
            switch (returnCode)
            {
                case 32758:
                    Debug.Log("Room code does not exist"); //popup pleeeease >> 
                    ErrorMsg.text = ArabicFixer.Fix("رقم الغرفة المدخلة غير متوفرة");
                    break;
                case 32765:
                    Debug.Log("Room already full");//popup pleeeease
                    ErrorMsg.text = ArabicFixer.Fix("رقم الغرفة المدخلة ممتلئة");
                    break;
            }
            valid = false;
        }
        public override void OnJoinedRoom()
        {
            Debug.Log("Success! joined room");

            SceneManager.LoadSceneAsync("WaitingRoomScene");
        }

        async void InitializeFirebase()
        {
            reference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;
            //valid = await isRoomCodeValid();
            // if(valid){
            //     SceneManager.LoadSceneAsync("WaitingRoomScene"); //has joined the room and will now go to the waiting room
            // }

        }

        async Task<bool> isRoomCodeValid()
        {

            Query doesRoomCodeExist = reference.Root.Child("WaitingRooms").OrderByChild("RoomCode");
            Query q1 = doesRoomCodeExist.EqualTo(Num1.text + Num2.text + Num3.text + Num4.text + Num5.text);

            DataSnapshot result = await Task.Run(() => q1.GetValueAsync().Result);
            if (result.Exists)
            {
                //PhotonNetwork.JoinRoom(Num1.text + Num2.text + Num3.text + Num4.text + Num5.text);
                if (!PhotonNetwork.InRoom)
                { // more validation cases (full or code not valid)
                    return false;
                }
                // DatabaseReference parent = q1.Reference.Parent.Child("KindergartnersID").Child(user.UserId);
                // await Task.Run(() => parent.SetValueAsync(user.UserId));
                return true;
            }
            return false;

        }



        ///These methods from @shhdSU for Front-End matters
        public void GoToNext()
        {

            switch (WhereIsTheFoucs)
            {
                case 0:
                    if (Num1.text != "")
                    {
                        Num2.interactable = true;
                        Num2.ActivateInputField();
                        WhereIsTheFoucs++;
                    }
                    break;
                case 1:
                    if (Num2.text != "")
                    {
                        Num3.interactable = true;
                        Num3.ActivateInputField();
                        WhereIsTheFoucs++;
                    }
                    break;
                case 2:
                    if (Num3.text != "")
                    {
                        Num4.interactable = true;
                        Num4.ActivateInputField();
                        WhereIsTheFoucs++;
                    }
                    break;
                case 3:
                    if (Num4.text != "")
                    {
                        Num5.interactable = true;
                        Num5.ActivateInputField();
                        WhereIsTheFoucs++;
                    }
                    break;
                    // case 4:
                    //     WhereIsTheFoucs++;
                    //     break;

            }

        }

        public void clearFields()
        {
            ErrorMsg.text = ArabicFixer.Fix("");
            Num1.text = "";
            Num2.text = "";
            Num3.text = "";
            Num4.text = "";
            Num5.text = "";
            Num4.interactable = false;
            Num2.interactable = false;
            Num3.interactable = false;
            Num5.interactable = false;
            WhereIsTheFoucs = 0;
        }




    }



}

