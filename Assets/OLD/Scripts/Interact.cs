using System;
using UnityEngine;
using System.Collections;
//Written by Gibson Bethke
public class Interact : MonoBehaviour {
	
Manager manager;
Movement movement;

// --- -

GUITexture messageBoardUI;
string currentNpcName;	
Chat chatScript;
	
// --- -
	
GameObject currentNPC;
public Transform rayStart;

bool busy = false;
bool looking = false;
public bool chatting = false;
	
	void Awake ()
	{
		
		manager = GameObject.FindGameObjectWithTag ("Manager").GetComponent<Manager>();
		movement = gameObject.GetComponent<Movement>();
		if(GameObject.FindGameObjectWithTag ("MessageBoardUI") != null)
			messageBoardUI = GameObject.FindGameObjectWithTag ("MessageBoardUI").guiTexture;
		else
			messageBoardUI = null;
		
		if(GameObject.FindGameObjectWithTag ("Manager").GetComponent<Chat>() != null)
			chatScript = GameObject.FindGameObjectWithTag ("Manager").GetComponent<Chat>();
		else
			chatScript = null;
	}
	
	void Update ()
	{
		
		Vector3 fwd = transform.TransformDirection(Vector3.forward) * 2;		
		RaycastHit hit;
		Debug.DrawRay(rayStart.position, fwd, Color.red);
		
        if (Input.GetButtonUp ("Spacebar"))
		{
			
			if(busy == false)
			{

				if(Physics.Raycast(rayStart.position, fwd, out hit, 2))
				{
											
 			       	if(hit.collider.tag == "NPC")
					{
						
						movement.playerAnimations.Play ("Wait");
						currentNPC = hit.collider.gameObject;
						
						chatting = true;
						busy = true;
						
						currentNPC.SendMessage ("StopToChat");
						movement.canMove = false;
						
						currentNpcName = currentNPC.name.Substring (0, currentNPC.name.Length - 7);
						chatScript.SendMessage("FirstLine", currentNpcName);						
					}
					
					if(hit.collider.tag == "FenceGate")
					{
						
						looking = true;
						Application.LoadLevel ("PlayerHouse");
    				}
					
					if(hit.collider.tag == "MessageBoard")
					{
						
						busy = true;
						looking = true;
						messageBoardUI.enabled = true;
						movement.canMove = false;
						
					}
				
					if(hit.collider.tag == "Gyroid")
					{
						
						looking = true;
						manager.SendMessage("Save");
    				}
				}
				
			} else {
						
				if(chatting == true)
					
					chatScript.SendMessage ("NextLine");
				if(looking == true)
				{
					
					messageBoardUI.enabled = false;
					movement.canMove = true;
					looking = false;
					busy = false;
				}
			}
		}
	}
	
	void ResetChat()
	{
		
		busy = false;
		chatting = false;
		movement.canMove = true;
		currentNPC.SendMessage ("DoneChatting");
	}
}