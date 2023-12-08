using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
	[SerializeField] private GameObject bulletObj;
	private float deSpawnTime = 1f;
	public Character shooter;
	public Vector3 target;
	public float bulletSpeed = 10f;
	public Vector3 destination;


	public Transform bulletTransform;

	private void Start()
	{
		bulletTransform = transform;
		bulletTransform.Rotate(new Vector3(0f, 0f, 0f));
	}

	// Update is called once per frame
	void Update()
	{
		Spinning();
		destination = new Vector3(target.x, 0f, target.z);
		bulletTransform.position = Vector3.MoveTowards(bulletTransform.position, destination, Time.deltaTime * bulletSpeed);
		Invoke(nameof(OnDespawn), deSpawnTime);
	}

	public void Spinning()
	{
		bulletTransform.Rotate(new Vector3(0f, 0f, 100 * bulletSpeed * Time.deltaTime), Space.Self);
	}

	private void OnTriggerEnter(Collider other)
	{
		Character victim = other.GetComponent<Character>();

		if (victim != null && victim != shooter)
		{
			victim.OnDeath();
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
