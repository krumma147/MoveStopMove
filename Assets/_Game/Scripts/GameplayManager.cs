using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
	[SerializeField] private Character playerPrefab;
	[SerializeField] private Transform playerSpawnLoc;
    [SerializeField] private EndGamePanel endGamePanel;
	[SerializeField] private GameObject gamePlayUI;
	[SerializeField] private FloatingJoystick _joystick;
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
		Player player = playerPrefab.GetComponent<Player>();
		player._joystick = _joystick;
		Instantiate(player, playerSpawnLoc);
		player.OnInit();
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
}
