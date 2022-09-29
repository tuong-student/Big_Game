using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseCharacter
{
    PlayerScripts playerScripts;

    float gold = 10f;

    public void Move()
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