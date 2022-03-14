using System.Collections;
using UnityEngine;

namespace ClumsyWizard.Maths
{
	public static class MathCW
	{
		public static Vector3 BisectorOf(Vector3 a, Vector3 b)
		{
			return (a.normalized + b.normalized).normalized;
		}
	}
}