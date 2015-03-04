using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Written by Michael Bethke
public class World
{		
	
	internal GameObject parent;
	internal WorldTile[,] worldTiles;
	internal int season;
	
	public static World world = new World ();
	
	internal void Initialize ( GameObject thisParent, int worldWidth, int worldHeight )
	{
		
		parent = thisParent;
		worldTiles = new WorldTile[worldWidth, worldHeight];
		season = GameObject.FindGameObjectWithTag ( "Manager" ).GetComponent<TimeManager>().currentSeason ();
		Color[,] heightmap = GeneratePerlinNoise ( 1.0f, 1.0f, worldWidth, worldHeight );

		int widthIndex = 0;
		int heightIndex = 0;

		while ( heightIndex < worldTiles.GetLength ( 1 ))
		{
			
			while ( heightIndex < worldHeight )
			{
				
				while ( widthIndex < worldWidth )
				{
					
					float localHeight = 0;
					if ( heightmap[ widthIndex, heightIndex ].grayscale > 0.5f )
					{
						
						localHeight = 5;
					}
					
					WorldTile newTile = new WorldTile ();
					newTile.Initialize ( this, widthIndex, localHeight, heightIndex);
					
					worldTiles[widthIndex, heightIndex] = newTile;
					
					widthIndex += 1;
				}
				
				widthIndex = 0;
				heightIndex += 1;
			}
		}
	}


	private Color[,] GeneratePerlinNoise ( float xSeed, float ySeed, int perlinWidth, int perlinHeight )
	{
		
		Color[,] pixels = new Color [ perlinWidth, perlinHeight ];
		
		float scale = 2.0f;

		float y = 0;
		while ( y < perlinHeight )
		{
			
			float x = 0;
			while ( x < perlinWidth )
			{
				
		    	float xCoordinate = xSeed + x / perlinWidth * scale;
		    	float yCoordinate = ySeed + y / perlinHeight * scale;
		    	float sample = Mathf.PerlinNoise ( xCoordinate, yCoordinate );
				
				pixels [Convert.ToInt32 ( x ), Convert.ToInt32 ( y )] = new Color ( sample, sample, sample, 1 );
				
				x += 1;
			}
			
			y += 1;
		}
		
		return pixels;
	}
}


class WorldTile
{
	
	//bool initialized = false;
	//GameObject tileObject;
	
	internal void Initialize ( World thisWorld, int x, float y, int z )
	{
		
		GameObject newPlane = GameObject.CreatePrimitive ( PrimitiveType.Plane );
		newPlane.transform.position = new Vector3 ( x * 10, y, z * 10 );
		newPlane.transform.parent = thisWorld.parent.transform;
		newPlane.renderer.material = thisWorld.parent.GetComponent<WorldManager>().seasons[thisWorld.season];
		//tileObject = newPlane;
	}
}


public class WorldManager : MonoBehaviour
{
	
	public int worldWidth = 4;
	public int worldDepth = 4;
	
	public Material[] seasons = new Material[4];
	
	
	internal void GenerateTerrain ()
	{
		
		World.world.Initialize ( this.gameObject, worldWidth, worldDepth );
	}
}
