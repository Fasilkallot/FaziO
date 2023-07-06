using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                if(isGameFinished) return null;
                GameObject gameObject = new GameObject();
                gameObject.AddComponent<GameManager>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    private static GameManager instance;
    public static bool isGameFinished = false;
    public static Vector2 lastCheckPoint = new Vector2(0, 1);
    

    public int kiwis = 0;
    public GameState currentState = GameState.playing;

    public SceneControler sceneControler;
    public PlayerMovementScript player;

    private void Awake()
    {
       if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else Destroy(gameObject);
    }





    private void OnDestroy()
    {
        if(GameManager.Instance == this) isGameFinished = true;
    }
}

public enum GameState
{
    playing,pause
}
