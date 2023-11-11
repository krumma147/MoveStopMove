using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public GameObject weaponObj;
    void Start()
    {
        OnInit();  

	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInit() {
        rb = weaponObj.GetComponent<Rigidbody>();
        rb.velocity = Vector3.forward * 10f;
		Invoke(nameof(OnDespawn), 2f);
	}

    public void OnDespawn()
    {
		Destroy(gameObject);
	}
}
