using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Written by Michael Bethke
public class World
{		
	
	internal GameObject parent;
	internal WorldTile[,] worldTiles;
	
	
	internal void Initialize ( GameObject thisParent, int worldWidth, int worldDepth )
	{
		
		parent = thisParent;
		worldTiles = new WorldTile[worldWidth, worldDepth];

		int widthIndex = 0;
		int depthIndex = 0;

		while ( depthIndex < worldTiles.GetLength ( 1 ))
		{
			
			while ( depthIndex < worldDepth )
			{
				
				while ( widthIndex < worldWidth )
				{
					
					//UnityEngine.Debug.Log ( "Create " + widthIndex + " + " + depthIndex );
					
					WorldTile newTile = new WorldTile ();
					newTile.Initialize ( this, widthIndex, depthIndex );
					
					worldTiles[widthIndex, depthIndex] = newTile;
					
					widthIndex += 1;
				}
				
				widthIndex = 0;
				depthIndex += 1;
			}
		}
	}
}


class WorldTile
{
	
	bool initialized = false;
	GameObject plane;
	
	internal void Initialize ( World thisWorld, int x, int y )
	{
		
		GameObject newPlane = GameObject.CreatePrimitive ( PrimitiveType.Plane );
		newPlane.transform.position = new Vector3 ( x * 10, 1, y * 10 );
		newPlane.transform.parent = thisWorld.parent.transform;
		plane = newPlane;
		
	}
}


public class WorldManager : MonoBehaviour
{

	public int worldWidth = 6;
	public int worldDepth = 4;
	
	internal World world = new World ();

	void Start ()
	{
		
		GenerateTerrain ();
	}
	
	
	void GenerateTerrain ()
	{
		
		world.Initialize ( this.gameObject, worldWidth, worldDepth );
	}
}
