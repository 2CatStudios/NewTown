using UnityEngine;
using System.Collections;
//Written by Gibson Bethke
public class AIWander : MonoBehaviour
{

public int Speed;
public int mapSize;
	
int waitTime;
public int minWaitTime;
public int maxWaitTime = 10;
	
public bool canMove = true;
	
Vector3 wayPoint;
public GameObject child;
public Transform raystart;
public LayerMask layers;
bool chatting = false;
	
public GameObject player;
	
	void Start()
	{
		
		player = GameObject.FindWithTag ("Player");
		wayPoint = transform.localPosition + new Vector3(0, 0, -3);
	}

	void Update()
	{	
		
		Debug.DrawLine(raystart.position, wayPoint, Color.white);
		if(canMove == true)
		{
			transform.position += transform.TransformDirection(Vector3.forward)*Speed*Time.deltaTime;
			if((transform.position - wayPoint).magnitude <= 0.1F)
			{
				StartCoroutine(Delay());
				child.animation.CrossFade("Wait", 0.3F);
				canMove = false;
			}
		}
	}

	void Wander()
	{	
		
		RaycastHit hit;
		wayPoint = Random.insideUnitSphere*mapSize;
		wayPoint.y = 0;
		if (Physics.Linecast(raystart.position, wayPoint, out hit, layers))
		{
//    		print("Finding a new path, " + hit.collider.name + " is in our way");
			canMove = false;
			Wander();
		} else {
			canMove = true;
			transform.LookAt(wayPoint);
		}
	}
	
	IEnumerator Delay()
	{
		
		waitTime = Random.Range(minWaitTime, maxWaitTime);
//		print("Waiting for: " + waitTime + " seconds");
		yield return new WaitForSeconds(waitTime);
		if(chatting == false)
		{
			Wander ();
			child.animation.CrossFade("Walk", 0.2F);
			canMove = true;
		}
	}
	
	void OnDrawGizmos ()
	{
		
		Gizmos.color = new Color(0.2F, 0.1F, 1, 0.25F);
		Gizmos.DrawCube(wayPoint, new Vector3(1, 1, 1));
	}
	
	void StopToChat ()
	{
		
		chatting = true;
		canMove = false;
		child.animation.CrossFade("Wait");
		transform.LookAt(player.transform);
	}
	
	void DoneChatting ()
	{
		
		chatting = false;
		canMove = true;
		Wander ();
		child.animation.CrossFade("Walk");
		Wander();
	}
}