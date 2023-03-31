using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.UI
{
	public class HighscoreButton : MonoBehaviour
	{
		public void OpenHighscore()
		{
			LevelLoader.OpenHighscore();
		}
	}
}
