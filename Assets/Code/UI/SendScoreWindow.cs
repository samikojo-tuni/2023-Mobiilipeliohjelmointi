using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

namespace Platformer2D.UI
{
	public class SendScoreWindow : MonoBehaviour
	{
		private const string password = "RandomPassword_3e10";
		public const string ScoreURL = "https://highscore-example.onrender.com/";

		[SerializeField] private TMP_InputField nameField;
		[SerializeField] private TMP_InputField scoreField;
		[SerializeField] private MessageWindow messageWindow;

		public void SendScore()
		{
			StartCoroutine(UploadScore());
		}

		public void Close()
		{
			gameObject.SetActive(false);
		}

		private IEnumerator UploadScore()
		{
			WWWForm form = new WWWForm();
			form.AddField("name", nameField.text);
			form.AddField("score", scoreField.text);
			form.AddField("pw", password);

			using (UnityWebRequest webRequest = UnityWebRequest.Post(ScoreURL, form))
			{
				messageWindow.ShowMessage("Sending score", true);
				yield return webRequest.SendWebRequest();
				messageWindow.Close();

				if (webRequest.result != UnityWebRequest.Result.Success)
				{
					// Virhe!
					Debug.LogError("ERROR: " + webRequest.error);
					messageWindow.ShowMessage(webRequest.error, false);
				}
				else
				{
					Debug.Log(webRequest.downloadHandler.text);
					messageWindow.ShowMessage(webRequest.downloadHandler.text, false);
				}
			}
		}
	}
}
