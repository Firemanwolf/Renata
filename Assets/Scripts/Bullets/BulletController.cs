using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public BulletData data;
    protected float timer;
    [SerializeField]protected bool Destroyable;
    private Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Fire()
    {
        StartCoroutine(Shoot());
    }

    protected virtual IEnumerator Shoot()
    {
        transform.parent = null;
        while (timer < data.GetStat(BulletStat.Life))
        {
            timer += Time.fixedDeltaTime;
            Vector3 position = transform.position;
            position += Time.fixedDeltaTime * transform.right * data.GetStat(BulletStat.Speed);
            rb.MovePosition(position);
            yield return null;
        }
        if (Destroyable) Destroy(this.gameObject);
    }
}
