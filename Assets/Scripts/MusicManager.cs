using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Written by Gibson Bethke
public class MusicManager : MonoBehaviour
{

AudioSource audiosource;
public AudioClip[] music;
public Vector3[] musicZones;
public AudioClip musicQueue;
	
public int daytimeMusic = 4;
public int nighttimeMusic = 3;

	
	void Start ()
	{
	
		audiosource = transform.GetComponent<AudioSource>();
		int i = 0;
		
		while (i < musicZones.Length)
		{
			
			GameObject musicZone = new GameObject();
			musicZone.AddComponent("SphereCollider");
			musicZone.AddComponent("MusicZoneScript");
			musicZone.GetComponent<MusicZoneScript>().musicClip = music[i];
			musicZone.name = "MusicZone" + i;
			
			musicZone.transform.GetComponent<SphereCollider>().radius = 10;
			musicZone.transform.position = musicZones[i];
			musicZone.transform.GetComponent<SphereCollider>().isTrigger = true;
			
			i++;
			
		}
	}
	
	void Update ()
	{
		
		if(audiosource.clip != musicQueue)
		{
		
			audiosource.loop = false;
			if(audiosource.isPlaying == false)
			{
			
				audiosource.clip = musicQueue;
				audiosource.Play ();
			}
		} else {
			audiosource.loop = true;
		}
	}
}
