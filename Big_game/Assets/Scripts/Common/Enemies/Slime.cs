using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : BaseEnemy
{
    public static Slime GeInstance { get { return (Slime)Instance; } private set { } }
    public override void Move()
    {
        base.Move();
    }
}
