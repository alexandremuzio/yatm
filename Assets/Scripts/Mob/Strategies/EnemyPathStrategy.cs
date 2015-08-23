using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Mob.Strategies
{
    public class EnemyPathStrategy : IStrategy
    {
        private const float NEXT_NODE_THRESHOLD = 4.0f;
        Enemy enemy;
        List<Vector2> path;
        int node;

        Rigidbody2D rb2d;

        public event EventHandler OnPathFinished;
         
        public EnemyPathStrategy(Enemy enemy, List<Vector2> path)
        {
            this.enemy = enemy;
            this.path = path;
            node = 0;
            rb2d = enemy.GetComponent<Rigidbody2D>();
        }

        public void Run()
        {
            var moveToDir = ((Vector3)path[node] - enemy.transform.position);
            if(moveToDir.sqrMagnitude < NEXT_NODE_THRESHOLD)
            {
                node++;
                if(node >= path.Count)
                {
                    if(OnPathFinished != null)
                    {
                        OnPathFinished(this, null);
                        return;
                    }
                    else
                    {
                        node = 0;
                    }
                }
                moveToDir = ((Vector3)path[node] - enemy.transform.position);
            }

            moveToDir = moveToDir.normalized;

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
