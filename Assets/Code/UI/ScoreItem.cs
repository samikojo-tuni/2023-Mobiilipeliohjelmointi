using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Platformer2D.UI
{
    public class ScoreItem : MonoBehaviour
    {
		[SerializeField] private TMP_Text placeText;
		[SerializeField] private TMP_Text nameText;
		[SerializeField] private TMP_Text scoreText;

		public void Setup(int place, string name, int score)
		{
			placeText.text = place.ToString();
			nameText.text = name;
			scoreText.text = score.ToString();

			gameObject.SetActive(true);
		}
    }
}
