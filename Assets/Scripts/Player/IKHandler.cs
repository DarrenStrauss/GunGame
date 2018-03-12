using UnityEngine;
using RootMotion.FinalIK;

public class IKHandler : MonoBehaviour {
    [SerializeField]
    private GameObject weapons;

    private AimIK aimIk;
    private FullBodyBipedIK bodyIK;    
    private vp_FPPlayerEventHandler eventHandler;
    private WeaponIK currentWeaponIK;

    void Awake()
    {
        aimIk = GetComponent<AimIK>();
        bodyIK = GetComponent<FullBodyBipedIK>();

        eventHandler = vp_LocalPlayer.EventHandler; 
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
            SetAimTarget();
            SetLeftHandTarget();
        }
        catch
        {
            aimIk.enabled = false;
            bodyIK.enabled = false;
            eventHandler.Unregister(this);
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

    private void SetAimTarget()
    {
        aimIk.solver.transform = currentWeaponIK.AimTarget.transform;
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
