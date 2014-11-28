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
	internal string savedGamesPath;
	

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
		savedGamesPath = newTownPath + Path.DirectorySeparatorChar + "Saved Games" + Path.DirectorySeparatorChar;

#region support
		if ( Directory.Exists ( supportPath ) == false )
		{
			
			Directory.CreateDirectory ( supportPath );
		}
		
		if ( ReadPreferences () != true )
		{
			
			if ( WritePreferences ( true ) != true )
			{
				
				UnityEngine.Debug.Log ( "Unable to Write Preferences!" );
			} else {
				
				if ( ReadPreferences () != true )
				{
					
					UnityEngine.Debug.LogError ( "Unable to R+WR Preferences" );
				} else { UnityEngine.Debug.Log ( "New Preferences File Created" ); }
			}
		}// else { UnityEngine.Debug.Log ( "Read Preferences Successfully" ); }
#endregion
#region savedGames
		if ( Directory.Exists ( savedGamesPath ) == false )
		{
			
			Directory.CreateDirectory ( savedGamesPath );
		}
		
		if ( ReadSavedGames () != true )
		{
			
			if ( WriteSavedGames ( true ) != true )
			{
				
				UnityEngine.Debug.Log ( "Unable to Write SavedGames!" );
			} else {
				
				if ( ReadSavedGames () != true )
				{
					
					UnityEngine.Debug.LogError ( "Unable to R+WR SavedGames" );
				} else { UnityEngine.Debug.Log ( "New SavedGames File Created" ); }
			}
		}// else { UnityEngine.Debug.Log ( "Read SavedGames Successfully" ); }
#endregion
		
	}
	
	
	bool ReadPreferences ()
	{
		
		try {
			
			StreamReader prefsReader = new System.IO.StreamReader ( supportPath + "Preferences.xml" );
			string prefsXML = prefsReader.ReadToEnd();
			prefsReader.Close();
			
			informationManager.preferences = prefsXML.DeserializeXml<Preferences> ();
			
			if ( informationManager.PrefsUpToDate () != true )
			{
				
				return false;
			}
		} catch ( Exception error )
		{
			
			UnityEngine.Debug.LogWarning ( "Unable to Read Preferences, " + error );
			return false;
		}
		return true;
	}
	
	
	bool WritePreferences ( bool rewrite = false )
	{
		
		if ( rewrite == true )
		{
			
			UnityEngine.Debug.Log ( "Rewriting Preferences" );
			if ( informationManager.NewPreferences () != true )
			{
				
				return false;
			}
		}
		
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


	bool ReadSavedGames ()
	{
	
		try {
			
			StreamReader savesReader = new System.IO.StreamReader ( savedGamesPath + "SavedGames.xml" );
			string savesXML = savesReader.ReadToEnd();
			savesReader.Close();
			
			informationManager.savedGames = savesXML.DeserializeXml<SavedGames> ();
		} catch ( Exception error )
		{
			
			UnityEngine.Debug.LogWarning ( "Unable to Read SavedGames, " + error );
			return false;
		}
		return true;
	}
	
	
	bool WriteSavedGames ( bool rewrite = false )
	{
		
		if ( rewrite == true )
		{
			
			UnityEngine.Debug.Log ( "Rewriting SavedGames" );
			if ( informationManager.NewSavedGames () != true )
			{
				
				return false;
			}
		}
		
		try {
			
			XmlSerializer savedGamesSerializer = new XmlSerializer ( informationManager.savedGames.GetType ());
			StreamWriter savedGamesWriter = new StreamWriter ( savedGamesPath + "SavedGames.xml" );
			savedGamesSerializer.Serialize ( savedGamesWriter.BaseStream, informationManager.savedGames );
			savedGamesWriter.Close ();
		} catch ( Exception error ) {
			
			UnityEngine.Debug.LogError ( "Unable to Write SavedGames, " + error );
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