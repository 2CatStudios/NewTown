using UnityEngine;
using System.Collections;
//Written by Michael Bethke
public class ErrorUI : MonoBehaviour
{

	ErrorLog errorLog;
	internal Vector2 debugScrollPosition = new Vector2 ( 0, 0 );
	
	
	void Start ()
	{
		
		errorLog = gameObject.GetComponent<ErrorLog>();
	}
	

	void OnGUI ()
	{
			
		GUILayout.BeginArea ( new Rect ( 0, 0, Screen.width, Screen.height ));
		debugScrollPosition = GUILayout.BeginScrollView ( debugScrollPosition, false, false, GUILayout.Width ( Screen.width ), GUILayout.Height ( Screen.height ));
		GUILayout.FlexibleSpace ();
			
		for ( int index = 0; index < errorLog.log.Count; index += 1 )
		{
				
			GUILayout.Label ( errorLog.log[index] );
		}
				
		GUILayout.EndScrollView ();
		GUILayout.EndArea ();
	}
}
