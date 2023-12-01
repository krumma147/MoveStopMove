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
		if (chars != null && chars != character)
        {
			character.AddEnemy(chars);
		}
		
	}

	private void OnTriggerExit(Collider other)
	{
		Character chars = other.GetComponent<Character>();
		if (chars != null && chars != character)
		{
			character.RemoveEnemy(chars);
		}
	}

	public void IncreaseRange(float range)
	{
		myCollider.radius += range;
	}
}
