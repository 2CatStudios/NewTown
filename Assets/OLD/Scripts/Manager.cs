using System;
using UnityEngine;
using System.Collections;
//Writen by Gibson Bethke
public class Manager : MonoBehaviour
{
	
public bool debugMode = false;
public int season;
public int mapSize;
DateTime lastPlayed;
System.TimeSpan differenceInDays;
public int currentTime;


	void Start ()
	{
		
		DontDestroyOnLoad (transform.gameObject);
		
		string lastPlayedTemp;
		lastPlayedTemp = PlayerPrefs.GetString("Last Played");
		if ( String.IsNullOrEmpty ( lastPlayedTemp ) == false )
			lastPlayed = Convert.ToDateTime(lastPlayedTemp);
		else
			lastPlayed = DateTime.Today;

		differenceInDays = DateTime.Today - lastPlayed;
		
		if(debugMode == false)
		{
			
			switch(DateTime.Today.Month)
			{
			
/*January*/		case 1:
/*February*/	case 2:
					season = 3;
	  				break;
/*March*/		case 3:
/*April*/		case 4:
/*May*/			case 5:
					season = 0;
	   				break;
/*June*/		case 6:
/*July*/		case 7:
/*August*/		case 8:
					season = 1;
					break;
/*Spetember*/	case 9:
/*October*/		case 10:
/*November*/	case 11:
					season = 2;
					break;
/*December*/	case 12:
					season = 3;
					break;
				default:
					Debug.LogError ("There is an error in the Switch Statement!");
					DebugLog ();
					break;
			}
		} else
		{
				
			DebugLog();
		}
	}
	
	
/*	void Update ()
	{
		
		currentTime = DateTime.Today.Minute;
		Debug.Log (currentTime);
	}
*/	
	
	void Save ()
	{
		
		lastPlayed = DateTime.Today;
		PlayerPrefs.SetString("Last Played", lastPlayed.ToString ());
		if(debugMode == true)
			Debug.Log ("Saved");
	}
	
	
	void OnDrawGizmosSelected()
	{
		
        Gizmos.color = new Color(0,0,0,.5F);
        Gizmos.DrawSphere(new Vector3(0, 0, 0), mapSize);
    }
	
	
	void DebugLog ()
	{
		
		Debug.Log ("Last played on: " + lastPlayed);
		Debug.Log ("Today is: " + DateTime.Today);
		Debug.Log ("The difference in days is: " + differenceInDays);
		Debug.Log ("This season is: " + season);
	}
}