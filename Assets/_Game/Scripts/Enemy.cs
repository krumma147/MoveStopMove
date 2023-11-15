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

	private NavMeshAgent agent;
	public float range = 5.2f; //radius of sphere
	public Transform centrePoint;
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
		}
		botState = newState;
		if (botState != null)
		{
			botState.OnEnter(this);
		}
	}

	public void Moving()
	{
		if (agent.remainingDistance <= agent.stoppingDistance) //done with path
		{
			Vector3 point;
			if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
			{
				Debug.DrawRay(point, Vector3.up, UnityEngine.Color.blue, 1.0f); //so you can see with gizmos
				anim.SetBool("IsIdle", false);
				agent.SetDestination(point);
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
		currentState = PlayerState.Idle;
		anim.SetBool("IsIdle", true);
		agent.SetDestination(transform.position);
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
