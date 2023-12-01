using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackState : IState<Enemy>
{
	public void OnEnter(Enemy enemy)
	{

	}

	public void OnExecute(Enemy enemy)
	{
		if (enemy.isDead)
		{
			return;
		}
		Character target = enemy.SelectTarget();
		if (target != null)
		{
			enemy.Attack(target);
			//Debug.Log($"{enemy.name} attack {target.name}");
		}
		else
		{
			enemy.ChangeState(new IdleState());
		}
	}

	public void OnExit(Enemy enemy)
	{

	}

}
