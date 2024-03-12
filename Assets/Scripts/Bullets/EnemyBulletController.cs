using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;

namespace Item
{ 
    public class EnemyBulletController : BulletController
    {
        public void Damage(PlayerController other)
        {
            lifeEndEvent?.Invoke();
            other.Health -= data.GetStat(BulletStat.Damage);
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);
            if(collision.gameObject.layer == 7)
            {
                PlayerController player = collision.transform.GetComponent<PlayerController>();
                Damage(player);
            }
        }
    }
}
