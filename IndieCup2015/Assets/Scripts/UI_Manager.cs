using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {
    private Text HealthText;
    private Text MoneyText;
    private GameObject RewardUI;
    private GameObject PurchasedInfo;
    private GameObject TowerInfoUI;
    private GameObject UpgradeScreen;

	// Use this for initialization
	void Start () {
        HealthText = GameObject.Find("HealthText").GetComponent<Text>();
        MoneyText = GameObject.Find("MoneyText").GetComponent<Text>();
        RewardUI = GameObject.Find("RewardScreen");
        PurchasedInfo = GameObject.Find("TowerPurchasedInfo");
        TowerInfoUI = GameObject.Find("TowerInfo");
        UpgradeScreen = GameObject.Find("TowerUpgradeScreen");
        UpgradeScreen.SetActive(false);
        PurchasedInfo.SetActive(false);
        TowerInfoUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (GameJolt.API.Manager.Instance.CurrentUser != null)
            GameJolt.API.Trophies.Unlock(34433);
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

    public void activatePurchasedText()
    {
        PurchasedInfo.SetActive(true);
        PurchasedInfo.GetComponent<Animator>().Play("PURCHASEDINFO");
    }

    public void deactivatePurchasedText()
    {
        PurchasedInfo.GetComponent<Animator>().Play("PURCHASEDINFO_REVERSE");
    }

    public void displayTowerInfo(Tower_Manager.Tower_Class type, float power, float speed)
    {
        TowerInfoUI.SetActive(true);
        TowerInfoUI.transform.GetChild(0).GetComponent<Text>().text = "" + type;
        TowerInfoUI.transform.GetChild(1).GetComponent<Text>().text = "" + power;
        TowerInfoUI.transform.GetChild(2).GetComponent<Text>().text = "" + speed;
    }

    public void hideTowerInfo()
    {
        TowerInfoUI.SetActive(false);
    }

    public void showUpgradeScreen(Tower_Manager towerManager)
    {
        UpgradeScreen.SetActive(true);
        UpgradeScreen.GetComponent<Animator>().Play("DISPLAY");
        GameObject.Find("SELLCOST").GetComponent<Text>().text = " + $   " + towerManager.getValue();
        GameObject.Find("UPGRADECOST").GetComponent<Text>().text = " - $   " + Mathf.FloorToInt(towerManager.getValue() * .85f);
        UpgradeScreen.GetComponent<UpgradeScreen_Manager>().setTowerManager(towerManager);
    }

    public void hideUpgradeScreen()
    {
        UpgradeScreen.SetActive(false);
    }
}
