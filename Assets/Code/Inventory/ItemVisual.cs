using UnityEngine;

namespace Platformer2D.InventorySystem
{
	public class ItemVisual : MonoBehaviour
	{
		[SerializeField]
		private Item item;

		[SerializeField]
		private ParticleSystem collectEffect;

		public Item GetItem()
		{
			return item;
		}

		public ParticleSystem GetCollectEffect()
		{
			return collectEffect;
		}
	}
}
