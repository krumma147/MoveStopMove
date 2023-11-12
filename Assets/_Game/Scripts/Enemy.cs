using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // Start is called before the first frame update
    private float distance;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

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
        currentState = PlayerState.Death;
        anim.SetBool("IsDead", true);
    }

	public void OnDestroy()
	{
		Destroy(gameObject);
	}
}
