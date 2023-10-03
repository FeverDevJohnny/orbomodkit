# Working With JTrigger

[(API Reference for JTrigger)](https://github.com/FeverDevJohnny/orbomodkit/tree/main/Docs/5.%20JTrigger/API%20Reference)

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
- **On Timer**: Once the timer runs out, JTrigger will run its events. If this context is selected, JTrigger will expose a _Trigger Timer Range_ field that you can modify. JTrigger will pick a random value in the range between X and Y and assign it to its internal clock before counting down. To have the timer run consistently, just set both X and Y to the same value. Timers only run while the object is active, so if you want to control when the countdown occurs, simply deactivate the JTrigger object, and activate it via another JTrigger to start counting. Timers will reset themselves once they run out, so if you'd like to have it only run once, either set _One Shot_ to true, or have the timer deactivate its own game object at the end of its Events().
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
