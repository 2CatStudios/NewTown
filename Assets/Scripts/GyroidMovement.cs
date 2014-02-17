using UnityEngine;
using System.Collections;
//Written by Gibson Bethke
public class GyroidMovement : MonoBehaviour {
public Transform target;
Vector3 player;

	void Update ()
	{
		player = target.position;
		player.y = transform.position.y;
		transform.LookAt(player);
	}
}