using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;

public class Barrel : MonoBehaviour
{
    [SerializeField] private float bombRadius;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float bombDamage;
    [SerializeField] private GameObject portion;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.TryGetComponent<EnemyBulletController>( out EnemyBulletController enemy))
        {
            if(enemy.data.GetName() == "Shell")
            {
               Collider2D blownup = Physics2D.OverlapCircle(transform.position, bombRadius, targetLayer);
                if (blownup.TryGetComponent<PlayerController>(out PlayerController player))
                {
                    player.Health -= bombDamage;
                }
                else if (blownup.CompareTag("weakpoint")) GameManager.instance.OnGameWon();
                Instantiate(portion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
