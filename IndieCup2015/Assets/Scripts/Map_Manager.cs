using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Non_Mono;
using System;

public class Map_Manager : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public Vector2 spawnLocation = new Vector2();
    public Vector2 goalLocation = new Vector2();

    public GameObject prefab_temple;

    public Map_Creator creator;
    private Map_Info map_info;
    private List<Node> path;

    public GameObject enemy;
    private GameObject map;

    // Use this for initialization
    void Start()
    {
        if (createMap() == false)
            Application.LoadLevel("default");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void destroyMap()
    {
        Destroy(GameObject.FindGameObjectWithTag("Map"));
    }

    public bool createMap()
    {
        try
        {
            creator = gameObject.GetComponent<Map_Creator>();
            map = creator.createMap(height, width);

            map_info = map.GetComponent<Map_Info>();
            //        goalLocation.Set((int)width / 2, (int)height / 2);
            path = map_info.init(spawnLocation, goalLocation);

            foreach (Node node in path)
            {
                //DEBUG:
                node.getGameObject().GetComponent<Tile_Manager>().liftTile();
                node.getGameObject().GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
        catch(Exception e)
        {
            Debug.Log("Map created incorrectly: " + e.Message);
            destroyMap();
            return false;
        }

        if (path.Count == 0)
            return false;

        //TODO: Place Temple (Tile_Manager)
        GameObject temple = (GameObject) Instantiate(prefab_temple, map_info.getTileMap()[(int) goalLocation.x, (int) goalLocation.y].transform.position, transform.localRotation);
        Tower_Manager towerManager = temple.GetComponent<Tower_Manager>();
        map_info.getTileMap()[(int)goalLocation.x, (int)goalLocation.y].GetComponent<Tile_Manager>().setTowerManager(towerManager);
        temple.transform.parent = map_info.getTileMap()[(int)goalLocation.x, (int)goalLocation.y].transform;
        temple.transform.localPosition = new Vector3(0f, 1f, 0f);
        towerManager.init(Tower_Manager.Tower_Class.TEMPLE, 40, 25);

        return true;
        
    }

    public void spawnEnemy()
    {
        Vector3 pos = map_info.getTileMap()[(int)spawnLocation.x, (int)spawnLocation.y].transform.position;
        pos.y = -1f;
        GameObject obj = (GameObject)Instantiate(enemy, pos, transform.localRotation);
        obj.GetComponent<Enemy_Manager>().init(path, this.map.transform);
        obj.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

    public void spawnEnemy(float health, float speed)
    {
        Vector3 pos = map_info.getTileMap()[(int)spawnLocation.x, (int)spawnLocation.y].transform.position;
        pos.y = -1f;
        GameObject obj = (GameObject)Instantiate(enemy, pos, transform.localRotation); obj.GetComponent<Enemy_Manager>().init(path, this.map.transform, health, speed);
        obj.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }
}