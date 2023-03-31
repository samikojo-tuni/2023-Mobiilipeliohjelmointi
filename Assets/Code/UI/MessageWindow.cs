using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Platformer2D.UI
{
	public class MessageWindow : MonoBehaviour
	{
		[SerializeField] private GameObject loadingIndicator;
		[SerializeField] private TMP_Text message;

		private void Start()
		{
			loadingIndicator.SetActive(false);
			message.text = "";
		}

		public void ShowMessage(string message, bool showLoading)
		{
			this.message.text = message;
			loadingIndicator.SetActive(showLoading);
			gameObject.SetActive(true);
		}

		public void Close()
		{
			gameObject.SetActive(false);
		}
	}
}
