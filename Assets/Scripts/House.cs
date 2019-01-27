using UnityEngine;

public class House : MonoBehaviour
{
    void Awake()
    {
        m_RockGraphics = transform.Find("RockGraphics");
        m_WoodGraphics = transform.Find("WoodGraphics");
        m_StrawGraphics = transform.Find("StrawGraphics");
    }

    void Start()
    {
        m_CurrentHealth = 3;
    }

    public void LooseHealth()
    {
        GameObject toHide = null;
        GameObject toShow = null;
        m_CurrentHealth--;
        switch (m_CurrentHealth)
        {
            case 2:
                toHide = m_RockGraphics.gameObject;
                toShow = m_WoodGraphics.gameObject;
                break;
            case 1:
                toHide = m_WoodGraphics.gameObject;
                toShow = m_StrawGraphics.gameObject;
                break;
            case 0:
                toHide = m_StrawGraphics.gameObject;
                break;
        }

        PathComponent pathComponent = GetComponent<PathComponent>();
        if (pathComponent != null)
        {
            pathComponent.ClearPath();
            pathComponent.AddPathPoint(OutPoint.transform);
            pathComponent.PathCompleteAction = () =>
            {
                if (toHide != null)
                {
                    toHide.SetActive(false);
                }
                if (toShow != null)
                {
                    toShow.SetActive(true);
                    pathComponent.ClearPath();
                    pathComponent.AddPathPoint(InPoint.transform);
                }
            };
        }
    }

    public GameObject OutPoint;
    public GameObject InPoint;

    private int m_CurrentHealth;
    private Transform m_RockGraphics;
    private Transform m_WoodGraphics;
    private Transform m_StrawGraphics;
}
