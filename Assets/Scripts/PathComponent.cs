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
    }

    void Start()
    {
        FollowPath = true;
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
                Vector3 newPosition = transform.position + direction * Speed * Time.deltaTime;
                Vector3 newPositionToNextPoint = nextPoint.position - newPosition;

                if (Vector3.Dot(newPositionToNextPoint, direction) <= 0)
                {
                    Debug.Log("OK");
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

    public float Speed = 1.0f;

    public Action PathCompleteAction { private get; set; }
    public bool FollowPath { private get; set; }

    private List<Transform> m_Path;
    private Rigidbody m_Rigidbody;
}
