using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Lane : MonoBehaviour
{
    void Awake()
    {
        m_MobSpawnPoint = transform.Find("MobSpawnPoint");
        m_Towers = new List<Tour>();
        m_IsOccupied = false;
    }

    void Update()
    {
        for (int i = m_Towers.Count - 1; i >= 0; --i)
        {
            if (m_Towers[i] == null)
            {
                m_Towers.RemoveAt(i);
            }
        }

        for (int i = 0; i < m_Towers.Count; ++i)
        {
            if (!m_Towers[i].gameObject.activeSelf)
            {
                continue;
            }

            Rigidbody rigidbody = m_Towers[i].GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                Vector3 position = TowerPlacements[i].position;
                position.y = m_Towers[i].GetCurrentHeight();
                if (m_Towers[i].IsBuilding)
                {
                    rigidbody.MovePosition(position);
                }
                else
                {
                    rigidbody.MovePosition(Vector3.Lerp(rigidbody.transform.position, position, 6.0f * Time.deltaTime));
                }
            }
        }
    }

    public void SpawnMob(GameObject mobPrefab)
    {
        GameObject mob = Instantiate(mobPrefab, m_MobSpawnPoint.position - Vector3.up * 2.0f, Quaternion.identity);
        PathComponent pathComponent = mob.GetComponent<PathComponent>();
        Mob mobComponent = mob.GetComponent<Mob>();
        if (pathComponent != null)
        {
            foreach (Transform pathPoint in MobPath)
            {
                pathComponent.AddPathPoint(pathPoint);
            }

            if (mobComponent != null)
            {
                pathComponent.PathCompleteAction = () => mobComponent.OnPathComplete();
            }

            pathComponent.FollowPath = false;
        }

        if (mobComponent != null)
        {
            mobComponent.Setup(m_MobSpawnPoint.position.y);
        }
    }

    public void AddTower(GameObject towerPrefab, float buildTime)
    {
        if (m_Towers.Count == TowerPlacements.Count || TowerPlacements.Count == 0)
        {
            return;
        }

        Tour tower = Instantiate(towerPrefab, TowerPlacements[0].position - Vector3.up * 1.5f, Quaternion.identity).GetComponent<Tour>();
        if (tower != null)
        {
            tower.StartBuild(buildTime, TowerPlacements[0].position.y);
            m_Towers.Insert(0, tower);
        }
    }

    public void SetIsOccupied(bool isOccupied)
    {
        m_IsOccupied = isOccupied;
    }
    public bool GetIsOccupied()
    {
        return m_IsOccupied;
    }

    public void RemoveTower(Tour tower)
    {
        m_Towers.Remove(tower);
    }

    public List<Transform> TowerPlacements;
    public List<Transform> MobPath;
    public List<Transform> PigPath;

    private Transform m_MobSpawnPoint;
    private List<Tour> m_Towers;
    private bool m_IsOccupied;
}
