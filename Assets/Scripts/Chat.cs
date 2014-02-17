using UnityEngine;
using System.Collections;
//Written by Gibson Bethke
public class Chat : MonoBehaviour
{
	
public Interact interact;
public Manager manager;
public Movement movement;
bool showPrompt = false;

public GUISkin guiSkin;
public GUIText chatText;
public GUIText npcName;
public GUITexture promptTexture;
public GUITexture chatBox;
		
	void OnGUI ()
	{
		
		GUI.skin = guiSkin;
		GUI.color = Color.black;
		if(showPrompt == true)
		{
			
			if (GUI.Button(new Rect(Screen.width - 550, Screen.height - 380, 400, 40), "Need Any Help"))
			{
				
				if(manager.debugMode == true)
					Debug.Log ("Great, me too");
			}
			
			if (GUI.Button(new Rect(Screen.width - 550, Screen.height - 330, 400, 40), "Care to Chat"))
			{
				
				if(manager.debugMode == true)
					Debug.Log ("Oh, and what do you call this?!");
			}
			
			if (GUI.Button(new Rect(Screen.width - 550, Screen.height - 280, 400, 40), "Never Mind"))
			{
				
				StopChat ();
				if(manager.debugMode == true)
					Debug.Log ("Fine!");
			}
		}
	}
	
	void FirstLine (string tempNPCName)
	{
		
		npcName.text = tempNPCName;
		chatBox.enabled = true;
		chatText.text = "Woah, buddy, it's been a really long time... \n I've missed you!";
	}
	
	void StopChat ()
	{
		
		npcName.text = null;
		showPrompt = false;
		chatBox.enabled = false;
		chatText.text = null;
		promptTexture.enabled = false;
		movement.SendMessage("ResetChat");
		interact.chatting = false;
		
	}
	
	void NextLine ()
	{
		
		showPrompt = true;
		promptTexture.enabled = true;
		chatText.text = "Anyway, what do you want?";
	}
}