Special thanks to Deadcows for having developed [MyBox](https://github.com/Deadcows/MyBox), which is imported via package-manager in the Orbo's Odyssey Modkit to make interfacing with the modkit objects a lot cleaner.

Another special thanks to Facepunch for the [Facepunch Steamworks API](https://github.com/Facepunch/Facepunch.Steamworks), which is what I'm using to send data to the steam workshop.

## Installation

To install the **Orbo's Odyssey modkit**, you can clone a copy of the modding project files from [the github page](https://github.com/FeverDevJohnny/orbomodkit).

You will need to have **Unity 2022.3.4f1** installed to open the modkit project. After that, you are ready to start working on your mod! No need for visual studio or any other installations, mod scripting is done entirely in-engine using JTrigger.

**NOTICE**: I **STRONGLY** recommend [learning unity's interface](https://learn.unity.com/project/start-creating-1?pathwayId=5f7bcab4edbc2a0023e9c38f&missionId=5f77cc6bedbc2a4a1dbddc46) before trying to use the modding toolset. This documentation is explicitly here to tutorialize the mod tools themselves, not the engine.

---

## Getting Started

To begin, open the modkit project in Unity 2022.3.4f1.

In the Resources folder, you will find a section for Mods. This is where your content will be stored once you start working. If you would like, you can open up the included example mod's scene folder and load a level to see what a mod looks like in-editor. You can also play the example mod by installing it from the steam workshop. 

To create a mod, head to the toolbar at the top of the unity editor and select **Modding > Modkit Helper**

This will open a window where you can generate the required mod files. You will be prompted to provide a name. Once you're good to go, you can hit create mod.

Inside of Resources/Mods/, you will find a new folder for your mod. Inside of this folder is your **Mod Core File** and the **ModContent** Folder.

The **Mod Core File** is your hub. Selecting it will allow you to define information about your mod, as well as the option to build and upload it. We'll come back to this in a bit.

The **ModContent** Folder is where you will store all of your assets. There's no specific folder architecture requirement beyond making sure your content is packed in here, so feel free to organize the contents however you like.

---

### Creating Your First Scene

Inside of your **ModContent** folder, Right-Click and hit **Create > New Scene**. You can title this whatever you want, but this will be the level the player will load into when your mod is activated in-game.

Delete the Main Camera that's packed in with the scene, since we don't need it. You'll want to create some kind of floor for Orbo to stand on, so right-Click inside of the scene hierarchy and hit **Create > 3D Object > Plane**, and place it at world origin. Maybe scale it up on the X and Z axes so we have some extra space to move around.

---

### Building a Mod

Now, we're going to configure the mod so you can actually play it. Go over to your **Mod Core File** and select it. In the inspector, we need to go to the **scenes** list and drag our new level to it. Once that's done, go the the **initial level to load** parameter, and type in the name of the level.

Once you've done that, you might notice a little yellow circle with an arrow showing up in the scene view. This is the spawn point of your first scene. Inside the core file you can find settings to change the position of this spawn point as well as the initial spawn rotation the player will pop into the level with.

With these out of the way, congratulations! You've made a very basic level.

To test this out, go back to your core file and hit the "Build Mod" button. After a short while, the windows explorer should open up to show you the mod folder. You've done it! You've made a mod.

Now then, take this mod folder, and go to your installation directory for Orbo, open up the data folder, and you should find a folder labelled "Mods" in there. Place your new mod folder into there. Now you can open up the game and load your mod!

To add a custom icon for your mod, create a **16:9 image** and export it as a .jpeg (for file size).

---

### Uploading to Steam Workshop

You will need to be logged into your steam account on this computer for uploading to work.

It's pretty simple to upload to the workshop: Just make sure **you've filled out your mod info** and **create a build**.
Then you can configure the privacy of your workshop mod, and hit **Upload to Workshop** to begin the process.
Once you've made an upload, you can press the **Update on Steam Workshop** button to rebuild to the workshop as you please.

**KNOWN ISSUES:**
- Steam Workshop HATES icon files that are larger than 1MB. Now, the game itself doesn't care, but if you want to upload to the workshop, please make sure your file size is correct. Otherwise you'll receive an error during the upload process.

---

## Working With In-Game Systems

Naturally, a single plane in the default unity skybox doesn't make much of a level. Let's learn how to work with gameplay implements like pips, gear parts and time trials!

---

### Shells vs Built-In Components

If you've made the effort to learn how to work with unity's interface, you've probably learned about the component system, where scripts can be attached to objects to add modularized behavior.

The modkit includes quite a few scripts that work in this fashion, divided into **Shell Components** and **Built-In Components**.

Shell Components are essentially spawners. When they're attached to an object, they'll create an object of an associated type when the game starts running. This includes prefabs like time trial rings, buttons, cannons, gust plates, etc. 

Built-In Components don't create an associated object, as they are pure logic systems. These include BGM volumes, JTriggers, JComposers, etc.

---

### Creating Modkit Objects

If you go to the scene hierarchy and right-click, you will notice that alongside the standard selection of items you can add to a level, there's now a "modkit" section. In here you'll find various options to instantiate game logic systems into your level.

Right-click in the hierarchy, go down to **Modkit > Visual > Post Processing** and select it to spruce up your mod. These effects are from the base-game, and by adding this object to your level the player can choose between the regular post processing or several other options (tape reel, rose-tinted, etc). You cannot preview what they'll look like until the game's running.

---

### Working With Cannons and Gust Plates

[(API Reference for Cannons)](#cannon-api-reference)

Mobility is the main focus of Orbo's Odyssey, so naturally you can use cannons and gust plates in your modded levels as well!

Let's go over how to create these objects.

Right-Click in the hierarchy, and navigate to **Modkit > Movement**. Here, you'll find both **Cannon** and **Gust Plate** as creation options. 

Here are the settings for a **Cannon**:

- **Override Player Control** - This determines whether or not the player is allowed to launch themselves from the cannon. If set to true, they cannot move unless the cannon has **LaunchPlayer()** called on it.
- **Enable Spotlight** - Cannons have a white spotlight attached to them to better emphasize them against darker environments. This is default behavior, but you can also disable the light on cannons if it conflicts with your mod.
- **Cannon Head** - This is pre-assigned if you create the cannon through the Modkit creation menu, but essentially the cannon head determines what direction the player will be launched in when the cannon is activated. If you open up the cannon object in the hierarchy you can find the cannon head as a child. Feel free to rotate and re-orient this head as necessary to control the launch direction.
- **Launch Force** - The amount of force, in units per second, that the game will apply to Orbo when launching out of the cannon.
- **On Cannon Enter** - This is a JTrigger event that will be ran when the player touches the cannon.
- **On Cannon Exit** - This is a JTrigger event that will be ran when the player is launched out of the cannon.

And here are the settings for a **Gust Plate**:

- **Launch Force** -  How much upward force, in units per second, to apply to the player when struck.
- **On Gust Plate Use** - This is a JTrigger event that will be ran when the gust plate is struck with a melee attack.

---

### Working With Level Binders

A level binder allows you to customize a couple settings for the scene it's in.

To create one, Right-Click in the hierarchy and navigate to **Modkit > Management > Level Binder**.

You can define the kill plane for your level and tie JTriggers into a couple global game events.

**On Player Death** has a few applications, but one of the more interesting ones is to create a sort of old-school game system where dying respawns you at a hub level. You could also introduce a sort of lives system that gets reduced each time the player dies.

**On Player Get Gear Part** is the global event for collecting any gear part, regardless if you've already collected it or not. Similar to the on player death event, you could use this to have the player return to a hub once they've collected a level's gear.

---

### Working With Gear Parts

[(API Reference for Gear Parts)](#gear-part-api-reference)

Right-Click in the hierarchy and navigate to **Modkit > Items > Gear Part**.

Gear parts are pretty simple, so we'll just review their settings and you can get to grips with them on your own!

- **Gear Part Override ID** - This is an optional override you can provide if you'd like to access the trigger for this gear part on your end. This trigger returns 0 if the part hasn't been collected, and 1 if it has.
- **Active By Default** - This allows you to either keep the gear part exposed when the scene's loaded, or to have it hidden so it can be exposed as a part of an objective. By default this is on.
- (if Active By Default is false) **Poof In When Activated** - Determines whether or not this part should create a lil poof effect when exposed. True by default.
- **On Collection Cutscene End** - This is a JTrigger that will be invoked when the cutscene where Orbo collects the gear part ends. This only runs if we've already collected the gear part before.
- **On Collection Cutscene End First Time** - This is a JTrigger that will be invoked the first time we collect a specific gear part. Do you want to release a bunch of screaming murderers into a room when the player gets a treat? Your call.

---

### Working With Screen Shakers

[(API Reference for Screen Shakers)](#screen-shaker-api-reference)

Right-Click in the hierarchy and navigate to **Modkit > Visual > Screen Shaker**.

Sometimes you will find yourself in a situation where a single shake of the screen isn't enough. For example, if you have a large stone slab moving you might want the screen to be shaking the entire time it's shifting.

This is where you use a Screen Shaker.

Here are the parameters:

- **Active** - If this is true, the screen shaker will shake the screen.
- **Global** - Determines whether or not this screen shaker will affect the camera no matter where the player is, or if it'll only affect the player if they're within range.
- **Intensity** - The amount the camera should be shaken, in units.
- (If Global is false) **Minimum Range** - If the player is within this range, the intensity is at full effect.
- (If Global is false) **Maximum Range** - If the player is within this range, they start to get affected by screen shake at the lowest intensity possible. The closer they get to the Minimum Range, the more intense the effect becomes.

---

### Working With A Size Reference

You can Right-Click in the hierarchy and navigate to **Modkit > Misc > Size Reference** to make a size reference. This shows you how tiny Orbo is in the grand scheme of things.

Also it has a readonly Size Reference value that oughta help you if you're trying to create models that need to match Orbo's scale.

---

### Working With Kill Triggers and Hurt Volumes

[(API Reference for Player Kill Trigger)](#player-kill-trigger-api-reference)

[(API Reference for Hurt Volumes)](#hurt-volume-api-reference)

If you Right-Click in the hierarchy and navigate to **Modkit > Pain** you will find both **Player Kill Trigger** and **Hurt Volume** as options.

The player kill trigger will instantly end the player's life if Orbo crosses its threshold. You can use this to introduce death pits or killer spikes into your levels.

The hurt volume is a bit different:

**Volume Alignment** determines what types of damageable the hurt volume is allowed to hit. If the alignment is with the player, it won't hurt the player but will hurt damageables aligned as an enemy. If it's neutral, it'll hurt everything that touches it. And finally, if the hurt volume is an enemy it won't hurt other enemy damageables, but will hurt the player.

**Damage Amount** is, of course, the amount of damage to inflict per-tick.

**Damage Timer** defines how frequently hurt volumes can deal damage to damageables inside of its volume. This is essentially a countdown, so if you leave it as its default **0.1**, then every 0.1 seconds it'll damage everything inside of its volume, meaning a total of 10 ticks per second.

Both the Player Kill Trigger and Hurt Volume objects have an **Active** toggle that determines if they should actually try to deal damage.

---

### Working With BGMs

While playing sound effects is fairly straight-forward using JTrigger and JComposer, music has to use a standalone system to help blend between different tracks based on the game's state.

This is handled through the BGM Volume system, which is a straight forward solution for managing music in your modded level.

A BGM Volume can be created by Right-Clicking in the hierarchy and navigating to **Modkit > Audio > BGM Volume**

BGM Volumes can either be configured as global or local.

Global volumes will affect the current track no matter what. These are typically used to change/stop tracks during special moments, or to otherwise provide the general background music for your level.

Local volumes affect the current track if the player occupies their space. You can use any of unity's built-in physics colliders (except for mesh colliders) configured as a trigger to work with the local volume system.

The priority value of a volume determines how important it is compared to other volumes. If you activate a global volume with a higher priority than one that already exists, it'll crossfade the current song into the new volume's track.

Higher priority can be helpful with local volumes, as you can create zones in your level where the BGM changes, and once you exit them it'll default back to the standard track (or whichever track is next in line).

To control whether or not a global/local volume can actually contribute to the level BGM, you can use **GameObject > SetActive()** with JTrigger to turn them on and off.

---

### Working With Slab Doors and Keys

Slab Doors are intended as a very easy solution for adding gated areas to your modded level.

To create a slab door, Right-Click in the hierarchy and navigate to **Modkit > Dynamic > Slab Door**. They come in four flavors: **red**, **green**, **pink** and **blue**.

A slab door is opened with its associated key orb. To create a key orb, Right-Click in the hierarchy and navigate to **Modkit > Items > Key Orb**.

These are very self-explanatory systems, so I'm mostly just going to explain the settings for Slab Doors (since the only setting for a key orb is its color).

Slab Doors should have their front facing the direction the player will approach them from, since they do not have any details on the other side. The front side is designated by a white arrow.

**Displacement Goal** determines how far the slab door must move from its current position to be considered "open". This is determined in world-space, so if you shrink or expand your slab door you will have to readjust the displacement goal to match.

**Displacement Speed** determines how fast, in seconds, the door will reach its goal.

**Dust Offset** determines where the dust effect for the door opening will be positioned when it's unlocked. Typically you should adjust the offset so that the white boundary gizmo is aligned to the floor.

**On Unlock Door Slab** is a JTrigger hook that will run events once a door receives its key orb. 

---

### Working With Checkpoints

[(API Reference for Checkpoints)](#checkpoint-api-reference)

You can create a checkpoint by Right-Clicking in the hierarchy and navigating to **Modkit > Dynamic > Checkpoint**.

You'll see that the checkpoint has two parts when rendered in the scene view: a green orb, and a cube with an arrow.

The green orb is the actual checkpoint itself, while the green cube and arrow is a proxy object that represents where the player will be spawned if they die while this checkpoint is active.

If you open the checkpoint object in the hierarchy, you can grab and move the player spawn point wherever you please. Keep in mind that while you can rotate this spawnpoint all you want, **it will always align to World-Up when spawning the player**. Only the yaw (the Y-axis) is accounted for when spawning Orbo.

Checkpoints have two events that can be hooked into with JTriggers: 

**On Checkpoint Activate** - Only runs when a checkpoint has been established for the first time.

**On Checkpoint Strike** - Runs anytime the player melees the checkpoint, even on the first time they've activated it. 

On a final note, checkpoints can be activated without needing to be struck by calling **SetCheckpointAsCurrent()** or **SetCheckpointAsCurrentQuietly()** via JTrigger. The first function will make the checkpoint activate as though you struck it (with all the sounds and gravitas you'd expect), while the second is a silent method that can be used to covertly enable checkpoints without the player realizing. 

----

### Working With Interactable Objects

[(API Reference for Interactable_Events)](#interactable_events-api-reference)

To construct an interactable object, let's start by navigating to **Modkit > Event > Interactable Events**. 

Interactables make up a large segment of player interaction, and are important for creating certain game elements such as NPCs or puzzles. They are a type of [World Object](#what-are-world-objects), so please review the documentation for World Objects to learn more about the settings under World Object - General Settings.

The interactable you've just created will not be useful unless you tie a JTrigger into its **On Interaction** property in the Interactable - General Settings foldout.

The final thing to highlight about interactables is the Interact Prompt object, which is an optional way to show the player when something can be interacted with.

Navigate to **Modkit > Event > Interact Prompt** to create a new prompt.

In the scene view your new prompt will show two orbs (one red, and one green) with a line connecting them. This represents where the interact prompt will shift to when the player is within range to interact with its associated interactable. You can scale, rotate and adjust the **Prompt Active Displacement** setting to change how this is displayed.

The important part of the Interact Prompt object is the **Target** property, which tells the Interact Prompt which interactable object it's working with. You should assign this to your newly created interactable object.

And you're done! Once you've got a JTrigger hooked up to the **On Interaction** and the player's standing inside of the interactable object's volume, you can press the interact button to run some events.

---

### Working With Damageables

[(API Reference for Damageable_Events)](#damageable_events-api-reference)

Damageables are a type of object the player can melee, and eventually kill. They are a type of [World Object](#what-are-world-objects), so please review the documentation for World Objects to learn more about the settings under World Object - General Settings.

You can create one with **Modkit > Damageable > Events**.

Let's check the foldout under Damageable Events - Triggers:

- **On Damageable Start** - This is ran when the object first comes into existence.
- **On Damageable Take Damage** - This occurs whenever this damageable takes damage, whether from a player's melee or from a hurt volume.
- **On Damageable Death** - When the damageable runs out of health, it'll run this. It's your responsibility to decide how the damageable should be disposed of, whether you use a JTrigger to deactivate the object, or something else. You can also use this event to create some death effects, like a poof or an explosion.

---

### What Are World Objects?

You may have noticed some object types include a foldout section labelled **World Object - General Settings**, which contains several options.

World Objects are a general definition for objects that need to contain additional data for damage handling. The player is a type of world object, interactables are a type of world object, and damageables are a type of world object.

Let's review the properties of a world object so you can understand how to configure them:

**Center Override** - This is used to provide a proxy-object that defines an object's center. For Orbo, this is used because his object's origin is at his feet, but there are many functions that might need to know where his core is. As you might expect from the inclusion of "override" in the title, this setting is optional. It'll automatically configure itself to the origin of the current object if a center is not defined.

**Primary Renderer** - This is a fairly unused property that can generally be ignored, but if your world object has a renderer you might as well just attach it to this for the sake of it. It was originally used for an earlier implementation of damageables, but it was sort of made obsolete. A few campaign world objects repurpose it for some niche usages, so it was kept.

**Is Invincible** - You typically want to set this as true if you're creating an NPC, because otherwise it's possible to melee and kill them. This just prevents world objects from taking damage.

**Has Recoil** - When you deal damage to world objects they're typically expected to launch the player away from them. You can set this to true to keep that behavior, or false if you don't want the player to get knocked back.

**Max World Object Health** - Self explanatory! This more relevant for Damageable - Events since you're intended to kill them.

---

### Working With Buttons

[(API Reference for Buttons)](#button-api-reference)

To create a button, navigate to **Modkit > Event > Button**.

Buttons have a few different states, including timed buttons, impulse buttons, and special buttons. We'll start by talking about the properties shared between them, and then we can talk about the unique properties each type has.

**On Button Press** - This is the JTrigger to run when a button is struck. For impulse and timed buttons this can be invoked infinitely, but standard buttons will only invoke it once until the level is reloaded.

**Trigger To Affect** - This trigger is a requirement for your button, which will store the button's current state in the scene. If Invert Trigger Output is false, your button trigger will be 0, and turn to 1 when it's pressed. Impulse buttons don't use this trigger since they don't have a permanent state to rest in, so it can be safely ignored as long as you're using an impulse.

**Invert Trigger Output** - This takes the trigger output from the previous property and inverts the value the trigger sets. So if this is set to true, then the button's trigger will be 1 by default, and will turn to 0 when pressed. Again, this is irrelevant for impulse buttons.

Now, let's talk about the different button types:

**Timed Buttons**

- Timed buttons will spawn with a timer ring around them that will count down before running some events.
- **Button Time** - When a timed button is struck, this is how long it takes for the button to reset. If the player strikes a timed button while it's already pressed, it'll reset the countdown back to this value.
- **On Button Timer End** - This is a JTrigger to run when the button re-emerges after the countdown runs out.

**Impulse Buttons**

- Impulse buttons never enter a proper "pressed" state, and instead will just invoke the **On Button Press** event when struck.

**Special Buttons**

- These don't have any special API applications lol, they just make a button blue instead of orange. Timed buttons won't accept this setting because a special button variant was never made. This does work on Impulse and Standard buttons, though.

---

### Working With Scene Transition Volumes

[(API Reference for Scene Transition Volumes)](#scene-transition-volume-api-reference)

You're probably going to want more than a single level in your modded campaign, and that's where Scene Transition Volumes become necessary.

To create one, navigate to **Modkit > Event > Scene Transition Volume**.

The properties for the Scene Transition Volume are as follows:

- **Destination Scene** - This determines which scene inside of your mod to load. This only works with scenes included inside of your mod core file's scenes list.
- **Fade Color** - Determines the color to use for the transition.
- **Fade Time** - Describes, in seconds, how long it takes to fade out/in.
- **Destination Position** - This is the position the player will spawn in when the new scene loads.
- **Destination Rotation Yaw** - This describes, in degrees, the yaw of the player when they spawn into the new scene.
- **Pop Orbo** - This makes orbo explode into little white balls when transitioning. This effect is used when entering portals in the main campaign.

Scene transition volumes will run when the player enters their volume, but it's also possible to force a volume-less Scene Transition to occur through JTrigger. This is covered further in the [API Reference for Scene Transition Volumes](#scene-transition-volume-api-reference).

---

### Working With Pips

Before we start doing anything, we'll add a gear part. Navigate to **Modkit > Items > Gear Part**, and we can place this wherever we please. We're going to go into this object's inspector and just turn **Active By Default** off. You'll see why soon enough.

Let's set up some pips. Go to **Modkit > Management > Pip Manager**. This object determines how many pips we need to collect before a gear part appears. Inside the pip manager's inspector, we'll configure the **Total Pips** to be 50. Next, we'll take that gear part we made previously and link it into the **Gear Part To Reveal** setting. Remember how we turned "Active By Default" off? This is why. Now, we're going to set up one final thing for our manager, and that's a camera position for when the gear part is revealed. Create a new empty Game Object. Now, move your scene-view camera to look at the gear part however you please. Then, with the new empty Game Object selected, we'll go to the toolbar at the top of the editor window, and hit **GameObject > Align With View**. This will snap the new Game Object to our camera orientation, and we're basically set! The last thing to do for the manager is to drag this new Game Object to the **Gear Part Camera Position** setting in the manager's inspector.

Okay! Now we've got the manager done. Let's get some pips set up.

Go to **Modkit > Items > Pip Placer**. You'll notice a single pip appear in the scene. We can go to this placer object, and by changing the **Arrangement Type** we can determine how to construct a set of pips. There are four settings, Single, Ring, Line and Cross. Select **Ring** and you'll notice some new settings pop up in the inspector.

Set the **Pip Count** to 50, and the **Ring Radius** to **7**. Find a nice spot for your pip ring, and you're good to go!

Before we move onto Time Trials, let's test out what we've done so far. Go to your mod core file, select "Build Mod", and take the generated mod folder and move it into your game's mod directory. Run the game, and you should be able to collect all 50 pips to unlock a gear part!

---

### Working With Time Trials

Navigate to **Modkit > Management > Trial Manager**.

To start, we're going to be setting the *Time Ttrial ID*. If you see an ID field and it doesn't include "Override" in its title, then you need to provide a unique name for it. For the purposes of this tutorial, we'll just label **Time Trial ID** as **Trial_Tutorial**.

As with the Pip Manager, you should create a new gear part and disable **Active By Default**. Then make a new Game Object, align it to your camera as seen in the pip manager instructions, and then we can take these two items and apply them to the Time Trial Manager's **Gear Part To Reveal** and **Gear Part Camera Position** field respectively.

Now we should move onto the process of actually making the time trial.

Go to **Modkit > Dynamic > Trial Ring**.

Take the constructed trial ring, and position it where you'd like the time trial to start. After that, duplicate the trial ring, and add some more to create the path of the time trial.

Now, to assign them to the time trial manager.

Select your time trial manager, and in the upper-right part of the inspector you should see a lock icon. Hit that, and it'll prevent your inspector from losing focus on the time trial manager.

Now you're going to select the time trial rings in the order you want the player to clear them. Take this group of rings, and drag them into the **Rings** array in the time trial manager's inspector.

Congratulations! You've made a time trial. Go ahead and select the lock icon again to turn it off, save your scene, then go to the mod core file and re-build your mod. Bring it into the game's mod directory, and now you can test your time trial out!

To formulate your dev and green times, I recommend clearing the time trial as fast as you possibly can first. This is, of course, the dev time. In my experience, the green time is typically about 5 seconds longer than that. Of course if your time trial is quite short (or achieving a dev time cuts too close to the maximum time limit), you need to increase the time limit because the average player is unlikely to perform as well as you can.

---

## Additional Components

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

[(API Reference for Constant Mover)](#constant-mover-api-reference)

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

[(API Reference for Speaker Targets)](#speaker-target-api-reference)

This is an interface component that JComposers will use to inform an NPC when they're speaking in a dialog.

They only have a single parameter: A target [NPC Mood Controller](#NPC Mood Controller).

---

### Speaker Target Animator

[(API Reference for Speaker Target Animators)](#speaker-target-animator-api-reference)

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

[(API Reference for NPC Mood Controller)](#npc-mood-controller-api-reference)

This component is a system designed to handle texture-based animations for NPCs.

It uses what are known as Moods, which are data files that contain information about what textures to use, and which emotional states they're tied into.

To create a mood for the NPC Mood Controller, Right-Click in your project folder, and navigate to **Create > Create New Mood**. 

Here you can define the state this mood is associated with, a texture for the idle expression, a blinking graphic, and then a set of images for a talk cycle.

Once you've got a mood, you can head to the NPC Mood Controller component and assign it to the **Mood Pool** list. This will allow the NPC to access this mood's texture data when the mood is changed.

Now, we'll go through the other properties of the NPC Mood Controller:

- **Target Renderer** - Defines which renderer will have its textures modified with the data in the current mood.
- **Target Material Index** - Defines which material on the renderer we're going to target. You can leave this at 0 if you only have a single material on your target, but if you add additional materials this value must match the index of your desired material on the renderer.
- **Target Properties** - Defines which properties in the material are being modified. You can assign multiple properties here, but by default you will typically assign a single property labelled *\_BaseMap*, which is the default main texture property on URP Lit shaders. 
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

[(API Reference for Rotate To Face Transform)](#rotate-to-face-transform-api-reference)

Also very straightforward:

- **Target** - The transform to try and look at.
- **Rotation Speed** - How quickly to lerp to the target. If set at 0.1, it'll rotate 10% of the way to face its target every second.
- **Lock Y** - This prevents rotation that affects this object on the Y axis. The main use-case is to allow for objects that look in the player's direction, but not up or down at them. This might be applied to an NPC's body, while their head doesn't have Lock Y enabled so it can look up and down.

---

### Squash And Stretch

[(API Reference for Squash And Stretch)](#squash-and-stretch-api-reference)

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

[(API Reference for Transform Mover)](#transform-mover-api-reference)

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

## Working With JTrigger

[(API Reference for JTrigger)](#jtrigger-api-reference)

The modding toolset for Orbo's Odyssey doesn't include the ability to import custom scripts. Instead, level logic is built around the JTrigger system, a visual scripting tool that operates a lot like an if()else{} conditional branch.

JTriggers are built around contexts and trigger values. Contexts define when a JTrigger should perform its conditional check, and the trigger values make up the stored inter-scene data as an integer value.

JTriggers themselves are quite simple, but have a great deal of versatility when chained together. It's nowhere near as powerful as native C# code, but the ability to quickly set up events based on player actions is more than enough to build fairly interesting levels.

So let's start learning how to use JTrigger!

---

### Creating Your First JTrigger

Right-Click in the hierarchy and navigate to **Modkit > Event > JTrigger**.

We now have a generic trigger instance. We're going to go through all of the options to get accustomed to the system.

**One Shot** causes a JTrigger to fire only a single time. It'll reset itself when you re-enter the level the JTrigger is contained in.

**Context** is the most important setting, since it determines when a JTrigger is supposed to run. By default, it'll be set to On Demand. The available options are as follows:

- **On Start**: When the level is first loaded, this JTrigger will activate. This can be useful for quite a few reasons, like pre-setting trigger values or changing parts of the level to reflect trigger changes in another (in Orbo this is used to display Bunston's Scone's dead body if he's been fed the radioactive cake).
- **On Trigger**: If the associated JTrigger object has a collider on it configured as a trigger and detects an object with a matching tag entering its volume, it will run its events. By default it will seek out the player. This makes up the bulk of player interactions with the JTrigger system, but you can also have other physics objects trigger this behavior as well.
- **On Timer**: Once the timer runs out, JTrigger will run its events. If this context is selected, JTrigger will expose a *Trigger Timer Range* field that you can modify. JTrigger will pick a random value in the range between X and Y and assign it to its internal clock before counting down. To have the timer run consistently, just set both X and Y to the same value. Timers only run while the object is active, so if you want to control when the countdown occurs, simply deactivate the JTrigger object, and activate it via another JTrigger to start counting. Timers will reset themselves once they run out, so if you'd like to have it only run once, either set *One Shot* to true, or have the timer deactivate its own game object at the end of its Events().
- **On Trigger Exit**: It's the same as the On Trigger context, but events are run whenever the object with the associated tag leaves its trigger collider.
- **On Demand**: The default JTrigger value. This will cause JTriggers to only run if another JTrigger or logic object invokes OnDemand() on it. This is helpful as a code storage block you can call from other objects.

**Logic Table - Trigger Requirement** is the actual set of conditions a trigger must validate in order to run its Events. If this field is left empty, a JTrigger will run its events immediately upon the context being met.

Pressing the little + button will create a new condition to fulfill. Inside of the condition is a **Trigger Address**, a **Comparison Type**, and a **Reference Value**.

All triggers are stored as integers and accessed via string. They will default to 0 if a value is not pre-assigned to them. Triggers are also kept between levels, so you can use them to store data mod-wide.

The **Trigger Address** is the internal ID JTrigger uses to look up specific trigger values.

The **Comparison Type** is the operation to perform between the value returned by the Trigger Address and the Reference Value. By default it will check to see if the value at the trigger address equals the reference value, but you can use any of the typical comparison operators.

The **Reference Value** is matched to the value address by the comparison type.

If you make more than one conditional, you will see a boolean operator block show up between them. These comparisons run from top-to-bottom, so if you have, say:

```[CONDITIONAL]
[CONDITIONAL]

[AND]

[CONDITIONAL]

[OR]

[CONDITIONAL]
```

The JTrigger will compare the first two to see if they're both true, and if either the result of that or the final conditional are true, then it executes the code.

**Events** are a list of object functions JTrigger will run when a context is met and all conditionals return back as true. They are structured as UnityEvents.

When you add an event, it will be empty. Drag the gameobject you want to affect into the None (Object) slot, and the dropdown next to it will activate and allow you to pick an function to run.

A typical use case is to drag the JTrigger itself into the object slot, that way you can run some of its API events on itself. This is particularly helpful if you want to make changes to the player's state or modify trigger values, since JTrigger has a bunch of helper functions packed into it.

To learn about the available API functions across all of the modkit's object types, visit the [API Reference](#api-reference) in this documentation.

**Events On Fail** is the same as the Events field, but these will be run if the context is met and the conditionals return back as false. If you don't provide any conditionals, Events On Fail will never run, since the JTrigger will always evaluate empty conditionals as true.

---

### Setting JTrigger Values

Let's get accustomed to one of the most important API functions: TriggerSet().

- Make a new JTrigger object. 
- Change its context to On Start.
- Leave the conditionals empty so this trigger is guaranteed to run when the level begins.
- In the Events, make a new event.
- Drag this JTrigger object into the Object slot.
- Click the function dropdown, and navigate to **JTrigger > TriggerSet (string)**
- Now, let's type in **my_trigger|5** (all arguments using the JTrigger system are delineated by **|**, which is accessed by holding shift and pressing backslash)

Here's what'll happen:

When you build your mod and run it, this JTrigger object will activate on level start, and will set the trigger at **my_trigger** to **5**.

This is the most basic approach to working with JTriggers. Instead of numbers, you can also provide other JTrigger IDs into this operator, and it'll set the JTrigger value on the left hand to the one found at the address on the right, as in:

**my_trigger|0**

**other_trigger|5**

**my_trigger|other_trigger**

and the end result is that **my_trigger** equals **5**.

Finally, all trigger modification operations support using random values, which can be done by adding an additional | and providing another value, like so:

**my_trigger|5|9**

Which will pick a random number between 5 and 8 (random number generation with integers in Unity is upper-exclusive, since it's intended for use in arrays) and assign it to my_trigger.

You'll also find that there are **TriggerAdd**, **TriggerSubtract**, **TriggerMultiply**, and **TriggerDivide** options as well. These are self explanatory, and use the same syntax as the TriggerSet function.

Attempting to divide by 0 will not run the operation, and will instead create a popup in-game telling you that a divide by 0 error has occurred.

---

## Working With JComposer

[(API Reference for JComposer)](#jcomposer-api-reference)

JComposer is a toolset designed for creating sequenced events, cutscenes and NPCs.

Some behavior in JComposer is covered in detail in the reference section, but for now we're going to take it steady, learn the toolset, and discuss some common use-cases.

---

### Creating Your First JComposer

To begin, Right-Click in the hierarchy, and navigate to **Modkit > Event > JComposer**.

This will create an empty JComposer object. Just like we did with JTrigger, we'll visit each of the properties and talk about how they work.

**Loop** determines if the JComposer should run its sequence repeatedly until told to stop. By default this is turned off since we're expecting to use JComposer mainly for cutscenes and NPCs, but if you want to create a precisely timed series of events in your level that need to run on a clock, then you can set this to true.

**Sequence** is the meat and potatoes of the JComposer. Pressing the + button will create a new sequence part, which we will dissect:

- **Progress Type** determines what condition this sequence part is waiting on before it'll progress to the next part. This includes **Delay**, **Any Key** and **Dialog Part** as options. Delay waits for a set amount of time, Any Key will hold until the user presses a controller button, keyboard key or mouse button to progress, and dialog part will present the player with some dialog that must be completed before it'll progress. We'll come back to that last one later because it's the core that allows NPCs to function.
- **Camera Changes Are Snap** is a helper toggle that determines whether or not changes to the camera made on this sequence part are going to smoothly interpolate into position, or snap. Smooth interpolation feels good when interacting with characters, while snap tends to feel good when displaying an event that occurs in another room.
- **Sequence Event** determines what events this sequence part will run once the composer has reached it. It's just like JTrigger, where it can run functions attached to objects. If you place the JComposer into the Object slot, you can access some API handling for managing the camera and modifying the player's state. These are pivotal for cutscenes, but we'll wait a moment before we discuss the JComposer API. A helpful reminder: as implied at the start here, JComposer will run events the moment a sequence part is reached. So your first sequence part's events run the second the composer is activated by a logic system. If you need to wait before running the first part of your composer, you can make a sequence part that has no events, set the progress type to delay, and configure the delay time for as long as you need.

**Speaker Targets** are used in the dialog system as a reference table for NPCs. By default the player is pre-defined as its own speaker target, so the only ones you need to add into this list are other characters in a dialog. If your cutscene lacks dialog, or the dialog only involves Orbo or the Narrator, then you can safely ignore this property.

---

### Making Your First Cutscene

Now that we've gotten to grips with the individual parts of a JComposer, let's try making a simple cutscene that locks the player down, moves the camera to look at an orb, shakes the camera violently, and then returns the camera back to the player.

Create a JComposer, and let's begin by making a new sequence part. This part will be responsible for locking the player down and taking control of the camera.

We're going to first set the delay time to be **1.5 seconds**. This is because this first sequence part will just modify the camera, and the next sequence part is where we'll shake it.

Now, in the sequence event section we're going to add a few events. Click the "+" button, and link the JComposer into the object slot. 

Our first event will be under **JComposer > SetPlayerLockState()**. We're going to set this to true, which will completely lock the player's position down during the cutscene so they cannot move.

Our next event will be **JComposer > SetPlayerCinematicBarState()**, which will be true so we get some nice cinematic bars.

After that we'll add **JComposer > SetPlayerCameraPosition()**. For this function, we're going to make a gameobject to be our camera boom, similar to how we did it for the pip and time trial managers. Create the object, use **GameObject > Align With View** in the toolbar, and then assign this new empty object to this function's input.

Next we'll use **JComposer > SetPlayerViewTarget()**. This time, create a sphere by Right-Clicking in the hierarchy and going to **3D Object > Sphere**. Now we're going to use this sphere as the input for this function.

Now, make a new sequence part. This is where the camera shake will be applied.

Add a delay of **1.5 seconds** to this part as well.

Unity has a tendency to copy the information stored in the last element when you add something new to an array, so we're going to clear out the events of this sequence part, since we only need one event.

Add a new function under **JComposer > SetPlayerCameraShake()**. The input for this will be a float, which we can set to **1** to shake the camera around by 1 world unit.

Now, let's add our final sequence part, which will terminate the cutscene. Think of it as the inverse of the events we did in the first sequence part.

The delay for this part can be **0 seconds**, since it's the end.

Our first event function will be **JComposer > SetPlayerLockState()**, but this time we'll set it to false, to allow the player to move again.

Next we'll use **JComposer > SetPlayerCinematicBarState()**, and set this to false too so we can remove the cinematic bars.

Now we're going to use **JComposer > ReturnPlayerCamera()** to cancel both SetCameraPosition() and SetCameraViewTarget().

And finally, now that the player has been restored to their initial condition, we'll terminate the sequence by using **JComposer > SetComposerState()** and set this to false. It's not a necessity that you do this, but it does make it feel more concrete when you're ending a sequence.

Alright! Congratulations on sticking through that, I know it was tedious. Our composer is done, but you might have realized something: It's not going to do anything! This is because we have to activate it by using **JComposer > SetComposerState()** from a JTrigger.

Let's make a JTrigger, mark it as One Shot, set the context to Timer, set both sides of the timer range to 4, and then in this JTrigger's events, we'll add a new function that calls **JComposer > SetComposerState()** and mark this as **true**.

And there we have it! Go ahead and build your mod, add it to the game's mod directory, and we can finally test this cutscene in action.

Once you load in, after 4 seconds Orbo will be locked down, the camera will move and rotate to look at the orb, and after 1.5 seconds the camera will shake, and after another 1.5 seconds we'll be back to normal.

---

### Making Your First NPC

If you've already learned how to make your first cutscene, you'll find that making a custom NPC is fairly similar.

Let's begin by creating an interactable capsule we can chat with.

To begin, Right-Click in the hierarchy and navigate to **3D Object > Capsule**.

Next, we'll need to add an interactable handler to our capsule. Use **Add Component** in the capsule's inspector and search for **Interactable_Events**.

Now in the Interactable_Events' **World Object - General Settings** foldout, we're going to turn on **Is Invincible**, so our NPC doesn't get murdered if we melee it.

Now we need an **Interact Prompt**. These can be created by navigating to **Modkit > Event > Interact Prompt**. You should make it a child of your capsule, and then adjust the settings until the green sphere is visible over the capsule's top. After that, drag the capsule into the Interact Prompt's **Target** parameter so it can connect with your interactable.

Okay! Making progress. Next, we're going to create a new JTrigger (**Modkit > Event > JTrigger**) configured as **On Demand**, and we'll use this to actually start up our NPC dialog. Head to the capsule and drag the new JTrigger into the Interactable_Events' **On Interaction** slot.

But wait! **Interactable_Events** needs a trigger volume so it knows when the player is in a good spot to interact! We'll just add a new **Sphere Collider** component to our capsule, and mark **Is Trigger** as true. Good! Now we'll actually be detected when we approach the interactable.

Okay! So our capsule's interactable now, but we don't actually have an NPC dialog to use it on. We'll get around to that in a bit, but before we can start working on the dialog we need to add a simple yet very important part to our capsule: a **Speaker Target** component.

**Speaker Targets** allow JComposer to differentiate between several speakers in a conversation. All we have to do is add a Speaker Target component to our capsule (by using **Add Component** and looking up **Speaker Target**) and we're good to go!

(For the purposes of this tutorial, we're going to leave the **Mood Controller** slot on our speaker target empty. If you'd like to learn more about having NPCs express and use talk cycles, visit the references for [Speaker Target](#speaker-target), [Speaker Target Animator](#speaker-target-animator), and [NPC Mood Controller](#npc-mood-controller)).

Let's start working on the dialog.

Create a JComposer, and let's begin by making a new sequence part. Like last time, this part will be responsible for locking the player down and taking control of the camera.

Instead of leaving our progress type as Delay, we need to swap to **Dialog**.

You'll notice some new settings pop up once we do this:

- **Speaker Name** - Determines what name will come up on the name plate when a character speaks. If you leave this empty, the game will hide the name-plate (which is good for narration). This field fully supports both Text Mesh Pro and Text Animator markup tags, which can be found in the [text markup reference]().
- **Speaker Dialog** - The actual dialog characters have. This field fully supports both Text Mesh Pro and Text Animator markup tags, which can be found in the [text markup reference]().
- **Speaker Talk Sounds** - This is a list of sounds to randomly choose between when a character speaks.
- **Speaker Target** - This is the actual character instance that's speaking. **Narration** is used when no character should speak (this will also hide the nameplate by default), **Player** is for Orbo, and the **NPCs 0 - 9** can be customized by adding new targets to the JComposer's **Speaker Targets** array. 

Since we want our NPC to be the first to speak, we need to set the **Speaker Target** for this sequence part to **NPC0**.

Now, in the sequence event section we're going to add a few events. Click the "+" button, and link the JComposer into the object slot. 

Our first event will be under **JComposer > SetPlayerControlState()**. We're going to set this to false, which will completely disable the player's controls during the dialog so they cannot move.

After that we'll add **JComposer > SetPlayerCameraPosition()**. For this function, we're going to make a gameobject to be our camera boom for the NPC dialog, similar to how we did it for our first cutscene. Create the empty object, use **GameObject > Align With View** in the toolbar, and then assign this new empty object to the function's input.

Next we'll use **JComposer > SetPlayerViewTarget()**. This time, however, we're going to assign our NPC's transform to the input of this function so the camera looks at them.

Okay! Now for the fun part. **Write whatever you want for your new character!** They're going to be the first to speak, after all.

Now, let's address how to make Orbo respond to them.

Create a new sequence part. You might want to clear out the old data so we can start fresh.

Just like last time, this sequence part will be a **Dialog**, but we'll set the speaker name as "Orbo" and you can write whatever you'd like for the dialog.

As for the talk sounds, **You can copy the Orbo talk sound from the example mod into your own mod content folder**. Then we'll just assign the Snd_Talk_Orbo sample to this array.

The **Speaker Target** setting has a pre-made option for the player, so just switch it to that.

And finally, we need to actually establish a view target for the player! To do that, we can create a new empty object and use **Add Component** to add a **Track To Player Head** component. This will allow us to use this new empty object as a proxy for the player's head.

Let's return to our JComposer, and for this sequence event we'll use **JComposer > SetPlayerViewTarget()** and feed in our new track to player head proxy as the input. And there we go! Now the camera will look at Orbo when we get to this part.



To add more dialog to this exchange, just repeat adding new sequence parts and use **JComposer > SetPlayerViewTarget()** to change who we're looking at! You can also change the camera angle or anything else you'd like as you go. Just treat the NPC conversation like it's a cutscene.

Once you're done, we just need to create a final sequence part that will wrap up the conversation:

This sequence part should be configured as a **Delay** with a **Delay Time** of **0**.

Then, we need the following events:

- **JComposer > ReturnPlayerCamera()**
- **JComposer > SetPlayerControlState()**, set to **true** to return the player's controls.
- **JComposer > SetComposerState()**, set to **false** to end the composer sequence.

**BUT WAIT!** Remember, JComposers don't just automatically run on their own! Let's head to the JTrigger we made for this NPC, and add a new object function that targets our composer and calls **JComposer > SetComposerState()** and sets it to **true**,

Congratulations! You've just made your first NPC!

If you'd like to do more advanced stuff during cutscenes and dialog, visit the [API Reference for JComposer]() and you can find a list of functions to use to spice things up.

---

## Working With JSwitch

[(API Reference for JSwitch)](#jswitch-api-reference)

While a JTrigger is essentially an if()else{} statement, a JSwitch is... Well, a switch statement.

The main difference is that JSwitches have a priority system, because unlike a switch statement multiple individual branches of a JSwitch can be true at once.

Anyhow, we'll briefly discuss the settings for a JSwitch and then we can talk about actually using one.

The **Branches** setting is the actual array of conditionals you'll define. A branch element has three settings: 

- **Conditions** - These are the same as JTrigger conditions, and when they're all met this branch is considered valid for operation.
- **Priority** - This is important: Since multiple branches can be true at once, the priority defines which one is more important, and therefor, the one that should be ran once we're done analyzing the branches.
- **Events** - This is a JTrigger, configured as On Demand, that the JSwitch will run if it both its conditions are met and it's priority is the highest among all valid branches.

The **Default Event** setting is an optional event you can provide that will be ran if none of the branches return as true. If you're using a JSwitch to handle dialog progression for an NPC, this can be used to link into an intro conversation.

To actually run a JSwitch, a JTrigger or JComposer must call **JSwitch > Operate()**, which will cause the JSwitch to analyze its branches and subsequently run the winning branch's event as On Demand.

---

## Caveats and Known Issues

Believe it or not: Unity doesn't do a great job supporting most of their features, and I'm also prone to mistakes! So there are some technical issues with mods that I would like to address here.

---

### Lightmapping

Orbo mods support using lightmaps on your models. You can even use light probes!

However, **you must use Non-Directional lightmaps**. Directional lightmaps do not work with the shader variants generated by the scriptable compatibility build pipeline, so your lightmapped objects will break if you do it this way.

---

### Complexity

I tried to integrate a decent chunk of universalized systems so you could try and add fun set pieces that didn't show up in the original game, but I want to acknowledge now that this toolset isn't going to give you everything you could ask for. I made it as a way of saying thanks for the people who bought the game and were kind to me while I was in a very bad place.

Think of this modding toolkit like Nintendo's *Super Mario Maker*. You can get creative with the tools to create levels that feature really intriguing mechanics not present in the base game, but at the end of the day the toolset is just there for you to make fun levels.

The game wasn't built around the idea that it would receive mods, so keep in mind that this modding toolkit is scaffolded around the base game in a way that might make some things feel clunky or strange. I tried to simplify the process where I could, but that's about all I could do. I still have to get to work on my main game lol.

----

### On Trigger Passthrough and Duplicate Events

This is a really helpful system, but keep in mind that if your passthrough's trigger volume overlaps with its source object's trigger volume, it can generate duplicate OnTriggerEnter and OnTriggerExit events on your target object. This might be fine if you're dealing with modkit objects that have one-shot behavior (key orbs, for example, only get collected on the first trigger event and will ignore duplicates), but with JTriggers and Interactables this could cause some unexpected behavior. In these two explicit cases, **try to make sure you're only using passthroughs when there is absolutely no way to have the player make contact with your target object's trigger collider**.

I haven't devised a way to deal with this yet that I feel satisfied with, so maybe at somepoint down the road I'll rework how passthroughs behave with triggerable objects to make it a bit more friendly. For now, just accept that this is a known issue and try to be careful not to walk into a trap.

---

## API Reference

This section covers object functions that can be accessed inside of modkit objects via JTrigger or JComposer.

---

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

### JTrigger API Reference

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

### JComposer API Reference

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

---

### JSwitch API Reference

```
Operate() - This is how you actually use a JSwitch. By calling Operate, the JSwitch will analyze all of its branches and see if it can find ones that have valid conditions. These are ranked by their priority, and the branch with the highest priority has OnDemand() called on its JTrigger. If none of the branches are valid, it can run OnDemand() on a default event if one is assigned.
```
