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

	void Start()
    {
		OnInit();
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
		if(detectedEnemies.Count > 0)
		{
			//Debug.Log(detectedEnemies[0]);
		}
	}

	public override void OnInit()
	{
		base.OnInit();	
		agent = gameObject.GetComponent<NavMeshAgent>();
		DisableTargetCircle();
		ChangeState(new PatrolState());
		ChangeWeapon(WeaponType.AXE2);
		currentWeapon.ChangeColor();
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
		if (isMoving)
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

	public bool RandomPoint(Vector3 center, float range, out Vector3 result) // Random giua viec di chuyen hoac tan cong.
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

    override public void OnDeath() // Tach ra lam 2 ham, dung pooling de despawn enemy va
	{
		selectedCircle.SetActive(false);
		ChangeState(new IdleState());
		base.OnDeath();
	}
}
