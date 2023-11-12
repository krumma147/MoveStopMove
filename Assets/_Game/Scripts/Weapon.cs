using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
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
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * bulletSpeed);
        if(transform.position == target)
		{
            OnDespawn();
        }
    }

    public void OnInit() {
        rb = weaponObj.GetComponent<Rigidbody>();
    }

    public void OnDespawn()
    {
		Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
            //Debug.Log("Hit Enemy!");
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.OnDeath();
            Debug.Log(enemy);
		}
	}
}
