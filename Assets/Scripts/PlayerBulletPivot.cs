using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletPivot: MonoBehaviour
{
    [SerializeField] private float massScale;
    [SerializeField] private float rotationSpeed;
    [SerializeField] PlayerController player;
    [SerializeField] private Transform bulletPos;
    [SerializeField] private BulletController bulletPrefab;
    private BulletController loadedBullet;
    private float coolDown;

    [Header("Main Camera")]
    [SerializeField] private Camera m_camera;

    void Update()
    {
        Vector3 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        RotateBullet(mousePos);
        if (coolDown <= 0)
        {
            Fire();
        }
        else coolDown -= Time.deltaTime;
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            loadedBullet = Instantiate<BulletController>(bulletPrefab, bulletPos.position, Quaternion.identity, transform);
            loadedBullet.transform.rotation = transform.rotation;
            player.MassIncrement(loadedBullet.data.GetStat(BulletStat.Mass));
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if(loadedBullet != null)
            {
                player.MassIncrement(-loadedBullet.data.GetStat(BulletStat.Mass));
                loadedBullet?.Fire();
                coolDown = bulletPrefab.data.GetStat(BulletStat.ReloadRate);
                loadedBullet = null;
            }
        }
    }
    void RotateBullet(Vector3 lookPoint)
    {
        Vector3 distanceVector = lookPoint - transform.position;
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime*rotationSpeed/(bulletPrefab.data.GetStat(BulletStat.Mass)*massScale));
    }
}
