using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public sound soundtype;
}
public enum sound
{
    laserWeapon,
    matterWeapon,
    pistolWeapon,
    buttonClick,
    pickUp,
    lose,
    hitEnemy,
    telePotarl,
    gateClose,
}


