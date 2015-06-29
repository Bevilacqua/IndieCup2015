using UnityEngine;
using System.Collections;

public class Game_Manager : MonoBehaviour {
    public static int MAX_HEALTH = 100;
    public static float MAX_DIFFICULTY = 1f;

    private int health = 100;
    private float difficulty = 1f;
    private int money = 0;

	// Use this for initialization
	void Start () {
        reset(MAX_HEALTH, MAX_DIFFICULTY);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void reset(int health, float difficulty)
    {
        this.health = health;
        this.difficulty = difficulty;
    }

    public void enemyPass(float enemyHealth)
    {
        health -= (int)(enemyHealth * difficulty);
    }

    public void enemyKilled()
    {
        this.money += (int)(100 / difficulty);
    }

    public int getMoney()
    {
        return this.money;
    }
}
