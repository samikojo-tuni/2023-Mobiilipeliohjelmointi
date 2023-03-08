using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
	public class LoadNewLevel : MonoBehaviour
	{
		[SerializeField]
		private string levelName;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.tag == "Player")
			{
				LevelLoader.LoadLevel(levelName);
			}
		}
	}
}
