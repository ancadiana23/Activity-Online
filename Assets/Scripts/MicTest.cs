using UnityEngine;
using System;

public class MicTest : MonoBehaviour {

	int lastSample;
	AudioClip c;
	int FREQUENCY = 44100;


	void Start () 
	{
		c = Microphone.Start(null, true, 100, FREQUENCY);
		while(Microphone.GetPosition(null) < 0) {} // HACK from Riro
	}

	void Update()
	{
		int pos = Microphone.GetPosition(null);
		int diff = pos-lastSample;
		if (diff > 0) 
		{
			float[] samples = new float[diff * c.channels];
			c.GetData(samples, lastSample);
		}
		lastSample = pos;
	}
}