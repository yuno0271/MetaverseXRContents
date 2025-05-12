﻿using UnityEngine;

public class RotateEffect : MonoBehaviour
{
    [SerializeField]
    private float   rotateSpeed = 100;
	[SerializeField]
	private	bool	isPlay = true;

	private void Update()
	{
		if ( !isPlay ) return;

		transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
	}
}

