using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;

public class Weakpoint : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.TryGetComponent<PlayerBulletController>(out PlayerBulletController killer))
        {
            GameManager.instance.OnGameWon();
        }
    }
}
