using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.InventorySystem
{
	public class Inventory
	{
		private List<Item> items;
		private float weightLimit;

		public Inventory(float weightLimit)
		{
			this.weightLimit = weightLimit;
			items = new List<Item>();
		}

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
		/// inventoryn ollessa täynnä.</returns>
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

		public List<Item> GetItems()
		{
			return items;
		}

		public float GetWeightLimit()
		{
			return weightLimit;
		}

		public float GetValue()
		{
			float value = 0;
			foreach(Item item in items)
			{
				value += item.Value * item.Count;
			}

			return value;
		}
	}
}
