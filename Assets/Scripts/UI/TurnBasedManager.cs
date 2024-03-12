using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TurnBasedManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] GameObject TurnBasedSystem;
    [SerializeField] BulletData[] playerBulletsTypes;
    [SerializeField] BulletButton BulletButtonPrefab;
    [SerializeField] DialogueObject dialogue;

    [SerializeField] Transform itemGroup;

    private void Awake()
    {
        GameManager.instance.ChangeGameState(GameState.Start);
    }

    private void Update()
    {
        //if (TypeWriter.instance.finishedTyping) { TurnBasedSystem.SetActive(false); GameManager.instance.ChangeGameState(GameState.Combat); }
        if (GameManager.instance.gameState == GameState.Start) TurnBasedSystem.SetActive(true);
    }

    public void OnAttackButton()
    {
        if (GameManager.instance.gameState != GameState.Start) return;
        PlayerBulletPivot.instance.canFire = true;
        GameManager.instance.ChangeGameState(GameState.Selection);
        descriptionText.gameObject.SetActive(false);
        foreach (BulletData bullet in playerBulletsTypes)
        {
            BulletButton btn = Instantiate<BulletButton>(BulletButtonPrefab, itemGroup);
            btn.SetData(bullet);
            btn.button.onClick.AddListener(() => { 
                TurnBasedSystem.SetActive(false); 
                GameManager.instance.ChangeGameState(GameState.Combat);
                for (int i = 0; i < itemGroup.childCount; i++)
                {
                    Destroy(itemGroup.GetChild(i).gameObject);
                }
            });
        }
        GameManager.instance.ChangeGameState(GameState.Selection);
    }

    public void OnActButton()
    {
        if (GameManager.instance.gameState != GameState.Start) return;
        GameManager.instance.ChangeGameState(GameState.Selection);
        TypeWriter.instance.ShowDialogue(dialogue, descriptionText);
        PlayerBulletPivot.instance.canFire = false;
    }
}
