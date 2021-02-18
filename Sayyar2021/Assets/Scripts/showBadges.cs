using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showBadges : MonoBehaviour
{

    // Will attach a VideoPlayer to the main camera.
    GameObject camera = GameObject.Find("Main Camera");

    // Videos definition
    public GameObject EARTH_VID;
    public GameObject JUPITER_VID;
    public GameObject MARS_VID;
    public GameObject MERCURY_VID;
    public GameObject NEPTUNE_VID;
    public GameObject SATURN_VID;
    public GameObject SUN_VID;
    public GameObject URANUS_VID;
    public GameObject VENUS_VID;

    //Other components definition
    public GameObject parentPanel;
    public GameObject VideosPanel;


    // Start is called before the first frame update
    void Start()
    {
        parentPanel.SetActive(true);
        VideosPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void popUp_Cancel_Button()
    {
        parentPanel.SetActive(false);
    }

    public void SUN_Button()
    {

        VideosPanel.SetActive(true);
        EARTH_VID.SetActive(false);
        JUPITER_VID.SetActive(false);
        MARS_VID.SetActive(false);
        MERCURY_VID.SetActive(false);
        NEPTUNE_VID.SetActive(false);
        SATURN_VID.SetActive(false);
        SUN_VID.SetActive(true);
        URANUS_VID.SetActive(false);
        VENUS_VID.SetActive(false);

    }

    public void EARTH_Button()
    {

        VideosPanel.SetActive(true);
        EARTH_VID.SetActive(true);
        JUPITER_VID.SetActive(false);
        MARS_VID.SetActive(false);
        MERCURY_VID.SetActive(false);
        NEPTUNE_VID.SetActive(false);
        SATURN_VID.SetActive(false);
        SUN_VID.SetActive(false);
        URANUS_VID.SetActive(false);
        VENUS_VID.SetActive(false);

    }

    public void JUPITER_Button()
    {

        VideosPanel.SetActive(true);
        EARTH_VID.SetActive(false);
        JUPITER_VID.SetActive(true);
        MARS_VID.SetActive(false);
        MERCURY_VID.SetActive(false);
        NEPTUNE_VID.SetActive(false);
        SATURN_VID.SetActive(false);
        SUN_VID.SetActive(false);
        URANUS_VID.SetActive(false);
        VENUS_VID.SetActive(false);

    }

    public void MARS_Button()
    {

        VideosPanel.SetActive(true);
        EARTH_VID.SetActive(false);
        JUPITER_VID.SetActive(false);
        MARS_VID.SetActive(true);
        MERCURY_VID.SetActive(false);
        NEPTUNE_VID.SetActive(false);
        SATURN_VID.SetActive(false);
        SUN_VID.SetActive(false);
        URANUS_VID.SetActive(false);
        VENUS_VID.SetActive(false);

    }

    public void MERCURY_Button()
    {

        VideosPanel.SetActive(true);
        EARTH_VID.SetActive(false);
        JUPITER_VID.SetActive(false);
        MARS_VID.SetActive(false);
        MERCURY_VID.SetActive(true);
        NEPTUNE_VID.SetActive(false);
        SATURN_VID.SetActive(false);
        SUN_VID.SetActive(false);
        URANUS_VID.SetActive(false);
        VENUS_VID.SetActive(false);

    }

    public void NEPTUNE_Button()
    {

        VideosPanel.SetActive(true);
        EARTH_VID.SetActive(false);
        JUPITER_VID.SetActive(false);
        MARS_VID.SetActive(false);
        MERCURY_VID.SetActive(false);
        NEPTUNE_VID.SetActive(true);
        SATURN_VID.SetActive(false);
        SUN_VID.SetActive(false);
        URANUS_VID.SetActive(false);
        VENUS_VID.SetActive(false);

    }

    public void SATURN_Button()
    {

        VideosPanel.SetActive(true);
        EARTH_VID.SetActive(false);
        JUPITER_VID.SetActive(false);
        MARS_VID.SetActive(false);
        MERCURY_VID.SetActive(false);
        NEPTUNE_VID.SetActive(false);
        SATURN_VID.SetActive(true);
        SUN_VID.SetActive(false);
        URANUS_VID.SetActive(false);
        VENUS_VID.SetActive(false);

    }

    public void URANUS_Button()
    {

        VideosPanel.SetActive(true);
        EARTH_VID.SetActive(false);
        JUPITER_VID.SetActive(false);
        MARS_VID.SetActive(false);
        MERCURY_VID.SetActive(false);
        NEPTUNE_VID.SetActive(false);
        SATURN_VID.SetActive(false);
        SUN_VID.SetActive(false);
        URANUS_VID.SetActive(true);
        VENUS_VID.SetActive(false);

    }

    public void VENUS_Button()
    {

        VideosPanel.SetActive(true);
        EARTH_VID.SetActive(false);
        JUPITER_VID.SetActive(false);
        MARS_VID.SetActive(false);
        MERCURY_VID.SetActive(false);
        NEPTUNE_VID.SetActive(false);
        SATURN_VID.SetActive(false);
        SUN_VID.SetActive(false);
        URANUS_VID.SetActive(false);
        VENUS_VID.SetActive(true);

    }

    public void video_Cancel_Button()
    {
        VideosPanel.SetActive(false);
    }


}
