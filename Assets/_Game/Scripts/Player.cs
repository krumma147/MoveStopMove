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
    private PlayerState currentState;

    public enum PlayerState
	{
        Idle,
        Moving,
        Attacking,
        Death
	}
    
    void Start()
    {
        currentState = PlayerState.Idle;
        anim = GetComponent<Animator>();
		enemies = new List<Enemy>();
	}

    // Update is called once per frame
    void Update()
    {
        Moving();
        DetectEnemy();
        if (enemies.Count > 0 && currentState != PlayerState.Moving)
        {
			Enemy target = SelectEnemy();
            if(target != null)
			{
                //Debug.Log("Found target: " + target.name + ", with distance of:" + target.getDistanceToPlayer());
                //Attack(); // add delay time by using Caroutine or something similar
                enemies.Clear();
                StartCoroutine(Attack(target));
                target = null;
            }
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
            if(currentState != PlayerState.Moving)
			{
                currentState = PlayerState.Moving;
                anim.SetBool("IsIdle", false);
                anim.SetBool("IsAttack", false);
            }
			transform.position = nextDestination;
		}
        else
        {
            if (currentState != PlayerState.Idle)
            {
                currentState = PlayerState.Idle;
                anim.SetBool("IsIdle", true);
            }
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
        enemies.Clear();
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);
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
    // Let Attack function to allow user to look at target direction
	IEnumerator Attack(Enemy enemy)
	{
        if (currentState == PlayerState.Moving)
        {
            yield break;
        }
        currentState = PlayerState.Attacking;
        anim.SetBool("IsAttack", true);
        anim.SetBool("IsIdle", false);
        yield return new WaitForSeconds(2);
        Debug.Log("Done Attack after 2 second cd");
        anim.SetBool("IsAttack", false);
        if (currentState != PlayerState.Moving)
        {
            anim.SetBool("IsAttack", false);
            currentState = PlayerState.Idle;
            anim.SetBool("IsIdle", true);
        }
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
