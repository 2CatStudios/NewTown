using System.Xml;
using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
//Written by Michael Bethke
[XmlRoot ( "Preferences" )]
public class Preferences
{

	[XmlElement ( "LastPlayerID" )]
	public string lastPlayerID;
}


[XmlRoot ( "SavedGame" )]
public class SavedGame
{
	
	[XmlElement ( "PlayerID" )]
	public string playerID;
	
	[XmlElement ( "LastSeen" )]
	public string lastSeen;
	
	[XmlElement ( "DateModifier" )]
	public string dateModifier;
}


public class InformationManager : MonoBehaviour
{
	
	internal Preferences preferences = new Preferences ();


	void Start ()
	{
	
		
	}
}
