using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum State
    {
        Idle,
        Shotgun,
        Machinegun
    }
    private Transform player;
    Transform weakPoint;

    [SerializeField] private float fireRate;
    [SerializeField] private float shotgunRange;
    [SerializeField] private float MachinegunRange;

    private void Start()
    {
        player = PlayerController.playerPosition;
    }

    public void MachinegunAttack()
    {

    }

    public void ShotgunAttack()
    {

    }

}
