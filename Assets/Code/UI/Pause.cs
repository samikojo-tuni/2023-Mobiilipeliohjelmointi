using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.UI
{
	public class Pause : MonoBehaviour
	{
		public void PauseGame()
		{
			LevelLoader.OpenOptions();
		}
	}
}
