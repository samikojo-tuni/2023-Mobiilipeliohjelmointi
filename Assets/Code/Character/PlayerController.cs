using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.InventorySystem;
using System;
using Platformer2D.InventorySystem.UI;

namespace Platformer2D
{
	public class PlayerController : MonoBehaviour
	{
		private const string AnimatorX = "Look X";
		private const string AnimatorY = "Look Y";
		private const string AnimatorSpeed = "Speed";

		private static Inventory Inventory;

		[SerializeField]
		private float inventoryWeightLimit = 10;

		private InputReader inputReader;
		private IMover mover;
		private Animator animator;
		private InventoryUI inventoryUI;

		private void Awake()
		{
			this.inputReader = GetComponent<InputReader>();
			this.mover = GetComponent<IMover>();
			this.animator = GetComponent<Animator>();

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
			this.mover.Move(movement);
			UpdateAnimator(movement);
		}

		private void UpdateAnimator(Vector2 movement)
		{
			this.animator.SetFloat(AnimatorX, movement.x);
			this.animator.SetFloat(AnimatorY, movement.y);
			this.animator.SetFloat(AnimatorSpeed, this.mover.GetSpeed());
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
