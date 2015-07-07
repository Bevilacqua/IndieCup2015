using UnityEngine;
using System.Collections;

public class Tower_Manager : MonoBehaviour
{

    public enum Tower_Class
    {
        ATTACK,
        SLOW,
        MONEY,
        TEMPLE
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

        if (globalTimeBetweenAttack < (.0001f * speedOfAttack))
        {
            shootAtClosestEnemy();
            return;
        }

        if (coolDownElapsedTime >= (globalTimeBetweenAttack - (.01f * speedOfAttack)))
        {
            if (towerClass == Tower_Class.TEMPLE && !Application.isMobilePlatform && Input.GetMouseButton(0))
                shootAtMousePos();
            else
                shootAtClosestEnemy();

            coolDownElapsedTime = 0f;
        }
        else
        {
            coolDownElapsedTime += Time.deltaTime;
        }
    }

    private void shootAtMousePos()
    {
        if (towerClass == Tower_Class.MONEY)
        {
            addMoney((int)damage);
            return;
        }

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            return;

        Vector3 posOfMouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 25f));
        posOfMouse.y = transform.position.y;
        //Add force towards position
        GameObject pre_bullet = null;
        if (towerClass == Tower_Class.SLOW)
            pre_bullet = GameObject.Find("Manager_Map").GetComponent<Tower_Prefab_List>().prefab_SlowProjectile;
        else
            pre_bullet = GameObject.Find("Manager_Map").GetComponent<Tower_Prefab_List>().prefab_AttackProjectile;

        GameObject bullet = (GameObject)Instantiate(pre_bullet, new Vector3(transform.position.x, 2.5f, transform.position.z), transform.localRotation);
        bullet.GetComponent<Bullet_Info>().init(this.towerClass, this.damage);
        transform.LookAt(posOfMouse);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1000f * speedOfAttack * Time.smoothDeltaTime);
        if (towerClass == Tower_Class.TEMPLE)
            transform.localRotation = new Quaternion();
    }

    private void shootAtEnemyWithMostProgress()
    {
        if (towerClass == Tower_Class.MONEY)
        {
            addMoney((int)damage);
            return;
        }

        GameObject[] listOfEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject farthest = null;
        if (listOfEnemies.Length == 0)
            return;
        else
            farthest = listOfEnemies[0];

        foreach (GameObject gobj in listOfEnemies)
        {
            Enemy_Manager enemy = gobj.GetComponent<Enemy_Manager>();

            if (enemy.getProgress() > farthest.GetComponent<Enemy_Manager>().getProgress())
                farthest = gobj;
        }

        //Find position of farthest
        Vector3 posOfFarthest = farthest.transform.position;
        posOfFarthest.y = transform.position.y;
        //Add force towards position
        GameObject pre_bullet = null;
        if (towerClass == Tower_Class.SLOW)
            pre_bullet = GameObject.Find("Manager_Map").GetComponent<Tower_Prefab_List>().prefab_SlowProjectile;
        else
            pre_bullet = GameObject.Find("Manager_Map").GetComponent<Tower_Prefab_List>().prefab_AttackProjectile;

        GameObject bullet = (GameObject)Instantiate(pre_bullet, new Vector3(transform.position.x, 2.5f, transform.position.z), transform.localRotation);
        bullet.GetComponent<Bullet_Info>().init(this.towerClass, this.damage);
        transform.LookAt(posOfFarthest);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1000f * speedOfAttack * Time.smoothDeltaTime);
        bullet.transform.LookAt(posOfFarthest);
        if(towerClass == Tower_Class.TEMPLE)
            transform.localRotation = new Quaternion();
    }

    private void shootAtClosestEnemy()
    {
        if (towerClass == Tower_Class.MONEY)
        {
            addMoney((int)damage);
            return;
        } 
        
        GameObject[] listOfEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float closestValue = 0;

        if (listOfEnemies.Length == 0)
            return;
        else
        {
            closestValue = Vector3.Distance(transform.position, listOfEnemies[0].transform.position);
            closest = listOfEnemies[0];
        }

        foreach (GameObject gobj in listOfEnemies)
        {
            float distance = Vector3.Distance(transform.position, gobj.transform.position);
            if (distance < closestValue)
            {
                closestValue = distance;
                closest = gobj;
            }
        }

        //Find position of farthest
        Vector3 posOfClosest = closest.transform.position;
        posOfClosest.y = transform.position.y;
        //Add force towards position
        GameObject pre_bullet = null;
        if (towerClass == Tower_Class.SLOW)
            pre_bullet = GameObject.Find("Manager_Map").GetComponent<Tower_Prefab_List>().prefab_SlowProjectile;
        else
            pre_bullet = GameObject.Find("Manager_Map").GetComponent<Tower_Prefab_List>().prefab_AttackProjectile;

        GameObject bullet = (GameObject)Instantiate(pre_bullet, new Vector3(transform.position.x, 2.5f, transform.position.z), transform.localRotation);
        bullet.GetComponent<Bullet_Info>().init(this.towerClass, this.damage);
        transform.LookAt(posOfClosest);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1000f * speedOfAttack * Time.smoothDeltaTime);
        bullet.transform.LookAt(posOfClosest);
        if (towerClass == Tower_Class.TEMPLE)
            transform.localRotation = new Quaternion();
    }

    private void addMoney(int amount)
    {
        GameObject.Find("Manager_Game").GetComponent<Game_Manager>().addMoneyFromTower(amount);
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