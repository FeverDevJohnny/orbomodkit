# Working With JComposer

[(API Reference for JComposer)](https://github.com/FeverDevJohnny/orbomodkit/tree/main/Docs/6.%20JComposer/API%20Reference)

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
