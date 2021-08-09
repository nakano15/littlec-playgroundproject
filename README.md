# Little C# Playground Project
This is a project I made to play a bit with the Windows Console Application.
Due to the application working on the console, I don't need to bother with messing on interface and stuff, so I can simply 
create the functions and stuff, to further enhance my scripting skills.

I took the liberty to make a quite cool dialogue system, which is partially broken due to some anomaly, probably caused by the font used by the windows console.

This program will allow you to create yourself users, and even allow you to pick one to enter the application with, while having the aid of a console, which can 
have a name and a gender, and interacts with you, or does comments during the registration process. The user you create can also have password, which you can define, 
and you can also delete the user when you feel like It.

I added an Application system, which will not only have hooks for when executed and loading, but also has a built in saving and loading system for infos they may need.
The Math Challenge application makes use of it for the Hi-Score loading and saving.

All the infos in the application are saved locally, of course, and please, DO NOT set your actual password for your user in It. I still need to figure out how to encode strings, 
since BinaryWriter and BinaryReader doesn't actually encode string values.
