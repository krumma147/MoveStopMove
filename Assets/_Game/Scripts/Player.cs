using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{
	[SerializeField] private FloatingJoystick _joystick;
	[SerializeField] private Transform playerCircle;
	protected PlayerState currentState;
	void Start()
    {
		OnInit();
	}

	override public void OnInit()
	{
		if (currentWeapon == null)
		{
			currentWeapon = DataManager.Instance.GetWeaponData(WeaponType.HAMMER);
			Debug.Log(DataManager.Instance.GetWeaponData(WeaponType.HAMMER));
		}
		Debug.Log(currentWeapon);
		anim = GetComponent<Animator>();
		CameraManager.Instance.SetTarget(gameObject.transform);
		currentState = PlayerState.Idle;
		Instantiate(currentWeapon.weapon, weaponBox);
	}

    // Update is called once per frame
    void Update()
    {
        Moving();
		DetectEnemy();
		if (enemies.Count > 0 && currentState != PlayerState.Moving)
		{
			Character target = SelectEnemy();
			if (target != null && !target.isDead)
			{
				//Debug.Log("Found target: " + target.name + ", with distance of:" + target.getDistanceToPlayer());
				//Attack(target); // add delay time by using Caroutine or something similar
				StartCoroutine(Attack(target));
				currentState = PlayerState.Attacking;
				enemies.Clear();
			}
		}
	}

	public void Moving()	
	{
		if (Input.GetMouseButton(0))
		{
			isMoving = true;
			Vector3 joyDir = new Vector3(_joystick.Direction.x, 0f, _joystick.Direction.y);
			Vector3 nextDestination = transform.position + joyDir * Time.deltaTime * movementSpeed;
			if (joyDir != Vector3.zero)
			{
				transform.LookAt(transform.position + joyDir);
			}
			if (currentState != PlayerState.Moving)
			{
				currentState = PlayerState.Moving;
				anim.SetBool("IsIdle", false);
				anim.SetBool("IsAttack", false);
			}
			transform.position = nextDestination;
		}
		else
		{
			if (currentState != PlayerState.Idle)
			{
				isMoving = false;
				currentState = PlayerState.Idle;
				anim.SetBool("IsIdle", true);
			}
		}
	}

	

	protected override void ResetAttack()
	{
		base.ResetAttack();
		currentState = PlayerState.Idle;
	}

	private void OnDrawGizmos()
	{
		//Gizmos.color = Color.yellow;
		//Gizmos.DrawSphere(transform.position, 1);
	}
}
