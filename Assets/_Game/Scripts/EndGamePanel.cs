using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EndGamePanel : MonoBehaviour
{
	[SerializeField] private UnityEngine.UI.Button continueButton;
	private GameObject gameOverPanel;
	private void Start()
	{
		gameOverPanel = this.gameObject;
		TMP_Text buttonTxt = continueButton.GetComponentInChildren<TMP_Text >();
		buttonTxt.text = "Continue";
		continueButton.onClick.AddListener(ContinueButton);
	}
	public void OpenGameOverPanel()
	{
		gameOverPanel.SetActive(true);
	}

	public void CloseGameOverPanel()
	{
		gameOverPanel.SetActive(false);
	}

	public void ContinueButton()
	{
		CloseGameOverPanel(); // Game Over UI doesn't close
		GameplayManager.Instance.SpawnPlayer();
	}
}
