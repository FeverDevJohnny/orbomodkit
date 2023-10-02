## Installation

To install the **Orbo's Odyssey modkit**, you can clone a copy of the modding project files from [the github page](https://github.com/FeverDevJohnny/orbomodkit).

## You will need to have [**Unity 2022.3.4f1**](https://unity.com/releases/editor/whats-new/2022.3.4) installed to open the modkit project. After that, you are ready to start working on your mod! No need for visual studio or any other installations, mod scripting is done entirely in-engine using JTrigger.

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
