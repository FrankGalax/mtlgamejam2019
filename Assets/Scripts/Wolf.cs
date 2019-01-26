using UnityEngine;

public class Wolf : MonoBehaviour
{
    void Awake()
    {
        m_Graphics = transform.Find("Graphics");
    }

    void Start()
    {
        m_State = State.Waiting;
        m_Timer = UnityEngine.Random.Range(MinWaitTime, MaxWaitTime);
    }

    void Update()
    {
        switch (m_State)
        {
            case State.Waiting:
                Waiting();
                break;
            case State.GoingIn:
                GoingIn();
                break;
            case State.Blowing:
                Blowing();
                break;
            case State.GoingOut:
                GoingOut();
                break;
        }
    }

    private void Waiting()
    {
        m_Timer -= Time.deltaTime;
        DontWiggle();
        if (m_Timer < 0)
        {
            m_State = State.GoingIn;
            PathComponent pathComponent = GetComponent<PathComponent>();
            if (pathComponent != null)
            {
                pathComponent.ClearPath();
                pathComponent.AddPathPoint(InPoint);
                pathComponent.FollowPath = true;
                pathComponent.PathCompleteAction = () =>
                {
                    m_State = State.Blowing;
                    pathComponent.FollowPath = false;
                    m_Timer = BlowTime;
                };
            }
        }
    }
    
    private void GoingIn()
    {
        Wiggle();
    }

    private void Blowing()
    {
        m_Timer -= Time.deltaTime;
        DontWiggle();
        if (m_Timer < 0)
        {
            m_State = State.GoingOut;
            PathComponent pathComponent = GetComponent<PathComponent>();
            if (pathComponent != null)
            {
                pathComponent.ClearPath();
                pathComponent.AddPathPoint(OutPoint);
                pathComponent.FollowPath = true;
                pathComponent.PathCompleteAction = () =>
                {
                    m_State = State.Waiting;
                    pathComponent.FollowPath = false;
                    m_Timer = UnityEngine.Random.Range(MinWaitTime, MaxWaitTime);
                };
            }
        }
    }

    private void GoingOut()
    {
        Wiggle();
    }

    private void Wiggle()
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

    private void DontWiggle()
    {
        if (m_Graphics != null)
        {
            m_Graphics.transform.rotation = Quaternion.Slerp(m_Graphics.transform.rotation, Quaternion.identity, 3.0f * Time.deltaTime);
        }
    }

    public float WiggleSpeed;
    public float WiggleAmplitude;
    public float MinWaitTime = 5;
    public float MaxWaitTime = 10;
    public float BlowTime = 5;
    public Transform OutPoint;
    public Transform InPoint;

    private enum State
    {
        Waiting,
        GoingIn,
        Blowing,
        GoingOut
    }

    private Transform m_Graphics;
    private float m_GraphicsTimer;

    private float m_Timer;
    private State m_State;
}