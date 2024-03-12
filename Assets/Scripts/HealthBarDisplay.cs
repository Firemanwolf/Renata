using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarDisplay : MonoBehaviour
{
    Slider healthbar;
    float totalAmount = 100;
    [SerializeField] private PlayerController player;
    void Start()
    {
        healthbar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = player.Health / totalAmount;
    }
}
