using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public RectTransform m_curtainL;
    public RectTransform m_curtainR;
    public RectTransform m_Top;
    public GameObject m_background;
    public GameObject m_titleelement;

    public float m_AnimTime;
    private bool m_playPressed;
    private float m_animFactor;
    private bool m_sceneLoaded;
    // Start is called before the first frame update
    void Start()
    {
        m_playPressed = false;
        m_sceneLoaded = false;
    }

    public void OnPlayPressed()
    {
        float rectFact = (Screen.width) /855f;

        Debug.Log(" " + rectFact);
        m_animFactor = (rectFact * (m_curtainR.rect.width) + 10) / m_AnimTime;

        m_playPressed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_playPressed)
        {
            m_AnimTime -= Time.deltaTime;

            if (m_AnimTime < 2)
            {
                m_background.SetActive(false);
                m_titleelement.SetActive(false);
            }

            if(m_AnimTime < 1)
            {
                Vector3 top = new Vector3(m_Top.position.x, m_Top.position.y, m_Top.position.z);
                top.y = top.y + (500f * Time.deltaTime);
                m_Top.SetPositionAndRotation(top, m_Top.rotation);
            }

            if(m_AnimTime < 5 && !m_sceneLoaded)
            {
                SceneManager.LoadScene("main", LoadSceneMode.Additive);
                m_sceneLoaded = true;
            }

            if (m_AnimTime <= 0)
            {
                m_playPressed = false;
            }
            else
            {
                Vector3 newPositionL = new Vector3(m_curtainL.position.x, m_curtainL.position.y, m_curtainL.position.z);
                newPositionL.x = newPositionL.x - (m_animFactor * Time.deltaTime);
                m_curtainL.SetPositionAndRotation(newPositionL, m_curtainL.rotation);

                Vector3 newPositionR = new Vector3(m_curtainR.position.x, m_curtainR.position.y, m_curtainR.position.z);
                newPositionR.x = newPositionR.x + (m_animFactor * Time.deltaTime); 
                m_curtainR.SetPositionAndRotation(newPositionR, m_curtainR.rotation);
            }
        }
    }
}
