using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RessourceType
{
    RessourceType_Straw,
    RessourceType_Wood,
    RessourceType_Rock
}
public struct Ressource
{
    public RessourceType m_RessourceType;
    public uint m_HP;

    public uint Attack(uint damage)
    {
        if(m_HP < damage)
        {
            return m_HP = 0;
        }
        else
        {
            return m_HP = m_HP - damage;
        }
    }
}
