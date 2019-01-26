using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Awake()
    {
        isReady = false;
    }

    public void IsReady()
    {
        isReady = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && isReady)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 10.0f, Color.white, 1.0f);
            RaycastHit hit;
            int laneMask = 1 << LayerMask.NameToLayer("Lane");
            if (Physics.Raycast(ray, out hit, 100.0f, laneMask))
            {
                Lane lane = hit.collider.GetComponent<Lane>();
                if (lane != null)
                {
                    Game.Instance.StartActionOnLane(lane);
                }
            }
        }
    }

    private bool isReady;
}