using UnityEngine;
using System.Collections;
//Written by Michael Bethke
public class UserInterface : MonoBehaviour
{
	
	WorldManager worldManager;
	
	GUIStyle styleEmpty;
	GUIStyle styleLabelCenterLarge;
	
	
	void Start ()
	{
		
		worldManager = GameObject.FindGameObjectWithTag ( "World" ).GetComponent<WorldManager> ();
		
		styleEmpty = new GUIStyle ();
		styleEmpty.fontSize = 48;
		styleEmpty.alignment = TextAnchor.MiddleLeft;
		
		styleLabelCenterLarge = new GUIStyle ();
		styleLabelCenterLarge.fontSize = 48;
		styleLabelCenterLarge.alignment = TextAnchor.MiddleCenter;
		//styleLabelCenterLarge.padding = new RectOffset ( 0, 0, 0, 0 );
		styleLabelCenterLarge.margin = new RectOffset ( 4, 4, 4, 4 );
	}


	void OnGUI ()
	{
		
		GUILayout.Window ( 0, new Rect ( 0, 0, Screen.width, Screen.height ), Window, "", styleEmpty );
	}
	
	
	void Window ( int windowID )
	{
		
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace ();
		
		GUILayout.BeginVertical ();
		//GUILayout.FlexibleSpace ();
		
		//GUILayout.Label ( "NewTown", styleLabelCenterLarge );
		
		GUILayout.FlexibleSpace ();
		
		if ( GUILayout.Button ( "Generate Terrain" ))
		{
			
			worldManager.GenerateTerrain ();
		}
		
		GUILayout.FlexibleSpace ();
		GUILayout.EndVertical ();
		
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();
	}
}
