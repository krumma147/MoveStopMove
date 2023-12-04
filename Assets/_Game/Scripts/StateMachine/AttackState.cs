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
		Character target = enemy.SelectTarget();
		if (target != null && !enemy.isDead)
		{
			int number = Random.Range(1, 3);
			if(number == 2)
			{
				Debug.Log(number);
				enemy.Attack(target);
			}
			else
			{
				enemy.ChangeState(new IdleState());
			}
			//Debug.Log($"{enemy.name} attack {target.name}");
		}
		enemy.ChangeState(new IdleState());
	}

	public void OnExit(Enemy enemy)
	{

	}

}
