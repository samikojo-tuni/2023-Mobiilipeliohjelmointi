using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace Platformer2D.UI
{
	public class HighscoreWindow : MonoBehaviour
	{
		[System.Serializable]
		public class Score
		{
			public string name;
			public int score;
		}

		[System.Serializable]
		public class ScoreList
		{
			public List<Score> scores;
		}

		private const string ScoreURL = "https://highscore-example.onrender.com/";

		[SerializeField]
		private ScoreItem scoreTemplate;

		[SerializeField]
		private TMP_Text message;

		[SerializeField]
		private GameObject loadingIndicator;

		private Coroutine fetchScoreRoutine;
		private ScoreList scores;
		private List<ScoreItem> items = new List<ScoreItem>();

		void Start()
		{
			scoreTemplate.gameObject.SetActive(false);
			HideMessage();
			loadingIndicator.SetActive(false);

			fetchScoreRoutine = StartCoroutine(FetchScores());
		}

		private IEnumerator FetchScores()
		{
			using (UnityWebRequest webRequest = UnityWebRequest.Get(ScoreURL))
			{
				ShowMessage("Fetching Highscore data");
				loadingIndicator.SetActive(true);
				// Request and wait for the desired page.
				yield return webRequest.SendWebRequest();

				loadingIndicator.SetActive(false);

				switch (webRequest.result)
				{
					case UnityWebRequest.Result.ConnectionError:
					case UnityWebRequest.Result.DataProcessingError:
						Debug.LogError("Error: " + webRequest.error);
						ShowMessage(webRequest.error);
						break;

					case UnityWebRequest.Result.ProtocolError:
						Debug.LogError("HTTP Error: " + webRequest.error);
						ShowMessage(webRequest.error);
						break;

					case UnityWebRequest.Result.Success:
						Debug.Log("Received: " + webRequest.downloadHandler.text);
						HideMessage();

						scores = JsonUtility.FromJson<ScoreList>(webRequest.downloadHandler.text);

						ClearScoreItems();

						int index = 0;
						foreach (Score score in scores.scores)
						{
							index++;
							ScoreItem item = Instantiate(scoreTemplate, scoreTemplate.transform.parent);
							item.Setup(index, score.name, score.score);
						}

						break;
				}
			}
		}

		private void HideMessage()
		{
			this.message.gameObject.SetActive(false);
		}

		private void ShowMessage(string message)
		{
			this.message.text = message;
			this.message.gameObject.SetActive(true);
		}

		private void ClearScoreItems()
		{
			foreach (ScoreItem scoreItem in items)
			{
				Destroy(scoreItem.gameObject);
			}

			items.Clear();
		}

		public void Close()
		{
			LevelLoader.CloseHighscore();
		}
	}
}
