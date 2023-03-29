using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Platformer2D
{
	public class AudioControl : MonoBehaviour
	{
		private AudioMixer mixer;
		private Slider slider;
		private string volumeName;

		private void Awake()
		{
			slider = GetComponentInChildren<Slider>();
		}

		public void Setup(AudioMixer mixer, string volumeName)
		{
			this.mixer = mixer;
			this.volumeName = volumeName;

			if (mixer.GetFloat(volumeName, out float decibel))
			{
				slider.value = ToLinear(decibel);
			}
		}

		public void Save()
		{
			mixer.SetFloat(volumeName, ToDB(slider.value));
		}

		private float ToDB(float linear)
		{
			return linear <= 0 ? -80f : 20f * Mathf.Log10(linear);
		}

		private float ToLinear(float decibel)
		{
			return Mathf.Clamp01(Mathf.Pow(10f, decibel / 20f));
		}
	}
}
