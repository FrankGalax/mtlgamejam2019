using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
public class WaveData
{
    public float TimeLimit;
    public float SpawnTime;
    public float Mob1Chance;
    public float Mob2Chance;
    public float Mob3Chance;
    public float MinWolfTime;
    public float MaxWolfTime;
}

public class MobSpawner : MonoBehaviour
{
    void Awake()
    {
        m_Lanes = FindObjectsOfType<Lane>().ToList();
        if (WaveDatas.Count > 0)
        {
            WaveData waveData = WaveDatas[0];
            
            Wolf wolf = FindObjectOfType<Wolf>();
            if (wolf != null)
            {
                wolf.MinWaitTime = waveData.MinWolfTime;
                wolf.MaxWaitTime = waveData.MaxWolfTime;
            }
        }
    }

    void Start()
    {
        m_GameTime = 0;
        WaveData waveData = GetWaveData();
        if (waveData != null)
        {
            m_Timer = waveData.SpawnTime;
        }
    }

    void Update()
    {
        m_Timer -= Time.deltaTime;
        m_GameTime += Time.deltaTime;
        WaveData waveData = GetWaveData();
        if (waveData != null)
        {
            Wolf wolf = FindObjectOfType<Wolf>();
            if (wolf != null)
            {
                wolf.MinWaitTime = waveData.MinWolfTime;
                wolf.MaxWaitTime = waveData.MaxWolfTime;
            }
        }

        if (m_Timer < 0)
        {
            if (waveData != null)
            {
                m_Timer = waveData.SpawnTime;

                if (m_Lanes.Count > 0)
                {
                    Lane lane = m_Lanes[UnityEngine.Random.Range(0, m_Lanes.Count)];
                    GameObject mobPrefab = GetMobPrefab(waveData);
                    if (mobPrefab != null)
                    {
                        lane.SpawnMob(mobPrefab);
                    }
                }
            }
        }
    }

    private WaveData GetWaveData()
    {
        foreach (WaveData waveData in WaveDatas)
        {
            if (waveData.TimeLimit > m_GameTime)
            {
                return waveData;
            }
        }
        return null;
    }

    private GameObject GetMobPrefab(WaveData waveData)
    {
        float r = UnityEngine.Random.value;
        float current = waveData.Mob1Chance;
        if (r < current)
        {
            return Mob1;
        }
        current += waveData.Mob2Chance;
        if (r < current)
        {
            return Mob2;
        }
        return Mob3;
    }

    public GameObject Mob1;
    public GameObject Mob2;
    public GameObject Mob3;
    public List<WaveData> WaveDatas;

    private List<Lane> m_Lanes;
    private float m_Timer;
    private float m_GameTime;
}
