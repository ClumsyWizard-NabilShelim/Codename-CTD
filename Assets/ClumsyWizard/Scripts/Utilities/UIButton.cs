using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClumsyWizard.Utilities
{
	public class UIButton : MonoBehaviour
	{
		private AudioSource clickAudio;

		private void Start()
		{
			clickAudio = GetComponent<AudioSource>();
		}

		public void Click()
		{
			if (clickAudio.clip != null)
				clickAudio.Play();
		}
	}
}
