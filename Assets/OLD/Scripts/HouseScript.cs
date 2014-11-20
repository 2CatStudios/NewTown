using UnityEngine;
using System.Collections;
//Written by Gibson Bethke
public class HouseScript : MonoBehaviour {
public bool inUse;
public GameObject npc;


	void Start ()
	{
		
		if(inUse == true)
		{
			
			SpawnAnimal();
		}
	}
	
	void SpawnAnimal ()
	{
		
		Instantiate(npc, transform.localPosition + new Vector3(-8, 0, -10), transform.rotation);
	}
}