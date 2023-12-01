using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Weapon
{
	public Vector3 target;
	public float bulletSpeed = 10f;

	// Update is called once per frame
	void Update()
	{
		Spinning();
		transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * bulletSpeed);
		Invoke(nameof(OnDespawn), 2f);
	}

	public void Spinning()
	{
		weaponObj.transform.Rotate(new Vector3(0f, 0f, 100 * bulletSpeed * Time.deltaTime), Space.Self);
	}

	private void OnTriggerEnter(Collider other)
	{
		Character victim = other.GetComponent<Character>();

		if (victim != null)
		{
			victim.OnDeath();
			OnDespawn();
		}
	}
	public void OnDespawn() // Despawn sau 1 khoang thoi gian va khi va cham vs dich.....
	{
		Destroy(gameObject);
	}
}
