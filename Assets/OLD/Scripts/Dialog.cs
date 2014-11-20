using UnityEngine;
using System.Collections;
//Written by Gibson Bethke
public class Dialog : MonoBehaviour
{
	
	public GUISkin guiSkin;
	public GUIText dialogText;
	public GUITexture prompt;
	bool promptText = false;
	
	void Start ()
	{
	
		dialogText.text = "Well, it's good to see you finally\n made it! I was begining to think\n you wern't going to come back!";
	}
	
	void Update ()
	{
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			
			promptText = true;
			prompt.guiTexture.enabled = true;
			dialogText.text = "So, are you ready\n to enter town?";
		}
	}
	
	void OnGUI ()
	{
		
		GUI.skin = guiSkin;
		GUI.color = Color.black;
		if(promptText == true)
		{
			
			if (GUI.Button(new Rect(Screen.width - 450, Screen.height - 355, 200, 40), "Yeah"))
				Application.LoadLevel ("GameWorld");
			
			if (GUI.Button(new Rect(Screen.width - 450, Screen.height - 305, 200, 40), "No, sorry"))
				Application.Quit();
		}
	}
}
