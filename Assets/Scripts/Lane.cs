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
    }

    public List<Transform> TowerPlacements;

    private Transform m_MobSpawnPoint;
}
