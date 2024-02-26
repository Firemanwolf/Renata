using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TurnBasedManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] GameObject TurnBasedSystem;
    [SerializeField] AllyBulletData[] playerBulletsTypes;
    [SerializeField] BulletButton BulletButtonPrefab;

    [SerializeField] Transform itemGroup;

    private void Awake()
    {
        GameManager.instance.ChangeGameState(GameState.Start);
    }

    public void OnAttackButton()
    {
        descriptionText.gameObject.SetActive(false);
        foreach (AllyBulletData bullet in playerBulletsTypes)
        {
            BulletButton btn = Instantiate<BulletButton>(BulletButtonPrefab, itemGroup);
            btn.SetData(bullet);
            btn.button.onClick.AddListener(() => { TurnBasedSystem.SetActive(false); GameManager.instance.ChangeGameState(GameState.Combat); });
        }
        GameManager.instance.ChangeGameState(GameState.Selection);
    }
}
