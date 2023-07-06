

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControler : MonoBehaviour
{

    [SerializeField] private GameObject winnerScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private PlayerControls controls;

    private bool isPaused = false ;
    int currentSceneIndex;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Enable();
        controls.Land.Escape.performed += PasueAndResume;

    }

    private void Start()
    {
        GameManager.Instance.sceneControler = this;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        GameManager.Instance.currentState = GameState.playing;

    }

    private void OnDestroy()
    {
        controls.Land.Escape.performed -= PasueAndResume;
    }

    private void PasueAndResume(InputAction.CallbackContext context)
    {
        if (isPaused)
        {
            pauseMenu.SetActive(false);
            GameManager.Instance.currentState = GameState.playing;
        }
        else
        {
            pauseMenu.SetActive(true);
            GameManager.Instance.currentState = GameState.pause;
        }

        isPaused = !isPaused;
    }

    public void HelthUpdate(int playerHelth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHelth) hearts[i].sprite = fullHeart;
            else hearts[i].sprite = emptyHeart;
            
        }
    }
    

    public void Restart()
    {
        SceneManager.LoadScene(currentSceneIndex);
        GameManager.Instance.currentState = GameState.playing;

    }
    public void Home()
    {
        GameManager.lastCheckPoint = new Vector2(0, 1);
        SceneManager.LoadScene(0);
        GameManager.Instance.currentState = GameState.playing;

    }
    public void Next()
    {
        int nextLevel = ++currentSceneIndex;
        if (currentSceneIndex >= PlayerPrefs.GetInt("levelsUnlocked")) PlayerPrefs.SetInt("levelsUnlocked", nextLevel);
        SceneManager.LoadScene(nextLevel);
        GameManager.Instance.currentState = GameState.playing;
        GameManager.lastCheckPoint = new Vector2(0, 1);

    }

    public void Winner()
    {
        winnerScreen.SetActive(true);
        GameManager.Instance.currentState = GameState.pause;
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        GameManager.Instance.currentState = GameState.pause;

    }




}
