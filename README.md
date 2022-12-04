# Big_Game

# Overview for manager scripts
 - All manager scripts in "Assets/Resources/Prefabs/Manager"
 - **ExplodeManager**: get explode of the bullet when bullet hit enemy or hit wall, explode will be got from **PoolingManager**
 - **GameManger**: Control the stats of the game (When to open UI)
 - **GoldManager**: Manage the gold of the player and save to disk (Add gold, Minus gold)
 - **LevelManager**: Manage when to change and which level to change
 - **PoolingManager**: Use to get **poolingBullet** and **poolingExplode**
 - **SettingManager**: Where to change setting.
 - **UIManager**: Where to manage UI (HP, MP, show gold...)
 - **WeaponManager**: Get GunData random or base on the index of the gun.

# Weapon
 - Change weapon stats at "Assets/Prefabs/Weapons/Data". Different data is for different gun.
    - Animation index: the shoot animation see in "Assets/Animations/Weapons/ShootEff".
    - Damage: damage of the gun, this damage will be set to bullet when shoot.
    - Fire rate: number of bullet can shoot in 1 second.
    - Bullet force: how fast does the bullet fly.
    - Range: how far the bullet can fly. When out of range or hit the wall, the bullet will be explode.
 - Change player stats at "Assets/Resources/Prefabs/Game/Player/Player.prefab".
    - Player view 1 2 3 is the different sprite of the player. This will be use in future.
    - Player num is the numerical order of the sprite, 1 is player view 1, 2 is player view 2...
    - PlayerAnimation: fadeTime is the speed of disappearance when player dead.
