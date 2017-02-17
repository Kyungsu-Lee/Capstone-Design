using System;
using UnityEngine;

namespace StepObject
{
	public class Step : StepInterface
	{
		
		private Step ()
		{
			
		}

		public Step(GameObject obj) : base(obj)
		{			
			//this.show ();
			this.y = Position.y;
			Debug.Log (y);
			delay = 0;
		}

		public override MapObject newInstance()
		{
			return new Step(MonoBehaviour.Instantiate (this.obj));
		}

		public float delay {
			get;
			set;
		}
			

		public float y {
			get;
			set;
		}
		float time = 0;

		protected override void showActionMethod()
		{


			if ((time += Time.deltaTime) > delay) {
				if (Math.Abs (y - Position.y) < 3)
					this.Position = new Vector3 (Position.x, Position.y + 0.3f);
				else {
					stop ();
				}
			}

		}

		protected override void deleteActionMethod()
		{
			
		}


	}
}

