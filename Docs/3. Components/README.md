# Components

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

[(API Reference for Cannons)](https://github.com/FeverDevJohnny/orbomodkit/tree/main/Docs/API%20Reference)

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

- **Launch Force** - How much upward force, in units per second, to apply to the player when struck.
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

[(API Reference for Gear Parts)](https://github.com/FeverDevJohnny/orbomodkit/tree/main/Docs/API%20Reference)

Right-Click in the hierarchy and navigate to **Modkit > Items > Gear Part**.

Gear parts are pretty simple, so we'll just review their settings and you can get to grips with them on your own!

- **Gear Part Override ID** - This is an optional override you can provide if you'd like to access the trigger for this gear part on your end. This trigger returns 0 if the part hasn't been collected, and 1 if it has.
- **Active By Default** - This allows you to either keep the gear part exposed when the scene's loaded, or to have it hidden so it can be exposed as a part of an objective. By default this is on.
- (if Active By Default is false) **Poof In When Activated** - Determines whether or not this part should create a lil poof effect when exposed. True by default.
- **On Collection Cutscene End** - This is a JTrigger that will be invoked when the cutscene where Orbo collects the gear part ends. This only runs if we've already collected the gear part before.
- **On Collection Cutscene End First Time** - This is a JTrigger that will be invoked the first time we collect a specific gear part. Do you want to release a bunch of screaming murderers into a room when the player gets a treat? Your call.

---

### Working With Screen Shakers

[(API Reference for Screen Shakers)](https://github.com/FeverDevJohnny/orbomodkit/tree/main/Docs/API%20Reference)

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

[(API Reference for Player Kill Trigger)](https://github.com/FeverDevJohnny/orbomodkit/tree/main/Docs/API%20Reference)

[(API Reference for Hurt Volumes)](https://github.com/FeverDevJohnny/orbomodkit/tree/main/Docs/API%20Reference)

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

[(API Reference for Checkpoints)](https://github.com/FeverDevJohnny/orbomodkit/tree/main/Docs/API%20Reference)

You can create a checkpoint by Right-Clicking in the hierarchy and navigating to **Modkit > Dynamic > Checkpoint**.

You'll see that the checkpoint has two parts when rendered in the scene view: a green orb, and a cube with an arrow.

The green orb is the actual checkpoint itself, while the green cube and arrow is a proxy object that represents where the player will be spawned if they die while this checkpoint is active.

If you open the checkpoint object in the hierarchy, you can grab and move the player spawn point wherever you please. Keep in mind that while you can rotate this spawnpoint all you want, **it will always align to World-Up when spawning the player**. Only the yaw (the Y-axis) is accounted for when spawning Orbo.

Checkpoints have two events that can be hooked into with JTriggers:

**On Checkpoint Activate** - Only runs when a checkpoint has been established for the first time.

**On Checkpoint Strike** - Runs anytime the player melees the checkpoint, even on the first time they've activated it.

On a final note, checkpoints can be activated without needing to be struck by calling **SetCheckpointAsCurrent()** or **SetCheckpointAsCurrentQuietly()** via JTrigger. The first function will make the checkpoint activate as though you struck it (with all the sounds and gravitas you'd expect), while the second is a silent method that can be used to covertly enable checkpoints without the player realizing.

---

### Working With Interactable Objects

[(API Reference for Interactable_Events)](https://github.com/FeverDevJohnny/orbomodkit/tree/main/Docs/API%20Reference)

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

[(API Reference for Damageable_Events)](https://github.com/FeverDevJohnny/orbomodkit/tree/main/Docs/API%20Reference)

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

[(API Reference for Buttons)](https://github.com/FeverDevJohnny/orbomodkit/tree/main/Docs/API%20Reference)

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

[(API Reference for Scene Transition Volumes)](https://github.com/FeverDevJohnny/orbomodkit/tree/main/Docs/API%20Reference)

You're probably going to want more than a single level in your modded campaign, and that's where Scene Transition Volumes become necessary.

To create one, navigate to **Modkit > Event > Scene Transition Volume**.

The properties for the Scene Transition Volume are as follows:

- **Destination Scene** - This determines which scene inside of your mod to load. This only works with scenes included inside of your mod core file's scenes list.
- **Fade Color** - Determines the color to use for the transition.
- **Fade Time** - Describes, in seconds, how long it takes to fade out/in.
- **Destination Position** - This is the position the player will spawn in when the new scene loads.
- **Destination Rotation Yaw** - This describes, in degrees, the yaw of the player when they spawn into the new scene.
- **Pop Orbo** - This makes orbo explode into little white balls when transitioning. This effect is used when entering portals in the main campaign.

Scene transition volumes will run when the player enters their volume, but it's also possible to force a volume-less Scene Transition to occur through JTrigger. This is covered further in the [API Reference for Scene Transition Volumes](https://github.com/FeverDevJohnny/orbomodkit/tree/main/Docs/API%20Reference).

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

To start, we're going to be setting the _Time Ttrial ID_. If you see an ID field and it doesn't include "Override" in its title, then you need to provide a unique name for it. For the purposes of this tutorial, we'll just label **Time Trial ID** as **Trial_Tutorial**.

As with the Pip Manager, you should create a new gear part and disable **Active By Default**. Then make a new Game Object, align it to your camera as seen in the pip manager instructions, and then we can take these two items and apply them to the Time Trial Manager's **Gear Part To Reveal** and **Gear Part Camera Position** field respectively.

Now we should move onto the process of actually making the time trial.

Go to **Modkit > Dynamic > Trial Ring**.

Take the constructed trial ring, and position it where you'd like the time trial to start. After that, duplicate the trial ring, and add some more to create the path of the time trial.

Now, to assign them to the time trial manager.

Select your time trial manager, and in the upper-right part of the inspector you should see a lock icon. Hit that, and it'll prevent your inspector from losing focus on the time trial manager.

Now you're going to select the time trial rings in the order you want the player to clear them. Take this group of rings, and drag them into the **Rings** array in the time trial manager's inspector.

Congratulations! You've made a time trial. Go ahead and select the lock icon again to turn it off, save your scene, then go to the mod core file and re-build your mod. Bring it into the game's mod directory, and now you can test your time trial out!

To formulate your dev and green times, I recommend clearing the time trial as fast as you possibly can first. This is, of course, the dev time. In my experience, the green time is typically about 5 seconds longer than that. Of course if your time trial is quite short (or achieving a dev time cuts too close to the maximum time limit), you need to increase the time limit because the average player is unlikely to perform as well as you can.
