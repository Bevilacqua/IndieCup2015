﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {
    private Text HealthText;
    private Text MoneyText;
    private GameObject RewardUI;

	// Use this for initialization
	void Start () {
        HealthText = GameObject.Find("HealthText").GetComponent<Text>();
        MoneyText = GameObject.Find("MoneyText").GetComponent<Text>();
        RewardUI = GameObject.Find("RewardScreen");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setHealth(int health)
    {
        HealthText.text = "" + health;
    }

    public void setMoney(int money)
    {
        MoneyText.text = "" + money;
    }

    public void alertRound(int round)
    {
        GameObject parentUI = (GameObject) GameObject.FindGameObjectWithTag("RoundTextParent");

        foreach (GameObject gobj in GameObject.FindGameObjectsWithTag("RoundText"))
        {
            gobj.GetComponent<Text>().text = "Round # " + round;
        }

        parentUI.GetComponent<Animator>().Play("Round", -1, 0f);
    }

    public GameObject getRewardUI()
    {
        return this.RewardUI;
    }
}
