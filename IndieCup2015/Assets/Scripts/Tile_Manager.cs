using UnityEngine;
using System.Collections;
using Assets.Scripts.Non_Mono;

public class Tile_Manager : MonoBehaviour {
    private Tower_Manager towerManager;
    private Tower_Prefab_List towerPrefabs;


	// Use this for initialization
	void Start () {
        towerPrefabs = GameObject.Find("Manager_Map").GetComponent<Tower_Prefab_List>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter()
    {
        if(!gameObject.GetComponent<Tile_Info>().transversable)
            liftTile();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !gameObject.GetComponent<Tile_Info>().transversable)
        {
            if (hasTower()) 
            {
                getTower().upgrade();
            }
            else
            {
                //DEBUG: PLACES AN ATTACK TOWER
                lowerTile();
                GameObject tower = (GameObject)Instantiate(towerPrefabs.prefab_AttackTower, transform.position, transform.localRotation);
                towerManager = tower.GetComponent<Tower_Manager>();
                tower.transform.parent = transform;
                tower.transform.localPosition = new Vector3(0f, tower.transform.lossyScale.y, 0f);
                towerManager.init(Tower_Manager.Tower_Class.ATTACK, 10f, 1f);
                liftTile();
            } 
        }
    }

    void OnMouseExit()
    {
        if (!gameObject.GetComponent<Tile_Info>().transversable)
            lowerTile();
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
