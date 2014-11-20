using UnityEngine;
using System.Collections;
//Written by Gibson Bethke
public class MusicZoneScript : MonoBehaviour
{
	
public AudioClip musicClip;
public MusicManager musicManager;
	
	void Start ()
	{
		
		musicManager = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<MusicManager>();
	}
	
	void OnTriggerEnter ()
	{
	
		musicManager.musicQueue = musicClip;
		Debug.Log ("Queued: " + musicClip.name);
	}
}
