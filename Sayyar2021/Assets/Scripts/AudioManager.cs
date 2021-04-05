using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip ahsant, tryAgain, noOne, endTime, winner, holdedPlanet ;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        ahsant = Resources.Load<AudioClip>("ahsant");
        tryAgain = Resources.Load<AudioClip>("tryAgain");
        noOne = Resources.Load<AudioClip>("noOne");
        endTime = Resources.Load<AudioClip>("endTime");
        winner = Resources.Load<AudioClip>("winner");
        holdedPlanet = Resources.Load<AudioClip>("holdedPlanet");


        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void playSound(string sound){

        switch(sound){

            case "ahsant":
            audioSrc.PlayOneShot(ahsant);
            break;

            case "tryAgain":
            audioSrc.PlayOneShot(tryAgain);
            break;

            case "noOne":
            audioSrc.PlayOneShot(noOne);
            break;

            case "endTime":
            audioSrc.PlayOneShot(endTime);
            break;

            case "winner":
            audioSrc.PlayOneShot(winner);
            break;

            case "holdedPlanet":
            audioSrc.PlayOneShot(holdedPlanet);
            break;
            

        }

    }
}
