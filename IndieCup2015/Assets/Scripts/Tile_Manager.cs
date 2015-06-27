﻿using UnityEngine;
using System.Collections;
using Assets.Scripts.Non_Mono;

public class Tile_Manager : MonoBehaviour {
    private Tower tower;

	// Use this for initialization
	void Start () {
	
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
        if (Input.GetMouseButtonDown(0) && gameObject.GetComponent<Tile_Info>().transversable)
        {
            Debug.Log("Node " + gameObject.GetComponent<Tile_Info>().getNode().getIndex() + "'s "+ gameObject.GetComponent<Tile_Info>().getTileMapCoords() + " neighbors:");
            foreach (Node node in gameObject.GetComponent<Tile_Info>().getNode().getNeighboringNodes())
            {
                Debug.Log("   -| " + node.getIndex() + " | " + node.getMapCoordinates());
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

    public void setTower(Tower tower)
    {
        this.tower = tower;

    }

    public Tower getTower()
    {
        return this.tower;
    }

    public bool hasTower()
    {
        if (tower == null) return false;
        else return true;
    }
}
