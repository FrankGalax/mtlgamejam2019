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

    private bool m_IsBeingBuild;
    private bool m_IsBuilded;

    // Start is called before the first frame update
    void Start()
    {
        FinishBuilding();
    }

    public void FinishBuilding()
    {
        m_IsBuilded = true;
        m_IsBeingBuild = false;
    }

    public virtual void DoUpdate(float deltaTime) { }

    // Update is called once per frame
    void Update()
    {
        if(m_IsBuilded)
        {
            DoUpdate(Time.deltaTime);
        }
    }
}
