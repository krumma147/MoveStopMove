using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class Character : MonoBehaviour
{
	//Try using queue for enemies, add indicator and UI
	[SerializeField] protected float movementSpeed = 5f;
	protected Animator anim;
	public List<Enemy> enemies;
	[SerializeField] protected Transform weaponBox;
	[SerializeField] LayerMask characterLayer;
	protected WeaponItemData currentWeapon;

	private float attackRange = 5.2f;
	public bool isDead = false;
	public bool canAtack = false;
	public bool isMoving = false;	
	
	// Start is called before the first frame update

	virtual public void OnInit()
	{
		enemies = new List<Enemy>();
	}

	public Character SelectEnemy()
	{
		float minDist = Mathf.Infinity;
		Character enemy = null;
		if (enemies.Count == 1 && enemies[0].getDistanceToPlayer() >= 5.2f)
		{
			enemy = enemies[0];
		}
		else
		{
			for (int i = 0; i < enemies.Count; i++)
			{
				if (enemies[i].getDistanceToPlayer() < minDist && enemies[i].getDistanceToPlayer() <= 5.2f)
				{
					minDist = enemies[i].getDistanceToPlayer();
					enemy = enemies[i];
				}
			}
		}
		return enemy;
	}

	public void DetectEnemy()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, 5f, characterLayer);
		for (int i = 0; i < colliders.Length; i++)
		{
			Enemy enemy = colliders[i].GetComponent<Enemy>();
			if (enemy != null && colliders[i].CompareTag("Enemy") && !enemy.isDead && enemy.name != gameObject.name)
			{
				float dis = Vector3.Distance(transform.position, enemy.transform.position);
				enemy.setDistanceToPlayer(dis);
				enemy.EnableTargetCircle();
				enemies.Add(enemy);
			}
		}
	}
	//Change this function to be normal and use invoke instead. Similar to the first game project, could add canAttack var
	public IEnumerator Attack(Character enemy)
	{
		if (isMoving)
		{
			yield break;
		}
		if (!canAtack && enemy.isDead)
		{
			//AnimatorClipInfo[] atkAnim = anim.GetCurrentAnimatorClipInfo(0); // Add clip time info to know the duration of animation and CD between attacks
			canAtack = true;
			transform.LookAt(enemy.transform.position);
			anim.SetBool("IsAttack", true);
			anim.SetBool("IsIdle", false);
			//Set weapon prefab to be false cause the bullets not visible
			ThrowWeapon(enemy);
			yield return new WaitForSeconds(1.5f);
			ResetAttack();
		}
	}

	protected virtual void ResetAttack()
	{
		anim.SetBool("IsAttack", false);
		canAtack = false;
	}

	public void ThrowWeapon(Character enemy)
	{
		GameObject obj = Instantiate(currentWeapon.bullet, weaponBox.position, weaponBox.rotation);
		Weapon weap = obj.GetComponent<Weapon>();
		weap.OnInit();
		weap.target = enemy.transform.position;
	}

	
	// add these 
	//Hat hat;
	//pants
	//Wings

	public void ChangeWeapon(WeaponType weapon)
	{
		//Destroy old weap and create new one
		currentWeapon = DataManager.Instance.GetWeaponData(weapon);
	} // convert int type to enum type.


	public virtual void OnDeath() // Tach ra lam 2 ham, dung pooling de despawn enemy va
	{
		isDead = true;
		anim.SetBool("IsDead", true);
	}

	public virtual void OnDespawn()
	{

	}

}
