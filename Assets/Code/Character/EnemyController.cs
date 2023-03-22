using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
	public class EnemyController : CharacterBase
	{
		private const float TargetReachedThreshold = 0.1f;

		[SerializeField]
		private Transform[] path;

		private int targetIndex = 0;

		public Transform GetTarget()
		{
			return path[targetIndex];
		}

		private void Update()
		{
			Vector2 direction = TraversePath();
			UpdateAnimator(direction);
		}

		private Vector2 TraversePath()
		{
			Vector2 targetDirection = GetTargetDirection();
			GetMover().Move(targetDirection);
			return targetDirection;
		}

		private Vector2 GetTargetDirection()
		{
			Vector2 toTarget = GetTarget().position - transform.position;
			if (toTarget.magnitude <= TargetReachedThreshold)
			{
				// Päivitä kohde
				targetIndex++;

				// Jos ollaan saavutettu polun pää, resetoidaan targetIndex.
				if (targetIndex >= path.Length)
				{
					targetIndex = 0;
				}

				toTarget = GetTarget().position - transform.position;
			}

			return toTarget.normalized;
		}
	}
}
