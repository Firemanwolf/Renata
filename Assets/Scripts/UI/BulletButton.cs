using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BulletButton : MonoBehaviour
{
    BulletData bulletData;
    [HideInInspector]public Button button;
    TextMeshProUGUI text;

    private void Awake()
    {
        button = GetComponent<Button>();
        text = GetComponent<TextMeshProUGUI>();
    }

    public void SetData(BulletData data)
    {
        bulletData = data;
        text.text = "* " + data.GetName();
        button.onClick.AddListener(OnBulletButton);
    }

    void OnBulletButton()
    {
        PlayerBulletPivot.instance.LoadBullet(bulletData);
    }
}
