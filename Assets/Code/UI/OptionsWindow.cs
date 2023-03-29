using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Platformer2D.UI
{
	public class OptionsWindow : MonoBehaviour
	{
		[SerializeField]
		private AudioMixer mixer;

		[SerializeField]
		private AudioControl masterControl;
		[SerializeField]
		private AudioControl musicControl;
		[SerializeField]
		private AudioControl sfxControl;

		[SerializeField]
		private string sfxName;
		[SerializeField]
		private string musicName;
		[SerializeField]
		private string masterName;

		private void Start()
		{
			masterControl.Setup(mixer, masterName);
			musicControl.Setup(mixer, musicName);
			sfxControl.Setup(mixer, sfxName);
		}

		public void CloseOptions()
		{
			masterControl.Save();
			musicControl.Save();
			sfxControl.Save();

			LevelLoader.CloseOptions();
		}
	}
}
