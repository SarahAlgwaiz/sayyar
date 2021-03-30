using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using TMPro;
using ArabicSupport;

namespace com.cactusteam.Sayyar
{
    public class JoinRoomScript : MonoBehaviourPunCallbacks
    {

        [SerializeField]
        [Header("JoinRoomCode")]
        public InputField Num1, Num2, Num3, Num4, Num5;
        public TextMeshProUGUI ErrorMsg;
        public static int WhereIsTheFoucs;

        public void OnClickRoomCodeConfirmButton()
        {

            JoinRoom();

        }

        public void JoinRoom()
        {
            string roomCode = Num1.text + Num2.text + Num3.text + Num4.text + Num5.text;
            if (roomCode.Length != 5)
            {
                ErrorMsg.text = ArabicFixer.Fix("لطفًا أدخل رقم قاعدة الانطلاق كاملًا");
                Debug.Log("Room Code length should be 5");
                return;
            }
            PhotonNetwork.JoinRoom(roomCode);

        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("disconnected from server"); //popup pleeeease, Where this should apear ? @HadeelHamad
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
                    ErrorMsg.text = ArabicFixer.Fix("رقم قاعدة الانطلاق المدخلة غير متوفرة");
                    break;
                case 32765:
                    Debug.Log("Room already full");//popup pleeeease
                    ErrorMsg.text = ArabicFixer.Fix("رقم قاعدة الانطلاق المدخلة ممتلئة");
                    break;
            }
        }
        public override void OnJoinedRoom()
        {
            Debug.Log("Success! joined room");

            SceneManager.LoadSceneAsync("WaitingRoomScene");
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

