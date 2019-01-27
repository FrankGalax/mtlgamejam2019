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
        m_actionsSelection = transform.Find("Canvas").transform.Find("Selection").GetComponent<ActionsSelection>();
    }

    public void ShowEndGamePanel()
    {
        m_EndGamePanel.gameObject.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("main");
    }

    public void ActivePig(string name)
    {
        m_actionsSelection.OnActivePigUpdate(name, true);
    }
    public void DeactivePig(string name)
    {
        m_actionsSelection.OnActivePigUpdate(name, false);
    }

    public void ShowChoice(Vector2 position, Lane laneComponent)
    {
        m_actionsSelection.Active(position, laneComponent);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private Transform m_EndGamePanel;
}
