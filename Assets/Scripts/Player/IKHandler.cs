using UnityEngine;
using RootMotion.FinalIK;

public class IKHandler : MonoBehaviour {
    [SerializeField]
    private Transform aimTarget;
    [SerializeField]
    private GameObject weapons;

    private vp_PlayerEventHandler eventHandler;
    private AimIK aimIk;
    private FullBodyBipedIK bodyIK;    
    private WeaponIK currentWeaponIK;

    void Awake()
    {
        eventHandler = transform.parent.GetComponent<vp_PlayerEventHandler>();
        eventHandler.Register(this);

        aimIk = GetComponent<AimIK>();
        bodyIK = GetComponent<FullBodyBipedIK>();
    }

    void Start()
    {
        aimIk.solver.target = aimTarget;
    }

    void OnEnable()
    {
        if (eventHandler != null)
        {
            eventHandler.Register(this);
        }
    }

    void OnDisable()
    {
        if (eventHandler != null)
        {
            eventHandler.Unregister(this);
        }        
    } 

    void OnStart_SetWeapon()
    {
        aimIk.enabled = false;
        bodyIK.enabled = false;
    }

    void OnStop_SetWeapon()
    {
        if (!aimIk.enabled || !bodyIK.enabled)
        {
            aimIk.enabled = true;
            bodyIK.enabled = true;
        }

        int currentWeaponIndex = (int)eventHandler.SetWeapon.Argument - 1;

        try
        {
            currentWeaponIK = weapons.transform.GetChild(currentWeaponIndex).GetComponent<WeaponIK>();

            SetAimTransform();
            SetAimTransform();
            SetLeftHandTarget();
        }
        catch
        {
            aimIk.enabled = false;
            bodyIK.enabled = false;
        }        
    }

    void OnStart_Reload()
    {
        SetLeftHandTargetReload();
    }

    void OnStop_Reload()
    {
        SetLeftHandTarget();
    }

    void OnStart_Dead()
    {
        aimIk.enabled = false;
        bodyIK.enabled = false;
    }

    private void SetAimTransform()
    {
        aimIk.solver.transform = currentWeaponIK.AimTransform.transform;
    }

    private void SetLeftHandTarget()
    {
        bodyIK.solver.leftHandEffector.target = currentWeaponIK.LeftHandTarget.transform;
        bodyIK.solver.leftHandEffector.positionWeight = currentWeaponIK.LeftHandTargetWeight;
    }

    private void SetLeftHandTargetReload()
    {
        bodyIK.solver.leftHandEffector.target = currentWeaponIK.LeftHandTargetReload.transform;
        bodyIK.solver.leftHandEffector.positionWeight = currentWeaponIK.LeftHandTargetReloadWeight;
    }
}
