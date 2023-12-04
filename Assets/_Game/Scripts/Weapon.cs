using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	protected Rigidbody rb;
	[SerializeField] protected GameObject weaponObj;

	public void OnInit()
	{
		rb = weaponObj.GetComponent<Rigidbody>();
		transform.Rotate(90.0f, 0.0f, 0.0f);
	}

	public void ChangeColor()
	{
		Debug.Log(weaponObj == null);
		Debug.Log(weaponObj.GetComponent<Renderer>() == null);
		Debug.Log(weaponObj.GetComponent<Renderer>().materials.Length > 0);
		List<Material> myMaterials = weaponObj.GetComponent<Renderer>().materials.ToList();
		myMaterials[0].color = GetColor(PartColor.BLUE);
		myMaterials[1].color = GetColor(PartColor.GRAY);
		//Debug.Log(myMaterials[0].color + myMaterials[1].color);
	}

	public Color GetColor(PartColor color) {
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