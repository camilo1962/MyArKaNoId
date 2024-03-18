# Arkanoid Remake
 Remake of **Arkanoid** Nintendo Game in Unity Engine.
 
 **Bricks:**
 
 <img src="/Assets/Sprites/Bricks/Bricks_Red.png" width="200"/> <img src="/Assets/Sprites/Bricks/Bricks_Green.png" width="200"/> <img src="/Assets/Sprites/Bricks/Bricks_Blue.png" width="200"/>
 
 - 3 random colors of bricks (red, green, blue)
 - 3 types of bricks (type depends of hitpoints needed to destroy)

 **PowerUps:**
 
 <img src="/Assets/Sprites/Buffs/Buff_MultiBall.png" width="48"/> <img src="/Assets/Sprites/Buffs/Buff_ExtendPaddle.png" width="48"/> <img src="/Assets/Sprites/Buffs/Buff_SuperBall.png" width="48"/>
 
 - **Multi Ball** (spawn more balls, max 3 on screen)
 - **Expand Paddle** (expand paddle from size 2 to size 2.5 for 10 seconds)
 - **Super Ball** (add particle effect on ball and don't care about hitpoints needed to destroy brick for 10 seconds)

 **Features:**
 
 - [x] Saving high score (PlayerPrefs)
 - [x] Possible to create own levels
 - [x] Graphics effects (with URP and ParticleSystem)
 - [x] Spawning random colors of bricks
 - [x] Available powerups
 - [x] Random music when playing
 - [ ] Custom mode for player (possible to change powerup chance/duration/limits, ball speed, lives, etc.)

# Gallery
 <img src="https://i.imgur.com/kNDz9GO.png" width="400"/><img src="https://i.imgur.com/drQ7Z21.png" width="400"/>
 <img src="https://i.imgur.com/Z6D0oi2.png" width="400"/><img src="https://i.imgur.com/bU6qN1c.png" width="400"/>

# How to create levels
 **LEVEL GENERATOR**: Open [levenGenerator.html](https://htmlpreview.github.io/?https://github.com/mateuszji/Arkanoid-Remake/blob/main/levelGenerator.html) in your browser, create your custom level in webUI and save it to *'/Resources/Levels/'* dir with filename *{LevelIndex}.txt*.
 
 
 All levels data are in 'Resources/Levels/' directory. If you want to add new level or edit existing you need to add/edit file with level index in filename (*for example: first level is 1.txt file*).
 
 - **Max columns in game:** 5
 
 - **Max rows in game:** 10
 
 **Example file with level**:
 ```
2,1,3,1,2
3,1,1,1,3
1,1,1,1,1
0,0,3,0,0
0,0,3,0,0
1,1,1,1,1
3,1,1,1,3
2,2,3,2,2
1,1,1,1,1
0,2,2,2,0
```

Numbers (0-3) are bricks:
 - **0** - no brick
 - **1** - brick with one hitpoint to break
 - **2** - brick with two hitpoints to break
 - **3** - brick with three hitpoints to break

# Credits
 **Graphics**
  - [Kacper Woźniak](mailto:kacper.wozniiak@gmail.com)
  - [Pixel Font Tripfive](https://assetstore.unity.com/packages/2d/fonts/pixel-font-tripfive-64734#description)
 
 **Audio**
  - ["Miru, Far From Home" FREE 8BIT Inspired Music Pack](https://assetstore.unity.com/packages/audio/music/electronic/miru-far-from-home-free-8bit-inspired-music-pack-202961)
  - [ZapSplat](https://www.zapsplat.com/)
