using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Item;
using Pathfinding;


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
        [SerializeField] public BulletData data;
    }
    [SerializeField] AttackStats shotGun;
    [SerializeField] AttackStats machineGun;

    [SerializeField] public EnemyBulletController bulletPrefab;

    [SerializeField] Transform weakPoint;
    [SerializeField] Transform firePosition;
    [SerializeField] Transform spawnPos;

    [SerializeField]AttackState currentState;

    [Header("Enemy AI")]
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private float nextWayPointDistance = 3f;
    [SerializeField] private LayerMask targetLayer;
    Path path;
    int currentWayPoint;
    bool reachedEndOfPath = false;

    private float pathFindCD = .5f;
    private float pathfindingTimer;

    Seeker seeker;
    Rigidbody2D rb;


    float currentTimer;
    int currentAttackTimes;
    private AttackStats currentStats;

    private List<BulletController> bullets = new List<BulletController>();
   

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    void UpdatePath()
    {

        if(seeker.IsDone())seeker.StartPath(rb.position, player.position, OnPathComplete);
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.gameState != GameState.Combat || GameManager.instance.gameState == GameState.Lost || GameManager.instance.gameState == GameState.Win) return;
        if (pathfindingTimer > pathFindCD)
        {
            pathfindingTimer = 0;
            UpdatePath();
        }
        else pathfindingTimer += Time.fixedDeltaTime;
        switch (currentState)
        {
            case AttackState.Shotgun:
                if (Physics2D.OverlapCircle(rb.position, currentStats.attackRange, targetLayer)) 
                {
                    Vector3 distanceVector = -transform.position + player.position;
                    float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    goto default;
                }
                else 
                {
                    AIMove();
                    break;
                }
            case AttackState.Machinegun:
                transform.Rotate(Vector3.forward * 2);
                goto default;
            default:
                if (currentAttackTimes <= 0)
                {
                    if (bullets.Count == 0)
                    {
                        GameManager.instance.ChangeGameState(GameState.Start);
                        player.GetComponent<PlayerController>().currentBuffs.Clear();
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
                //transform.position = spawnPos.position;
                //float nextAttack = UnityEngine.Random.value;
                float nextAttack = 1f;
                currentStats = nextAttack < 0.5f ? machineGun : shotGun;
                CombatInit();
                return;
        }

    }

    void CombatInit()
    {
        currentAttackTimes = currentStats.attackTime;
        currentState = currentStats.combatState;
        bulletPrefab.data = currentStats.data;
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

    void OnPathComplete(Path p)
    {
        Debug.Log("activated");
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    void AIMove()
    {
        if (path == null) return;

        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else reachedEndOfPath = false;

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed* Time.deltaTime;

        Debug.Log("direction: " + direction);
        Debug.Log("force: "+ force * rb.mass);
        Debug.Log("position: " + rb.position);
        Debug.Log("path positon:" + (Vector2)path.vectorPath[currentWayPoint]);

        rb.AddForce(force*rb.mass);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if(distance < nextWayPointDistance)
        {
            Debug.Log("index: " + currentWayPoint);
            currentWayPoint++;
        }
        return;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, currentStats.attackRange);
    }

}
