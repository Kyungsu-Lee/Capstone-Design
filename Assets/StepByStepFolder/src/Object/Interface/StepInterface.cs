using UnityEngine;
using System.Collections;

namespace StepObject
{
	public abstract class StepInterface : MapObject
	{
		ACTION ShowAction;
		ACTION DeleteAction;


		protected StepInterface()
		{
		}

		protected StepInterface(GameObject obj)
		{
			this.obj = obj;
			objects.Add (this);

			this.ShowAction += showActionMethod;
			this.DeleteAction += deleteActionMethod;
		}

		protected abstract void showActionMethod();
		protected abstract void deleteActionMethod();

		public void showActionUpdate()
		{
			ShowAction ();
		}

		public void deleteActionUpdate()
		{
			DeleteAction ();
		}

		private void showActionTrue()
		{
			this.obj.GetComponent<StepAction> ().stepShowAction = true;
		}

		private void showActionFalse()
		{
			this.obj.GetComponent<StepAction> ().stepShowAction = false;
		}

		private void deleteActionTrue()
		{
			this.obj.GetComponent<StepAction> ().stepDeleteAction = true;
		}

		private void deleteActionFalse()
		{
			this.obj.GetComponent<StepAction> ().stepDeleteAction = false;
		}

		public void show()
		{
			showActionTrue ();
		}

		public void delete()
		{
			showActionTrue ();
		}

		public void stop()
		{
			deleteActionFalse ();
			showActionFalse ();
		}
	}
}
