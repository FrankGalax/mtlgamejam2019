using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : GameSingleton<GameUI>
{
    private ActionsSelection m_actionsSelection;
    void Awake()
    {
        Transform canvas = transform.Find("Canvas");
        m_EndGamePanel = canvas.Find("EndGamePanel");
        m_actionsSelection = gameObject.GetComponent<ActionsSelection>();
    }

    public void ShowEndGamePanel()
    {
        m_EndGamePanel.gameObject.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("main");
    }

    public RessourceType GetCurrentPig()
    {
        Debug.Log("SAluttt: " + m_actionsSelection.m_PigType);
        return m_actionsSelection.m_PigType;
    }

    public RessourceType GetCurrentTourRessource()
    {
        return m_actionsSelection.m_TourRessource;
    }

    public TourType GetCurrentTourType()
    {
        return m_actionsSelection.m_TourType;
    }

    public void Quit()
    {
        Application.Quit();
    }

    private Transform m_EndGamePanel;
}
