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

		private PlayerInput playerInput;

		private ControlMethod currentControlMethod = ControlMethod.Tap;

		// T�h�n muuttujaan tallennetaan k�ytt�j�n Move-actionia vastaava arvo.
		private Vector2 moveInput;

		// Piste pelimaailmassa, johon k�ytt�j� haluaa hahmon liikkuvan
		private Vector3 worldTouchPosition;

		private void Awake()
		{
			playerInput = GetComponent<PlayerInput>();
		}

		private void Start()
		{
			// HACK: Est� PlayerInputia vaihtamasta kontrolliskeemaa, jos virtual joystick on
			// k�yt�ss�!
			if (playerInput.currentActionMap.name == "Game")
			{
				// Jos VirtualJoystick on k�yt�ss�, estet��n Unity� vaihtamasta kontrolliskeemaa
				playerInput.neverAutoSwitchControlSchemes = true;
				playerInput.SwitchCurrentControlScheme(Gamepad.current);
			}
			else
			{
				worldTouchPosition = transform.position;
			}
		}

		public void OnMove(InputAction.CallbackContext context)
		{
			currentControlMethod = ControlMethod.VirtualJoystick;
			// Luetaan k�ytt�j�n hahmoa liikuttava sy�te muuttujaan.
			moveInput = context.ReadValue<Vector2>();
			Debug.Log($"Sy�te: {moveInput}");
		}

		public void OnTapMove(InputAction.CallbackContext context)
		{
			if (context.phase == InputActionPhase.Performed)
			{
				currentControlMethod = ControlMethod.Tap;

				// Lue t�pp�yksen koodrdinaatti n�yt�ll�
				Vector2 touchPosition = context.ReadValue<Vector2>();

				// Implisiittinen tyyppimuunnos, Vector2 muuttuu siis Vector3:ksi automaattisesti
				// eli sama kuin
				// Vector3 screenCoordinate = new Vector3(touchPosition.x, touchPosition.y, 0);
				Vector3 screenCoordinate = touchPosition;

				// Koordinaattimuunnos n�yt�n koordinaatista pelimaailman koordinaatistoon
				this.worldTouchPosition = Camera.main.ScreenToWorldPoint(screenCoordinate);
			}
		}

		public Vector2 GetMoveInput()
		{
			if (currentControlMethod == ControlMethod.VirtualJoystick)
			{
				return this.moveInput;
			}

			Vector2 toTarget = (Vector3)(worldTouchPosition - transform.position);

			if (toTarget.magnitude < targetOffset)
			{
				// Kohde saavutettu
				return Vector2.zero;
			}

			// Tehd��n vektorista yhden mittainen ja palautetaan se.
			return toTarget.normalized;
		}
	}
}
