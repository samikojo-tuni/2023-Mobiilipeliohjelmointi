using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Platformer2D.InventorySystem.UI
{
	public class InventoryUI : MonoBehaviour
	{
		[System.Serializable]
		public class ItemImage
		{
			public ItemType Type;
			public Sprite image;
		}

		[SerializeField]
		private ItemImage[] icons;

		[SerializeField]
		private InventoryUIItem template;

		[SerializeField]
		private int slots = 8;

		[SerializeField]
		private TMP_Text infoText;

		private InventoryUIItem[] items;
		private Inventory inventory;

		private void Awake()
		{
			items = new InventoryUIItem[slots];
			for(int i = 0; i < slots; i++)
			{
				items[i] = Instantiate(template, template.transform.parent);
				items[i].Setup(this);
			}

			// Piilota template
			template.gameObject.SetActive(false);
		}

		private void SetInfoText()
		{
			float weight = inventory.GetWeight();
			float totalWeight = inventory.GetWeightLimit();
			float value = inventory.GetValue();

			infoText.text = $"Weight: {weight}/{totalWeight}, Value: {value}";
		}

		private InventoryUIItem GetItem(ItemType type)
		{
			foreach (InventoryUIItem uiItem in items)
			{
				Item item = uiItem.GetItem();
				if (item != null && item.Type == type)
				{
					return uiItem;
				}
			}

			// Tyyppiä vastaavaa UI itemiä ei löytynyt
			foreach (InventoryUIItem uiItem in items)
			{
				if (uiItem.GetItem() == null)
				{
					return uiItem;
				}
			}

			return null;
		}

		public void SetInventory(Inventory inventory)
		{
			this.inventory = inventory;
			UpdateInventory();
		}

		public void UpdateInventory()
		{
			foreach (Item item in inventory.GetItems())
			{
				InventoryUIItem uiItem = GetItem(item.Type);
				if (uiItem != null)
				{
					uiItem.SetItem(item);
				}
			}

			SetInfoText();
		}

		public Sprite GetImage(ItemType itemType)
		{
			foreach(ItemImage item in icons)
			{
				if (item.Type == itemType)
				{
					return item.image;
				}
			}

			return null;
		}

		public void Save()
		{
			inventory.Save();
		}

		public void Load()
		{
			inventory.Load();
			UpdateInventory();
		}

		public void Clear()
		{
			inventory.Clear();
			UpdateInventory();
		}
	}
}
