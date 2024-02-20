using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {Start,Selection,Combat,Win,Lost }
public class GameManager : MonoBehaviour
{
    public GameState gameState;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
