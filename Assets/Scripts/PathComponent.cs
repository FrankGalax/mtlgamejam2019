using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathComponent : MonoBehaviour
{
    void Awake()
    {
        m_Path = new List<Transform>();
        m_Rigidbody = GetComponent<Rigidbody>();
        FollowPath = true;
    }

    void Start()
    {
        m_internalSpeed = Speed;
    }

    void FixedUpdate()
    {
        if (FollowPath && m_Path.Count != 0)
        {
            Transform nextPoint = m_Path[0];
            Vector3 direction = nextPoint.position - transform.position;
            if (direction.sqrMagnitude > 0)
            {
                direction.Normalize();
                Vector3 newPosition = transform.position + direction * m_internalSpeed * Time.deltaTime;
                Vector3 newPositionToNextPoint = nextPoint.position - newPosition;

                if (Vector3.Dot(newPositionToNextPoint, direction) <= 0)
                {
                    m_Path.RemoveAt(0);
                    m_Rigidbody.MovePosition(nextPoint.position);

                    if (m_Path.Count == 0 && PathCompleteAction != null)
                    {
                        PathCompleteAction();
                        PathCompleteAction = null;
                    }
                }
                else
                {
                    m_Rigidbody.MovePosition(newPosition);
                }
            }
        }
    }
    
    public void AddPathPoint(Transform pathPoint)
    {
        m_Path.Add(pathPoint);
    }

    public void ClearPath()
    {
        m_Path.Clear();
    }

    public void SpeedReduction(float reductionRation)
    {
        m_internalSpeed = Speed - (reductionRation * Speed);
    }

    public void ResetSpeed()
    {
        m_internalSpeed = Speed;
    }

    public float Speed = 1.0f;

    public Action PathCompleteAction { private get; set; }
    public bool FollowPath { private get; set; }

    private List<Transform> m_Path;
    private Rigidbody m_Rigidbody;
    private float m_internalSpeed;
}
