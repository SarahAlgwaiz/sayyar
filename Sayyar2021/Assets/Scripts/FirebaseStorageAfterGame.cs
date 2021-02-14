using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using System.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
public class FirebaseStorageAfterGame : MonoBehaviour
{

     [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public DatabaseReference reference;
    private FirebaseUser user;


        async void InitializeFirebase(){
        reference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;
        await saveVirtualPlayroomData();
       //await saveBadgeData();
          }

     async Task saveVirtualPlayroomData(){
       if(PhotonNetwork.IsMasterClient)
        await Task.Run(() => reference.Child("VirtualPlayrooms").Child(CreateRoomScript.virtualPlayroomKey).Child("HostID").SetValueAsync(user.UserId));
        else
        await Task.Run(() =>reference.Child("VirtualPlayrooms").Child(CreateRoomScript.virtualPlayroomKey).Child("KindergartnerIDs").Child(PhotonNetwork.LocalPlayer.userId).setValueAsync(user.UserId)); 

          }

        public async Task saveBadgeData(){
            int badgeID = Random.Range(0,12);
            var path = await Task.Run(() => reference.Child("Badges").Child(""+badgeID).Child("BadgePath").GetValueAsync().Result.Value);
            var result = await Task.Run(() => reference.Child("playerinfo").Child(user.UserId).Child("BadgeIDs").Child(""+badgeID).GetValueAsync().Result);
            if(result.Exists){
              int badgeCount = (int) await Task.Run(() => reference.Child("playerinfo").Child(user.UserId).Child("BadgeIDs").Child(""+badgeID).Child("BadgeCount").GetValueAsync().Result.Value); 
              badgeCount++;
              await Task.Run(() => reference.Child("playerinfo").Child(user.UserId).Child("BadgeIDs").Child(""+badgeID).Child("BadgeCount").SetValueAsync(badgeCount)); 
            }
            else{
            await Task.Run(() => reference.Child("playerinfo").Child(user.UserId).Child("BadgeIDs").Child(""+badgeID).Child("BadgeID").SetValueAsync(badgeID));        
            await Task.Run(() => reference.Child("playerinfo").Child(user.UserId).Child("BadgeIDs").Child(""+badgeID).Child("BadgeCount").SetValueAsync(1));        
            await Task.Run(() => reference.Child("playerinfo").Child(user.UserId).Child("BadgeIDs").Child(""+badgeID).Child("BadgePath").SetValueAsync(path));        
                }

            //await Task.Run(() => reference.Child("Game").Child(gameID).Child("Badge").Child(""+badgeID).SetValueAsync(badgeID));        
        }
}
