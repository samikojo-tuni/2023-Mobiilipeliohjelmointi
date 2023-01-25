using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
	public class DemoMover : MonoBehaviour
	{
		[SerializeField] private Vector2 direction = Vector2.zero;
		[SerializeField] private float distance = 1;
		[SerializeField] private float speed = 1;

		private float travelledSoFar = 0;

		// Start is called before the first frame update
		void Start()
		{
			// Normalisoi suuntavektori, eli tee siitä yhden mittainen.
			// Tällöin se antaa liikkeelle vain suunnan eikä
			// vaikuta nopeuteen lisäävästi tai vähentävästi.
			direction = direction.normalized;
		}

		// Update is called once per frame
		void Update()
		{
			// Lasketaan liikevektori, eli se vektori, jonka kuljemme
			// tämän framen ainana.
			Vector2 movement = direction * speed * Time.deltaTime;

			// Niin kauan kun kokonaismatka on pienempi kuin haluttu etäisyys,
			// liikutaan liikevektorin mukainen matka.
			if (travelledSoFar < distance)
			{
				// Pidetään kirjaa tähän mennessä kuljetusta matkasta.
				travelledSoFar += movement.magnitude;

				// Pienellä kirjoitettuna transform viittaa aina sen GameObjectin Transform-
				// komponenttiin, johon tämä skripti on kytketty. Isolla kirjoitettuna
				// Transform viittaa luokkaan Transform (jonka olio transform on).
				transform.Translate(movement);
			}
		}
	}
}