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
		if (enemy.botTarget.isDead)
		{
			enemy.ChangeState(new PatrolState());
		}else
		{
			enemy.BotAttack(enemy.botTarget);
		}

	}

	public void OnExit(Enemy enemy)
	{
		enemy.botTarget = null;
	}

}
