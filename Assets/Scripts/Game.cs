using UnityEngine;

public class Game : GameSingleton<Game>
{
    public int HouseHealth { get; set; }

    void Start()
    {
        HouseHealth = 3;
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