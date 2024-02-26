using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Item;


public class EnemyController : MonoBehaviour
{
    private enum AttackState
    {
        Idle,
        Shotgun,
        Machinegun
    }

    [Serializable]
    private struct AttackStats
    {
        [SerializeField] public float attackRange;
        [SerializeField] public float fireRate;
        [SerializeField] public BulletController bulletPrefab;
    }
    [SerializeField] AttackStats shotGun;
    [SerializeField] AttackStats machineGun;

    private Transform player;
    Transform weakPoint;
    [SerializeField] Transform firePosition;

    AttackState currentState = AttackState.Idle;

    float currentTimer;
   

    private void Start()
    {
        player = PlayerController.playerPosition;
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.gameState != GameState.Combat) return;
        AttackStats currentStats = new AttackStats();
        switch (currentState)
        {
            case AttackState.Shotgun:
                currentStats = shotGun;
                goto default;
            case AttackState.Machinegun:
                currentStats = machineGun;
                transform.Rotate(Vector3.forward);
                goto default;
            default:
                if (!Physics2D.OverlapCircle(transform.position, currentStats.attackRange, 7)) return;
                if (currentTimer < 0)
                {
                    currentTimer = currentStats.fireRate;
                    BulletController bullet = Instantiate<BulletController>(currentStats.bulletPrefab, firePosition.position, Quaternion.identity);
                    bullet?.Fire();
                }
                else
                {
                    currentTimer -= Time.deltaTime;
                }
                break;
            case AttackState.Idle:
                break;
        }
        
    }

}
