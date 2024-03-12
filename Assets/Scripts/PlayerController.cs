using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private float mass;
    Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    public Action movingAction;
    private Vector2 velocity;
    private float delay;
    private CircleCollider2D circleCollider;
    [SerializeField] private float health = 100;
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = Math.Clamp(value, 0, 100);
        }
    }

    [Header("Test Only")]
    [SerializeField] private float trust;

    [SerializeField] private float detectRadius;

    [SerializeField] private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        delay = Mathf.Lerp(1f, Time.deltaTime, trust);
        mass = rb.mass;
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (GameManager.instance.gameState != GameState.Combat) return;
        if (health <= 0) Destroy(this.gameObject);
        Movement();
    }

    void FixedUpdate()
    {
        if (GameManager.instance.gameState != GameState.Combat) 
        {
            rb.velocity = Vector2.zero;
            return;
        }
        Vector2 position = rb.position;
        position += velocity * Time.fixedDeltaTime;
        rb.MovePosition(position);
    }

    public void MassIncrement(float itemMass)
    {
        mass += itemMass;
    }

    void Movement()
    {
        LayerMask targetlayer = LayerMask.GetMask("Wall");
        float xInputAxis = Input.GetAxis("Horizontal");
        float yInputAxis = Input.GetAxis("Vertical");
        velocity.x = Mathf.MoveTowards(velocity.x, xInputAxis * moveSpeed/mass, moveSpeed * Time.deltaTime/delay);
        velocity.y = Mathf.MoveTowards(velocity.y, yInputAxis * moveSpeed/mass, moveSpeed * Time.deltaTime/delay);
        anim.SetBool("IsWalkingX", Mathf.Abs(xInputAxis) > 0.1);
        anim.SetBool("IsWalkingY", Mathf.Abs(yInputAxis) > 0.1);
        anim.SetFloat("VelocityX", velocity.x);
        anim.SetFloat("VelocityY", velocity.y);
        if (Physics2D.CircleCast(transform.position,0.25f,(Vector2.right*velocity.x).normalized, circleCollider.bounds.extents.x, targetlayer))
        {
            velocity.x = 0;        
        }
        if (Physics2D.CircleCast(transform.position, 0.25f, (Vector2.up * velocity.y).normalized, circleCollider.bounds.extents.y, targetlayer))
        {
            velocity.y = 0;
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.OnGameLost();
    }
}
