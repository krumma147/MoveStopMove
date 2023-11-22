using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponDatas", menuName = "ScriptableObject/WeaponList", order =1)]
public class WeaponList : ScriptableObject
{
   public List<WeaponItemData> weaponList;
    // Start is called before the first frame update
}

[Serializable]
public class WeaponItemData
{
	// Start is called before the first frame update
	public GameObject weapon;
	public GameObject bullet;
	public WeaponType weaponType;

}
