using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Platformer2D
{
	public class InputReader : MonoBehaviour
	{
		private const float targetOffset = 0.1f;

		public enum ControlMethod
		{
			VirtualJoystick,
			Tap
		}

		private ControlMethod currentControlMethod = ControlMethod.Tap;

		// Tähän muuttujaan tallennetaan käyttäjän Move-actionia vastaava arvo.
		private Vector2 moveInput;

		// Piste pelimaailmassa, johon käyttäjä haluaa hahmon liikkuvan
		private Vector3 worldTouchPosition;

		public void OnMove(InputAction.CallbackContext context)
		{
			// Luetaan käyttäjän hahmoa liikuttava syöte muuttujaan.
			moveInput = context.ReadValue<Vector2>();
			Debug.Log($"Syöte: {moveInput}");
		}

		public void OnTapMove(InputAction.CallbackContext context)
		{
			if (context.phase == InputActionPhase.Performed)
			{
				currentControlMethod = ControlMethod.Tap;

				// Lue täppäyksen koodrdinaatti näytöllä
				Vector2 touchPosition = context.ReadValue<Vector2>();

				// Implisiittinen tyyppimuunnos, Vector2 muuttuu siis Vector3:ksi automaattisesti
				// eli sama kuin
				// Vector3 screenCoordinate = new Vector3(touchPosition.x, touchPosition.y, 0);
				Vector3 screenCoordinate = touchPosition;

				// Koordinaattimuunnos näytön koordinaatista pelimaailman koordinaatistoon
				this.worldTouchPosition = Camera.main.ScreenToWorldPoint(screenCoordinate);
			}
		}

		public Vector2 GetMoveInput()
		{
			if (currentControlMethod == ControlMethod.VirtualJoystick)
			{
				return moveInput;
			}

			Vector2 toTarget = (Vector3)(worldTouchPosition - transform.position);

			if (toTarget.magnitude < targetOffset)
			{
				// Kohde saavutettu
				return Vector2.zero;
			}

			return toTarget.normalized;
		}
	}
}
