using System;
using UnityEngine;
using System.Collections;

namespace StepObject
{
	public class LSteps : Steps
	{
		private LSteps () : base()
		{
			
		}

		public LSteps(GameObject obj, GameObject wallL, GameObject wallR) : base(obj, wallL, wallR) 
		{
			
		}

		public override MapObject newInstance ()
		{
			return new LSteps (MonoBehaviour.Instantiate (obj), wallL, wallR);
		}

		protected override Vector2 bringWall(int idx)
		{
			return new Vector2 (
				Position.x - 2.55f * blockSize + idx * blockSize * 0.863f, 
				Position.y + 3.4f * blockSize - idx * blockSize * 0.434f
			);
		}

		public override int order
		{
			get
			{
				SpriteRenderer[] t = this.obj.GetComponentsInChildren<SpriteRenderer> ();
				return t [t.Length-1].GetComponent<SpriteRenderer> ().sortingOrder;
			}

			set
			{
				SpriteRenderer[] t = this.obj.GetComponentsInChildren<SpriteRenderer> ();
				for (int i = 0; i < t.Length; i++)
					t [i].GetComponent<SpriteRenderer> ().sortingOrder = value + i;
			}
		}

	}
}

