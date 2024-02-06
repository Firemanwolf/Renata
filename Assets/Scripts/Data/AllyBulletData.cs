using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ally Bullet Data", menuName = "Game/Bullet/Ally", order = 1)]
public class AllyBulletData : BulletData
{
    [SerializeField] protected float reloadRate;
    [SerializeField] [Range(0.01f,1f)]protected float mass;
    public override float GetStat(BulletStat bulletStat)
    {
        switch (bulletStat)
        {
            case BulletStat.Life:
                return life;
            case BulletStat.Speed:
                return speed;
            case BulletStat.Damage:
                return damage;
            case BulletStat.ReloadRate:
                return reloadRate;
            case BulletStat.Mass:
                return mass;
        }
        return 0;
    }
}
