using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Non_Mono;

public class Enemy_Manager : MonoBehaviour
{
    private int pathFindingProgress;
    private List<Node> path = new List<Node>();
    private bool initialized = false;

    private float speed = 1f;
    private float health = 100f;

    void Start()
    {

    }

    public void init(List<Node> nodeList, Transform mapParent)
    {
        this.path = nodeList;
        this.pathFindingProgress = 0;
        transform.parent = mapParent;
        transform.localPosition = new Vector3(path[pathFindingProgress].getGameObject().transform.localPosition.x, path[pathFindingProgress].getGameObject().transform.localPosition.y + 1f, path[pathFindingProgress].getGameObject().transform.localPosition.z);
        this.initialized = true;
    }

    public void init(List<Node> nodeList, Transform mapParent, float speed, float health)
    {
        this.speed = speed;
        this.health = health;
        init(nodeList, mapParent);
    }

    void OnTriggerEnter(Collider collider)
    {
        Destroy(collider.gameObject);
        if (collider.gameObject.GetComponent<Bullet_Info>().getBulletClass() == Tower_Manager.Tower_Class.ATTACK)
            health -= collider.gameObject.GetComponent<Bullet_Info>().getDamage();
        else if (collider.gameObject.GetComponent<Bullet_Info>().getBulletClass() == Tower_Manager.Tower_Class.SLOW)
        {
            if(speed > 0.1f)
                speed -= collider.gameObject.GetComponent<Bullet_Info>().getDamage() / 100f;
        }
    }


    void Update()
    {
        if (initialized)
        {
            Vector3 diff = new Vector3(path[pathFindingProgress].getGameObject().transform.position.x - transform.position.x, transform.position.y, path[pathFindingProgress].getGameObject().transform.position.z - transform.position.z);
            Vector3 alterations = new Vector3();
            if (diff.x < 0)
            {
                if (Mathf.Abs(diff.x) < (speed * Time.deltaTime))
                    alterations.x = diff.x;
                else
                    alterations.x = -speed * Time.deltaTime;
            }
            else
            {
                if (Mathf.Abs(diff.x) < (speed * Time.deltaTime))
                    alterations.x = diff.x;
                else
                    alterations.x = speed * Time.deltaTime;
            }

            if (diff.z < 0)
            {
                if (Mathf.Abs(diff.z) < (speed * Time.deltaTime))
                    alterations.z = diff.z;
                else
                    alterations.z = -speed * Time.deltaTime;
            }
            else
            {
                if (Mathf.Abs(diff.z) < (speed * Time.deltaTime))
                    alterations.z = diff.z;
                else
                    alterations.z = speed * Time.deltaTime;
            }

            transform.position += alterations;

            if (diff.x == 0f && diff.z == 0f)
            {
                pathFindingProgress++;

                //DEBUG
                if (pathFindingProgress > path.Count - 1) Destroy(gameObject);
            }

            if (health <= 0) Destroy(gameObject);
        }
    }

    public int getProgress()
    {
        return this.pathFindingProgress;
    }
}