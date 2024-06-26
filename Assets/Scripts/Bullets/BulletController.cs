using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Item
{
    public class BulletController : MonoBehaviour
    {
        public BulletData data;
        protected float timer;
        private Rigidbody2D rb;
        public SpriteRenderer itemArt;

        public UnityEvent lifeEndEvent;

        protected virtual void Awake()
        {
            itemArt = GetComponent<SpriteRenderer>();
        }

        protected virtual void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            itemArt.sprite = data.GetImage();
        }

        public virtual void Fire()
        {
            StartCoroutine(Shoot());
        }

        protected virtual IEnumerator Shoot()
        {
            yield return new WaitForFixedUpdate();
            transform.parent = null;
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.angularDrag = 0;
            rb.gravityScale = 0;
            rb.freezeRotation = true;
            while (timer < data.GetStat(BulletStat.Life))
            {
                timer += Time.fixedDeltaTime;
                Vector3 position = transform.position;
                position += Time.fixedDeltaTime * transform.right * data.GetStat(BulletStat.Speed);
                rb.MovePosition(position);
                yield return null;
            }
            lifeEndEvent?.Invoke();
            Destroy(gameObject);
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == 6 || collision.gameObject.layer == 8 || collision.transform.GetComponent<BulletController>()!=null)
            {
                lifeEndEvent?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
