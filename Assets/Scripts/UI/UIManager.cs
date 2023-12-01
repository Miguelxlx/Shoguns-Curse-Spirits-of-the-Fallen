using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
{
    if (Input.GetKeyDown(KeyCode.Escape))
    {
        Debug.Log("Escape key pressed");
        if (pauseScreen.activeSelf)
            PauseGame(false);
        else
            PauseGame(true);
    }
}
    #region Game Over
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        //Play game over sound
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    #endregion 

    #region Pause
    public void PauseGame(bool status)
    {
        Debug.Log("PauseGame called with status: " + status);
        pauseScreen.SetActive(status);
        Time.timeScale = status ? 0 : 1;
    }

    public void soundVolume()
    {
       SoundManager.instance.changeSoundVolume(0.2f);
    }

    public void musicVolume()
    {
        SoundManager.instance.changeMusicVolume(0.2f);
    }

    #endregion
}

