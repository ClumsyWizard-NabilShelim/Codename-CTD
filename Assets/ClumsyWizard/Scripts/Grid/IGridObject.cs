using UnityEngine;

namespace ClumsyWizard.Grid
{
	public interface IGridObject
	{
		public Vector3 WorldPosition { get; set; }
		public int GridX { get; set; }
		public int GridY { get; set; }
	}
}
