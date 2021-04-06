using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using System.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class FirebaseStorageAfterGame : MonoBehaviour
{

    


    public static string gameKey;
    private static string virtualPlayroomKey;

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

        var result = await Task.Run(() => reference.Child("VirtualPlayrooms").OrderByChild("RoomCode").EqualTo(PhotonNetwork.CurrentRoom.Name).GetValueAsync().Result);
        var result2 = await Task.Run(() => result.Children.ElementAt(0).Child("VirtualPlayroomID").Value);
        virtualPlayroomKey = result2.ToString();
        if (PhotonNetwork.IsMasterClient)
        {
            await Task.Run(() => reference.Child("VirtualPlayrooms").Child(virtualPlayroomKey).Child("HostID").SetValueAsync(PhotonNetwork.NickName));
            await Task.Run(() => reference.Child("VirtualPlayrooms").Child(virtualPlayroomKey).Child("GameID").SetValueAsync(gameKey));

        }
        else
        {
            await Task.Run(() => reference.Child("VirtualPlayrooms").Child(virtualPlayroomKey).Child("KindergartnerIDs").Child(PhotonNetwork.LocalPlayer.UserId).SetValueAsync(PhotonNetwork.NickName));

        }
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
            await Task.Run(() => reference.Child("Game").Child(gameKey).Child("Status").SetValueAsync(PlanetsOnPlane.status));
            await Task.Run(() => reference.Child("Game").Child(gameKey).Child("NumOfPlayers").SetValueAsync(PhotonNetwork.CurrentRoom.PlayerCount));
        }
    }



    private static int generateRandomId()
    {
        reference = reference.Root;
        int randomID = Random.Range(1, 10);
        return (randomID);

    }
    public static async Task storeBadgeData()
    {
        int randomID = generateRandomId();
        var badgeID = await Task.Run(() => reference.Child("Badges").Child("" + randomID).Child("BadgeID").GetValueAsync().Result.Value);
        //var path = await Task.Run(() => reference.Child("Badges").Child("" + randomID).Child("BadgePath").GetValueAsync().Result.Value);
        await Task.Run(() => reference.Child("Game").Child(gameKey).Child("Badge").SetValueAsync(badgeID + ""));
        //store the same badge for the all 
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)

        {
            string playerFirebaseId = playerInfo.Value.NickName;
            var result = await Task.Run(() => reference.Child("playerInfo").Child(playerFirebaseId).Child("Badges").Child("" + badgeID).GetValueAsync().Result);
            if (result.Exists)
            {
                int badgeCount = int.Parse(await Task.Run(() => reference.Child("playerInfo").Child(playerFirebaseId).Child("Badges").Child("" + badgeID).Child("BadgeCount").GetValueAsync().Result.Value.ToString()));
                badgeCount++;
                await Task.Run(() => reference.Child("playerInfo").Child(playerFirebaseId).Child("Badges").Child("" + badgeID).Child("BadgeCount").SetValueAsync(badgeCount));
            }
            else
            {
                await Task.Run(() => reference.Child("playerInfo").Child(playerFirebaseId).Child("Badges").Child("" + badgeID).Child("BadgeID").SetValueAsync(badgeID));
                await Task.Run(() => reference.Child("playerInfo").Child(playerFirebaseId).Child("Badges").Child("" + badgeID).Child("BadgeCount").SetValueAsync(1));
            }
        }


    }


    



    public static async Task storeTimeAndStatus()
    {
        reference = reference.Root;
        await Task.Run(() => reference.Child("Game").Child(gameKey).Child("Duration").SetValueAsync(Timer.gameExactDuration));
        await Task.Run(() => reference.Child("Game").Child(gameKey).Child("Status").SetValueAsync(PlanetsOnPlane.status));
        

    }
}
