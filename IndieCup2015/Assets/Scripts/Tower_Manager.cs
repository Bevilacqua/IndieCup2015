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
    private float coolDownElapsedTime = 0f;
    private const float globalTimeBetweenAttack = 2f;

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

    void Update()
    {
        if (!initialized) return;

        if(globalTimeBetweenAttack < (.0001f * speedOfAttack))
        {
            Debug.Log("Shot fired - default");
            shootAtEnemyWithMostProgress();
            return;
        }

        if(coolDownElapsedTime >= (globalTimeBetweenAttack - (.01f * speedOfAttack)))
        {
            Debug.Log("Shot fired");
            shootAtEnemyWithMostProgress();
            coolDownElapsedTime = 0f;
        }
        else
        {
            coolDownElapsedTime += Time.deltaTime;
        }        
    }

    private void shootAtEnemyWithMostProgress()
    {
        GameObject[] listOfEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject farthest = null;
        if (listOfEnemies.Length == 0)
            return;
        else
            farthest = listOfEnemies[0];

        foreach(GameObject gobj in listOfEnemies)
        {
            Enemy_Manager enemy = gobj.GetComponent<Enemy_Manager>();

            if (enemy.getProgress() > farthest.GetComponent<Enemy_Manager>().getProgress())
                farthest = gobj;
        }

        //Find position of farthest
        Vector3 posOfFarthest = farthest.transform.position;
        //Add force towards position
        GameObject bullet = (GameObject)Instantiate(GameObject.Find("Manager_Map").GetComponent<Tower_Prefab_List>().prefab_AttackProjectile, transform.position, transform.localRotation);
        bullet.GetComponent<Bullet_Info>().init(this.towerClass, this.damage);
        transform.LookAt(posOfFarthest);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1000f * speedOfAttack * Time.smoothDeltaTime);
    }

    public void upgrade()
    {
        this.speedOfAttack *= 1.05f;
        this.damage *= 1.05f;
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
