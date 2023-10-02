# API Reference

This section covers object functions that can be accessed inside of modkit objects via JTrigger or JComposer.

---

## Tags and Layers

You cannot define new tags and layers for your modded levels due to a technical constraint with how Unity handles asset bundles and these types of data.
I have tried to supply some options to make things easier, and I will list and explain all of the tags and layers here:

**Tags**

- **KeyOrb_Blue** - Used on blue key orbs to transmit data to slab doors. Strongly recommend you avoid using this for your own objects as it might confuse the doors and cause errors.
- **KeyOrb_Pink** - Used on pink key orbs to transmit data to slab doors. Strongly recommend you avoid using this for your own objects as it might confuse the doors and cause errors.
- **KeyOrb_Green** - Used on green key orbs to transmit data to slab doors. Strongly recommend you avoid using this for your own objects as it might confuse the doors and cause errors.
- **KeyOrb_Red** - Used on red key orbs to transmit data to slab doors. Strongly recommend you avoid using this for your own objects as it might confuse the doors and cause errors.
- **IgnoreTracking** - Tells the composer system that Orbo's body shouldn't try to look at this associated object if it's marked as the camera's current view target.
- **GnomeHead** - Internal ID used in the main game for the gnome quest. You can use this if you want, there's no scripts in the modding system to conflict with this.
- **NukeBall** - Internal ID for the boss's aerial mine attack. Also used in the gnome quest.
- **CrazyShapeGate** - Internal ID for the crazy shape gate in the monolith station puzzle.
- **Tag_A** - A pre-reserved tag you can use for your modded objects!
- **Tag_B** - A pre-reserved tag you can use for your modded objects!
- **Tag_C** - A pre-reserved tag you can use for your modded objects!
- **Tag_D** - A pre-reserved tag you can use for your modded objects!
- **Tag_E** - A pre-reserved tag you can use for your modded objects!
- **Tag_F** - A pre-reserved tag you can use for your modded objects!
- **Tag_G** - A pre-reserved tag you can use for your modded objects!
- **Tag_H** - A pre-reserved tag you can use for your modded objects!
- **Tag_I** - A pre-reserved tag you can use for your modded objects!

**Layers**

- **Player** - This is the contact layer for the player. I recommend not using this one or else you might encounter some technical issues.
- **UIPrerender** - This was used back when the game had 3D UI elements, but has since become deprecated. This can be ignored.
- **PlayerNoCollide** - Useful! This tells an object to avoid collding with the player. You can use this to create objects that can interact with triggers without interfering with the player.
- **Gib** - Used internally for breakable sand walls and other destructive remnants. Won't collide with the player.
- **Damageable** - Marks an object for use in the aim-assist feature (causes Orbo to curve towards the object while drilling). Players will slide off of these objects if they land on them.
- **Damageable_WalkOn** - Same as Damageable, but the player won't slide while walking on them. Typically used for gust plates and the like.
- **PlayerBlocker** - This makes it so an object will collide with the player, but the camera won't. Quite useful for decorative elements in a scene.
- **BlockerPassthrough** - This object won't colide with player blockers, but will collide with everything else. Used on Peeb during the hallway cutscene so he can head into the light while Orbo can't.
- **NoWallslide** - The player and the camera will collide with this object, but the player cannot wallslide on it. Used in a few places in-game, like the hall leading out of the boss's office that Orbo cannot access without fighting the HR Rep first.

---

## C#

### Cannon API Reference

```
LaunchPlayer() : If the player is currently inside of the cannon, calling this will launch them out.
```

---

### Gear Part API Reference

```
SetGearPartState(bool state) : This will allow you to either hide or expose a gear part through script. Useful if you want to have your own objectives for unlocking a gear part in a level!
```

---

### Screen Shaker API Reference

```
SetShakerState(bool state) : Determines whether or not a shaker should be active.
```

---

### Player Kill Trigger API Reference

```
SetKillTriggerState(bool state) : Determines whether or not this kill trigger actually kills the player.
```

---

### Hurt Volume API Reference

```
SetHurtVolumeState(bool state) : Determines whether or not this hurt volume should apply damage to its inhabitants.

SetDamageTimer(float time) : Sets how rapidly hurt volumes can apply their damage to inhabitants.

SetDamageAmount(int amount) : Sets how much damage an inhabitant takes from a hurt volume. If it's negative, this heals them instead!
```

---

### Checkpoint API Reference

```
SetCheckpointAsCurrent() : Marks this checkpoint as the current one for the player to spawn at, with sounds and effects. Good if you're trying to create a sort of auto-save feature.

SetCheckpointAsCurrentQuietly() : Marks this checkpoint as the current one for the player to spawn at, but doesn't play any sounds or effects. This can be useful if you need to subtley teleport the player somewhere else in the level when they die...
```

---

### Interactable_Events API Reference

```
ForceKill() : Instantly kills this world object.
```

---

### Damageable_Events API Reference

```
ForceKill() : Instantly kills this world object.
```

---

### Button API Reference

```
CancelTimer() : If this button is marked as a timed button, this will end the timer and run the attached On Button Timer End JTrigger.

CancelTimerWithoutEvent() : If this button is marked as a timed button, this will end the timer but it won't run the attached On Button Timer End JTrigger.

SetButtonState(bool state) : This only works on buttons that aren't configured with a timer. This allows you to override the button's current state without invoking events. For standard buttons, setting this to true will mean the button is being held down. If you set it to false, the button is up and can be hit. For impulse buttons, you can set this to true to lock them down so they can't be pressed anymore. Pretty nifty!
```

---

### Scene Transition Volume API Reference

```
BeginSceneTransition() : This is extremely helpful! You can force a scene transition to start without the player needing to cross a scene transition volume's trigger. In fact, scene transitions can be made without giving the volume a trigger at all, and then you can call this function on them when you need to actually make the transition. It's the equivalent of using On Demand on a JTrigger.
```

---

### Constant Mover API Reference

```
SetMoverOverrideTarget(Transform target) : This allows you force a constant mover to move towards a specific object instead of moving in a single direction.

UnsetMoverOverrideTarget() : Since you can't pass null values in object functions, you can use this if you want to stop a constant mover from trying to reach a specific object. It will automatically fall back onto the direction vector.

SetMoverSpeed(float speed) : If the mover has an override target defined for it, you can tell the mover how quickly it should move to the target. If your mover doesn't have an override target defined for it, then this will change the magnitude of the direction vector.

SetLerpSpeed(float lerpSpeed) : If your mover is configured with an override target and has lerp enabled, you can feed a value between 0 - 1 to tell the mover how fast it should arrive at its target.

SetDirectionTowards(Transform target) : Instead of defining an override target to reach, this sets the single direction vector to point towards another object. Good for bullets.

SetDirectionAlignedWith(Transform target) : This will cause the mover's direction vector to be the same as the forward vector of another object.

SetMoverState(bool state) : Determines whether or not this mover should try to move towards a target or in a given direction.
```

---

### Speaker Target API Reference

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
SetCharacterMood(int Mood) : Changes the mood of this speaker target's NPC Mood Controller, if one is attached. Check the section above for the different mood IDs.
```

---

### Speaker Target Animator API Reference

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
SetCharacterMood(int Mood) : Changes the mood of this speaker target's NPC Mood Controller, if one is attached. Check the section above for the different mood IDs.
```

---

### NPC Mood Controller API Reference

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
ChangeMood(int Mood) : This is virtually the same as the function found Speaker Target. Speaker Target just implements it for ease-of-use.
```

---

### Rotate To Face Transform API Reference

```
SetRotationTarget(Transform target) : Defines a target for this object to look towards.

UnsetRotationTarget() : Removes a target for this object, causing it to stop tracking.

SetRotationSpeed(float speed) : A value between 0 - 1 that determines how quickly this object will look at its target.
```

---

### Squash And Stretch API Reference

```
Impulse(float size) : Allows you to make a squash and stretch object throb. A feverdream classic, really. The size value corresponds with the current base-scale of the object, so 1.5 will make it 150% larger than normal, 2 will make it 200%, and so on.
```

---

### Transform Mover API Reference

```
SetGoalPositionTime(float time) : Determines how long it should take, in seconds, for this transform mover's position to match its target. Always call this before trying to set a transform goal.

SetGoalRotationTime(float time) : Determines how long it should take, in seconds, for this transform mover's rotation to match its target. Always call this before trying to set a transform goal.

SetTransformGoal(Transform target) : Defines the target transform this object wants to match. When this is called, the interpolation will start. Try to avoid calling this too many times while the object is moving, since it'll recalculate the starting point of the interpolation at its current position, which can look weird.

UnsetTransformGoal() : This can be used to stop a transform mover from acting by ripping out a goal.
```

---
