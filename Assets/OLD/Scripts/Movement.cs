using UnityEngine;
using System.Collections;
//Written by Gibson Bethke
public class Movement : MonoBehaviour
{	
	
public float walkSpeed;
public float runSpeed;
internal Animation playerAnimations;
public bool canMove = true;
bool idle;
	
	void Start ()
	{
		
//		DontDestroyOnLoad (transform.gameObject);
		playerAnimations = gameObject.GetComponentInChildren<Animation>();
	}
	
    void Update () 
	{
		
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        float hitdist = 0.0f;
        if (playerPlane.Raycast (ray, out hitdist) && canMove == true) 
        {
			
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, walkSpeed * Time.time);	
        }
		
		if(canMove == true)
		{
			
			if(Input.GetButtonDown("Spacebar") && idle == true)
			{
				
				playerAnimations.Play ("Activate");
			}

			if(Input.GetButton("RMouse"))
			{
				
				idle = false;
				playerAnimations.Play ("Run");
				transform.Translate(Vector3.forward * runSpeed);
			} else {
				
				idle = true;
				playerAnimations.CrossFade("Wait", 0.2F);
				
			if(Input.GetButton("LMouse"))
			{
				
				idle = false;
				playerAnimations.Play ("Walk");
				transform.Translate(Vector3.forward * walkSpeed);
			} else {
				
				idle = true;
				playerAnimations.CrossFade("Wait", 0.2F);
				}
			}
		}
    }
}