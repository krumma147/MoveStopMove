using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{
	[SerializeField] private FloatingJoystick _joystick;
	void Start()
    {
		anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        Moving();
		DetectEnemy();
		if (enemies.Count > 0 && currentState != PlayerState.Moving)
		{
			Enemy target = SelectEnemy();
			if (target != null)
			{
				//Debug.Log("Found target: " + target.name + ", with distance of:" + target.getDistanceToPlayer());
				Attack(target); // add delay time by using Caroutine or something similar
				enemies.Clear();
				StartCoroutine(Attack(target));
				target = null;
			}
		}
	}

	public void Moving()	
	{
		if (Input.GetMouseButton(0))
		{
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
				currentState = PlayerState.Idle;
				anim.SetBool("IsIdle", true);
			}
		}
	}


}
