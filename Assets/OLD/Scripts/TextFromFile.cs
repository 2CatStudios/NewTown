using System;
using UnityEngine;
using System.IO;

public class TextFromFile : MonoBehaviour
{
	
public const string Morning = "/Users/gibsonbethke/Desktop/Greetings Morning.txt";
	
    public void Start()
    {
		
        if (!File.Exists(Morning))
        {
			
            Debug.Log(Morning + " Does not exist!");
            return;
        }
        using (StreamReader sr = File.OpenText(Morning))
        {
			
            String input;
            while ((input = sr.ReadLine()) != null)
            {
				
                Debug.Log(input);
            }
        }
    }
}

