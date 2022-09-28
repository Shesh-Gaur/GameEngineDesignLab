# GameEngineDesignLab
Description:
Hello! This repo contains my lab project for INFR 3110U – Game Engine Design & Implementation.

My Role:
I'm Shesh Gaur (100786399) and I'm part of Revision Interactive. I'm primarily the team's
AI & Gameplay Programmer, as well as the Sound Designer & Composer. I also do some project management stuff.

My Video Report:
https://youtu.be/S35g7G_9sQw

Week 1:
For the first week of the lab, we started by laying out the foundation for the project. We added in some assets for the ground and the player, 
and set up the projects's file structure. We then started to implement Unity's new input system, which is significantly more streamlined 
than I'm used to with the old one. We set up some player animations using a finite state machine in Unity, and ended off by creating a player controller script, 
and setting up some parameters.

Week 2:
This week we continued where we left off. We set up and got player movement working, 
along with jump and shooting objects by intatiating and applying forces to a rigidbody. We then added a singleton class called ScoreManager, 
that has a function called ChangeScore(). This function is called whenever our newly added coin objects are collected, updating the game's score value.
After this we began working on an In-Game editor mode with a script called EditorManager. We also set up inputs for it in a new Editor action map, 
including a toggle to enable and disable it, as well as buttons to add and drop items.

Assignment 1:
To customize the project, I began by adding additional camera controls. I made it so the 'Q' and 'E' keys controled a 1D vector/axis. In the player controller 
I mapped this to the character's rotation. I extended the level with some extra platforming challenges and collectables. I then added a respawn system and health to the player. I created a variable called lastPosOnGround, 
which is written to with the player's position whenever they're on the ground. I teleport the player to this position when they fall below a certain y value, 
aswell as taking away some of their health. I finished this assignment off by adding some UI for the score and player hp, 
set up win & lose conditions and screens in ScoreManager.

Credits:
Got my .gitattributes file (tells git LFS which file types to track) from here: https://gist.github.com/Srfigie/77b5c15bc5eb61733a74d34d10b3ed87 
