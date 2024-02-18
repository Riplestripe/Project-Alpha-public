using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerGunSelector : MonoBehaviour
{
    [SerializeField]
    private GunType Gun;
    [SerializeField]
    private Transform GunParent;
    [SerializeField]
    private List<GunsScriptbleObject> Guns;


    [Space]
    [Header("Runtime Filled")]
    public GunsScriptbleObject ActiveGun;


    private void Start()
    {
        GunsScriptbleObject gun = Guns.Find(gun => gun.Type == Gun);
        
        if(gun == null)
        {
            Debug.LogError($"No GunScriptbleObject found for GunType: {gun}");
            return;
        }

        ActiveGun = gun;
        gun.Spawn(GunParent, this);
    }

}
