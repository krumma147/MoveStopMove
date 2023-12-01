using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Enemy>
{
	float timer;
    float randomTime;
	public void OnEnter(Enemy enemy)
	{
		//Debug.Log($"{enemy.name} in Idle state!");
		enemy.StopMoving();
		timer = 0;
		randomTime = Random.Range(1f, 3f);
		enemy.isMoving = false;
	}

	public void OnExecute(Enemy enemy)
	{
		timer += Time.deltaTime*5;
		if (timer > randomTime)
		{
			enemy.ChangeState(new PatrolState());
		}
	}

	public void OnExit(Enemy enemy)
	{
		
	}

}
