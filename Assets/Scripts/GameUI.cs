using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : GameSingleton<GameUI>
{
    void Awake()
    {
        Transform canvas = transform.Find("Canvas");
        m_EndGamePanel = canvas.Find("EndGamePanel");
    }

    public void ShowEndGamePanel()
    {
        m_EndGamePanel.gameObject.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("main");
    }

    public void Quit()
    {
        Application.Quit();
    }

    private Transform m_EndGamePanel;
}
