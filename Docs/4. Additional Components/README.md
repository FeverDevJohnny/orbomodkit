# Additional Components

There are some scripts that don't have object equivalents, but are designed to be attached to one of your own objects to add some extra behavior. This section will cover these components.

---

### Bind Audio Mixer

This is a component for taking a pre-existing Audio Source object and binding it into the in-game audio mixers. If you want to allow players to control the volume of your custom sources, you have to use this component.

Bind Audio Mixer only has a single property, which defines which mixer to use:

- **Master** - The master volume slider. If you're not sure whether or not your audio source is an effect or music, this is the setting to use.
- **Music** - If you're using an audiosource to play music separately to the BGM Volume system, this is a helpful way to tie it into the volume slider.
- **Effects** - This is for sound effects, and is most likely the mixer you'll use most often.

---

### Constant Mover

[(API Reference for Constant Mover)](https://github.com/FeverDevJohnny/orbomodkit/tree/main/Docs/9.%20API%20Reference)

Constant movers will take an object, and as expected, constantly move it in a direction.

Let's go over the settings.

- **Is Active** - Whether or not this mover should be trying to move the object it's attached to.
- **Override Target** - If you want your object to move towards a specific transform, you can place the target transform into this slot and it'll change the settings for the Constant Mover to match.
- **Direction** - This vector determines what direction the mover is going in, and how fast.
- **Relative** - Determines if the mover will travel in its defined Direction in world space or local space.
- **Look At Movement Direction** - Aligns the object's forward vector to the direction the Constant Mover is heading in.
- (If Look At Movement Direction is True) **Look At Speed** - Determines how quickly the Constant Mover object will interpolate to face its travel direction.

If **Override Target** is set, Constant Mover will expose some unique settings:

- **Stopping Distance** - The distance before the Constant Mover stops trying to approach the target object. If this is set to 0, the Constant Mover will try to reach the exact position of its target.
- **Lerp To Target** - Switches between standard movement and lerp movement. Lerp movement is exponential, and as such will travel faster than standard movement.
- **Move Speed** - How fast the object moves. This is disabled if you enable lerp movement.
- (If Lerp To Target is enabled) **Lerp Speed** - Determines how quickly lerp brings the Constant Mover object to the target transform. Expressed in terms of percent, so if it's 0.1 then the Constant Movers travels 10% of the distance between it and its goal.

---

### Speaker Target

[(API Reference for Speaker Targets)](https://github.com/FeverDevJohnny/orbomodkit/tree/main/Docs/9.%20API%20Reference)

This is an interface component that JComposers will use to inform an NPC when they're speaking in a dialog.

They only have a single parameter: A target [NPC Mood Controller](#NPC Mood Controller).

---

### Speaker Target Animator

[(API Reference for Speaker Target Animators)](https://github.com/FeverDevJohnny/orbomodkit/tree/main/Docs/9.%20API%20Reference)

This is a special variant of a Speaker Target that can be used to interface with an NPC's animator component instead of using texture moods.

- **Target Animator** - The animator component to modify when speaking to this character.
- **Is Talking Property** - This is a **bool property** in your animator that will be set to true or false depending on the speaker's talking state.
- **Mood Property** - This is an **integer property** in your animator that will correspond with the speaker's current mood. Unlike NPC Mood Controllers, the Speaker Target Animator doesn't store its mood state locally, since it's already being stored in the animator.

---

### On Trigger Passthrough

This component is quite important if you want to modify how triggers are handled on several different modkit types!

It only has two properties:

- **Target** - The ITriggerPassthrough to affect.
- **Passthrough ID** - The passthrough to feed in.

Let's go over how this component works in practice.

So, if you have an object with a collider (set as a trigger) and this component, you can effectively transmit On Trigger Enter and On Trigger Exit data to certain objects from a distance. This can have some helpful applications, like having objects the player's not supposed to collide with still detect when they've crossed their trigger threshold, or to add a bigger volume to join a player to a cannon.

Here is a list of modkit types that you can target with OnTriggerPassthrough:

- **Shell Cannons**
- **JTriggers**
- **Shell Key Orbs**
- **Interactable_Event**
- **Shell Door Slabs**
- **Shell Trial Rings**
- **Shell Gear Parts**

Now, hold your horses! Before you go connecting any of these types into your passthrough, you need to understand the usage for **Passthrough ID**. The idea is that there might be situations where you have multiple of these modkit types in a single object (like a cannon that contains both a JTrigger and the Shell_Cannon component).

To actually take advantage of a Trigger Passthrough, you have to know what ID the target components are looking for.

Here is a list of IDs (they are not case-sensitive):

- **cannon** - This passthrough will sends On Trigger Enter data to cannons, and if a player caused the trigger it'll connect the player to the cannon instantly.
- **trigger, jtrigger** - This passthrough sends On Trigger Enter and On Trigger Exit data to JTriggers.
- **key, keyorb** - This passthrough sends On Trigger Enter data to key orbs, so if a player hits the trigger passthrough the key orb will be collected.
- **interact, interaction, interactable** - This passthrough sends On Trigger Enter and On Trigger Exit data to interactable_event, so as long as the player is inside of this passthrough, they can interact with its target interactable (provided they are looking in the target's direction).
- **doorslab, door** - This passthrough sends On Trigger Enter data to doorslabs, so if the player is holding the correct key orb and touches this passthrough, the door slab will accept the key and unlock.
- **trial, trialring, ring** - This passthrough sends On Trigger Enter data to a trial ring, so if the player touches this passthrough the ring will be completed.
- **gear, gearpart** - This passthrough sends On Trigger Enter data to a gear part, so if the player touches it the associated gear part is collected.

---

### NPC Mood Controller

[(API Reference for NPC Mood Controller)](https://github.com/FeverDevJohnny/orbomodkit/tree/main/Docs/9.%20API%20Reference)

This component is a system designed to handle texture-based animations for NPCs.

It uses what are known as Moods, which are data files that contain information about what textures to use, and which emotional states they're tied into.

To create a mood for the NPC Mood Controller, Right-Click in your project folder, and navigate to **Create > Create New Mood**.

Here you can define the state this mood is associated with, a texture for the idle expression, a blinking graphic, and then a set of images for a talk cycle.

Once you've got a mood, you can head to the NPC Mood Controller component and assign it to the **Mood Pool** list. This will allow the NPC to access this mood's texture data when the mood is changed.

Now, we'll go through the other properties of the NPC Mood Controller:

- **Target Renderer** - Defines which renderer will have its textures modified with the data in the current mood.
- **Target Material Index** - Defines which material on the renderer we're going to target. You can leave this at 0 if you only have a single material on your target, but if you add additional materials this value must match the index of your desired material on the renderer.
- **Target Properties** - Defines which properties in the material are being modified. You can assign multiple properties here, but by default you will typically assign a single property labelled _\_BaseMap_, which is the default main texture property on URP Lit shaders.
- **Blink Delay** - How long, in seconds, this character will take to start a blink (essentially how long their eyes are open).
- **Blink Time** - How long, in seconds, a blink takes for this character.

An NPC Mood Controller isn't terribly useful by itself, and will require a [Speaker Target](#Speaker Target) to function.

---

### Post Processing Updater

There may be situations where you'd like to have custom post processing volumes in your scene that still comply with the game's settings (specifically being able to turn bloom on and off).

To subscribe to changes in the player's settings, you need to create an object with a Post Processing Updater on it, and then assign your post processing volume to **Target Volume**.

---

### Rotate Object

An extremely simple script.

**Speed** - The amount of degrees to rotate every second. So if you have 90 on X, it'll rotate a quarter of the way around the X-axis every second.

---

### Rotate To Face Transform

[(API Reference for Rotate To Face Transform)](https://github.com/FeverDevJohnny/orbomodkit/tree/main/Docs/9.%20API%20Reference)

Also very straightforward:

- **Target** - The transform to try and look at.
- **Rotation Speed** - How quickly to lerp to the target. If set at 0.1, it'll rotate 10% of the way to face its target every second.
- **Lock Y** - This prevents rotation that affects this object on the Y axis. The main use-case is to allow for objects that look in the player's direction, but not up or down at them. This might be applied to an NPC's body, while their head doesn't have Lock Y enabled so it can look up and down.

---

### Squash And Stretch

[(API Reference for Squash And Stretch)](https://github.com/FeverDevJohnny/orbomodkit/tree/main/Docs/9.%20API%20Reference)

A Feverdream classic.

- **Power** - Describes, in percentage, how intense the effect will be. 0.1 is a good value for this, but feel free to experiment.
- **Speed** - How quickly the squash and stretch should cycle. 10 is a good value, but again, feel free to experiment.
- **Affected By Pause** - Whether or not pausing the game stops this squash and stretch from running, or if it should continue regardless.

---

### Track To Player Head

This is crucial for creating a proxy object that, every frame, will match the player's current head position.

This is strongly recommended for working with cutscenes where you need the camera to look at Orbo. It's also really helpful if you want NPCs using [Rotate To Face Transform](#Rotate To Face Transform) to look at Orbo.

You can either have this track to the player's head, or to the facecam object attached to it, which can be used to create dramatic shots by setting the camera's position to match this tracking object (look at the documentation for [JComposer](#Working With JComposer) to learn more about camera handling).

---

### Transform Mover

[(API Reference for Transform Mover)](https://github.com/FeverDevJohnny/orbomodkit/tree/main/Docs/9.%20API%20Reference)

Transform movers are an alternative way to manage transforms that gives you more precise control over timing (and rotation). They can be used to teleport objects to specific positions, create timed interpolations for set pieces, or really for whatever. If you're creative you can do a lot with these.

- **Position Time** - Determines how long it should take this object to match the goal transform's position.
- **Rotation Time** - Determines how long it should take this object to match the goal transform's rotation.
- **Goal Transform** - The transform we should try to reach. If left unassigned, this object will not move until SetTransformGoal() is used to tell it where to move. Otherwise, on startup, this object will immediately try to reach its goal.
- **Position Lerp** - Determines how this object should interpolate to its goal position. Represented as an anim curve, so you can fully customize the movement.
- **Rotation Lerp** - Determines how this object should interpolate to its goal rotation. Represented as an anim curve, so you can fully customize the movement.

---

### Light Color Random Gradient

This is a fun lil tool for doing light animations on a gradient.

- **Target** - Which light to modify. This only modifies the light's color, not its intensity.
- **Mode** - Which sampling mode to use. Sine and Modulus are the best choices typically, but a random color selection option is also provided. **PLEASE BE WARNED: Random sampling could very easily be seizure inducing.** **Please use the strobe delay option to prevent the system from picking colors extremely fast.**
- **Color Range** - This is the actual gradient your light will sample across.
- **Speed** - How quickly to cycle through the gradient (ignored when using random sampling).
- **Strobe Delay** - This creates a pause, in seconds, between sample calls. This can be used to sort of quantize color changes in sine/modulus, and for random this helps you keep your lights from rapidly flashing by adding a delay before it picks a new color.

---
