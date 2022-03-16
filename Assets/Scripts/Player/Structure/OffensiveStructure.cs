using System.Collections;
using UnityEngine;

public class OffensiveStructure : Structure
{
	[SerializeField] private Transform rotateableMesh;
	[SerializeField] private float lookSpeed = 10.0f;

	protected override void RotateTowardsTarget()
	{
		Vector3 directionToTarget = attackTarget.position - rotateableMesh.position;
		Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
		Quaternion targetRotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
		rotateableMesh.rotation = Quaternion.Slerp(rotateableMesh.rotation, targetRotation, lookSpeed * Time.deltaTime);
	}
}