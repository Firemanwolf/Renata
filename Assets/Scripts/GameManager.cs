using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {Start,Selection,Combat,Win,Lost }
public class GameManager : MonoBehaviour
{
    public GameState gameState { get; private set; }
    public static GameManager instance { get; private set; }

    [SerializeField] private GameObject LosePage;
    [SerializeField] private GameObject WinPage;

    [HideInInspector]public List<ItemData> itemsList = new List<ItemData>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ChangeGameState(GameState newState)
    {
        gameState = newState;
    }

   public void OnGameLost()
    {
        LosePage.SetActive(true);
    }

    public void OnGameWon()
    {
        WinPage.SetActive(true);
    }
}
