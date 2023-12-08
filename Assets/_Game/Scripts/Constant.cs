using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant 
{
	public const string TAG_PLAYER = "";
	public const string AttackAnim = "IsAttack";
	public const string DeadAnim = "IsDead";

}

public enum PlayerState
{
	Idle,
	Moving,
	Attacking,
	Death
}

public enum WeaponType
{
	HAMMER = 0,
	AXE1 = 1,
	AXE2 = 2,
	BOOMERANG = 3,
	CANDY1 = 4,
	CANDY2 = 5,
	CANDY3 = 6,
	CANDY4 = 7,
	KNIFE = 9,
	UZI = 10,
	Z = 11,
}

public enum PartColor
{
	RED = 0,
	BLUE = 1,
	BROWN = 2,
	BLACK = 3,
	GREEN = 4,
	GRAY = 5,
}

