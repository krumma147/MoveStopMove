﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class Character : MonoBehaviour
{
	//Try using queue for enemies, add indicator and UI
	[SerializeField] protected float movementSpeed = 5f;
	protected Animator anim;
	public List<Enemy> enemies;
	protected PlayerState currentState;
	[SerializeField] private GameObject weaponPrefabs;
	[SerializeField] private Transform weaponBox;
	
	private float attackRange = 5.2f;
	public bool isDead = false;
	public bool canAtack = false;
	public bool isMoving = false;	
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
		Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);
		for (int i = 0; i < colliders.Length; i++)
		{
			Enemy enemy = colliders[i].GetComponent<Enemy>();
			if (enemy != null && colliders[i].CompareTag("Enemy") && enemy.currentState != PlayerState.Death && enemy.name != gameObject.name)
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
		if (currentState == PlayerState.Moving || currentState == PlayerState.Attacking)
		{
			yield break;
		}
		if (!canAtack && enemy.currentState != PlayerState.Death)
		{
			//AnimatorClipInfo[] atkAnim = anim.GetCurrentAnimatorClipInfo(0); // Add clip time info to know the duration of animation and CD between attacks
			canAtack = true;
			transform.LookAt(enemy.transform.position);
			currentState = PlayerState.Attacking;
			anim.SetBool("IsAttack", true);
			anim.SetBool("IsIdle", false);
			//Set weapon prefab to be false cause the bullets not visible
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

	public void ThrowWeapon(Character enemy)
	{
		GameObject obj = Instantiate(weaponPrefabs, weaponBox.position, weaponBox.rotation);
		Weapon weap = obj.GetComponent<Weapon>();
		weap.target = enemy.transform.position;
	}
}
