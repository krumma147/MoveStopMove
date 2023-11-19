using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	private Rigidbody rb;
	private bool isHit = false;
	private Vector3 spawnLoc;
	private float maxDis = 5f;
	[SerializeField] private GameObject weaponObj;
	public Vector3 target;
	public float bulletSpeed = 10f;
	
	
	void Start()
	{
		OnInit();
	}

	// Update is called once per frame
	void Update()
	{
		Spinning();
		transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * bulletSpeed);
		float travelDis = Vector3.Distance(spawnLoc, transform.position);
		if (travelDis >= maxDis || isHit )
		{
			OnDespawn();
		}
	}

	public void OnInit()
	{
		rb = weaponObj.GetComponent<Rigidbody>();
		transform.Rotate(90.0f, 0.0f, 0.0f);
		spawnLoc = transform.position;
	}

	public void OnDespawn()
	{
		Destroy(gameObject);
	}

	public void Spinning()
	{
		weaponObj.transform.Rotate( new Vector3(0f, 0f, 100 * bulletSpeed * Time.deltaTime), Space.Self);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			Enemy enemy = other.GetComponent<Enemy>();
			isHit = true;
			enemy.OnDeath();
		}
	}
}