# Project PROJECT_NAME

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

### Student Info

-   Name: Samuel Haile
-   Section: 04

## Game Design

-   Camera Orientation: TopDown
-   Camera Movement: The camera will be stationary
-   Player Health: The player will have icones for lives that depletes when hit
-   End Condition: When the player dies
-   Scoring: Destroying enemies and picking up star items

### Game Description

Survive as long as you can in this geometry wars inspired shoot em up! You can only destroy enemies with a matching color bullet and make sure not to use the wrong one or that will only make them stronger. Collect stars and get the highest score you can.

### Controls

-   Movement
    -   Up: W
    -   Down: S 
    -   Left: A
    -   Right: D
-   Fire: Left mouse click/Right mouse click

## You Additions

For my game I plan on implementing diffrent color bullets that will only destroy enemies of that same color. Assets will probably have to chane to provide better contrast but that is my main goal.

## Sources
-   All assets were created by me except for fonts:
-Arcade Classic: https://www.dafont.com/arcade-classic-2.font
-Liberation: https://fonts.adobe.com/fonts/liberation-sans
-Alien Encounters: https://www.dafont.com/alien-encounters.font
## Known Issues

I unfortunately ran out of time to properly delete game objects that pass the area of the screen. As a result, after some time, the uncollected stars and bullets that did not hit enemies begin to cause the game to lag. It does not become a noticeable problem until the player has played for a while.

### Requirements not completed
I've completed all requirements except that I used rigidBodies for the players bullet. I still calculate the velocity but set that value = to the bullets rigid body. Im not sure if that is allowed but you can see the specific code in the "PlayerBullet" script on lines 17-26.

## Other
Solution to shooting
In the update method of the "RotateAround" script I have a boolean that alows the player to shoot. Once a bullet is fired the boolean is set to false. I had a timer that increased until it was equal to the time I wanted to wait for firing. So after .3 seconds the boolean canFire is set to true and the cycle continues.