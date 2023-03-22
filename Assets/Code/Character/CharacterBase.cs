using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
	public class CharacterBase : MonoBehaviour
	{
		private const string AnimatorX = "Look X";
		private const string AnimatorY = "Look Y";
		private const string AnimatorSpeed = "Speed";

		private IMover mover;
		private Animator animator;

		protected virtual void Awake()
		{
			this.mover = GetComponent<IMover>();
			this.animator = GetComponent<Animator>();
		}

		protected void UpdateAnimator(Vector2 movement)
		{
			this.animator.SetFloat(AnimatorX, movement.x);
			this.animator.SetFloat(AnimatorY, movement.y);
			this.animator.SetFloat(AnimatorSpeed, this.mover.GetSpeed());
		}

		protected IMover GetMover()
		{
			return mover;
		}
	}
}
