using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip ahsant;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        ahsant = Resources.Load<AudioClip>("ahsant");

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

        }

    }
}
