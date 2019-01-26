using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourAOE : Tour
{
    public float m_SpeedReduction;
    public uint m_DamageOverTime;

    private Mob m_OverlappingMob;
    private bool m_MobIsOverlapping;

    void Start()
    {
        m_MobIsOverlapping = false;
    }

    void OnCollisionEnter(Collision collision)
    {

    }

    void OnCollisionExit(Collision collisionInfo)
    {

    }

    public override void DoUpdate(float deltaTime)
    {

    }
}
