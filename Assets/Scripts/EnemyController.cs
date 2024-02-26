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
        Machinegun,
        Attacking
    }

    [Serializable]
    private struct AttackStats
    {
        [SerializeField] public float attackRange;
        [SerializeField] public float fireRate;
        [SerializeField] public BulletController bulletPrefab;
        [SerializeField] public int attackTime;
    }
    [SerializeField] AttackStats shotGun;
    [SerializeField] AttackStats machineGun;

    private Transform player;
    Transform weakPoint;
    [SerializeField] Transform firePosition;

    [SerializeField]AttackState currentState;

    float currentTimer;
    int currentAttackTimes;
    private AttackStats currentStats;
   

    private void Start()
    {
        player = PlayerController.playerPosition;
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.gameState != GameState.Combat) return;
        switch (currentState)
        {
            case AttackState.Shotgun:
                currentStats = shotGun;
                goto default;
            case AttackState.Machinegun:
                currentStats = machineGun;
                goto default;
            default:
                currentAttackTimes = currentStats.attackTime;
                currentState = AttackState.Attacking;
                break;
            case AttackState.Idle:
                return;
            case AttackState.Attacking:
                break;
        }

        if (currentState != AttackState.Attacking&&!Physics2D.OverlapCircle(transform.position, currentStats.attackRange, 7)) return;
        if (currentAttackTimes <= 0) GameManager.instance.ChangeGameState(GameState.Start);
        if (currentTimer < 0)
        {
            transform.Rotate(Vector3.forward*30);
            currentTimer = currentStats.fireRate;
            BulletController bullet = Instantiate<BulletController>(currentStats.bulletPrefab, firePosition.position, Quaternion.identity, firePosition.transform);
            bullet.transform.rotation = transform.rotation;
            bullet?.Fire();
            currentAttackTimes--;
            Debug.Log(currentAttackTimes);
        }
        else
        {
            currentTimer -= Time.deltaTime;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5);
    }

}
