using System;
using System.IO;
using UnityEngine;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
//Written by Michael Bethke
public class ExternalInformation : MonoBehaviour
{
	
	InformationManager informationManager;

	string twoCatStudiosPath;
	internal string newTownPath;
	internal string supportPath;
	

	void Start ()
	{
		
		informationManager = /*GameObject.FindGameObjectWithTag ( "InformationManager" )*/gameObject.GetComponent<InformationManager> ();
	
		if ( Environment.OSVersion.ToString ().Substring ( 0, 4 ) == "Unix" )
		{

			twoCatStudiosPath = Path.DirectorySeparatorChar + "Users" + Path.DirectorySeparatorChar  + Environment.UserName + Path.DirectorySeparatorChar + "Library" + Path.DirectorySeparatorChar  + "Application Support" + Path.DirectorySeparatorChar + "2Cat Studios" + Path.DirectorySeparatorChar;
		} else
		{

			twoCatStudiosPath = Environment.GetFolderPath ( Environment.SpecialFolder.CommonApplicationData ) + Path.DirectorySeparatorChar  + "2Cat Studios" + Path.DirectorySeparatorChar;
		}
		
		newTownPath = twoCatStudiosPath + "NewTown" + Path.DirectorySeparatorChar;
		supportPath = newTownPath + Path.DirectorySeparatorChar + "Support" + Path.DirectorySeparatorChar;
		
		if ( Directory.Exists ( supportPath ) == false )
		{
			
			Directory.CreateDirectory ( supportPath );
		}
		
		
/*		if ( ReadPreferences () == true )
		{
		
			if ( WritePreferences () == true )
			{
				
				UnityEngine.Debug.Log ( "Preferences Loaded Successfully" );
			} else {
				
				UnityEngine.Debug.Log ( "Unable to Write Preferences, Read Successful" );
			}
		} else {
			
			if ( WritePreferences () != true )
			{
				
				UnityEngine.Debug.LogError ( "Unable to Write Preferences, Read Fail" );
			}
		}*/
		
		
		if ( ReadPreferences () != true )
		{
			
			if ( WritePreferences () != true )
			{
				
				UnityEngine.Debug.Log ( "Unable to Write Preferences!" );
			} else {
				
				if ( ReadPreferences () != true )
				{
					
					UnityEngine.Debug.LogError ( "Unable to R+WR Preferences" );
				} else { UnityEngine.Debug.Log ( "New Preferences File Created" ); }
			}
		} else { UnityEngine.Debug.Log ( "Read Preferences Successfully" ); }
	}
	
	
	bool ReadPreferences ()
	{
		
		try {
			
			StreamReader prefsReader = new System.IO.StreamReader ( supportPath + "Preferences.xml" );
			string prefsXML = prefsReader.ReadToEnd();
			prefsReader.Close();
			
			informationManager.preferences = prefsXML.DeserializeXml<Preferences> ();
		} catch ( Exception error )
		{
			
			UnityEngine.Debug.LogWarning ( "Unable to Read Preferences, " + error );
			return false;
		}
		return true;
	}
	
	
	bool WritePreferences ()
	{
		
		try {
			
			XmlSerializer prefsSerializer = new XmlSerializer ( informationManager.preferences.GetType ());
			StreamWriter prefsWriter = new StreamWriter ( supportPath + "Preferences.xml" );
			prefsSerializer.Serialize ( prefsWriter.BaseStream, informationManager.preferences );
			prefsWriter.Close ();
		} catch ( Exception error ) {
			
			UnityEngine.Debug.LogError ( "Unable to Write Preferences, " + error );
			return false;
		}
		return true;
	}
}


public static class XMLSupport 
{
	
	public static T DeserializeXml<T> (this string xml) where T : class
	{
		
		if( xml != null )
		{
			
			var s = new XmlSerializer ( typeof ( T ) );
			using ( var m = new MemoryStream ( Encoding.UTF8.GetBytes ( xml )))
			{
				
				return ( T ) s.Deserialize ( m );
			}
		}
	
		UnityEngine.Debug.LogError ( "A wild error has apperaed!" );
		return null;
	}
}