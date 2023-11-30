using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnList", menuName = "ScriptableObject/EnemySpawnList", order = 2)]
public class EnemySpawnList : ScriptableObject
{
	public List<Enemy> enemies;
}

[Serializable]
public class EnemySpawnData
{
	// Start is called before the first frame update
	public Transform spawnLoc;
}