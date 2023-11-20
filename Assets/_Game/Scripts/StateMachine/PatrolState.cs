using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class PatrolState : IState<Enemy>
{
	float timer;
	float randomTime;
	public void OnEnter(Enemy enemy)
	{
		timer = 0;
		randomTime = Random.Range(10f, 15f);
		enemy.isMoving = true;
	}

	public void OnExecute(Enemy enemy)
	{
		if (enemy.isDead)
        {
			return;
        }
		enemy.DetectEnemy();
		timer += Time.deltaTime*5;
		if (enemy.enemies.Count > 0)
		{
			enemy.botTarget = enemy.SelectEnemy();
			Enemy target = (Enemy)enemy.botTarget;
			if (enemy.botTarget != null && target.getDistanceToPlayer() <= 5.0f)
			{
				enemy.StopMoving();
				enemy.ChangeState(new AttackState());
				return;
			}
		}
		if (timer < randomTime)
		{
			enemy.Moving();
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
