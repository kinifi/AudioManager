using UnityEngine;
using System.Collections;

public class audioTest : MonoBehaviour {

	// Use this for initialization
	void Start () {

		//example on how to use
		//plays an audio clip that is listed in the array of File Names called fileName
		//sets the audio clip to NOT loop
		//sets the volume of the clip to max. 1.0f
		//sets it to the oneshot layer so the background and foreground music layers are left alone
		audioController.instance.playAudio ("fileName", false, 1.0f, "oneshot");

	}

}
