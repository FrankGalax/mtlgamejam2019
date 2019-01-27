using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TourType
{
    TourType_AOE,
    TourType_Attack,
    TourType_Static
}

public struct TowerData
{
    public TourType tourType;
    public GameObject gameObject;
}

public struct TowerDataLists
{
    public RessourceType ressourceType;
    public List<TowerData> towerData;
}

public struct Towers
{
    public List<TowerDataLists> towers;

    public GameObject GetTowerObject(RessourceType resssource, TourType tourType)
    {
        if(towers.Count <= 0)
        {
            return null;
        }

        foreach(TowerDataLists towerList in towers)
        {
            if(towerList.ressourceType == resssource)
            {
                foreach(TowerData tower in towerList.towerData)
                {
                    if(tower.tourType == tourType)
                    {
                        return tower.gameObject;
                    }
                }
            }
        }
        return null;
    }
}

public class Tour : MonoBehaviour
{
    public TourType m_TourType;
    public RessourceType m_RessourceType;
    public float m_MinBuildingTime;
    public float BuildBarOffset = 1.75f;
    public bool IsBuilding { get { return m_State == State.Building; } }
    
    private enum State
    {
        Building,
        Ready
    }

    private State m_State;
    private float m_BuildingTimer;
    private float m_BuildTime;
    private float m_StartY;
    private float m_EndY;

    // Start is called before the first frame update
    void Start()
    {
        m_State = State.Building;
    }

    public virtual void DoUpdate(float deltaTime) { }

    // Update is called once per frame
    void Update()
    {
        switch (m_State)
        {
            case State.Building:
                Building();
                break;
            case State.Ready:
                DoUpdate(Time.deltaTime);
                break;
        }
    }

    void OnGUI()
    {
        if (m_State == State.Building)
        {
            float ratio = 1.0f - m_BuildingTimer / m_BuildTime;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * BuildBarOffset);
            screenPosition.y = Screen.height - screenPosition.y;
            float fullWidth = 50.0f;
            screenPosition.x -= fullWidth / 2.0f;
            float sizeX = ratio * fullWidth;
            Rect rect = new Rect(screenPosition, new Vector2(fullWidth, 5));
            GUI.DrawTexture(rect, ResourceManager.GetTexture("buildingbar"));
            rect.size = new Vector2(sizeX, 5);
            GUI.DrawTexture(rect, ResourceManager.GetTexture("buildingbarprogress"));
        }
    }

    public void StartBuild(float buildTime, float targetHeight)
    {
        m_BuildingTimer = buildTime;
        m_BuildTime = buildTime;
        m_StartY = transform.position.y;
        m_EndY = targetHeight;
    }

    public float GetCurrentHeight()
    {
        if (m_State == State.Building)
        {
            return Mathf.Lerp(m_EndY, m_StartY, m_BuildingTimer / m_BuildTime);
        }

        return m_EndY;
    }

    private void Building()
    {
        m_BuildingTimer -= Time.deltaTime;

        if (m_BuildingTimer < 0)
        {
            m_State = State.Ready;
        }
    }
}
