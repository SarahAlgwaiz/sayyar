using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using System.Threading.Tasks;
using Photon.Pun;
using Photon;
using Photon.Realtime;
public class FirebaseStorageAfterGame : MonoBehaviour
{

    public static string gameKey;

    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public static DatabaseReference reference;
    private static FirebaseUser user;

    public static void InitializeFirebase()
    {
        reference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;
    }

    public static async Task storeVirtualPlayroomData()
    {
        reference = reference.Root;
        if (PhotonNetwork.IsMasterClient){
        Debug.Log(" the virtualPlayroomKey in storeVirtualPlayroomData is "+CreateRoomScript.virtualPlayroomKey);
            await Task.Run(() => reference.Child("VirtualPlayrooms").Child(CreateRoomScript.virtualPlayroomKey).Child("HostID").SetValueAsync(user.UserId));
        }else
            await Task.Run(() => reference.Child("VirtualPlayrooms").Child(CreateRoomScript.virtualPlayroomKey).Child("KindergartnerIDs").Child(PhotonNetwork.LocalPlayer.UserId).SetValueAsync(user.UserId));
    }

    public static async Task storeGameData()
    {
        reference = reference.Root;
        if (PhotonNetwork.IsMasterClient)
        {
            reference = reference.Child("Game").Push();
            gameKey = reference.Key;
            reference = reference.Root;
            await Task.Run(() => reference.Child("Game").Child(gameKey).Child("GameID").SetValueAsync(gameKey));
            await Task.Run(() => reference.Child("Game").Child(gameKey).Child("GameTitle").SetValueAsync("تركيب الكواكب"));
            Debug.Log("The val of status in class FireBaseStorage is "+ PlanetsOnPlane.status.ToString());
           // await Task.Run(() => reference.Child("Game").Child(gameKey).Child("Status").SetValueAsync(PlanetsOnPlane.status));
            await Task.Run(() => reference.Child("Game").Child(gameKey).Child("NumOfPlayers").SetValueAsync(PhotonNetwork.CurrentRoom.PlayerCount));
        }
    }
    public static async Task storeBadgeData()
    {
        reference = reference.Root;
        int randomID = Random.Range(0, 10);
                Debug.Log("The random id in storeBadgeData is "+randomID);

        var badgeID = await Task.Run(() => reference.Child("Badges").Child("" + randomID).Child("BadgeID").GetValueAsync().Result.Value);
        Debug.Log("The badge id in storeBadgeData is "+badgeID);
var path = await Task.Run(() => reference.Child("Badges").Child("" + randomID).Child("BadgePath").GetValueAsync().Result.Value);
        var result = await Task.Run(() => reference.Child("playerInfo").Child(user.UserId).Child("Badges").Child("" + badgeID).GetValueAsync().Result);
        if (result.Exists)
        {
            int badgeCount = (int)await Task.Run(() => reference.Child("playerInfo").Child(user.UserId).Child("Badges").Child("" + badgeID).Child("BadgeCount").GetValueAsync().Result.Value);
            badgeCount++;
            await Task.Run(() => reference.Child("playerInfo").Child(user.UserId).Child("Badges").Child("" + badgeID).Child("BadgeCount").SetValueAsync(badgeCount));
        }
        else
        {
            await Task.Run(() => reference.Child("playerInfo").Child(user.UserId).Child("Badges").Child("" + badgeID).Child("BadgeID").SetValueAsync(badgeID));
            await Task.Run(() => reference.Child("playerInfo").Child(user.UserId).Child("Badges").Child("" + badgeID).Child("BadgeCount").SetValueAsync(1));
            //await Task.Run(() => reference.Child("playerInfo").Child(user.UserId).Child("Badges").Child(""+badgeID).Child("BadgePath").SetValueAsync(path));   // I don't think we need to store bath inside playerinfo     
        }
if (PhotonNetwork.IsMasterClient)
        {
        await Task.Run(() => reference.Child("Game").Child(gameKey).Child("Badge").Child(""+badgeID).SetValueAsync(badgeID));        
    }}

    public static async Task storeTimeAndStatus()
    {
        reference = reference.Root;
        if (PhotonNetwork.IsMasterClient)
        {
            await Task.Run(() => reference.Child("Game").Child(gameKey).Child("Duration").SetValueAsync(Timer.fullDuration));
            await Task.Run(() => reference.Child("Game").Child(gameKey).Child("Status").SetValueAsync(PlanetsOnPlane.status));
        }

    }
}
