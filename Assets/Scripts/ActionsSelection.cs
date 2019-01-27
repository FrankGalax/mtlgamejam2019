using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ActionsSelection : MonoBehaviour
{
    public RessourceType m_PigType { get; set; }
    public TourType m_TourType { get; set; }
    public RessourceType m_TourRessource { get; set; }

    public GameObject m_RessourcePanel;
    public GameObject m_tourTypePanel;
    public GameObject m_pigsSelectionPanel;

    private bool m_ActionIsReady;
    private Lane m_laneOwner;
    // Start is called before the first frame update
    void Start()
    {
        m_ActionIsReady = false;
    }

    public void OnActivePigUpdate(string name, bool isActive)
    {
        Button pigButton = m_pigsSelectionPanel.transform.Find(name).GetComponent<Button>();
        if(pigButton)
        {
            pigButton.interactable = isActive;
        }
    }

    public void Active(Vector2 inputPosition, Lane ownerLane)
    {
        m_ActionIsReady = true;

        m_laneOwner = ownerLane;
        Quaternion rotation = gameObject.GetComponent<RectTransform>().rotation;
        Debug.Log("SETTTTERRRR " + inputPosition);
        gameObject.GetComponent<RectTransform>().SetPositionAndRotation(inputPosition, rotation);
        m_pigsSelectionPanel.SetActive(true);
    }
    public void PigSelection(string name)
    {
        switch(name)
        {
            case "Straw":
                m_PigType = RessourceType.RessourceType_Straw;
                break;
            case "Wood":
                m_PigType = RessourceType.RessourceType_Wood;
                break;
            case "Rock":
                m_PigType = RessourceType.RessourceType_Rock;
                break;
        }

        m_pigsSelectionPanel.SetActive(false);
        m_RessourcePanel.SetActive(true);
    }

    public void TourRessourceSelection(string name)
    {
        switch (name)
        {
            case "Straw":
                m_TourRessource = RessourceType.RessourceType_Straw;
                break;
            case "Wood":
                m_TourRessource = RessourceType.RessourceType_Wood;
                break;
            case "Rock":
                m_TourRessource = RessourceType.RessourceType_Rock;
                break;
        }
        m_RessourcePanel.SetActive(false);
        m_tourTypePanel.SetActive(true);
    }

    public void TourTypeSelection(string name)
    {
        switch (name)
        {
            case "AOE":
                m_TourType = TourType.TourType_AOE;
                break;
            case "Attack":
                m_TourType = TourType.TourType_Attack;
                break;
            case "Static":
                m_TourType = TourType.TourType_Static;
                break;
        }

        m_tourTypePanel.SetActive(false);

        m_ActionIsReady = false;
        Game.Instance.StartActionOnLane(m_laneOwner, m_TourRessource, m_PigType, m_TourType);
    }
    // Update is called once per frame
    void Update()
    {
    }
}
