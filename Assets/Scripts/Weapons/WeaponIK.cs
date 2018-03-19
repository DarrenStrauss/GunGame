using UnityEngine;

public class WeaponIK : MonoBehaviour {
    [SerializeField]
    private GameObject aimTransform;
    [SerializeField]
    private GameObject leftHandTarget;
    [SerializeField]
    private float leftHandTargetWeight;
    [SerializeField]
    private GameObject leftHandTargetReload;
    [SerializeField]
    private float leftHandTargetReloadWeight;

    public GameObject AimTransform
    {
        get { return aimTransform; }
    }

    public GameObject LeftHandTarget
    {
        get { return leftHandTarget;  }     
    }

    public float LeftHandTargetWeight
    {
        get { return leftHandTargetWeight; }
    }

    public GameObject LeftHandTargetReload
    {
        get { return leftHandTargetReload; }
    }    

    public float LeftHandTargetReloadWeight
    {
        get { return leftHandTargetReloadWeight; }
    }

}
