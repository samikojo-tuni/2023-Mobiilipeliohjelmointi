using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
	public class Mover : MonoBehaviour
	{
		[SerializeField]
		private float speed = 1;

		private InputReader inputReader;

		private void Awake()
		{
			// Palauttaa viittauksen samassa GameObjectissa olevaan InputReader komponenttiin.
			// Jos komponenttia ei löydy, GetComponent palauttaa null:in.
			// https://docs.unity3d.com/ScriptReference/GameObject.GetComponent.html
			inputReader = GetComponent<InputReader>();
		}

		private void Update()
		{
			Vector2 moveInput = inputReader.GetMoveInput();
			Move(moveInput);
		}

		private void Move(Vector2 direction)
		{
			Vector2 movement = direction * speed * Time.deltaTime;
			transform.Translate(movement);
		}
	}
}
