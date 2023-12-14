using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Range : MonoBehaviour
{
	
	[SerializeField] private Character character;
	private SphereCollider myCollider;
	public LayerMask characterLayer;
	private void Start()
	{
		myCollider = GetComponent<SphereCollider>();
		myCollider.radius = character.attackRange;
	}

	private void OnTriggerEnter(Collider other)
	{
		Character chars = other.GetComponent<Character>();
		if (chars != null && chars != character )
        {
			character.AddEnemy(chars);
			Enemy enemy = (Enemy)chars;
			enemy.EnableTargetCircle();
		}
		
	}

	private void OnTriggerExit(Collider other)
	{
		Character chars = other.GetComponent<Character>();
		if (character.ExistEnemy(chars))
		{
			character.RemoveEnemy(chars);
			Enemy enemy = (Enemy)chars;
			enemy.DisableTargetCircle();
		}
	}

	public void IncreaseRange(float range)
	{
		myCollider.radius += range;
	}
}
