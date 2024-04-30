using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffedItem : MonoBehaviour
{
    [SerializeField] private ItemData item;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.TryGetComponent<PlayerController>(out PlayerController player))
        {
            GameManager.instance.itemsList.Add(item);
            Destroy(gameObject);
        }
    }
}
