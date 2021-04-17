using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameLoading : MonoBehaviour


{


    [Header("GameLoading")]
    public Sprite[] animatedImgs;
    public Image animaterImg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    animaterImg.sprite = animatedImgs[(int)(Time.time*15)%animatedImgs.Length];

        
    }
}
