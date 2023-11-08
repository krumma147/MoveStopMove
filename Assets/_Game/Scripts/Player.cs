using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{
	[SerializeField] private float movementSpeed = 5f;
    [SerializeField] private FloatingJoystick _joystick;
	[SerializeField] private Animator anim;
    private List<Enemy> enemies;
    private bool isMoving;
    //private bool isAttack;
    
    void Start()
    {
		anim = GetComponent<Animator>();
		enemies = new List<Enemy>();
		isMoving = false;
		//isAttack = false;
	}

    // Update is called once per frame
    void Update()
    {
        Moving();
        Attack();
        DetectEnemy();
        if (enemies.Count > 0 || !isMoving)
        {
			Enemy target = SelectEnemy();
            //Debug.Log("Found target: " + target.name + ", with distance of:" + target.getDistanceToPlayer());
            Attack(); // add delay time by using Caroutine or something similar
		}
        
	}

	public void Moving()
	{
        if (Input.GetMouseButton(0))
        {
            Vector3 joyDir = new Vector3(_joystick.Direction.x, 0f, _joystick.Direction.y);
            Vector3 nextDestination = transform.position + joyDir * Time.deltaTime * movementSpeed;
			if (joyDir != Vector3.zero)
			{
				transform.LookAt(transform.position + joyDir);
			}
            anim.SetBool("IsIdle", false);
			transform.position = nextDestination;
			isMoving = true;
		}
        else
        {
			anim.SetBool("IsIdle", true);
			isMoving = false;
		}
	}

    public Enemy SelectEnemy()
    {
        float minDist = Mathf.Infinity;
        Enemy enemy = null;
        if(enemies.Count == 1)
        {
			enemy = enemies[0];
        }
        else
        {
			for (int i=0;i<enemies.Count;i++)
			{
				if (enemies[i].getDistanceToPlayer() < minDist)
				{
					minDist = enemies[i].getDistanceToPlayer();
                    enemy = enemies[i];
				}
			}
		}
        return enemy;
	}

    public void DetectEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);
        for(int i = 0; i < colliders.Length; i++)
        {
            Enemy enym = colliders[i].GetComponent<Enemy>();
            if (enym != null && colliders[i].CompareTag("Enemy"))
            {
                //Debug.Log("Found Enemy: " + enym.name);
                float dis = Vector3.Distance(transform.position, enym.transform.position);
                enym.setDistanceToPlayer(dis);
				enemies.Add(enym);
			}
        }
    }

	void OnDrawGizmosSelected()
	{
		// Draw a yellow sphere at the transform's position
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(transform.position, 10f);
	}

	public void Attack()
    {
      anim.SetBool("IsAttack", true);
      Invoke(nameof(StopAttack), 3f);
    }

    public void StopAttack()
    {
		anim.SetBool("IsAttack", false);
	}

	private Vector3 CheckGround(Vector3 nextDestination)
	{
        //RaycastHit hit;
        /*if (Physics.Raycast(nextDestination, Vector3.down, out hit, layerGround))
        {
            
        }*/
        return transform.position;
	}
}
