using System.Collections;
using UnityEngine;
using ClumsyWizard.Grid;
using ClumsyWizard.Utilities;

public class GridManager : StaticInstance<GridManager>
{
	public enum GridGizmosMode
	{
		None,
		GridBounds,
		PlaceableGrid
	}

	private CustomGrid<Node> grid;
	[SerializeField] private Vector2Int numberOfCells;
	private Vector2 gridBounds;
	[SerializeField] private float cellRadius;
	[SerializeField] private LayerMask obstacleLayer;
	[SerializeField] private GameObject placeableTilePrefab;

	[Header("Grid Gizmos")]
	public GridGizmosMode GIZMOS_GizmosMode;
	public Color GIZMOS_PlaceableNode = Color.green;
	public float GIZMOS_GridSpacing;
	[Range(0.1f, 1.0f)]
	public float GIZMOS_NodeAlpha;

	private void Start()
	{
		CreateGrid();

		for (int x = 0; x < grid.GridX; x++)
		{
			for (int y = 0; y < grid.GridY; y++)
			{
				Node node = grid.GetValue(x, y);
				if (node != null)
					Instantiate(placeableTilePrefab, node.WorldPosition, placeableTilePrefab.transform.rotation, transform);
			}
		}
	}

	private void CreateGrid()
	{
		gridBounds = new Vector2(cellRadius * 2 * numberOfCells.x, cellRadius * 2 * numberOfCells.y);
		grid = new CustomGrid<Node>(gridBounds, cellRadius, transform);

		for (int x = 0; x < grid.GridX; x++)
		{
			for (int y = 0; y < grid.GridY; y++)
			{
				Node node = grid.GetValue(x, y);
				bool empty = !Physics.CheckSphere(node.WorldPosition, grid.CellDiameter * 0.5f, obstacleLayer);
				if (!empty)
					grid.SetValue(x, y, null);
			}
		}
	}

	public Node GetNodeFrom(Vector3 worldPosition)
	{
		return grid.GetValue(worldPosition);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		if (grid == null)
			CreateGrid();

		if (GIZMOS_GizmosMode == GridGizmosMode.None)
			return;

		if (GIZMOS_GizmosMode == GridGizmosMode.GridBounds)
		{
			Gizmos.color = Color.black;
			Gizmos.DrawWireCube(transform.position, new Vector3(gridBounds.x, 0.1f, gridBounds.y));
		}

		if (GIZMOS_GizmosMode == GridGizmosMode.PlaceableGrid)
		{
			for (int x = 0; x < grid.GridX; x++)
			{
				for (int y = 0; y < grid.GridY; y++)
				{
					Node node = grid.GetValue(x, y);
					if (node == null)
						continue;

					Gizmos.color = new Color(GIZMOS_PlaceableNode.r, GIZMOS_PlaceableNode.g, GIZMOS_PlaceableNode.b, GIZMOS_NodeAlpha);
					Gizmos.DrawCube(node.WorldPosition, new Vector3(grid.CellDiameter - GIZMOS_GridSpacing / 2.0f, 0.1f, grid.CellDiameter - GIZMOS_GridSpacing / 2.0f));
					Gizmos.color = Color.black;
					Gizmos.DrawWireCube(node.WorldPosition, new Vector3(grid.CellDiameter, 0.1f, grid.CellDiameter));
				}
			}
		}
	}

	private void OnValidate()
	{
		CreateGrid();
	}
}