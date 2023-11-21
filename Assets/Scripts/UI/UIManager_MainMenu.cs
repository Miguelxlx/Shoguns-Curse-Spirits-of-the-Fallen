using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager_MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Commands()
    {
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
