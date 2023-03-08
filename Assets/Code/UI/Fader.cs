using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer2D
{
	public class Fader : MonoBehaviour
	{
		public enum State
		{
			None = 0,
			FadeIn,
			FadeOut
		}

		[SerializeField]
		private Image background;

		[SerializeField, Tooltip("Fade speed in seconds")]
		private float speed = 1;

		private State fadeState = State.None;
		private Color color; // Background's current color

		void Start()
		{
			// At the beginning the background should be fully transparent
			color = background.color;
			color.a = 0; // 0 means fully transparent
			background.color = color; // Update background's color
		}

		void Update()
		{
			switch(fadeState)
			{
				case State.FadeIn:
					// color.a from 0 to 1
					color.a = Mathf.Clamp01(color.a + Time.deltaTime * speed);
					background.color = color;

					if (Mathf.Approximately(color.a, 1))
					{
						fadeState = State.None;
					}
					break;

				case State.FadeOut:
					// color.a from 1 to 0
					color.a = Mathf.Clamp01(color.a - Time.deltaTime * speed);
					background.color = color;

					if (Mathf.Approximately(color.a, 0))
					{
						fadeState = State.None;
					}
					break;

				default:
					// If the fadeState is anything else, execute default
					break;
			}
		}

		public float FadeIn()
		{
			fadeState = State.FadeIn;
			return 1 / speed;
		}

		public float FadeOut()
		{
			fadeState = State.FadeOut;
			return 1 / speed;
		}
	}
}
