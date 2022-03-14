using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	//UI
	public void Play()
	{
		SceneManagement.instance.Load();
		Debug.Log("f");
	}
	public void Credits()
	{
		SceneManagement.instance.Load("Credits");
	}
	public void Quit()
	{
		Application.Quit();
	}
}
