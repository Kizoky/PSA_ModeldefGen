# Modified version of DN UnPSA Toolkit
'DN UnPSA Toolkit' is modified to have a Modeldef Generator button for GZDoom/Zandronum

All it does is use the program's built-in animation data array to access each animation's first and last frame, and based on that generate modeldef definitions for use with GZDoom or Zandronum.

You can specify the first 3 characters of a Sprite, the last character of the sprite will increase from A to Z and from Z to 0-9 everytime the alphabet for the frames have ran out.

It's also possible to skip every frame by 2.

This is a tool I used specially for Share the Doom (A Postal 2 GZDoom mod), but I figured there might be people out there that would enjoy this, as manually typing out the frames are tedious.

After generating the Modeldef Output text file, the program will automatically open it for you, it will be saved next to the exe's location.
## .NET Framework 4.5 REQUIRED
Bugs:
- Sometimes the spriteset for each animation frame gets skipped, or gets changed too early than it should, this doesn't happen that much frequently, and frankly it doesn't cause any serious problems

![Tool](https://i.imgur.com/l3LFOGA.png)
![Output](https://i.imgur.com/EWsbxYv.png)
