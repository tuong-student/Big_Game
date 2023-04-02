using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public sound soundType;
}
public enum sound
{
    flameThrowerWeapon,
    laserWeapon,
    matterWeapon,
    pistolWeapon,
    buttonClick,
    pickUp,
    lose,
    hitEnemy,
    teleportPortal,
    gateClose,
    gateOpen,
    win,
}


