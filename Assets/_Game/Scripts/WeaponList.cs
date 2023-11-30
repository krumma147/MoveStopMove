using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponDatas", menuName = "ScriptableObject/WeaponList", order =1)]
public class WeaponList : ScriptableObject
{
   public List<WeaponItemData> weaponList;
    // Start is called before the first frame update

	public WeaponItemData GetWeaponData(WeaponType weaponType)
	{
		WeaponItemData weaponItemData = null;
		foreach(WeaponItemData weapon in weaponList)
		{
			if(weapon.weaponType == weaponType)
			{
				weaponItemData = weapon;
			}
		}
		return weaponItemData;
	}
}

[Serializable]
public class WeaponItemData
{
	// Start is called before the first frame update
	public Weapon weapon;
	public Bullet bullet;
	public WeaponType weaponType;

	
}
