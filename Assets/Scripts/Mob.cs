using UnityEngine;

public class Mob : MonoBehaviour
{
    void Awake()
    {
        m_Graphics = transform.Find("Graphics");
    }

    void Update()
    {
        if (m_Graphics != null)
        {
            m_GraphicsTimer += Time.deltaTime;

            float sin = WiggleAmplitude * Mathf.Sin(WiggleSpeed * m_GraphicsTimer);
            Quaternion quaternion = m_Graphics.rotation;
            quaternion.eulerAngles = new Vector3(quaternion.eulerAngles.x, quaternion.eulerAngles.y, sin);
            m_Graphics.rotation = quaternion;
        }
        
    }

    public void OnPathComplete()
    {
        Destroy(gameObject);
    }

    public float WiggleSpeed;
    public float WiggleAmplitude;

    private Transform m_Graphics;
    private float m_GraphicsTimer;
}