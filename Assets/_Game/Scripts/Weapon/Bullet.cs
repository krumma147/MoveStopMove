using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
	[SerializeField] private GameObject bulletObj;
	[SerializeField] private float deSpawnTime = 1.5f;
	public Vector3 throwPosition;
	public Character shooter;
	public Character Target;
	public Vector3 targetPosition;
	public float bulletSpeed = 10f;
	public Vector3 destination;
	public Transform bulletTransform;

	//UNDONE: weapon target position.y = throw posotion.y
	private void Start()
	{
		bulletTransform = transform;
		bulletTransform.Rotate(new Vector3(-30f, 90f, 0f));
		destination = new Vector3(targetPosition.x, throwPosition.y, targetPosition.z);
	}

	// Update is called once per frame
	void Update()
	{
		Spinning();
		bulletTransform.position = Vector3.MoveTowards(bulletTransform.position, destination, Time.deltaTime * bulletSpeed);
		Invoke(nameof(OnDespawn), deSpawnTime);
		if (shooter.CompareTag("Player"))
		{
			Debug.Log($"pos: {transform.position}");
		}
	}

	public void Spinning()
	{

		bulletTransform.Rotate(new Vector3(0f, 0f, 100 * bulletSpeed * Time.deltaTime), Space.Self);
	}

	private void OnTriggerEnter(Collider other)
	{
		Target = other.GetComponent<Character>();

		if (Target != null && Target != shooter)
		{
			Target.OnDeath();
			OnDespawn();
		}
	}
	public void OnDespawn() // Despawn sau 1 khoang thoi gian va khi va cham vs dich.....
	{
		Destroy(gameObject);
	}

	public void ChangeColor()
	{
		List<Material> myMaterials = bulletObj.GetComponent<Renderer>().materials.ToList();
		myMaterials[0].color = GetColor(PartColor.BLUE);
		myMaterials[1].color = GetColor(PartColor.GRAY);
		//Debug.Log(myMaterials[0].color + myMaterials[1].color);
	}
	public Color GetColor(PartColor color)
	{
		Color result = new Color(0f, 0f, 0f, 0f);
		switch (color)
		{
			case PartColor.RED:
				result = Color.red;
				break;
			case PartColor.GREEN:
				result = Color.green;
				break;
			case PartColor.BLUE:
				result = Color.blue;
				break;
			// PartColor.BROWN: return Color.brown;
			case PartColor.BLACK:
				result = Color.black;
				break;
			case PartColor.GRAY:
				result = Color.gray;
				break;
			default:
				break;
		}
		return result;
	}
}
