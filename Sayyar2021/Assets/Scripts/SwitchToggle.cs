using UnityEngine ;
using UnityEngine.UI ;
//using DG.Tweening ;
using ArabicSupport;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Threading.Tasks;





public class SwitchToggle : MonoBehaviour {
   [SerializeField] RectTransform uiHandleRectTransform ;
   [SerializeField] Color backgroundActiveColor ;
   [SerializeField] Color handleActiveColor ;
   [SerializeField]  TextMeshProUGUI Text;
   public FirebaseAuth auth;
   public DatabaseReference DBreference;



   Image backgroundImage, handleImage ;

   Color backgroundDefaultColor, handleDefaultColor ;

   Toggle toggle ;

   Vector2 handlePosition ;

   void Awake ( ) {
      Text.text = ArabicFixer.Fix("السماح باستخدام البصمة");
      toggle = GetComponent <Toggle> ( ) ;

      handlePosition = uiHandleRectTransform.anchoredPosition ;

      backgroundImage = uiHandleRectTransform.parent.GetComponent <Image> ( ) ;
      handleImage = uiHandleRectTransform.GetComponent <Image> ( ) ;

      backgroundDefaultColor = backgroundImage.color ;
      handleDefaultColor = handleImage.color ;

      toggle.onValueChanged.AddListener (OnSwitch) ;

      if (toggle.isOn)
         OnSwitch (true) ;

    

   }

   async void Start (){

      InitializeFirebase();

      var isON = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("isFPallowed").GetValueAsync().Result.Value) as string;
      //string isOn = await Task.Run(() => isON.ToString()); 
      while(isON == null ){

         isON = await Task.Run(() => DBreference.Child("playerInfo").Child(auth.CurrentUser.UserId).Child("isFPallowed").GetValueAsync().Result.Value) as string;
         Debug.Log("IN TOGGLE WHILE");

      }

      if(isON == "1" ){

         toggle.isOn = true;

      }else{

         toggle.isOn = false;

      }
   }

   void OnSwitch (bool on) {
      uiHandleRectTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition ; // no anim
      //uiHandleRectTransform.DOAnchorPos (on ? handlePosition * -1 : handlePosition, .4f).SetEase (Ease.InOutBack) ;

      backgroundImage.color = on ? backgroundActiveColor : backgroundDefaultColor ; // no anim
      //backgroundImage.DOColor (on ? backgroundActiveColor : backgroundDefaultColor, .6f) ;

      handleImage.color = on ? handleActiveColor : handleDefaultColor ; // no anim
      //handleImage.DOColor (on ? handleActiveColor : handleDefaultColor, .4f) ;
   }

   void OnDestroy ( ) {
      toggle.onValueChanged.RemoveListener (OnSwitch) ;
   }

   private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;

    }
}
