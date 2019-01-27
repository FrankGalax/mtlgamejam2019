using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject DebugTower;

    void Awake()
    {
        isReady = true;
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
            RaycastHit hit;
            int laneMask = 1 << LayerMask.NameToLayer("Lane");
            if (Physics.Raycast(ray, out hit, 100.0f, laneMask))
            {
                Lane lane = hit.collider.GetComponent<Lane>();
                if (lane != null && !lane.GetIsOccupied())
                {
                    isReady = false;
                    GameUI.Instance.ShowChoice(Input.mousePosition, lane);
                }
            }
        }

        if (Input.GetMouseButtonUp(1) && DebugTower != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int laneMask = 1 << LayerMask.NameToLayer("Lane");
            if (Physics.Raycast(ray, out hit, 100.0f, laneMask))
            {
                Lane lane = hit.collider.GetComponent<Lane>();
                if (lane != null)
                {
                    lane.AddTower(DebugTower);
                }
            }
        }
    }

    private bool isReady;
}