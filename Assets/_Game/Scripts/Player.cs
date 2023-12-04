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
		this.OnInit();
	}

	override public void OnInit()
	{
		base.OnInit();
		if (currentWeapon == null)
		{
			ChangeWeapon((WeaponType) 2);
			currentWeapon.ChangeColor();
		}
		CameraManager.Instance.SetTarget(gameObject.transform);
		currentState = PlayerState.Idle;
	}

    // Update is called once per frame
    void Update()
    {
		Character target = SelectTarget();
		Moving();
		if (!isMoving && target != null && !target.isDead)
		{
			Attack(target);
			currentState = PlayerState.Attacking;
		}
	}

	public void Moving()	
	{
		if(isDead)
		{
			return;
		}
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

}
