using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.InventorySystem
{
	// Unityssä myös ei-MonoBehaviour -luokat toimivat, mutta niitä ei
	// voida suoraan kytkeä GameObjectiin.

	// Luokan on oltava serialisoituva, jotta Unity voi tallentaa sen tiedot sceneen tai
	// prefabiin ja piirtää sen editoriin.
	[System.Serializable]
	public class Item
	{
		public ItemType Type;
		public string Name;
		public int Value;
		public float Weight;
		public int Count;

		public float GetTotalWeight()
		{
			return Weight * Count;
		}
	}
}