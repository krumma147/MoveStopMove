using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
	//Clone
	private float distance;
	private IState<Enemy> botState;
	[SerializeField] private GameObject selectedCircle;
	private Vector3 destination;
	private NavMeshAgent agent;
	public float range = 5.2f; //radius of sphere
	public Transform centrePoint;
	public Character botTarget;

	void Start()
    {
		agent = gameObject.GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		DisableTargetCircle();
		ChangeState(new PatrolState());
	}

    // Update is called once per frame
    void Update()
    {
		if (botState != null && !isDead)
		{
			botState.OnExecute(this);
		}
		if(distance >= range)
		{
			DisableTargetCircle();
		}
	}

	public void ChangeState(IState<Enemy> newState)
	{
		if (botState != null)
		{
			botState.OnExit(this);
			//Debug.Log($"Exit this: {botState}");
		}
		botState = newState;
		if (botState != null)
		{
			botState.OnEnter(this);
			Debug.Log($"Enter this: {botState}");
		}
	}

	public void BotAttack(Character enemy)
	{
		
		if(enemy != null && !isMoving)
		{
			StopMoving();
			transform.LookAt(enemy.transform.position);
			anim.SetBool("IsAttack", true);
			canAtack = true;
			ThrowWeapon(enemy);
			Invoke(nameof(ResetAttack), 1.5f);
		}
		if (!canAtack && !isMoving)
		{
			return;
		}
	}

	public void Moving()
	{
		if (isMoving) //done with path
		{
			Vector3 point;
			if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
			{
				Debug.DrawRay(point, Vector3.up, UnityEngine.Color.blue, 1.0f); //so you can see with gizmos
				agent.isStopped = false;
				agent.SetDestination(point);
				anim.SetBool("IsIdle", false);
				
			}
		}
	}

	public bool RandomPoint(Vector3 center, float range, out Vector3 result)
	{
		Vector3 randomPoint = center + Random.insideUnitSphere * range;
		NavMeshHit hit;
		if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
		{
			result = hit.position;
			return true;
		}

		result = Vector3.zero;
		return false;
	}

	public void EnableTargetCircle()
	{
		selectedCircle.SetActive(true);
	}

	public void DisableTargetCircle()
	{
		selectedCircle.SetActive(false);
	}

	public void StopMoving()
	{
		agent.isStopped = true;
		currentState = PlayerState.Idle;
		anim.SetBool("IsIdle", true);
		isMoving = false;
		
		//agent.SetDestination(transform.position);
	}

	public void setDistanceToPlayer(float distance)
    {
        this.distance = distance;
    }

    public float getDistanceToPlayer()
    {
        return distance;
    }

    public void OnDeath()
	{
		isDead = true;
		currentState = PlayerState.Death;
		anim.SetBool("IsDead", true);
		selectedCircle.SetActive(false);
		ChangeState(new IdleState());
		Invoke(nameof(OnDestroy), 1.5f);
	}

	public void OnDestroy()
	{
		gameObject.SetActive(false);
	}
}
