using UnityEngine;
using System.Collections;

public class Game_Manager : MonoBehaviour {
    public static int MAX_HEALTH = 100;
    public static float MAX_DIFFICULTY = 1f;

    private int health = 100;
    private float difficulty = 1f;
    private int money = 0;

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

    public int getMoney()
    {
        return this.money;
    }
}
