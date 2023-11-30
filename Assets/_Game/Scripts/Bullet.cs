using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Weapon
{
	private Vector3 spawnLoc;
	public Vector3 target;
	public float bulletSpeed = 10f;

	// Update is called once per frame
	void Update()
	{
		Spinning();
		transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * bulletSpeed);
		Invoke(nameof(OnDespawn), 2f);
	}

	public void OnInit()
	{
		rb = weaponObj.GetComponent<Rigidbody>();
		transform.Rotate(90.0f, 0.0f, 0.0f);
		spawnLoc = transform.position;
	}

	public void Spinning()
	{
		weaponObj.transform.Rotate(new Vector3(0f, 0f, 100 * bulletSpeed * Time.deltaTime), Space.Self);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			Enemy enemy = other.GetComponent<Enemy>();
			//isHit = true;
			enemy.OnDeath();
			OnDespawn();
		}
	}
	public void OnDespawn() // Despawn sau 1 khoang thoi gian va khi va cham vs dich.....
	{
		Destroy(gameObject);
	}
}
