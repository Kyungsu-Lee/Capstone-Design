using System;
using UnityEngine;

namespace StepObject
{
	public delegate void ACTION();
	public delegate void DIRECTIONACTION(Vector3 vec3);

	public enum DIRECTION
	{
		RU, RD, LU, LD
	}
}
