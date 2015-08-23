using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Mob.Strategies
{
    public class EnemyAttackPeopleStrategy : IStrategy
    {
        private const float NEXT_NODE_THRESHOLD = 4.0f;
        Enemy enemy;

        Rigidbody2D rb2d;

        private Func<List<NPC>> getListOfNPC;
        private NPC target;

        public EnemyAttackPeopleStrategy(Enemy enemy, Func<List<NPC>> getListOfNPC)
        {
            this.enemy = enemy;
            this.getListOfNPC = getListOfNPC;
            rb2d = enemy.GetComponent<Rigidbody2D>();
            target = null;
        }

        private float GetDistance(NPC target)
        {
            return (target.transform.position - enemy.transform.position).sqrMagnitude;
        }

        public void Run()
        {
            if(target == null)
            {
                var npcs = getListOfNPC();
                 
                if(npcs.Count > 0)
                {
                    foreach (var npc in npcs)
                    {
                        if (target == null ||
                            GetDistance(npc) < GetDistance(target))
                        {
                            target = npc;
                        }
                    }
                }
            }

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
