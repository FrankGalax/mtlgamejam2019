using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourAOE : Tour
{
    public float m_SpeedReduction;
    public int m_DamageOverTime;
    public int m_ActionCost;

    private Dictionary<Mob, float> m_OverlappingMobs;
    private bool m_MobIsOverlapping;
    private DamageComponent m_damageComponent;
    private float m_powerTimer;

    void Awake()
    {
        m_OverlappingMobs = new Dictionary<Mob, float>();
        m_MobIsOverlapping = false;
        m_damageComponent = gameObject.GetComponent<DamageComponent>();
        m_powerTimer = 1.0f;
    }

    void OnTriggerEnter(Collider collision)
    {
        Mob mobComponent = collision.GetComponent<Mob>();
        if(mobComponent)
        {
            m_OverlappingMobs[mobComponent] =  1.0f;
            ApplySpeedReduction(mobComponent);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Mob mobComponent = other.GetComponent<Mob>();
        if (!mobComponent)
        {
            return;
        }

        m_MobIsOverlapping = true;

        float timer;
        if (!m_OverlappingMobs.TryGetValue(mobComponent, out timer)) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            ApplyDamage(mobComponent);
            timer = 1.0f;
        }
        m_OverlappingMobs[mobComponent] = timer;
    }
    void OnTriggerExit(Collider collision)
    {
        Mob mobComponent = collision.GetComponent<Mob>();
        if(mobComponent)
        {
            m_OverlappingMobs.Remove(mobComponent);
            PathComponent pathComponent = mobComponent.gameObject.GetComponent<PathComponent>();
            if (pathComponent)
            {
                pathComponent.ResetSpeed();
            }
        }
    }

    void ApplySpeedReduction(Mob mobComponent)
    {
        PathComponent pathComponent = mobComponent.gameObject.GetComponent<PathComponent>();
        if(pathComponent)
        {
            pathComponent.SpeedReduction(m_SpeedReduction);
        }

    }

    void ApplyDamage(Mob mobComponent)
    {
        if(!mobComponent)
        {
            return;
        }
        DamageComponent damageComponent = mobComponent.gameObject.GetComponent<DamageComponent>();
        if(damageComponent)
        {
            damageComponent.TakeDamage(m_DamageOverTime, mobComponent.gameObject);
        }

        if (!mobComponent)
        {
            m_OverlappingMobs.Remove(mobComponent);
        }
    }

    //void 
    public override void DoUpdate(float deltaTime)
    {  
        if(m_MobIsOverlapping)
        {
            m_powerTimer -= deltaTime;
            if(m_powerTimer <= 0)
            {
                m_damageComponent.TakeDamage(m_ActionCost, gameObject);
                m_powerTimer = 1.0f;
            }
            m_MobIsOverlapping = false;
        }
    }

    private void OnDestroy()
    {
        foreach(KeyValuePair<Mob, float> pair in m_OverlappingMobs)
        {
            if(pair.Key)
            {
                PathComponent pathComponent = pair.Key.GetComponent<PathComponent>();
                if (pathComponent)
                {
                    pathComponent.ResetSpeed();
                }
            }
        }
    }
}
