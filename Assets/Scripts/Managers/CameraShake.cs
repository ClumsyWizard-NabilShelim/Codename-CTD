using UnityEngine;
using ClumsyWizard.Utilities;

public enum ShakeMagnitude
{
	Small = 2,
	Medium = 2,
	Large = 4
}

public enum ShakeDuration
{
	Small = 1,
	Medium = 2,
	Large = 3
}

public class CameraShake : Singleton<CameraShake>
{
	private bool shake;

	private float duration;
	private float magnitude;

	private Camera cam;
	private Vector3 originalPos;

	private void Start()
	{
		cam = Camera.main;
		originalPos = cam.transform.localPosition;
	}

	void Update()
	{
		if (shake)
		{
			if (duration > 0)
			{
				Vector3 randomPosition = Random.insideUnitSphere;
				cam.transform.localPosition = Vector3.Slerp(cam.transform.localPosition, originalPos + new Vector3(randomPosition.x, 0, randomPosition.z) * magnitude, Time.deltaTime * 5.0f);
				duration -= Time.deltaTime;
			}
			else
			{
				ShakeOver();
			}
		}
		else
		{

		}
	}

	private void ShakeOver()
	{
		duration = 0;
		cam.transform.localPosition = originalPos;
		shake = false;
	}

	public void ShakeObject(ShakeDuration duration, ShakeMagnitude magnitude)
	{
		this.duration = ((int)duration) / 10.0f;
		this.magnitude = (int)magnitude;

		shake = true;
	}
}
