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
        if(collision.transform.TryGetComponent( out EnemyBulletController enemy))
        {
            if(enemy.data.GetName() == "Shell")
            {
                Instantiate(portion, transform.position, Quaternion.identity);
                enemy.lifeEndEvent?.Invoke();
                Destroy(enemy.gameObject);
                Destroy(gameObject);
                Debug.Log("it's hit");
                Collider2D blownup = Physics2D.OverlapCircle(transform.position, bombRadius, targetLayer);
                Debug.Log("name: "+blownup.name);
                if (blownup.TryGetComponent(out PlayerController player))
                {
                    player.Health -= bombDamage;
                }
                else if (blownup.CompareTag("weakpoint")) GameManager.instance.OnGameWon();
            }else if(enemy.data.GetName() == "Machine Gun")
            {
                enemy.lifeEndEvent?.Invoke();
                Destroy(enemy.gameObject);
            }
        }
    }
}
