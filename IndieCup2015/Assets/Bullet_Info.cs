using UnityEngine;
using System.Collections;

public class Bullet_Info : MonoBehaviour {
    private Tower_Manager.Tower_Class bulletClass;
    private float damage;

	// Use this for initialization
	void Start () {
	
	}

    public void init(Tower_Manager.Tower_Class bulletClass, float damage)
    {
        this.bulletClass = bulletClass;
        this.damage = damage;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public Tower_Manager.Tower_Class getBulletClass()
    {
        return this.bulletClass;
    }

    public float getDamage()
    {
        return this.damage;
    }
}
