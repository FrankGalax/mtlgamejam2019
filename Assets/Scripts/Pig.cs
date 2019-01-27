using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public RessourceType m_Ressource;
    public float m_BuildingSpeedModifier;

    private bool m_IsBuilding;
    private uint m_CurrentIndexLane;
    private Vector3 m_idlePosition;
    private Quaternion m_idleRotation;
    private bool m_isReturningToHome;

    private GameObject m_towerToBuild;
    private Lane m_buildingLane;
    private float m_buildTime;
    private List<Transform> m_returnPath;

    // Start is called before the first frame update
    private void Awake()
    {
        m_IsBuilding = false;
        m_isReturningToHome = false;
        m_CurrentIndexLane = 0;
        m_buildTime = 0;

        m_returnPath = new List<Transform>();
    }

    void Start()
    {
        m_idlePosition = gameObject.transform.position;
        m_idleRotation = gameObject.transform.rotation;
    }

    public void UsePig(List<Transform> path, GameObject tower, Lane buildingLane)
    {
        if(m_IsBuilding || m_isReturningToHome)
        {
            return;
        }

        m_towerToBuild = tower;
        m_buildingLane = buildingLane;

        // c'est wak un peu, j'enleve les postiions de fin pis de depart parce que le path finding marche
        // pas vu quon est deja sur les positions
        for(int i = path.Count - 1; i > 0; --i)
        {
            m_returnPath.Add(path[i - 1]);
        }

        gameObject.transform.SetPositionAndRotation(path[0].position, path[0].rotation);
        path.RemoveAt(0);

        PutInTrack(path);
    }

    public void PutInTrack(List<Transform> path)
    {
        PathComponent pathComponent = gameObject.GetComponent<PathComponent>();

        if (pathComponent != null)
        {
            foreach (Transform pathPoint in path)
            {
                pathComponent.AddPathPoint(pathPoint);
            }

            pathComponent.PathCompleteAction = () => OnPathComplete();
        }
    }

    public void OnPathComplete()
    {
        if (!m_isReturningToHome)
        {
            Build();
        }
        else
        {
            gameObject.transform.SetPositionAndRotation(m_idlePosition, m_idleRotation);
            m_isReturningToHome = false;
        }
        
    }

    void MoveToLane(uint laneIndex)
    {

    }

    void Build()
    {
        if (m_towerToBuild != null)
        {
            Debug.Log(" ffasfdfs");
            Tour tourComponent = m_towerToBuild.GetComponent<Tour>();
            if(tourComponent != null)
            {
                m_IsBuilding = true;
                m_buildTime = m_BuildingSpeedModifier * tourComponent.m_MinBuildingTime;
            }
        }
    }

    void ReturnToHome()
    {
        m_isReturningToHome = true;

        PathComponent pathComponent = gameObject.GetComponent<PathComponent>();

        if (pathComponent != null)
        {
            foreach (Transform pathPoint in m_returnPath)
            {
                pathComponent.AddPathPoint(pathPoint);
            }

            pathComponent.PathCompleteAction = () => OnPathComplete();
        }
    }

    void CancelBuild()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsBuilding)
        {
            m_buildTime -= Time.deltaTime;
            if(m_buildTime <= 0)
            {
                // pour linstant spawn jsute a la fin 
                m_buildingLane.AddTower(m_towerToBuild);

                m_IsBuilding = false;
                ReturnToHome();
            }
        }
    }
}
