using UnityEngine;
using System.Collections;

public class UpgradeScreen_Manager : MonoBehaviour {
    private Tower_Manager tower_manager;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setTowerManager(Tower_Manager manager)
    {
        tower_manager = manager;
    }

    public void upgrade()
    {
        GameObject.Find("Manager_Game").GetComponent<Game_Manager>().subtractMoney(Mathf.FloorToInt(tower_manager.getValue() * .85f));
        tower_manager.upgrade();
    }

    public void sell()
    {
        GameObject.Find("Manager_Game").GetComponent<Game_Manager>().addMoneyFromTower(tower_manager.getValue());
        Destroy(tower_manager.gameObject);
    }
}
