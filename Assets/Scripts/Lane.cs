using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    void Awake()
    {
        m_MobSpawnPoint = transform.Find("MobSpawnPoint");
    }

    public void SpawnMob(GameObject mobPrefab)
    {
        GameObject mob = Instantiate(mobPrefab, m_MobSpawnPoint.position, Quaternion.identity);
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
        }
    }

    public List<Transform> TowerPlacements;
    public List<Transform> MobPath;

    private Transform m_MobSpawnPoint;
}
