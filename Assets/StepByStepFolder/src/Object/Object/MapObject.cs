using System;
using System.Collections;
using UnityEngine;

namespace StepObject
{
	public abstract class MapObject
	{
		protected GameObject obj;
		protected static ArrayList objects = new ArrayList();

		public abstract MapObject newInstance();

		public GameObject getGameObject()
		{
			return this.obj;
		}

		public static MapObject findObject(Transform objectTransform)
		{
			foreach(MapObject o in objects)
			{
				if (o.getGameObject ().transform.Equals (objectTransform))
					return o;
			}

			return null;
		}

		public static void clearObjects()
		{
			objects.Clear ();
		}

		public Vector2 Position {
			get{
				return new Vector2 
					(
						this.obj.transform.GetComponent<Transform> ().position.x, 
						this.obj.transform.GetComponent<Transform> ().position.y
					);
			}
			set {
				this.obj.transform.GetComponent<Transform> ().position = 
					new Vector3 
					(
						value.x, 
						value.y, 
						this.obj.transform.GetComponent<Transform>().position.z
					);
			}
		}

		public Vector3 V3Position {
			get {
				return obj.GetComponent<Transform> ().position;
			}

			set {
				this.obj.GetComponent<Transform> ().position = new Vector3 (value.x, value.y, value.z);
			}
		}

		public int intMyProperty {
			get;
			set;
		}

		public Vector3 Scale {
			get {
				return this.obj.GetComponent<Transform> ().localScale;
			}
			set {
				this.obj.GetComponent<Transform> ().localScale = 
					new Vector3 (value.x, value.y, value.z);
			}
		}

		public Sprite sprite
		{
			set {
				this.obj.GetComponent<SpriteRenderer> ().sprite = value;
			}
		}

		public float rotation
		{
			set{
				this.obj.GetComponent<Transform> ().Rotate (
					new Vector3 (
						this.obj.GetComponent<Transform> ().rotation.x,
						this.obj.GetComponent<Transform> ().rotation.y,
						this.obj.GetComponent<Transform> ().rotation.z + value
					)
				);
			}
		}

		public Vector3 Rotation
		{
			set {
				this.obj.GetComponent<Transform> ().Rotate (
					new Vector3 (
						this.obj.GetComponent<Transform> ().rotation.x + value.x,
						this.obj.GetComponent<Transform> ().rotation.y + value.y,
						this.obj.GetComponent<Transform> ().rotation.z + value.z
					)
				);
			}
		}


		public void remake()
		{
			GameObject tmp = MonoBehaviour.Instantiate (this.obj);
			MonoBehaviour.Destroy (this.obj);
			this.obj = tmp;
		}

		public virtual void Destroy()
		{
			MonoBehaviour.Destroy (this.obj);
		}
	}
}

