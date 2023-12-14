using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Player;

public class Character : MonoBehaviour
{
	//TODO: Try using queue for enemies, add indicator and UI
	[SerializeField] protected float movementSpeed = 5f;
	[SerializeField] protected Animator anim;
	[SerializeField] LayerMask characterLayer;
	[SerializeField] protected WeaponList weaponList;
	protected WeaponItemData weaponData;
	protected Weapon currentWeapon;
	public Transform weaponBox;

	public List<Character> detectedEnemies;
	public float attackRange = 5.2f;
	public float attackCD = 1.5f;
	public bool isDead = false;
	public bool isAtack = false;
	public bool isMoving = false;	

	virtual public void OnInit()
	{
		detectedEnemies = new List<Character>();
		isDead = false;
	}

	public Character SelectTarget()
	{
		float minDist = Mathf.Infinity;
		Character target = null;
		for (int i = 0; i < detectedEnemies.Count; i++)
		{
			Character currentEnemy = detectedEnemies[i];
			float enemyDistance = Vector3.Distance(gameObject.transform.position, currentEnemy.transform.position);
			if (enemyDistance < minDist)
			{
				minDist = enemyDistance;
				target = currentEnemy;
			}
		}
		return target;
	}
	public virtual void Attack(Character target)
	{
		if (target != null && !isMoving && !isAtack && !target.isDead)
		{
			transform.LookAt(target.transform.position);
			anim.SetBool(Constant.AttackAnim, true);
			isAtack = true;
			currentWeapon.ThrowWeapon(target, this, weaponData.bullet);
			Invoke(nameof(ResetAttack), attackCD);
			
		}
		
		if (currentWeapon == null)
		{
			return;
		}
	}

	protected virtual void ResetAttack()
	{
		anim.SetBool(Constant.AttackAnim, false);
		isAtack = false;
	}

	// add these later on
	//Hat hat;
	//pants
	//Wings

	public void GetWeapon(WeaponType weapon)
	{
		if (weaponData == null)
		{
			weaponData = weaponList.GetWeaponData(weapon);
			currentWeapon = Instantiate(weaponData.weapon, weaponBox);
		}
	}

	public void ChangeWeapon(WeaponType weapon)
	{
		//Destroy old weap and create new one
		if (weaponData != null)
		{
			weaponData = null;
			Destroy(currentWeapon.gameObject);
		}
		weaponData = weaponList.GetWeaponData(weapon);
		currentWeapon = Instantiate(weaponData.weapon, weaponBox);
		
	}

	public void AddEnemy(Character chars)
	{
		detectedEnemies.Add(chars);
	}

	public void RemoveEnemy(Character chars)
	{
		if (!chars.isDead)
		{
			detectedEnemies.RemoveAt(detectedEnemies.IndexOf(chars));
		}
	}

	public bool ExistEnemy(Character chars)
	{
		return detectedEnemies.Contains(chars);
	}

	public virtual void OnDeath() // Tach ra lam 2 ham, dung pooling de despawn enemy va
	{
		isDead = true;
		anim.SetBool(Constant.DeadAnim, true);
		Invoke(nameof(OnDespawn), 1.5f);
	}

	public virtual void OnDespawn()
	{
		gameObject.SetActive(false);
	}

}
