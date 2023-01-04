using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    public StatsType upgradeStats;
    public float upgradeAmount;
    public int goldNeed;
}

public static class UpgradeMaster
{
    public static Array GetStatsTypeList()
    {
        return Enum.GetValues(typeof(StatsType));
    }

    public static Upgrade RandomUpgrade()
    {
        Array statsTypeList = GetStatsTypeList();
        Upgrade upgrade = new Upgrade();

        int r = UnityEngine.Random.Range(0, statsTypeList.Length);
        StatsType randomStats = (StatsType) statsTypeList.GetValue(r);

        r = UnityEngine.Random.Range(1, 5);

        upgrade.upgradeStats = randomStats;
        switch (randomStats)
        {
            case StatsType.attack:
            case StatsType.defense:
            case StatsType.maxHealth:
            case StatsType.mana:
                upgrade.upgradeAmount = r;
                break;
            case StatsType.movement:
            case StatsType.reloadSpeed:
            case StatsType.fireRate:
            case StatsType.critical:
                upgrade.upgradeAmount = r * 0.1f;
                break;
	    }
        upgrade.goldNeed = (r * 2);

        return upgrade;
    }

    public static Upgrade RandomUpgradeExcept(StatsType exception)
    {
        Array statsTypeList = GetStatsTypeList();
        Upgrade upgrade = new Upgrade();

        int r = UnityEngine.Random.Range(0, statsTypeList.Length);
        StatsType randomStats = exception;
        while (randomStats == exception)
        {
            r = UnityEngine.Random.Range(0, statsTypeList.Length);
            randomStats = (StatsType)statsTypeList.GetValue(r);
        }

        r = UnityEngine.Random.Range(1, 5);

        upgrade.upgradeStats = randomStats;
        upgrade.upgradeAmount = r;
        upgrade.goldNeed = (r * 2);


        return upgrade;
    }
}

public enum StatsType
{
    attack,
    defense,
    mana,
    maxHealth,
    movement,
    fireRate,
    critical,
    reloadSpeed
}