using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BulletButton : MonoBehaviour
{
    AllyBulletData bulletData;
    [HideInInspector]public Button button;
    TextMeshProUGUI text;

    private void Awake()
    {
        button = GetComponent<Button>();
        text = GetComponent<TextMeshProUGUI>();
    }

    public void SetData(AllyBulletData data)
    {
        bulletData = data;
        Debug.Log(text);
        text.text = "* " + data.GetName();
        button.onClick.AddListener(OnBulletButton);
    }

    void OnBulletButton()
    {
        PlayerBulletPivot.instance.LoadBullet(bulletData);
    }
}
