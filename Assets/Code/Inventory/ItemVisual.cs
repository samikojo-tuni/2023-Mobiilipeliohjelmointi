using UnityEngine;

namespace Platformer2D.InventorySystem
{
	public class ItemVisual : MonoBehaviour
	{
		[SerializeField]
		private Item item;

		public Item GetItem()
		{
			return item;
		}
	}
}
