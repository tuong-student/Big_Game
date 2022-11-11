using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseCharacter
{
    float gold = 10f;

    public virtual void Move()
    {
        
    }

    public void AddGold()
    {
        GoldManager.i.AddGold(gold);
    }

    public override void Die()
    {
        AddGold();
        base.Die();
    }
}

public enum EnemyType
{
    enemy1,
    enemy2
}