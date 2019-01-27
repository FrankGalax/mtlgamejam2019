using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourAOE : Tour
{
    public float m_SpeedReduction;
    public int m_DamageOverTime;

    private Mob m_OverlappingMob;
    private bool m_MobIsOverlapping;
    private DamageComponent m_damageComponent;

    void Start()
    {
        m_MobIsOverlapping = false;
        m_damageComponent = gameObject.GetComponent<DamageComponent>();
    }

    void OnTriggerEnter(Collider collision)
    {
        Mob mobComponent = collision.GetComponent<Mob>();
        ApplyDamage(mobComponent);
        Debug.Log("damage on :" + collision.name);

        //if()
    }

    void OnTriggerExit(Collider collision)
    {
        Mob mobComponent = collision.GetComponent<Mob>();
        if(mobComponent)
        {
            PathComponent pathComponent = mobComponent.gameObject.GetComponent<PathComponent>();
            if (pathComponent)
            {
                pathComponent.ResetSpeed();
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
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
        DamageComponent damageComponent = mobComponent.gameObject.GetComponent<DamageComponent>();
        if(damageComponent)
        {
            damageComponent.TakeDamage(m_DamageOverTime, mobComponent.gameObject);
        }
    }

    //void 
    public override void DoUpdate(float deltaTime)
    {
    }
}
