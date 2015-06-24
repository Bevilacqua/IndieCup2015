using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Non_Mono
{
    public class Tower
    {
        public enum Tower_Class
        {
            ATTACK,
            SLOW,
            MONEY
        };

        private Tower_Class towerClass;
        private float speedOfAttack;
        private float damage; //Value used for damage, slow down rate, money generated

        public Tower(Tower_Class towerClass, float speedOfAttack, float damage)
        {
            this.towerClass = towerClass;
            this.speedOfAttack = speedOfAttack;
            this.damage = damage;
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
    }
}
