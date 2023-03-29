using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.InventorySystem;
using System;
using Platformer2D.InventorySystem.UI;

namespace Platformer2D
{
	public class PlayerController : CharacterBase
	{
		private static Inventory Inventory;

		[SerializeField]
		private float inventoryWeightLimit = 10;

		private InputReader inputReader;
		private InventoryUI inventoryUI;

		protected override void Awake()
		{
			base.Awake();

			this.inputReader = GetComponent<InputReader>();

			if (Inventory == null)
			{
				Inventory = new Inventory(inventoryWeightLimit);
			}
		}

		private void Start()
		{
			this.inventoryUI = FindObjectOfType<InventoryUI>();
			this.inventoryUI.SetInventory(Inventory);
			this.inventoryUI.UpdateInventory();
		}

		private void Update()
		{
			Vector2 movement = inputReader.GetMoveInput();
			GetMover().Move(movement);
			UpdateAnimator(movement);
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			ItemVisual item = collision.gameObject.GetComponent<ItemVisual>();
			if (item != null)
			{
				Collect(item);
			}
		}

		private bool Collect(ItemVisual item)
		{
			if (Inventory.AddItem(item.GetItem()))
			{
				Debug.Log($"Item {item.GetItem().Name} collected!");

				// Toista ääniefekti
				float delay = 0;

				AudioSource audio = item.GetComponent<AudioSource>();
				if (audio != null)
				{
					delay = audio.clip.length;
					audio.Play();
				}

				// Piilota kerättävä esine
				Renderer renderer = item.GetComponent<Renderer>();
				if (renderer != null)
				{
					renderer.enabled = false;
				}

				// Estä myös uudet törmäykset esineen kanssa
				Collider2D collider = item.GetComponent<Collider2D>();
				if (collider != null)
				{
					collider.enabled = false;
				}

				// Toista visuaalinen efekti
				ParticleSystem collectEffectPrefab = item.GetCollectEffect();
				if (collectEffectPrefab != null)
				{
					ParticleSystem collectEffect = Instantiate(collectEffectPrefab,
						item.transform.position, Quaternion.identity);

					collectEffect.Play();

					Destroy(collectEffect.gameObject, collectEffect.main.duration);
				}

				inventoryUI.UpdateInventory();

				Destroy(item.gameObject, delay);
				return true;
			}

			return false;
		}
	}
}
