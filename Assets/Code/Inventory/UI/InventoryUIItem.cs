using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMesh Pro

namespace Platformer2D.InventorySystem.UI
{
	public class InventoryUIItem : MonoBehaviour
	{
		private Image image;
		private TMP_Text countText;
		private Item item;
		private InventoryUI inventoryUI;

		private void Awake()
		{
			image = GetComponent<Image>();
			countText = GetComponentInChildren<TMP_Text>();
		}

		public void Setup(InventoryUI ui)
		{
			inventoryUI = ui;
		}

		public void SetItem(Item item)
		{
			this.item = item;
			image.sprite = inventoryUI.GetImage(GetItemType());
			countText.text = item.Count.ToString();
		}

		public Item GetItem()
		{
			return item;
		}

		public ItemType GetItemType()
		{
			return item.Type;
		}
	}
}
