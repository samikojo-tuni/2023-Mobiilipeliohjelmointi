using System.Collections;
using System.Collections.Generic;
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

		public const string ScoreURL = "https://highscore-example.onrender.com/";

		[SerializeField] private ScoreItem scoreItemTemplate;
		[SerializeField] private SendScoreWindow sendScoreWindow;
		[SerializeField] private MessageWindow messageWindow;

		private List<ScoreItem> scoreItems = new List<ScoreItem>();

		private void Start()
		{
			scoreItemTemplate.gameObject.SetActive(false);
			sendScoreWindow.gameObject.SetActive(false);
			messageWindow.Close();
			StartCoroutine(FetchScores());
		}

		private IEnumerator FetchScores()
		{
			using (UnityWebRequest webRequest = UnityWebRequest.Get(ScoreURL))
			{
				messageWindow.ShowMessage("Loading scores", true);
				// Odotetaan vastausta serveriltä
				yield return webRequest.SendWebRequest();
				messageWindow.Close();

				// Suoritetaan, kun serveri on lähettänyt vastauksen
				switch(webRequest.result)
				{
					case UnityWebRequest.Result.ConnectionError:
					case UnityWebRequest.Result.DataProcessingError:
						Debug.LogError("Error: " + webRequest.error);
						messageWindow.ShowMessage(webRequest.error, false);
						break;
					case UnityWebRequest.Result.ProtocolError:
						Debug.LogError("HTTP ERROR: " + webRequest.error);
						messageWindow.ShowMessage(webRequest.error, false);
						break;
					case UnityWebRequest.Result.Success:
						// Pisteet saatiin onnistuneesti palvelimelta
						// Näytä pisteet käyttöliittymällä!
						string scoreJson = webRequest.downloadHandler.text;
						ScoreList scoreList = JsonUtility.FromJson<ScoreList>(scoreJson);

						int index = 0;
						foreach (Score score in scoreList.scores)
						{
							index++;
							ScoreItem scoreItem = Instantiate(scoreItemTemplate,
								scoreItemTemplate.transform.parent);
							scoreItem.Setup(index, score.name, score.score);
						}
						break;
				}
			}
		}

		public void Close()
		{
			LevelLoader.CloseHighscore();
		}

		public void OpenSendScoreWindow()
		{
			sendScoreWindow.gameObject.SetActive(true);
		}
	}
}
