using System.Collections;
using UnityEngine;

namespace ClumsyWizard.Utilities
{
	public static class Utility
	{
		private static Camera cam;

		private static Camera Camera
		{
			get
			{
				if (cam == null)
					cam = Camera.main;

				return cam;
			}
		}

		public static Vector3 GetMouseWorldPosition(bool setYToZero)
		{
			Vector3 mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);

			if(setYToZero)
				mousePos.y = 0.0f;

			return mousePos;
		}

		public static Ray ShootRayFromMouse()
		{
			return Camera.ScreenPointToRay(Input.mousePosition);
		}
	}
}