using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {Start,Selection,Combat,Win,Lost }
public class GameManager : MonoBehaviour
{
    public GameState gameState { get; private set; }
    public static GameManager instance { get; private set; }

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

    // Update is called once per frame
    void Update()
    {
    }
}
