using UnityEngine;
using RootMotion.FinalIK;

public class IKHandler : MonoBehaviour {
    private AimIK aimIk;
    private FullBodyBipedIK bodyIK;    
    private vp_FPPlayerEventHandler eventHandler;
    private WeaponIK currentWeaponIK;

    private bool weaponChanged;

    void Awake()
    {
        aimIk = GetComponent<AimIK>();
        bodyIK = GetComponent<FullBodyBipedIK>();        

        eventHandler = transform.GetComponentInParent<vp_FPPlayerEventHandler>();
        eventHandler.Register(this);
    }

	void Start () {
        weaponChanged = false;
    }

    void LateUpdate()
    {
        if (weaponChanged)
        {
            if (!aimIk.enabled || !bodyIK.enabled)
            {
                aimIk.enabled = true;
                bodyIK.enabled = true;
            }

            currentWeaponIK = GameObject.FindGameObjectWithTag(eventHandler.CurrentWeaponName.Get()).GetComponent<WeaponIK>();
            SetAimTarget();
            SetLeftHandTarget();

            weaponChanged = false;
        }
        
    }

    void OnStop_SetWeapon()
    {
        weaponChanged = true;
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
