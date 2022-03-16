using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClumsyWizard.Utilities;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : StaticInstance<GameManager>
{
	[SerializeField] private Animator gameoverCanvasAnimator;
	[SerializeField] private TextMeshProUGUI wavesClearedText;
	public int WavesCleared { get; set; }
	public bool IsGameOver { get; private set; }

	public void GameOver()
	{
		IsGameOver = true;
		gameoverCanvasAnimator.SetTrigger("Show");
		wavesClearedText.text = WavesCleared.ToString() + " Waves Cleared";
	}

	//UI
	public void MainMenu()
	{
		SceneManagement.instance.Load("MainMenu");
	}
	public void Retry()
	{
		SceneManagement.instance.Load(SceneManager.GetActiveScene().name);
	}
}
