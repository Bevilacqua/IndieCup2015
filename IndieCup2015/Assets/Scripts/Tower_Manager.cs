using UnityEngine;
using System.Collections;

public class Tower_Manager : MonoBehaviour {
    
    public enum Tower_Class
    {
        ATTACK,
        SLOW,
        MONEY
    };

    private bool initialized = false;

    private Tower_Class towerClass;
    private float speedOfAttack;
    private float damage; //Value used for damage, slow down rate, money generated

    public void init(Tower_Class towerClass, float speedOfAttack, float damage)
    {
        this.towerClass = towerClass;
        this.speedOfAttack = speedOfAttack;
        this.damage = damage;
        this.initialized = true;
    }

    public void upgrade()
    {
        this.speedOfAttack *= 1.5f;
        this.damage *= 1.5f;
    }

    public Tower_Class getTowerClass()
    {
        return this.towerClass;
    }

    public float getSpeedOfAttack()
    {
        return this.speedOfAttack;
    }

    public float getDamage()
    {
        return this.damage;
    }

    public bool getInitialized()
    {
        return this.initialized;
    }
}
