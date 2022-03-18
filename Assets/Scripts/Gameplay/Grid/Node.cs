using System.Collections;
using UnityEngine;
using ClumsyWizard.Grid;

public class Node : IGridObject
{
	public Vector3 WorldPosition { get; set; }
	public int GridX { get; set; }
	public int GridY { get; set; }
	public bool Empty { get; set; }

	public Node()
	{
		Empty = true;
	}
}
