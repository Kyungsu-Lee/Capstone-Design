using System;
using UnityEngine;

namespace MathU
{
	public class Line
	{
		public Vector3 directionVector;
		private Vector3 startVector;
		private float k;

		private Line ()
		{
			
		}

		public Line(Vector3 v1, Vector3 v2)
		{
			directionVector = (v2 - v1) / (float)Math.Sqrt (Vector3.Dot(v1-v2, v1-v2));
			startVector = v1;
		}

		public float fy(float x)
		{
			if(directionVector.x == 0)
				throw new Exception();

			k = (x - startVector.x) / directionVector.x;

			return (k * directionVector.y + startVector.y);
			
		}
	}
}

