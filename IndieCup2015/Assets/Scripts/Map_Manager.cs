﻿using UnityEngine;
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

    public Map_Creator creator;
    private Map_Info map_info;
    private List<Node> path;

    public GameObject enemy;
    private GameObject map;

    // Use this for initialization
    void Start()
    {
        createMap();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            spawnEnemy();
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            destroyMap();
            createMap();
        }
    }

    public void destroyMap()
    {
        Destroy(GameObject.FindGameObjectWithTag("Map"));
    }

    public void createMap()
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
            Debug.Log("Map destroyed - press backspace to regenerate");
        }
        
    }

    public void spawnEnemy()
    {
        GameObject obj = (GameObject)Instantiate(enemy, new Vector3(), transform.localRotation);
        obj.GetComponent<Enemy_Manager>().init(path, this.map.transform);
    }
}