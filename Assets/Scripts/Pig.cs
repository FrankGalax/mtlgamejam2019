using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public RessourceType m_Ressource;
    public float m_BuildingSpeedModifier;

    private bool m_IsBuilding;
    private uint m_CurrentIndexLane;

    // Start is called before the first frame update
    void Start()
    {
        m_IsBuilding = false;
        m_CurrentIndexLane = 0;
    }

    public void UsePig()
    {
        if(m_IsBuilding)
        {
            return;
        }

        PutInTrack();
    }

    public void PutInTrack()
    {

    }

    void MoveToLane(uint laneIndex)
    {

    }

    void Build()
    {

    }

    void ReturnToHome()
    {

    }

    void CancelBuild()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
