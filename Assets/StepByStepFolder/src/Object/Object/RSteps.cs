using System;
using UnityEngine;
using System.Collections;

namespace StepObject
{
	public class RSteps : Steps
	{
		private RSteps () : base()
		{

		}

		public RSteps(GameObject obj, GameObject wallL, GameObject wallR) : base(obj, wallL, wallR) 
		{

		}

		public override MapObject newInstance ()
		{
			return new LSteps (MonoBehaviour.Instantiate (obj), wallL, wallR);
		}

		protected override Vector2 bringWall(int idx)
		{
			return new Vector2 
				(
					Position.x - 2.3f * blockSize + idx * blockSize * 0.863f,
					Position.y + 4.56f * blockSize + idx * blockSize * 0.434f
				);
		}

		public override void bringWallL(int r)
		{
			RWallPosition = bringWall (r);
		}

		public override void bringWallR(int l)
		{
			LWallPosition = bringWall (l);
		}

		public override int order 
		{
			get
			{
				SpriteRenderer[] t = this.obj.GetComponentsInChildren<SpriteRenderer> ();
				return t [1].GetComponent<SpriteRenderer> ().sortingOrder;
			}

			set
			{
				SpriteRenderer[] t = this.obj.GetComponentsInChildren<SpriteRenderer> ();
				for (int i = t.Length-1; i >=0; i--)
					t [i].GetComponent<SpriteRenderer> ().sortingOrder = value + t.Length - i;
			}
		}
	}
}

