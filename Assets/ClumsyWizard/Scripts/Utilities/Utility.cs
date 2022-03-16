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

		public static Vector3 GetMouseWorldPosition()
		{
			Vector3 mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);
			return mousePos;
		}

		public static Ray ShootRayFromMouse()
		{
			if(Camera.orthographic)
			{
				Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				return new Ray(mousePos, Camera.transform.forward * 1000.0f);
			}

			Debug.LogWarning("Ray shooting for perpective camera not implemented!");
			return new Ray();
		}
	}
}