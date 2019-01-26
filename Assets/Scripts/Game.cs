using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : GameSingleton<Game>
{
    public GameObject[] m_pigs;
    public List<Transform> m_PigsStartPath;
    public List<GameObject> m_Lanes;

    public List<GameObject> m_TowersObjects;
    private Towers m_towers;

    private void Awake()
    {
        if(m_TowersObjects.Count > 0)
        {
            TowerDataLists strawTowers = new TowerDataLists();
            strawTowers.ressourceType = RessourceType.RessourceType_Straw;
            strawTowers.towerData = new List<TowerData>();

            TowerDataLists woodTowers = new TowerDataLists();
            strawTowers.ressourceType = RessourceType.RessourceType_Wood;
            woodTowers.towerData = new List<TowerData>();

            TowerDataLists rockTowers = new TowerDataLists();
            strawTowers.ressourceType = RessourceType.RessourceType_Rock;
            rockTowers.towerData = new List<TowerData>();

            foreach (GameObject towerObj in m_TowersObjects)
            {
                Tour tourComponent = towerObj.GetComponent<Tour>();
                if (!tourComponent)
                {
                    continue;
                }

                TowerData towerData = new TowerData();
                towerData.tourType = tourComponent.m_TourType;
                towerData.gameObject = towerObj;

                switch(tourComponent.m_RessourceType)
                {
                    case RessourceType.RessourceType_Rock:
                        rockTowers.towerData.Add(towerData);
                        break;
                    case RessourceType.RessourceType_Straw:
                        strawTowers.towerData.Add(towerData);
                        break;
                    case RessourceType.RessourceType_Wood:
                        woodTowers.towerData.Add(towerData);
                        break;
                }
            }
            m_towers.towers = new List<TowerDataLists>();
            m_towers.towers.Add(strawTowers);
            m_towers.towers.Add(woodTowers);
            m_towers.towers.Add(rockTowers);
        }
    }

    public int HouseHealth { get; set; }

    void Start()
    {
        HouseHealth = 3;

        /*if (m_pigs.Length <= 0)
        {
            return;
        }
        Pig pigComponent = m_pigs[0].GetComponent<Pig>();
        Lane laneComponent = m_Lanes[0].GetComponent<Lane>();

        if (laneComponent != null)
        {
            List<Transform> pigPath = new List<Transform>();
            foreach (Transform pathPoint in m_PigsStartPath)
            {
                pigPath.Add(pathPoint);
            }

            pigPath.Add(laneComponent.MobPath[0]); // get the begin of the lane

            if (pigComponent != null)
            {
                pigComponent.UsePig(pigPath, m_towers.GetTowerObject(RessourceType.RessourceType_Straw, TourType.TourType_AOE), laneComponent.TowerPlacements[0]);
            }
        }*/
    }

    public void LooseHouseHealth()
    {
        HouseHealth -= 1;
        if (HouseHealth <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Tour[] towers = FindObjectsOfType<Tour>();
        foreach (Tour tower in towers)
        {
            Destroy(tower.gameObject);
        }

        Mob[] mobs = FindObjectsOfType<Mob>();
        foreach (Mob mob in mobs)
        {
            Destroy(mob.gameObject);
        }

        Missile[] missiles = FindObjectsOfType<Missile>();
        foreach (Missile missile in missiles)
        {
            Destroy(missile.gameObject);
        }

        FindObjectOfType<MobSpawner>().enabled = false;

        GameUI.Instance.ShowEndGamePanel();
    }
}