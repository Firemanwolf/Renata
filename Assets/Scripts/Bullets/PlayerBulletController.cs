using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;
namespace Item
{
    public class PlayerBulletController : BulletController
    {
        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);
            Destroy(gameObject);
        }
    }
}
