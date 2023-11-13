using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class Character : MonoBehaviour
{
	[SerializeField] protected float movementSpeed = 5f;
	protected Animator anim;
	public List<Enemy> enemies;
	protected PlayerState currentState;

	[SerializeField] private GameObject weaponPrefabs;
	[SerializeField] private Transform weaponBox;
	protected bool canAtack = false;

	public enum PlayerState
	{
		Idle,
		Moving,
		Attacking,
		Death
	}

	// Start is called before the first frame update
	void Start()
    {
		OnInit();
	}

    // Update is called once per frame
    void Update()
    {
		
	}

	virtual public void OnInit()
	{
		currentState = PlayerState.Idle;
		enemies = new List<Enemy>();
		Instantiate(weaponPrefabs, weaponBox);
	}



	public Enemy SelectEnemy()
	{
		float minDist = Mathf.Infinity;
		Enemy enemy = null;
		if (enemies.Count == 1)
		{
			enemy = enemies[0];
		}
		else
		{
			for (int i = 0; i < enemies.Count; i++)
			{
				if (enemies[i].getDistanceToPlayer() < minDist)
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
		Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);
		for (int i = 0; i < colliders.Length; i++)
		{
			Enemy enemy = colliders[i].GetComponent<Enemy>();
			if (enemy != null && colliders[i].CompareTag("Enemy") && enemy.currentState != PlayerState.Death)
			{
				float dis = Vector3.Distance(transform.position, enemy.transform.position);
				enemy.setDistanceToPlayer(dis);
				enemies.Add(enemy);
			}
		}
	}
	//Change this function to be normal and use invoke instead. Similar to the first game project, could add canAttack var
	protected IEnumerator Attack(Enemy enemy)
	{
		if (currentState == PlayerState.Moving || currentState == PlayerState.Attacking)
		{
			yield break;
		}
		if (!canAtack)
		{
			canAtack = true;
			transform.LookAt(enemy.transform.position);
			currentState = PlayerState.Attacking;
			anim.SetBool("IsAttack", true);
			anim.SetBool("IsIdle", false);
			//weaponPrefabs.SetActive(false); //Set ưeapon prefab to be false cause the bullets not visible
			ThrowWeapon(enemy);
			yield return new WaitForSeconds(1.5f);
			ResetAttack();
		}
		
	}

	protected void ResetAttack()
	{
		currentState = PlayerState.Idle;
		anim.SetBool("IsAttack", false);
		canAtack = false;
	}

	public void ThrowWeapon(Enemy enemy)
	{
		GameObject obj = Instantiate(weaponPrefabs, weaponBox.position, weaponBox.rotation);
		Weapon weap = obj.GetComponent<Weapon>();
		weap.target = enemy.transform.position;
	}
}
