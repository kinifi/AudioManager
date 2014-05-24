using UnityEngine;
using System.Collections;

public class audioController : MonoBehaviour {

	public bool debugAudioController = false;
	public string[] audioFileNames;
	public AudioClip[] audioFiles;
	//Three layers of audio sources for one shot audio, background, foreground
	public AudioSource oneshotAudio, backgroundAudio, foregroundAudio;

	private static audioController _instance;

	public void Awake()
	{
		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance)
				Destroy(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {

		//start the audio controller
		setupAudioController();

		//Do not destroy the audio object during scene switches
		//DontDestroyOnLoad(this.gameObject);

	}

	/// <summary>
	/// Gets the instance this should never be used anywhere but this scene
	/// </summary>
	/// <value>The instance.</value>
	public static audioController instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<audioController>();
				
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}

	/// <summary>
	/// Sets the volume to a certain layer.
	/// </summary>
	/// <param name="volumeAmount">Volume amount.</param>
	/// <param name="layerName">Layer name: oneshot, foreground, background</param>
	public void setVolumeToLayer(float volumeAmount, string layerName) {
		if(layerName == "oneshot")
		{
			oneshotAudio.volume = volumeAmount;
			debugAudio("Setting Volume of: " + volumeAmount + " to " + layerName);
		}
		else if(layerName == "foreground")
		{
			foregroundAudio.volume = volumeAmount;
			debugAudio("Setting Volume of: " + volumeAmount + " to " + layerName);
		}
		else if(layerName == "background") {
			backgroundAudio.volume = volumeAmount;
			debugAudio("Setting Volume of: " + volumeAmount + " to " + layerName);
		}
		else
		{
			Debug.LogWarning("Audio Layer Does Not Exist");
		}

	}


	/// <summary>
	/// Unpauses the audio source.
	/// </summary>
	/// <param name="layerName">Layer name to unpause: oneshot, background, foreground</param>
	public void unpauseAudioSource(string layerName) {
		if(layerName == "oneshot")
		{
			oneshotAudio.Pause();
			debugAudio("Unpausing: " + layerName);
		}
		else if(layerName == "foreground")
		{
			foregroundAudio.Pause();
			debugAudio("Unpausing: " + layerName);
		}
		else if(layerName == "background") {
			backgroundAudio.Pause();
			debugAudio("Unpausing: " + layerName);
		}
		else
		{
			Debug.LogWarning("Audio Layer Does Not Exist");
		}
	}

	/// <summary>
	/// Pauses all audio.
	/// </summary>
	public void pauseAllAudio() {
		oneshotAudio.Pause();
		backgroundAudio.Pause();
		foregroundAudio.Pause();
	}

	/// <summary>
	/// Mutes all layers.
	/// </summary>
	public void muteAllLayers() {
		debugAudio("Muting all Layers");
		oneshotAudio.mute = true;
		backgroundAudio.mute = true;
		foregroundAudio.mute = true;
	}

	/// <summary>
	/// Unmutes all layers.
	/// </summary>
	public void unmuteAllLayers() {
		debugAudio("Muting all Layers");
		oneshotAudio.mute = false;
		backgroundAudio.mute = false;
		foregroundAudio.mute = false;
	}

	/// <summary>
	/// Pass a layer name to be unmuted
	/// </summary>
	/// <param name="layerName">Layer name to unmute: oneshot, foreground, background</param>
	public void selectLayerUnmute(string layerName) {

		if(layerName == "oneshot")
		{
			oneshotAudio.mute = false;
			debugAudio("Unmuting: " + layerName);
		}
		else if(layerName == "foreground")
		{
			foregroundAudio.mute = false;
			debugAudio("Unmuting: " + layerName);
		}
		else if(layerName == "background") {
			backgroundAudio.mute = false;
			debugAudio("Unmuting: " + layerName);
		}
		else
		{
			Debug.LogWarning("Audio Layer Does Not Exist");
		}

	}

	//iterate through the audio file names and see if any exist with the name being passed
	//if true play that audio
	/// <summary>
	/// Play the audio clip name
	/// </summary>
	/// <param name="Name">name of the audio clip</param>
	/// <param name="shouldLoop">If set to <c>true</c> should loop.</param>
	/// <param name="audioLevel">Audio level. 1.0 = max</param>
	/// <param name="layerName">Layer name to play audio clip on: oneshot, foreground, background</param>
	public void playAudio(string Name, bool shouldLoop, float audioLevel, string layerName) {

		for (int i = 0; i < audioFileNames.Length; i++) {

			if(Name == audioFileNames[i])
			{
				debugAudio("Play Audio File: " + audioFileNames[i]);
				audioManager(audioFiles[i], shouldLoop, audioLevel, layerName);
			}
		}

	}
	
	/// <summary>
	/// Audio Managaer that sets the layer and plays the audio
	/// </summary>
	/// <param name="soundToPlay">the audio clip to play</param>
	/// <param name="loop">If set to <c>true</c> loop.</param>
	/// <param name="audioLevel">Audio level to play audio clip being passed. 0.0 - 1.0 max</param>
	/// <param name="layerName">Layer name: oneshot, foreground, background</param>
	private void audioManager(AudioClip soundToPlay, bool loop, float audioLevel, string layerName) {

		if(layerName == "oneshot")
		{
			oneshotAudio.clip = soundToPlay;
			oneshotAudio.Play();
			oneshotAudio.volume = audioLevel;
			debugAudio("Playing on " + layerName);
			debugAudio("Oneshot layer cannot be looped");
		}
		else if(layerName == "background")
		{
				backgroundAudio.clip = soundToPlay;
				backgroundAudio.Play();
				backgroundAudio.loop = loop;
				backgroundAudio.volume = audioLevel;
				debugAudio("Playing on " + layerName);
		}
		else if(layerName == "foreground")
		{
			debugAudio("Secondary Audio is being used");
			foregroundAudio.clip = soundToPlay;
			foregroundAudio.Play();
			foregroundAudio.loop = loop;
			foregroundAudio.volume = audioLevel;
			debugAudio("Playing on " + layerName);
		}

	}

	/// <summary>
	/// Gets the Audio File Names
	/// </summary>
	/// <returns>The audio names in Array</returns>
	public string[] getAudioNames() {
		return audioFileNames;
	}

	/// <summary>
	/// Checks to see if the audio sources have been set yet. 
	/// </summary>
	/// <returns>True is setup; False if null</returns>
	public bool isAudioControllerSetup() {
		if(oneshotAudio != null && foregroundAudio != null && backgroundAudio != null)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	
	private void setupAudioController() {

		addAudioSources();

		checkArrayValues();
		oneshotAudio.playOnAwake = false;
		backgroundAudio.playOnAwake = false;
		foregroundAudio.playOnAwake = false;
		debugAudio("Starting Setup");
		debugAudio("Setting all Audio Sources to not play on awake");
	}

	/// <summary>
	/// Adds the audio sources to the gameobject and assigns them to a variable
	/// </summary>
	private void addAudioSources() {
		oneshotAudio = gameObject.AddComponent<AudioSource>();
		foregroundAudio = gameObject.AddComponent<AudioSource>();
		backgroundAudio = gameObject.AddComponent<AudioSource>();
	}

	/// <summary>
	/// Check to see if the audioFileNames and the audioFiles have the same length. If they don't send an error; 
	/// </summary>
	private void checkArrayValues () {

		if(audioFiles.Length != audioFileNames.Length)
		{
			Debug.LogError("Audio Array Lengths Do Not Match");
		}
		else
		{
			debugAudio("Audio Source Initialized");
		}
	}

	/// <summary>
	/// Debug logs a message if debugAudioController is set to true
	/// </summary>
	/// <param name="log">Message to send</param>
	private void debugAudio(string log) {
		
		if(debugAudioController)
		{
			Debug.Log(log);
		}
		
	}

}
