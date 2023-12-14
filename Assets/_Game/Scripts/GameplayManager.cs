using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
	[SerializeField] private Character playerPrefab;
	[SerializeField] private Character botPrefab;
	[SerializeField] private Transform playerSpawnLoc;
    [SerializeField] private EndGamePanel endGamePanel;
	[SerializeField] private GameObject gamePlayUI;
	[SerializeField] private FloatingJoystick _joystick;
	[SerializeField] private BotSpawnList botSpawnPositions;
	private List<Enemy> bots;
	private bool playerIsDead = false;
	// Start is called before the first frame update
	void Start()
    {
		DisableGameOverPanel();
		SpawnPlayer();
		EnableGamePlayUI();
	}
	private void Update()
	{

		if (playerIsDead)
		{
			Invoke(nameof(EnableGameOverPanel), 1.5f);
		}
	}

	public void SpawnPlayer()
	{
		Player player = Instantiate(playerPrefab, playerSpawnLoc.position, playerSpawnLoc.rotation) as Player;
		player._joystick = _joystick;
		player.OnInit();
		CameraManager.Instance.SetTarget(player.transform);
	}

	public void EnableGamePlayUI()
	{
		gamePlayUI.SetActive(true);
	}

	public void DisableGamePlayUI()
	{
		gamePlayUI.SetActive(false);
	}

	public void EnableGameOverPanel()
	{
		endGamePanel.OpenGameOverPanel();
	}

	public void DisableGameOverPanel()
	{
		endGamePanel.CloseGameOverPanel();
	}

	public void PlayerIsDead()
	{
		playerIsDead = true;
	}

	public void SpawnBots()
	{
		foreach(BotSpawnData spawnData in botSpawnPositions.spawnLocations)
		{
			Enemy enemy = Instantiate(botPrefab, spawnData.spawnLoc.position, spawnData.spawnLoc.rotation) as Enemy;
			enemy.OnInit();
			enemy.DisableBot();
			bots.Add(enemy);
		}
	}

}
