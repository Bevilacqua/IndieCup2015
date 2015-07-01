using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {
    private Text HealthText;
    private Text MoneyText;

	// Use this for initialization
	void Start () {
        HealthText = GameObject.Find("HealthText").GetComponent<Text>();
        MoneyText = GameObject.Find("MoneyText").GetComponent<Text>();
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
}
