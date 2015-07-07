using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Reward_Manager : MonoBehaviour {
    private Text speed_Text;
    private Text type_Text;
    private Text power_Text;
    private Text cost_Text;
    private Text cash_Text;

    public static bool placed = true;
    public static float speed;
    public static float power;
    public static Tower_Manager.Tower_Class type;
    public static int cost;

    public UI_Manager UIManager;

	// Use this for initialization
	void Start () {
        speed_Text = GameObject.Find("SPEEDVALUE").GetComponent<Text>();
        type_Text = GameObject.Find("TYPEVALUE").GetComponent<Text>();
        power_Text = GameObject.Find("POWERVALUE").GetComponent<Text>();
        cost_Text = GameObject.Find("COSTVALUE").GetComponent<Text>();
        cash_Text = GameObject.Find("CASHVALUE").GetComponent<Text>();

        UIManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UI_Manager>();

        generateTower(0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void generateTower(int round)
    {
        if(round == 0)
        {
            speed = 30f;
            power = 10f;
            cost = 0;
            type = Tower_Manager.Tower_Class.ATTACK;
            displayValues();
            return;
        }

        speed = Random.Range((20f + (round * .001f)), (25f + ((round * .001f) * 100f)));
        power = Random.Range(5f + (round / 2) * 2f, 5f + (round) * 2f);
        cost = Mathf.CeilToInt((speed * 4) + (power * 2));
        float randVal = Random.Range(0, 100);
        if (randVal < 50)
        {
            type = Tower_Manager.Tower_Class.ATTACK;
        }
        else if (randVal < 75)
        {
            type = Tower_Manager.Tower_Class.SLOW;
        }
        else
        {
            type = Tower_Manager.Tower_Class.MONEY;
        }

        if (type == Tower_Manager.Tower_Class.ATTACK) cost *= 2;

        displayValues();
    }

    private void displayValues()
    {
        speed_Text.text = "" + speed;
        power_Text.text = "" + power;
        type_Text.text  = type.ToString();
        cost_Text.text  = "$ " + cost;
        cash_Text.text  = "$ " + Mathf.FloorToInt(cost / 2f);

        if (cost == 0) cost_Text.text = "A gift from the Gods.";
    }

    public void displayPurchasedText()
    {
        UIManager.activatePurchasedText();
        placed = false;
    }

}
