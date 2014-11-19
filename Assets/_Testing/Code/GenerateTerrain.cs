using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Written by Michael Bethke
public class World
{
	
	int mapSize;
	List<WorldTile> worldTiles = new List<WorldTile> ();
	
	public int Length
	{
		
		get { return worldTiles.Count; }
	}
}


class WorldTile
{
	
	
}


public class GenerateTerrain : MonoBehaviour
{
	
	int squareMapSize = 10;

	internal World world = new World ();

	void Start ()
	{
		
		bool worldCreated = GenerateWorld ();
		
		if ( worldCreated == true )
		{
			
			UnityEngine.Debug.Log ( "No errors in world creation." );
		} else {
			
			UnityEngine.Debug.Log ( "Unable to create world!" );
		}
	}
	
	
	bool GenerateWorld ()
	{
		
		try
		{
			
			int value = 1 / int.Parse("0");
			UnityEngine.Debug.Log ( "Something went wrong at line 56." );
		} catch ( Exception e )
		{
			
			UnityEngine.Debug.LogError ( e );
			return false;
		}
		
		return true;
	}
}
