using TMPro;
using UnityEngine;

public class PlacementObject : MonoBehaviour
{
    [SerializeField]
    private bool isSelected;

    [SerializeField]
    private bool isLocked;

    private bool IsMoving;


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

        public bool Moving 
    { 
        get 
        {
            return this.IsMoving;
        }
        set 
        {
            IsMoving = value;
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