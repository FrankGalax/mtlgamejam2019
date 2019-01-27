﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
public struct WaveData
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
    }

    void Start()
    {
        m_Timer = MobSpawnTime;
    }

    void Update()
    {
        m_Timer -= Time.deltaTime;

        if (m_Timer < 0)
        {
            m_Timer = MobSpawnTime;

            if (m_Lanes.Count > 0)
            {
                Lane lane = m_Lanes[UnityEngine.Random.Range(0, m_Lanes.Count)];
                lane.SpawnMob(BasicMob);
            }
        }
    }

    public GameObject BasicMob;
    public float MobSpawnTime;
    public List<WaveData> WaveDatas;

    private List<Lane> m_Lanes;
    private float m_Timer;
}
