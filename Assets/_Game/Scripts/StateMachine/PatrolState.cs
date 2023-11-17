using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState<Enemy>
{
	float timer;
	float randomTime;
	public void OnEnter(Enemy enemy)
	{
		timer = 0;
		randomTime = Random.Range(4f, 8f);
	}

	public void OnExecute(Enemy enemy)
	{
		timer += Time.deltaTime;
		if (!enemy.isDead && timer < randomTime)
		{
			enemy.Moving();
			enemy.DetectEnemy();
		}
		else if (enemy.enemies.Count > 0)
		{
			enemy.StopMoving();
			//Debug.Log("Found enemy!");
			//Debug.Log($"Enemy found: {enemy.enemies.Count}");
			enemy.ChangeState(new AttackState());
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
