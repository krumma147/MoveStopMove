using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	private Rigidbody rb;
	[SerializeField] private GameObject weaponObj;

	public void OnInit()
	{
		rb = weaponObj.GetComponent<Rigidbody>();
	}

	public void ChangeColor()
	{
		List<Material> myMaterials = weaponObj.GetComponent<Renderer>().materials.ToList();
		myMaterials[0].color = GetColor(PartColor.BLUE);
		myMaterials[1].color = GetColor(PartColor.GRAY);
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