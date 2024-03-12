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
    }

    [Serializable]
    private struct AttackStats
    {
        [SerializeField] public AttackState combatState;
        [SerializeField] public float attackRange;
        [SerializeField] public int attackTime;
    }
    [SerializeField] AttackStats shotGun;
    [SerializeField] AttackStats machineGun;

    [SerializeField] public EnemyBulletController bulletPrefab;

    private Transform player;
    [SerializeField] Transform weakPoint;
    [SerializeField] Transform firePosition;
    [SerializeField] Transform spawnPos;

    [SerializeField]AttackState currentState;

    float currentTimer;
    int currentAttackTimes;
    private AttackStats currentStats;

    private List<BulletController> bullets = new List<BulletController>();
   

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
                goto default;
            case AttackState.Machinegun:
                transform.Rotate(Vector3.forward * 30);
                goto default;
            default:
                if (currentAttackTimes <= 0)
                {
                    if (bullets.Count == 0)
                    {
                        GameManager.instance.ChangeGameState(GameState.Start);
                        currentState = AttackState.Idle;
                    }
                    return;
                }
                if (currentTimer < 0)
                {
                    Fire();
                }
                else currentTimer -= Time.deltaTime;
                break;
            case AttackState.Idle:
                transform.position = spawnPos.position;
                float nextAttack = UnityEngine.Random.value;
                currentStats = nextAttack < 0.2f ? machineGun : shotGun;
                CombatInit();
                Debug.Log(currentState);
                return;
        }

    }

    void CombatInit()
    {
        currentAttackTimes = currentStats.attackTime;
        currentState = currentStats.combatState;
    }

    void Fire()
    {
        currentTimer = bulletPrefab.data.GetStat(BulletStat.ReloadRate);
        BulletController bullet = Instantiate<BulletController>(bulletPrefab, firePosition.position, Quaternion.identity, firePosition.transform);
        bullet.transform.rotation = transform.rotation;
        bullets.Add(bullet);
        bullet.lifeEndEvent.AddListener(() => bullets.Remove(bullet));
        bullet?.Fire();
        currentAttackTimes--;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5);
    }

}
