using System;
using UnityEngine;
using System.Collections;
//Written for Gibson Bethke
//[RequireComponent (typeof (Material))]
public class AnimateWater : MonoBehaviour
{
	
	public Texture2D[] images;
	public float frameDelay;
	public Material material;
	int i;
	
	void Start () 
	{
		
		StartCoroutine ("Go");
	}
	
	IEnumerator Go ()
	{
	
		while (true)
		{
			
			material.mainTexture = images[i];
			i++;
			if(i == images.Length)
				i = 0;
        	yield return new WaitForSeconds (frameDelay);
		}
    }
}
