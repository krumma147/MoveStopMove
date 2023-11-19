using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackState : IState<Enemy>
{
	private Enemy enemySelected;
	public void OnEnter(Enemy enemy)
	{
		enemySelected = enemy.SelectEnemy();
		//Debug.Log("Enemy Attack State!");
	}

	public void OnExecute(Enemy enemy)
	{
		if(enemySelected!= null)
		{
			if (!enemySelected.isDead && !enemy.canAtack)
			{
				//enemy.StartCoroutine(enemy.Attack(enemySelected));
				enemy.BotAttack(enemySelected);
				if (enemySelected.isDead)
				{
					enemy.ChangeState(new PatrolState());
				}

			}
		}
		
	}

	public void OnExit(Enemy enemy)
	{
		
	}

}
