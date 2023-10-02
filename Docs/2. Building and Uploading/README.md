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
