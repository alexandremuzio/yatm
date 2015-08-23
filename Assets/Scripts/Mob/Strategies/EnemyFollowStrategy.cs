using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Mob.Strategies
{
    public class EnemyFollowStrategy : IStrategy
    {
        [SerializeField]
        private float sightCooldown = 6;
        private float lastSightTimer;

        private const float MAX_WAIT_TIME = 1f;
        private float waitDelay = 0f;
        private float lastWaitTimer = 0f;

        Enemy enemy;
        Transform target;
        private Func<List<NPC>> getListOfNPC;
        private Func<List<Player>> getListOfPlayers;
        Rigidbody2D rb2d;


        public EnemyFollowStrategy(Enemy enemy, Func<List<NPC>> getListOfNPC, Func<List<Player>> getListOfPlayers)
        {
            this.enemy = enemy;
            this.getListOfNPC = getListOfNPC;
            this.getListOfPlayers = getListOfPlayers;
            rb2d = enemy.GetComponent<Rigidbody2D>();
            lastSightTimer = UnityEngine.Random.Range(0, sightCooldown);
            lastWaitTimer = MAX_WAIT_TIME;
        }

        private float GetDistance(Transform target)
        {
            return (target.position - enemy.transform.position).sqrMagnitude;
        }

        public void Run()
        {
            if(Time.time - lastWaitTimer < waitDelay)
            {
                return;
            }

            if (target == null || Time.time - lastSightTimer > sightCooldown)
            {
                var oldTarget = target;

                var npcs = getListOfNPC();
                var players = getListOfPlayers();
                foreach (var npc in npcs)
                {
                    if (target == null ||
                        GetDistance(npc.transform) < GetDistance(target))
                    {
                        target = npc.transform;
                    }
                }

                foreach (var player in players)
                {
                    if (target == null ||
                        GetDistance(player.transform) < GetDistance(target))
                    {
                        target = player.transform;
                    }
                }

                if (target == null)
                {
                    throw new Exception("Target not found");
                }
                if(target != oldTarget)
                {
                    waitDelay = UnityEngine.Random.Range(0.5f, MAX_WAIT_TIME);
                    lastWaitTimer = Time.time;
                    return;
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
