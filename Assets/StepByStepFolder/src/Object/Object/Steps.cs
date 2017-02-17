using System;
using UnityEngine;
using System.Collections;

namespace StepObject
{
	public abstract class Steps : MapObject
	{

		protected GameObject wallL;
		protected GameObject wallR;

		protected const float blockSize = 0.5f;
		private float time = 0;

		public float DestroyDelay {
			get;
			set;
		}

		protected Steps ()
		{

		}

		public Steps(GameObject obj, GameObject wallL, GameObject wallR) 
		{
			this.obj = obj;
			this.wallL = wallL;
			this.wallR = wallR;

            this.wallR.GetComponent<Transform>().position = new Vector3(-10, -10, 0);
            this.wallL.GetComponent<Transform>().position = new Vector3(-10, -10, 0);

            this.DestroyDelay = 0.35f;
		}
        
		public void delete(out bool flag, out bool change, out bool soundFlag)
		{
			
			flag = false;
			change = false;
            soundFlag = false;
			if (time < 0.05f) {
				this.obj.GetComponent<Animator> ().SetTrigger ("finish");
			}

			if (time > 0.4f && time < 1.5f * DestroyDelay) {
				removeCollider ();
				DestroyWalls ();
            }

			if (time < 1.5f * DestroyDelay)
				time += Time.deltaTime;
			else if (time < 2 * DestroyDelay) {
				DestroyBlocks ();
				time = 2f * DestroyDelay;
			} else
            {
                flag = true;
                change = true;
                soundFlag = true;
			}

           // Debug.Log(time);
		}

		public override void Destroy()
		{
			DestroyBlocks ();
			DestroyWalls ();
		}

		private void DestroyBlocks()
		{
			MonoBehaviour.Destroy (this.obj);
		}

		private void DestroyWalls()
		{
			MonoBehaviour.Destroy (this.wallL);
			MonoBehaviour.Destroy (this.wallR);
		}

		private void removeCollider()
		{
			this.obj.GetComponent<Collider2D> ().enabled = false;
		}

		public void bringWalls(int l, int r)
		{

			if (l == r)
				throw new Exception ("Boundary Exception");

			//swap
			if (l > r)
				swap (ref l, ref r);

			if(l < 0 || r > 10)
				throw new Exception ("Boundary Exception");

			bringWallL (l);
			bringWallR (r);
		}

		public virtual void bringWallR(int r)
		{
			RWallPosition = bringWall (r);
		}

		public virtual void bringWallL(int l)
		{
			LWallPosition = bringWall (l);
		}

		public Vector2 LWallPosition
		{
			get {
				return new Vector2 (
					this.wallL.transform.GetComponent<Transform> ().position.x,
					this.wallL.transform.GetComponent<Transform> ().position.y
				);
			}

			set {
				this.wallL.GetComponent<Transform> ().position = new Vector3 
					(
						value.x,
						value.y,
						0
					);
			}
		}

		public Vector2 RWallPosition
		{
			get {
				return new Vector2 (
					this.wallR.transform.GetComponent<Transform> ().position.x,
					this.wallR.transform.GetComponent<Transform> ().position.y
				);
			}

			set {
				this.wallR.GetComponent<Transform> ().position = new Vector3 
					(
						value.x,
						value.y,
						-3
					);
			}
		}

		protected void swap(ref int m, ref int n)
		{
			int tmp = m;
			m = n;
			n = tmp;
		}

		public float z {
			get {
				return this.obj.GetComponent<Transform> ().position.z;
			}
			set {
				this.obj.GetComponent<Transform> ().position = new Vector3 
					(
						Position.x,
						Position.y,
						value
					);
			}	
		}

		public void hideL(bool flag)
		{
			this.wallL.SetActive (!flag);
		}

		public void hideR(bool flag)
		{
			this.wallR.SetActive (!flag);
		}

		public abstract int order {
			get;
			set;
		}

		public void setGoal()
		{
			this.wallL.GetComponent<SpriteRenderer> ().color = new Color (1, 0, 1);
		}

		protected abstract Vector2 bringWall (int idx);
		public abstract override MapObject newInstance ();
	}
}

