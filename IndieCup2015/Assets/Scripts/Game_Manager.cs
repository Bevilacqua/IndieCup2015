using UnityEngine;
using System.Collections;

public class Game_Manager : MonoBehaviour {
    public static int MAX_HEALTH = 100;
    public static float MAX_DIFFICULTY = 1f;

    private int health = 100;
    private float difficulty = 1f;
    private int money = 0;

    private int round = 0;
    private int enemiesDeployed = 0;

    private UI_Manager UIManager;
    private Map_Manager mapManager;

	// Use this for initialization
	void Start () {
        UIManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UI_Manager>();
        mapManager = GameObject.Find("Manager_Map").GetComponent<Map_Manager>();
        this.health = MAX_HEALTH;
        this.difficulty = MAX_DIFFICULTY;
        this.money = 0;
        UIManager.setHealth(health);
        UIManager.setMoney(money);
    }
	
	// Update is called once per frame
	void Update () {
	    if(round > 0)
        {
            if(enemiesDeployed < round)
            {
               //TODO: randomize enemy deployment time
                if(Random.Range(0, 200) == 2)
                {
                    mapManager.spawnEnemy((50f * difficulty), difficulty);
                    enemiesDeployed++;
                }
            }
            else
            {
                if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    nextRound();
                }
            }
        }
	}

    public void nextRound()
    {
        round++;
        enemiesDeployed = 0;
        difficulty += .05f;
        UIManager.alertRound(round);
        if (round == 1) return; //Player won't have any money at this point
        UIManager.getRewardUI().SetActive(true);
        UIManager.getRewardUI().GetComponent<Animator>().Play("RoundEnd- ENTER");
        UIManager.getRewardUI().GetComponent<Reward_Manager>().generateTower(round);
    }

    public void addRoundNoReward()
    {
        round++;
        enemiesDeployed = 0;
        UIManager.alertRound(round);
    }

    public void enemyPass(float enemyHealth)
    {
        health -= (int)(enemyHealth * difficulty);
        UIManager.setHealth(health);
    }

    public void enemyKilled()
    {
        this.money += (int)(100 / difficulty);
        UIManager.setMoney(money);
    }

    public void addMoneyFromTower(int amount)
    {
        money += amount;
        UIManager.setMoney(money);
    }

    public int getMoney()
    {
        return this.money;
    }

    public void acceptTowerReward()
    {
        subtractMoney(Reward_Manager.cost);
    }

    public void subtractMoney(int money)
    {
        this.money -= money;
        UIManager.setMoney(this.money);
    }

    public void acceptCashReward()
    {
        money += Mathf.FloorToInt(Reward_Manager.cost / 2f);
        UIManager.setMoney(money);
    }
}
