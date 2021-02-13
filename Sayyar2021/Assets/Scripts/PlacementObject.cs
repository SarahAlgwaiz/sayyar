using TMPro;
using UnityEngine;

public class PlacementObject : MonoBehaviour
{
    [SerializeField]
    private bool isSelected;

    [SerializeField]
    private bool isLocked;

    public bool Selected 
    { 
        get 
        {
            return this.isSelected;
        }
        set 
        {
            isSelected = value;
        }
    }

    public bool Locked 
    { 
        get 
        {
            return this.isLocked;
        }
        set 
        {
            isLocked = value;
        }
    }


}