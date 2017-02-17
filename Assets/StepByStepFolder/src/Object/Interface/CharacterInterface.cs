using System;
using System.Collections;
using UnityEngine;

namespace StepObject
{
	public abstract class CharacterInterface : MapObject
	{
		public ACTION CharacterMovingAction;
		public ACTION CharacterMovingRUAction;
		public ACTION CharacterMovingRDAction;
		public ACTION CharacterMovingLUAction;
		public ACTION CharacterMovingLDAction;
		public ACTION CharacterFallAction;
		public ACTION CharacterGoalAction;
		public DIRECTIONACTION CharacterToWall;

		public DIRECTION Direction {
			get;
			set;
		}


		protected CharacterInterface()
		{
			
		}

		protected CharacterInterface(GameObject obj)
		{
			objects.Add (this);

			this.obj = obj;

			//Animation settings
			CharacterMovingAction += characterMovingAction;
			CharacterMovingRDAction += characterMovingRDAction;
			CharacterMovingRUAction += characterMovingRUAction;
			CharacterMovingLDAction += characterMovingLDAction;
			CharacterMovingLUAction += characterMovingLUAction;
			CharacterFallAction += characterFallAction;
			CharacterGoalAction += characterGoalAction;

			CharacterToWall += characterToWallMethod;
		}


		public Vector3 VectorDirection {
			get {
				return vectordirection;
			}
			set {
				vectordirection = value;
				currentState = Position;
			}
		}

		protected Vector2 currentState {
			get;
			set;
		}

		/// <summary>
		/// Animation for moving.
		/// </summary>
		protected abstract void characterMovingAction();

		protected abstract void characterMovingRDAction ();
		protected abstract void characterMovingRUAction ();
		protected abstract void characterMovingLDAction ();
		protected abstract void characterMovingLUAction ();

		protected abstract void characterFallAction ();
		protected abstract void characterGoalAction ();

		protected abstract void characterToWallMethod (Vector3 v3);


		/// <summary>
		/// Instrument for Moving
		/// </summary>
		public void characterMove()
		{
			CharacterMovingAction ();
		}

		public void characterMoveToRD()
		{
			CharacterMovingRDAction ();
		}

		public void characterMoveToRU()
		{
			CharacterMovingRUAction ();
		}

		public void characterMoveToLU()
		{
			CharacterMovingLUAction ();
		}

		public void characterMoveToLD()
		{
			CharacterMovingLDAction ();
		}

		public void characterToWall()
		{
			CharacterToWall (VectorDirection);
		}

		/// <summary>
		/// makes character fall.
		/// </summary>
		public void characterFall()
		{
			CharacterFallAction ();
		}

		/// <summary>
		/// Characters reached the goal.
		/// </summary>
		public void characterGoal()
		{
			CharacterGoalAction ();
		}

		protected void characterMovingRUTrue()
		{
			this.obj.GetComponent<CharacterAction> ().characterMovingRU = true;
		}

		protected void characterMovingRUFalse()
		{
			this.obj.GetComponent<CharacterAction> ().characterMovingRU = false;
		}

		protected void characterMovingLDTrue()
		{
			this.obj.GetComponent<CharacterAction> ().characterMovingLD = true;
		}

		protected void characterMovingLDFalse()
		{
			this.obj.GetComponent<CharacterAction> ().characterMovingLD = false;
		}

		protected void characterMovingRDTrue()
		{
			this.obj.GetComponent<CharacterAction> ().characterMovingRD = true;
		}

		protected void characterMovingRDFalse()
		{
			this.obj.GetComponent<CharacterAction> ().characterMovingRD = false;
		}

		protected void characterMovingLUTrue()
		{
			this.obj.GetComponent<CharacterAction> ().characterMovingLU = true;
		}

		protected void characterMovingLUFalse()
		{
			this.obj.GetComponent<CharacterAction> ().characterMovingLU = false;
		}

		protected void characterFallTrue()
		{
			this.obj.GetComponent<CharacterAction> ().characterFall = true;
		}

		protected void characterFallFalse()
		{
			this.obj.GetComponent<CharacterAction> ().characterFall = false;
		}

		protected void characterGoalTrue()
		{
			this.obj.GetComponent<CharacterAction> ().characterGoal = true;
		}

		protected void characterGoalFalse()
		{
			this.obj.GetComponent<CharacterAction> ().characterGoal = false;
		}

		protected void characterToWallTrue()
		{
			this.obj.GetComponent<CharacterAction> ().characterToWall = true;
		}

		protected void characterToWallFalse()
		{
			this.obj.GetComponent<CharacterAction> ().characterToWall = false;
		}

		/// <summary>
		/// Changes the direction of character.
		/// </summary>
		/// <param name="direction">Direction.</param>
		public void changeDirection(DIRECTION direction)
		{
			Direction = direction;

			if (direction == DIRECTION.LD) {
				characterMovingLDTrue ();
				characterMovingRUFalse ();
				characterMovingRDFalse ();
				characterMovingLUFalse ();
			} else if (direction == DIRECTION.LU) {
				characterMovingLUTrue ();
				characterMovingRUFalse ();
				characterMovingRDFalse ();
				characterMovingLDFalse ();
			} else if (direction == DIRECTION.RD) {
				characterMovingRDTrue ();
				characterMovingRUFalse ();
				characterMovingLDFalse ();
				characterMovingLUFalse ();
			} else if (direction == DIRECTION.RU) {
				characterMovingRUTrue ();
				characterMovingLDFalse ();
				characterMovingRDFalse ();
				characterMovingLUFalse ();
			}
		}

		private Vector3 vectordirection;
	}
}

