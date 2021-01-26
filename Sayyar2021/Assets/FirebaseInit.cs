using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Analytics;
public class FirebaseInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
Debug.Log("Success!");
});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
