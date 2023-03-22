using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.InventorySystem
{
	public class Inventory
	{
		private const string InventoryItemCountKey = "Inventory_Item_Count";
		private const string ItemWeightKey = "Item_Weight_{0}";
		private const string ItemNameKey = "Item_Name_{0}";
		private const string ItemValueKey = "Item_Value_{0}";
		private const string ItemTypeKey = "Item_Type_{0}";
		private const string ItemCountKey = "Item_Count_{0}";

		private List<Item> items;
		private float weightLimit;

		public Inventory(float weightLimit)
		{
			this.weightLimit = weightLimit;
			items = new List<Item>();
		}

		/// <summary>
		/// Calculates the total weight of the inventory.
		/// </summary>
		/// <returns>The weight of the inventory.</returns>
		public float GetWeight()
		{
			float weight = 0;
			foreach(Item item in items)
			{
				weight += item.GetTotalWeight();
			}

			return weight;
		}

		/// <summary>
		/// Lisää uuden esineen inventaarioon. 
		/// </summary>
		/// <param name="item">Lisättävä esine.</param>
		/// <returns>True, jos esine voidaan lisätä. False muuten, esim.
		/// inventoryn ollessInventory_Item_Counta täynnä.</returns>
		public bool AddItem(Item item)
		{
			// Uuden esineen paino ylittäisi inventoryn kokonaispainon.
			if (GetWeight() + item.GetTotalWeight() > weightLimit)
			{
				return false;
			}

			// Selvitetään, onko inventoriossa jo uutta itemiä vastaava esine
			// (tyypit vastaavat toisiaan)
			Item existing = null;
			foreach(Item currentItem in items)
			{
				if (currentItem.Type == item.Type)
				{
					existing = currentItem;
					break; // Haettu esine löytyi, keskeytetään silmukka
				}
			}

			if (existing != null)
			{
				// Esine on jo inventoryssä, lisätään sen määrää
				existing.Count += item.Count;
			}
			else
			{
				items.Add(item);
			}

			return true;
		}

		// TODO: Esineen poisto

		/// <summary>
		/// Returns all the items in the inventory.
		/// </summary>
		/// <returns>List of inventory items.</returns>
		public List<Item> GetItems()
		{
			return items;
		}

		/// <summary>
		/// Returns the weight limit of the inventory.
		/// </summary>
		/// <returns>Weight limit</returns>
		public float GetWeightLimit()
		{
			return weightLimit;
		}

		/// <summary>
		/// Calculates the total value of the inventory.
		/// </summary>
		/// <returns>Total value.</returns>
		public float GetValue()
		{
			float value = 0;
			foreach(Item item in items)
			{
				value += item.Value * item.Count;
			}

			return value;
		}

		public void Save()
		{
			// Tallennetaan ensin itemeiden määrä
			PlayerPrefs.SetInt(InventoryItemCountKey, items.Count);
			for(int i = 0; i < items.Count; i++)
			{
				Item item = items[i];

				int typeInt = (int)item.Type;
				PlayerPrefs.SetInt(string.Format(ItemTypeKey, i), typeInt);
				PlayerPrefs.SetString(string.Format(ItemNameKey, i), item.Name);
				PlayerPrefs.SetFloat(string.Format(ItemWeightKey, i), item.Weight);
				PlayerPrefs.SetInt(string.Format(ItemValueKey, i), item.Value);
				PlayerPrefs.SetInt(string.Format(ItemCountKey, i), item.Count);
			}

			PlayerPrefs.Save();
		}

		public void Load()
		{
			// Poistetaan olemassa oleva data items-listasta.
			items.Clear();

			int itemCount = PlayerPrefs.GetInt(InventoryItemCountKey);

			for (int i = 0; i < itemCount; i++)
			{
				int typeInt = PlayerPrefs.GetInt(string.Format(ItemTypeKey, i), (int)ItemType.None);
				ItemType type = (ItemType)typeInt;

				string name = PlayerPrefs.GetString(string.Format(ItemNameKey, i), string.Empty);
				float weight = PlayerPrefs.GetFloat(string.Format(ItemWeightKey, i), 0);
				int value = PlayerPrefs.GetInt(string.Format(ItemValueKey, i), 0);
				int count = PlayerPrefs.GetInt(string.Format(ItemCountKey, i), 0);

				if (type != ItemType.None)
				{
					Item item = new Item()
					{
						Type = type,
						Name = name,
						Weight = weight,
						Value = value,
						Count = count
					};

					AddItem(item);
				}
			}
		}

		public void Clear()
		{
			items.Clear();

			// Poistaa kaikki avaimet
			PlayerPrefs.DeleteAll();

			// TODO: Poista avaimet yksitellen!
		}
	}
}
