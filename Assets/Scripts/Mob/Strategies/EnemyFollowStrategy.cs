using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Mob.Strategies
{
    public class EnemyFollowStrategy : IStrategy
    {
        Enemy enemy;
        GameObject target;
        Rigidbody2D rb2d;


        public EnemyFollowStrategy(Enemy enemy, GameObject target)
        {
            this.enemy = enemy;
            this.target = target;
            rb2d = enemy.GetComponent<Rigidbody2D>();
        }

        public void Run()
        {            
            var moveToDir = (target.transform.position - enemy.transform.position).normalized;

            rb2d.MovePosition(new Vector2(enemy.transform.position.x + Time.deltaTime * enemy.GetSpeed() * moveToDir.x,
                                          enemy.transform.position.y + Time.deltaTime * enemy.GetSpeed() * moveToDir.y));

            var angle = 0.0f;
            var didAngleChange = false;

            if (moveToDir.sqrMagnitude > 0)
            {
                angle = Mathf.Atan2(moveToDir.y, moveToDir.x) * Mathf.Rad2Deg;
                didAngleChange = true;
            }

            if (didAngleChange)
            {
                var q = Quaternion.AngleAxis(angle - 90, Vector3.forward);

                enemy.transform.rotation = Quaternion.RotateTowards(enemy.transform.rotation, q, enemy.GetRotateSpeed() * Time.deltaTime);
            }
        }
    }
}
