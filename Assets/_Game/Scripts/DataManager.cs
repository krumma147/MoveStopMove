using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    // Start is called before the first frame update
    public WeaponList WeaponList;

    public WeaponItemData GetWeaponData(WeaponType weaponType)
    {
        List<WeaponItemData> weapons = WeaponList.weaponList;
        foreach (WeaponItemData weapon in weapons)
        {
            if(weapon.weaponType == weaponType)
            {
                return weapon;
            }
        }
        return null;
    }
}
