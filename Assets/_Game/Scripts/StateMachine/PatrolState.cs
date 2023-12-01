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
		timer += Time.deltaTime*5;
		if (enemy.detectedEnemies.Count > 0)
		{
			enemy.StopMoving();
			enemy.ChangeState(new AttackState());
		}
		if (timer > randomTime)
		{
			enemy.StopMoving();
			enemy.ChangeState(new IdleState());
		}
		enemy.Moving();
	}


	public void OnExit(Enemy enemy)
	{

	}

}
