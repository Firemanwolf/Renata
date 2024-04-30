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
            Debug.Log("working:" + collision);
            base.OnCollisionEnter2D(collision);
            if(!collision.transform.CompareTag("Player"))
            Destroy(gameObject);
        }
    }
}
