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

				// TODO: Toista ääniefekti
				// TODO: Toista visuaalinen efekti
				inventoryUI.UpdateInventory();

				Destroy(item.gameObject);
				return true;
			}

			return false;
		}
	}
}
