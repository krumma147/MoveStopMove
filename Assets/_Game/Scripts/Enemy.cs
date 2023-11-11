using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // Start is called before the first frame update
    private float distance;
    void Start()
    {

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

	public void OnDestroy()
	{
		Destroy(gameObject);
	}
}
