using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClumsyWizard.Grid
{
	public class CustomGrid<TGridObject> where TGridObject : IGridObject, new()
	{
		private Vector2 gridBounds;
		public int GridX { get; private set; }
		public int GridY { get; private set; }

		private float cellRadius;
		private float cellDiameter;
		public float CellDiameter
		{
			get
			{
				return cellDiameter;
			}
		}

		public Vector3 worldBottom;
		private Transform centre;
		private TGridObject[,] grid;

		public CustomGrid(Vector2 gridBounds, float cellRadius, Transform centre)
		{
			if(cellRadius == 0 || gridBounds == Vector2.zero || centre == null)
			{
				Debug.LogWarning("Wrong or zero value entered!");
				return;
			}

			this.gridBounds = gridBounds;
			this.cellRadius = cellRadius;
			cellDiameter = cellRadius * 2.0f;

			GridX = Mathf.RoundToInt(gridBounds.x / cellDiameter);
			GridY = Mathf.RoundToInt(gridBounds.y / cellDiameter);

			this.centre = centre;

			grid = new TGridObject[GridX, GridY];

			worldBottom = centre.position - (centre.right * (gridBounds.x / 2.0f)) - (centre.forward * (gridBounds.y / 2.0f));

			for (int x = 0; x < grid.GetLength(0); x++)
			{
				for (int y = 0; y < grid.GetLength(1); y++)
				{
					TGridObject gridObject = new TGridObject();
					gridObject.WorldPosition = worldBottom + (centre.right * (x * cellDiameter + cellRadius)) + (centre.forward * (y * cellDiameter + cellRadius));
					gridObject.GridX = x;
					gridObject.GridY = y;
					grid[x, y] = gridObject;
				}
			}
		}

		private void GetXY(Vector3 worldPosition, out int x, out int y)
		{
			x = Mathf.FloorToInt((worldPosition.x + Mathf.Abs(worldBottom.x)) / cellDiameter);
			y = Mathf.FloorToInt((worldPosition.z + Mathf.Abs(worldBottom.z)) / cellDiameter);
		}

		public void SetValue(int x, int y, TGridObject value)
		{
			if (x < 0 || x > GridX || y < 0 || y > GridY)
				return;

			grid[x, y] = value;
		}

		public TGridObject GetValue(int x, int y)
		{
			if (x < 0 || x >= GridX || y < 0 || y >= GridY)
				return default(TGridObject);

			return grid[x, y];
		}

		public TGridObject GetValue(Vector3 worldPosition)
		{
			int x, y;
			GetXY(worldPosition, out x, out y);
			return GetValue(x, y);
		}
	}
}
