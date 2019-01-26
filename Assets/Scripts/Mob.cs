using UnityEngine;

public class Mob : MonoBehaviour
{
    void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        m_Direction = -Vector3.right;
    }

    void FixedUpdate()
    {
        m_RigidBody.MovePosition(transform.position + m_Direction * Speed * Time.deltaTime);
    }

    public float Speed = 1.0f;

    private Rigidbody m_RigidBody;
    private Vector3 m_Direction;
}