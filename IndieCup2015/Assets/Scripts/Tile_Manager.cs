using UnityEngine;
using System.Collections;
using Assets.Scripts.Non_Mono;
using UnityEngine.UI;

public class Tile_Manager : MonoBehaviour {
    private Tower_Manager towerManager;
    private Tower_Prefab_List towerPrefabs;

    private UI_Manager UIManager;

	// Use this for initialization
	void Start () {
        towerPrefabs = GameObject.Find("Manager_Map").GetComponent<Tower_Prefab_List>();
        UIManager = GameObject.Find("UI_Canvas").GetComponent<UI_Manager>();
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter()
    {
        if(!gameObject.GetComponent<Tile_Info>().transversable)
        {
            liftTile();
        }

        if (hasTower())
        {
            UIManager.displayTowerInfo(towerManager.getTowerClass(), towerManager.getDamage(), towerManager.getSpeedOfAttack());
        }
        else
        {
            UIManager.hideTowerInfo();
        }
    }

    void OnMouseOver()
    {
        if (hasTower() && Input.GetMouseButtonDown(0)) 
        {
            UIManager.showUpgradeScreen(getTower());
        }

        if (Input.GetMouseButtonDown(0) && !gameObject.GetComponent<Tile_Info>().transversable)
        {
            if(!Reward_Manager.placed && !hasTower())
            {
                //DEBUG: PLACES AN ATTACK TOWER
                lowerTile();
                GameObject tower = null;

                switch(Reward_Manager.type)
                {
                    case Tower_Manager.Tower_Class.ATTACK:
                        tower = (GameObject)Instantiate(towerPrefabs.prefab_AttackTower, transform.position, transform.localRotation);
                        break;
                    case Tower_Manager.Tower_Class.MONEY:
                        tower = (GameObject)Instantiate(towerPrefabs.prefab_MoneyTower, transform.position, transform.localRotation);
                        break;
                    case Tower_Manager.Tower_Class.SLOW:
                        tower = (GameObject)Instantiate(towerPrefabs.prefab_SlowTower, transform.position, transform.localRotation);
                        break;
                }

                towerManager = tower.GetComponent<Tower_Manager>();
                tower.transform.parent = transform;
                tower.transform.localPosition = new Vector3(0f, 1f, 0f);
                towerManager.init(Reward_Manager.type, Reward_Manager.speed,Reward_Manager.power);
                liftTile();
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<UI_Manager>().deactivatePurchasedText();
                Reward_Manager.placed = true;
            }
        }
    }

    void OnMouseExit()
    {
        if (!gameObject.GetComponent<Tile_Info>().transversable)
            lowerTile();

        UIManager.hideTowerInfo();
       
    }

    public void liftTile()
    {
        transform.position = new Vector3(transform.position.x, .25f, transform.position.z);
    }

    public void lowerTile()
    {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }

    public void setTowerManager(Tower_Manager manager)
    {
        this.towerManager = manager;
    }

    public Tower_Manager getTower()
    {
        return this.towerManager;
    }

    public bool hasTower()
    {
        if (towerManager == null) return false;
        else return true;
    }
}
