using System.Xml;
using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
//Written by Michael Bethke
[XmlRoot ( "Preferences" )]
public class Preferences
{

	[XmlElement ( "Version" )]
	public string version;
}


public class InformationManager : MonoBehaviour
{
	
	internal Preferences preferences = new Preferences ();


	void Start ()
	{
	
		
	}
}
