using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TourType
{
    TourType_AOE,
    TourType_Attack,
    TourType_Static
}

public class Tour : MonoBehaviour
{
    public TourType m_TourType;
    public Ressource m_Ressource;
    public float m_MinBuildingTime;

    private bool m_IsBeingBuild;
    private bool m_IsBuilded;

    // Start is called before the first frame update
    void Start()
    {
        m_IsBeingBuild = true;
    }

    void FinishBuilding()
    {
        m_IsBuilded = true;
        m_IsBeingBuild = false;
    }

    public void Die()
    {

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
