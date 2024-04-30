using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    ItemData ItemData;
    [HideInInspector] public Button button;
    TextMeshProUGUI text;

    private void Awake()
    {
        button = GetComponent<Button>();
        text = GetComponent<TextMeshProUGUI>();
    }

    public void SetData(ItemData data)
    {
        ItemData = data;
        Debug.Log("data:"+ItemData);
        text.text = "* " + data.ItemName;
        button.onClick.AddListener(OnItemButton);
    }

    void OnItemButton()
    {
        PlayerBulletPivot.instance.player.currentBuffs.Add(ItemData.buff);
    }
}
