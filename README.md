AudioManager
============

Description: 

An Audio Manager for Unity. I found myself being annoyed by how audio works in Unity so I created a single non-destructable game object that has an array of names and an array of audio files. 

How it works:

1. Go to Window/Audio Manager
2. An Audio Manager window will pop up that will instruct you to go to the first scene of your project
3. Click the Setup Audio Manager button
4. An empty gameObject with a script called audioController.cs is created. 
5. Look at the AudioManager. There is an array called "audioFileNames". Open that and list the audio file names. Remember you will be calling these from code so keep them simple
6. Open the array called "audioFiles" change the array size to be the EXACT same size as your "audioFileNames" array. 
7. Now add the audio files in the exact same location as its name. Example: I have an audioFileName called "Jump" thats in the 1st position in the audioFileNames array. I now add the "Jump" sound file to the 1st position of the audioFiles array. 
8. Continue to do this for all audio files. Its an annoying process to setup. 


WARNING: Do not just drag over all your audio files on the array unless you plan on adding the audioFileNames after. Unity will not organize them in any manner that makes sense. 


##Documentation: 

Audio Layers: There are three audio layers in the audioManager.
1. oneshot - an audio layer to throw short and quick audio files such as a jump sound, or bullet sound. 
2. background - a layer that can be used as oneshot but should be used to loop a longer background noise such as looped music. 
3. foreground - a layer that can be used as a one shot but should be used to add addition depth to your audio. Example: If the background layer is playing a looping audio file of people talking in a coffee shot. The foreground can be set to play an audio file that plays "Order up!". ALso a looping audio file on top of the background layer will work also such as wind. 

###Example: 

```
//example on how to use
//plays an audio clip that is listed in the array of File Names called fileName
//sets the audio clip to NOT loop
//sets the volume of the clip to max. 1.0f
//sets it to the oneshot layer so the background and foreground music layers are left alone
audioController.instance.playAudio ("fileName", false, 1.0f, "oneshot");
```

###Method List: 


setVolumeToLayer
```
/// <summary>
/// Sets the volume to a certain layer.
/// </summary>
/// <param name="volumeAmount">Volume amount.</param>
/// <param name="layerName">Layer name: oneshot, foreground, background</param>
void setVolumeToLayer(float volumeAmount, string layerName)
```

unpauseAudioSource
```
/// <summary>
/// Unpauses the audio source.
/// </summary>
/// <param name="layerName">Layer name to unpause: oneshot, background, foreground</param>
void unpauseAudioSource(string layerName)
```

pauseAllAudio
```
/// <summary>
/// Pauses all audio.
/// </summary>
void pauseAllAudio()
```

muteAllLayers
```
/// <summary>
/// Mutes all layers.
/// </summary>
void muteAllLayers()
```

unmuteAllLayers
```
/// <summary>
/// Unmutes all layers.
/// </summary>
void unmuteAllLayers()
```

selectLayerUnmute
```
/// <summary>
/// Pass a layer name to be unmuted
/// </summary>
/// <param name="layerName">Layer name to unmute: oneshot, foreground, background</param>
void selectLayerUnmute(string layerName)
```

playAudio
```
//iterate through the audio file names and see if any exist with the name being passed
//if true play that audio
/// <summary>
/// Play the audio clip name
/// </summary>
/// <param name="Name">name of the audio clip</param>
/// <param name="shouldLoop">If set to <c>true</c> should loop.</param>
/// <param name="audioLevel">Audio level. 1.0 = max</param>
/// <param name="layerName">Layer name to play audio clip on: oneshot, foreground, background</param>
void playAudio(string Name, bool shouldLoop, float audioLevel, string layerName)
```

getAudioNames
```
/// <summary>
/// Gets the Audio File Names
/// </summary>
/// <returns>The audio names in Array</returns>
string[] getAudioNames()
```

