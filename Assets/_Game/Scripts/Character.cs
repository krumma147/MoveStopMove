using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Player;

public class Character : MonoBehaviour
{
	//Try using queue for enemies, add indicator and UI
	[SerializeField] protected float movementSpeed = 5f;
	protected Animator anim;
	public List<Character> detectedEnemies;
	[SerializeField] protected Transform weaponBox;
	[SerializeField] LayerMask characterLayer;
	[SerializeField] protected WeaponList weaponList;
	protected WeaponItemData weaponData;
	protected Weapon currentWeapon;

	public float attackRange = 5.2f;
	public float attackCD = 1.5f;
	public bool isDead = false;
	public bool isAtack = false;
	public bool isMoving = false;	
	
	// Start is called before the first frame update

	virtual public void OnInit()
	{
		detectedEnemies = new List<Character>();
		anim = GetComponent<Animator>();
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

	//Change this function to be normal and use invoke instead. Similar to the first game project, could add canAttack var
	public virtual void Attack(Character target)
	{

		if (target != null && !isMoving && !isAtack)
		{
			transform.LookAt(target.transform.position);
			anim.SetBool("IsAttack", true);
			isAtack = true;
			ThrowWeapon(target);
			Invoke(nameof(ResetAttack), attackCD);
		}
		if (isAtack && isMoving)
		{
			return;
		}
	}

	protected virtual void ResetAttack()
	{
		anim.SetBool("IsAttack", false);
		isAtack = false;
		detectedEnemies.Clear();
	}

	public void ThrowWeapon(Character target)
	{
		//Bullet have problem!
		Bullet obj = Instantiate(weaponData.bullet, weaponBox.position, weaponBox.rotation);
		obj.ChangeColor();
		obj.target = target.transform.position;
		obj.shooter = this;
		obj.OnInit();
	}

	
	// add these later on
	//Hat hat;
	//pants
	//Wings

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
		detectedEnemies.RemoveAt(detectedEnemies.IndexOf(chars));
	}

	public virtual void OnDeath() // Tach ra lam 2 ham, dung pooling de despawn enemy va
	{
		isDead = true;
		anim.SetBool("IsDead", true);
		Invoke(nameof(OnDespawn), 1.5f);
	}

	public virtual void OnDespawn()
	{
		gameObject.SetActive(false);
	}

}
