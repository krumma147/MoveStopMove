using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BotSpawnList", menuName = "ScriptableObject/BotSpawnList", order = 2)]
public class BotSpawnList : ScriptableObject
{
	public List<BotSpawnData> spawnLocations;
}

[Serializable]
public class BotSpawnData
{
	// Start is called before the first frame update
	public Transform spawnLoc;
}