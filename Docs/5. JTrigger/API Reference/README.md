# JTrigger API Reference

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
OnDemand() : This is the most common function to run on a JTrigger. It causes the JTrigger to try and evaluate its conditions, and if they return true it'll run its associated events. If they return false and eventsFailed is defined, it'l run those instead. Even if the JTrigger's context isn't set as OnDemand, you can invoke this. The only reason I typically recommend people to set the context accordingly is to prevent issues where you might accidentally find behavior running multiple times.

TriggerSet(string command) : This will set the trigger on the left-hand side to the value on the right. The command is split with the vertical bar character, so for example: my_trigger|5 will set a trigger to 5. You can also set trigger values between eachother, so my_trigger|other_trigger will set the value of my_trigger to the value of other_trigger. Adding an additional bar lets you generate a random number between two values, so you can do my_trigger|0|5 and it'll assign m_trigger as something between 0 and 5.

TriggerAdd(string command) : This will add the value on the right-hand to the trigger on the left-hand side. The command is split with the vertical bar character, so for example: my_trigger|5 will add 5 to my_trigger. You can also add trigger values between eachother, so my_trigger|other_trigger will add the value of other_trigger to the value of my_trigger. Adding an additional bar lets you generate a random number between two values, so you can do my_trigger|0|5 and it'll add a random number between 0 and 5 to m_trigger.

TriggerSubtract(string command) : This will subtract the value on the right-hand to the trigger on the left-hand side. The command is split with the vertical bar character, so for example: my_trigger|5 will subtract 5 from my_trigger. You can also subtract trigger values between eachother, so my_trigger|other_trigger will subtract the value of other_trigger from the value of my_trigger. Adding an additional bar lets you generate a random number between two values, so you can do my_trigger|0|5 and it'll subtract a random number between 0 and 5 from m_trigger.

TriggerMultiply(string command) : This will multiply the value on the left-hand side of the trigger by the value on the right-hand side. The command is split with the vertical bar character, so for example: my_trigger|5 will multiply my_trigger by 5. You can also multiply trigger values between eachother, so my_trigger|other_trigger will multiply my_trigger by the value of other_trigger. Adding an additional bar lets you generate a random number between two values, so you can do my_trigger|2|5 and it'll multiply m_trigger by a random value between 2 and 5.

TriggerDivide(string command) : This will divide the value on the left-hand side of the trigger by the value on the right-hand side, flooring to the nearest integer value. The command is split with the vertical bar character, so for example: my_trigger|5 will divide my_trigger by 5. You can also divide trigger values between eachother, so my_trigger|other_trigger will divide my_trigger by the value of other_trigger. Adding an additional bar lets you generate a random number between two values, so you can do my_trigger|2|5 and it'll divide m_trigger by a random value between 2 and 5. If you attempt to divide by 0, the game will warn you with a prompt.

PlaySound(AudioClip sound) : Plays a sound in 2D space with a randomized pitch betweenn 80% and 120%.

PlaySoundWithNormalPich(AudioClip sound) : Plays a sound in 2D space with normal pitch.

SetCameraShake(float amount) : Creates an impulse that will shake the player's camera by the amount of units specified. 0.1 is a good value for moderate shaking, while 1 and 2 are good for crazy moments.

PlaySoundAtPosition(AudioClip sound) : Plays a sound at this JTrigger's object position with normal pitch.

PushTutorialMessage(string command) : Places a tutorial message at the top of the player's screen. Commands are split with the vertical bar character |. You can either just have a single argument for the message, which will default to displaying the message for 5 seconds, or you can add a second argument to specify how long the tutorial message stays at the top of the screen. Exmaple: Welcome to my malicious fantasy!|6 will cause "Welcome to my malicious fantasy!" to display at the top of the screen for 6 seconds. Tutorial messages are queued, so you can run multiple of these commands in sequence and it will display your messages one after the other.

SetPlayerCameraPosition(Transform target) : Moves the player's camera to a specific object's position. This is a snap operation, so it will happen instantly. If you want smooth camera interpolation, it's recommended you use JComposers for camera controls.

SetPlayerViewTarget(Transform target) : Forces the player's camera to look at a specific object. This is a snap operation, so it will happen instantly. If you want smooth camera interpolation, it's recommended you use JComposers for camera controls.

ReturnPlayerCamera() : Disables any camera overrides, returning the camera back to the player.

SetPlayerRunTarget(Transform target) : Forces Orbo to run towards a specific transform's position. Good for outros where you want Orbo to automatically walk into an obscured pathway or something.

UnsetPlayerRunTarget() : Stops Orbo from running towards an object specified by SetPlayerRunTarget().

SetPlayerArtRootLookOverride(Transform target) : Allows you to force Orbo's body to look at something. By default Orbo will look at the current view target override if one is defined.

UnsetPlayerArtRootOverride() : Stops Orbo from staring at a specific art root override object. If a view target is defined he will return to staring at that instead, otherwise he'll just look in whatever direction he's moving (whether because he has a run target or because he has control).

SetPlayerMood(int mood) : Changes Orbo's current mood. Check the mood reference section above for info. Keep in mind that Orbo does not have moods defined for Misc A - D, so don't use values 4 - 7 or else you'll get errors.

HealPlayer(int amount) : Heals the player by a specified amount, capped at 8 units.

HurtPlayer(int amount) : Deals damage to the player by a specified amount.

SetOneShotState(bool state) : Allows you to reset or trip the oneshot switch on a JTrigger. This JTrigger must have oneShot enabled for this to actually do anything. If this is set to true, then the oneshot is tripped and the event code won't run. If it's false, then the object is reset and can re-trip its oneshot.

```

---
