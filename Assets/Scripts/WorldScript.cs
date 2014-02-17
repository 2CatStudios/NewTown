using UnityEngine;
using System.Collections;

public class WorldScript : MonoBehaviour
{

int gameSeason;
public Manager manager;
public Material springGround;
public Material summerGround;
public Material autumnGround;
public Material winterGround;
	
	void Start ()
	{
		
		gameSeason = manager.season;
		switch(gameSeason)
		{
			case 0:
				renderer.material = springGround;
				break;
			case 1:
				renderer.material = summerGround;
				break;
			case 2:
				renderer.material = autumnGround;
				break;
			case 3:
				renderer.material = winterGround;
				break;
			default:
				Debug.LogError ("A error occured in WorldScript Switch!");
				break;
		}
	}
}
