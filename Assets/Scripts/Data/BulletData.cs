using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet Data", menuName = "Game/Bullet", order = 0)]
public class BulletData : ScriptableObject
{
    [Header("Stats")]
    [SerializeField] protected string bulletName;
    [SerializeField] protected float life;
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected Sprite bulletArt;
    [SerializeField] protected float reloadRate;
    [SerializeField] [Range(0.01f, 1f)] protected float mass;
    public  float GetStat(BulletStat bulletStat)
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

    public virtual string GetName()
    {
        return bulletName;
    }

    public virtual Sprite GetImage()
    {
        return bulletArt;
    }
}
