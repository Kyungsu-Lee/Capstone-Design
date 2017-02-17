using System;
using UnityEngine;
using MathU;

namespace StepObject
{
	public class Character : CharacterInterface
	{
        static bool flag;

		private Character ()
		{
			
		}

		public Character(GameObject obj) : base(obj)
		{
            if (!flag)
            {
                Speed = 0.07f;
                flag = true;
            }
		}

		public override MapObject newInstance ()
		{
			return new Character (MonoBehaviour.Instantiate (this.obj));
		}

		protected override void characterMovingAction()
		{
			this.rotation = 47f;
		}

		protected override void characterMovingRDAction()
		{
			this.Position = new Vector2 (Position.x + Speed, Position.y - Speed);
		}

		protected override void characterMovingRUAction()
		{
			this.Position = new Vector2 (Position.x + Speed, Position.y + Speed);
			Debug.Log ("RU");
		}

		protected override void characterMovingLDAction()
		{
			this.Position = new Vector2 (Position.x - Speed, Position.y - Speed);
			Debug.Log ("LD");
		}

		protected override void characterMovingLUAction()
		{
			this.Position = new Vector2 (Position.x - Speed, Position.y + Speed);
		}

		protected override void characterFallAction ()
		{
			
		}

		protected override void characterGoalAction ()
		{
			
		}

		protected override void characterToWallMethod (Vector3 v3)
		{

			this.Position = new Vector2 
				(
					Position.x + (v3.x - currentState.x)* Speed / Vector3.Distance(v3, currentState),
					follow.fy(Position.x) - 0.25f
				);
		}

		public bool Gravity {
			set {
				if(this.obj != null)
					this.obj.GetComponent<Player> ().flag = value;
			}
		}

		public Line follow {
			get;
			set;
		}


		public void characterMoveToWall(Vector3 direction)
		{
			currentState = Position;
			VectorDirection = direction;
			characterToWallTrue ();
		}

		public void stop()
		{
			if (this.obj == null)
				return;

			characterMovingLDFalse ();
			characterMovingLUFalse ();
			characterMovingRDFalse ();
			characterMovingRUFalse ();
			characterToWallFalse ();
		}

		public static float Speed {
			get;
			set;
		}

		public override void Destroy ()
		{
			obj.GetComponent<CharacterAction> ().flag = false;
			clearObjects ();
			base.Destroy ();
		}
	}
}

