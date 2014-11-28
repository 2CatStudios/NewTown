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
	
	
	internal void Initialize ( GameObject thisParent, int worldWidth, int worldDepth )
	{
		
		parent = thisParent;
		worldTiles = new WorldTile[worldWidth, worldDepth];
		season = GameObject.FindGameObjectWithTag ( "Manager" ).GetComponent<TimeManager>().currentSeason ();
		Texture2D heightmap = CalcNoise ( worldWidth, worldDepth );

		int widthIndex = 0;
		int depthIndex = 0;

		while ( depthIndex < worldTiles.GetLength ( 1 ))
		{
			
			while ( depthIndex < worldDepth )
			{
				
				while ( widthIndex < worldWidth )
				{
					
					float heightIndex = heightmap.GetPixel( widthIndex, depthIndex ).grayscale;
					
					WorldTile newTile = new WorldTile ();
					newTile.Initialize ( this, widthIndex, heightIndex, depthIndex);
					
					worldTiles[widthIndex, depthIndex] = newTile;
					
					widthIndex += 1;
				}
				
				widthIndex = 0;
				depthIndex += 1;
			}
		}
	}
	
	
	Texture2D CalcNoise ( int width, int depth )
	{
		
		Texture2D texture = new Texture2D ( width, depth );
		Color[] pix = new Color [width * depth];
		
		int y = 0;
		while ( y < depth )
		{
			
			int x = 0;
			while ( x < width )
			{
				
				float sample = Mathf.PerlinNoise ( x, y );
				UnityEngine.Debug.Log ( "X:" + x + "Y:" + y + "=" + sample );
				pix [y * width + x] = new Color ( sample, sample, sample );
				x++;
			}
			
			y++;
		}
		
		texture.SetPixels ( pix );
		texture.Apply ();
		
		return texture;
	}
}


class WorldTile
{
	
	//bool initialized = false;
	GameObject self;
	
	internal void Initialize ( World thisWorld, int x, float y, int z )
	{
		
		GameObject newPlane = GameObject.CreatePrimitive ( PrimitiveType.Plane );
		newPlane.transform.position = new Vector3 ( x * 10, y, z * 10 );
		newPlane.transform.parent = thisWorld.parent.transform;
		newPlane.renderer.material = thisWorld.parent.GetComponent<WorldManager>().seasons[thisWorld.season];
		self = newPlane;
		
	}
}


public class WorldManager : MonoBehaviour
{
	
	public int worldWidth = 6;
	public int worldDepth = 4;
	
	public Material[] seasons = new Material[4];
	
	internal World world = new World ();

	void Start ()
	{
		
		//GenerateTerrain ();
	}
	
	
	void GenerateTerrain ()
	{
		
		world.Initialize ( this.gameObject, worldWidth, worldDepth );
	}
}
