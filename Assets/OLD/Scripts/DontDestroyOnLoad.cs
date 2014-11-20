using UnityEngine;
using System.Collections;
//Written by Gibson Bethke
public class DontDestroyOnLoad : MonoBehaviour
{

	void Start ()
	{
	
		DontDestroyOnLoad (gameObject.transform);
	}
}
