# JComposer API Reference

**Mood ID Reference**:

- 0 - Normal
- 1 - Happy
- 2 - Angry
- 3 - Disturbed
- 4 - Misc A
- 5 - Misc B
- 6 - Misc C
- 7 - Misc D

```
SetComposerState(bool state) : The main meat and potatoes of JComposers. Setting this to true will activate the sequence stored in this composer, and setting it to false will immediately close a sequence down and show the JComposer off.

SetPlayerLockState(bool state) : Determines whether or not this JComposer will completely stop all player momentum, while storing their velocity and movement state. Setting this to true performs the locking operation, and setting it to false will re-apply the user's velocity and movement state. Useful for cutscenes where you need to show something far away and don't want to risk the player falling to their death or something.

SetPlayerControlState(bool state) : Determines whether or not a player can actually move Orbo around. If this is set to true they have control, if it's set to false, Orbo will come to a stop on his own, and if he's in midair will begin falling down and landing. This is commonly used with NPCs since it feels more natural for Orbo to come to a stop like this rather than levitating in place with his states preserved as seen in the SetPlayerLockState function.

SetPlayerArtRootLookOverride(Transform target) : Allows you to force Orbo's body to look at something. By default Orbo will look at the current view target override if one is defined.

UnsetPlayerArtRootOverride() : Stops Orbo from staring at a specific art root override object. If a view target is defined he will return to staring at that instead, otherwise he'll just look in whatever direction he's moving (whether because he has a run target or because he has control).

SetPlayerCinematicBarState(bool state) : Determines whether or not to add cinematic bars to the screen. Fun in cutscenes, but weird in NPC dialogs unless an important moment just happened. True makes them show up, false sends them away.

SetPlayerCameraPosition(Transform target) : Moves the player's camera to a specific object's position. This is a snap operation, so it will happen instantly. If you want smooth camera interpolation, it's recommended you use JComposers for camera controls.

SetPlayerViewTarget(Transform target) : Forces the player's camera to look at a specific object. This is a snap operation, so it will happen instantly. If you want smooth camera interpolation, it's recommended you use JComposers for camera controls.

ReturnPlayerCamera() : Disables any camera overrides, returning the camera back to the player.

SetPlayerRunTarget(Transform target) : Forces Orbo to run towards a specific transform's position. Good for outros where you want Orbo to automatically walk into an obscured pathway or something.

UnsetPlayerRunTarget() : Stops Orbo from running towards an object specified by SetPlayerRunTarget().

SetPlayerMood(int mood) : Changes Orbo's current mood. Check the mood reference section above for info. Keep in mind that Orbo does not have moods defined for Misc A - D, so don't use values 4 - 7 or else you'll get errors.

PlaySoundEffect(AudioClip sound) : Plays a sound in 2D space at normal pitch.

SetPlayerCameraShake(float amount) : Creates an impulse that will shake the player's camera by the amount of units specified. 0.1 is a good value for moderate shaking, while 1 and 2 are good for crazy moments.

```
