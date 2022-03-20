using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector2 input;
	[SerializeField] private Vector2 horizontalLimit;
	[SerializeField] private Vector2 verticalLimit;
	private Vector3 target;

	private void Start()
	{
		target = transform.position;
	}

	private void Update()
	{
		input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		target += new Vector3(input.x * moveSpeed * Time.deltaTime, 0, input.y * moveSpeed * Time.deltaTime);

		target.x = Mathf.Clamp(target.x, horizontalLimit.x, horizontalLimit.y);
		target.z = Mathf.Clamp(target.z, verticalLimit.x, verticalLimit.y);

		transform.position = target;
	}
}
