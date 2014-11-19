using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Written by Michael Bethke
public class ErrorLog : MonoBehaviour
{
	
	ErrorUI userInterface;
	
	public bool startupOnlyWriteToScreen = true;
	public bool startupOnlyWriteToDisk = true;
	
	bool debugLogActive = false;
	bool writeLogToScreen = false;
	bool writeLogToDisk = false;
	public string writePath = null;
	
	internal List<String> log = new List<String> ();
	

	void Start ()
	{
		
		writeLogToScreen = startupOnlyWriteToScreen;
		writeLogToDisk = startupOnlyWriteToDisk;
	
		if ( writeLogToScreen == true || writeLogToDisk == true )
		{
			
			debugLogActive = true;
		} else {
			
			debugLogActive = false;
		}
	
		if ( debugLogActive == true )
		{
			
			if ( writeLogToScreen == true )
			{
				
				userInterface = gameObject.GetComponent<ErrorUI>();
				userInterface.enabled = true;
			} else {
				
				userInterface.enabled = false;
			}
			
			log.Add ( "Error Log Active" );
			
			if ( writeLogToDisk == true )
			{
				
				string writingWarning = "Writing to disk";
				
				if ( String.IsNullOrEmpty ( writePath.Trim ()) == true )
				{
					
					writingWarning = writingWarning + ", no destination given! Writing to default directory, Desktop.";
					writePath = Environment.GetFolderPath ( Environment.SpecialFolder.Desktop ) + Path.DirectorySeparatorChar + "ErrorLog.txt";
				}
				
				log.Add ( writingWarning );
			}
		} else {
			
			UnityEngine.Debug.LogWarning ( "ErrorLog disabled." );
		}
	}
	
	
	public void Post ( string logString )
	{
		
		if ( AddMessage ( logString + " (Manual)\n" ) == false )
		{
			
			UnityEngine.Debug.LogError ( "Unable to AddMessage from Post, " + logString );
		}
	}
	
	
	void OnEnable ()
	{
		
		Application.RegisterLogCallback ( HandleLog );
		UnityEngine.Debug.Log ( "Enabled" );
	}
	
	
	void OnDisable ()
	{
		
		Application.RegisterLogCallback ( null );
		UnityEngine.Debug.Log ( "Disabled" );
	}
	
	
	void HandleLog ( string logString, string stackTrace, LogType type )
	{
		
		UnityEngine.Debug.Log ( "MESSAGE" );
		
		if ( AddMessage ( logString + " (" + type + ") [" + stackTrace + "]\n" ) == false )
		{
			
			UnityEngine.Debug.LogError ( "Unable to AddMessage from CallBack, " + logString );
		}
	}
	
	
	bool AddMessage ( string messageToAdd )
	{
		
		try
		{
			
			messageToAdd = DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + " - " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "." + DateTime.Now.Millisecond + " || " + messageToAdd;
			
			using ( StreamWriter streamWriter = File.AppendText ( writePath )) 
			{
				
				streamWriter.WriteLine ( messageToAdd );
			}
			
			log.Add ( messageToAdd );
			userInterface.debugScrollPosition.y = Mathf.Infinity;	
		} catch ( Exception e ) {
			
			UnityEngine.Debug.LogError ( e );
			return false;
		}
		
		return true;
	}
}