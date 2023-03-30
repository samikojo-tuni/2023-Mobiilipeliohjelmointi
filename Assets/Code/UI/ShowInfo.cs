using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
	public class ShowInfo : MonoBehaviour
	{
		[SerializeField]
		private GameObject infoText;

		// Start is called before the first frame update
		void Start()
		{
			infoText.SetActive(false);
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.tag == "Player")
			{
				infoText.SetActive(true);
			}
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			if (collision.tag == "Player")
			{
				infoText.SetActive(false);
			}
		}
	}
}
