using UnityEngine;
using System.Collections;
//Written by Gibson Bethke
public class ExitHouse : MonoBehaviour
{

	void OnTriggerEnter ()
	{
	
		Application.LoadLevel ("GameWorld");
	}
}
