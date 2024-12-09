using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public bool isHostile;
    public Transform currWaypoint;
    public float speed;

    [Space(12)]
    public NavMeshAgent AIagent;
    GameObject Player;
    public Animator Animator;

    public float distToPlayer;
    public float distToPlayerFollow;
    public float distToPlayerAttack;

    public int currentHealth;
    public int maxHealth = 100;

    public float attackCD = 2f;
    public float cooldownTimer;
    public int damage = 10;
    bool canAttack;
    private void Start()
    {
        Animator = GetComponent<Animator>();
        Player = FindAnyObjectByType<PlayerController>().gameObject;
        currentHealth = maxHealth;
    }
    private void Update()
    {
        Die();
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer > attackCD)
        {
            canAttack = true;
        }

        distToPlayer = Vector3.Distance(transform.position, Player.transform.position);

        if (distToPlayer < distToPlayerFollow)
        {
            isHostile = true;
        }
        else
        {
            isHostile = false;
        }
        if (!isHostile)
        {
            AIagent.SetDestination(currWaypoint.position);
            Animator.SetBool("Walking", true);
            return;
        }
        else
        {
            if (distToPlayer < distToPlayerAttack)
            {
                AIagent.speed = 0;
                if (canAttack)
                {
                    canAttack = false;
                    AttackPlayer();
                }
            }
            else
            {
                AIagent.speed = speed;
                AIagent.SetDestination(Player.transform.position);
            }
            Animator.SetBool("Walking", true);
        }
    }

    void AttackPlayer()
    {
        cooldownTimer = 0;
        Animator.Play("Attack");
    }
    private void DamagePlayer()
    {
        if (distToPlayer < distToPlayerAttack)
        {
            Player.GetComponent<PlayerStats>().TakePhysicDamage(damage);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Animator.Play("Idle");
        AudioManager.instance.enemy = GetComponent<AudioSource>();
        AudioManager.instance.enemy.clip = AudioManager.instance.damageSFX;
        AudioManager.instance.enemy.Play();
            
    }
    void Die()
    {
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            currentHealth = maxHealth;
            Chest.enemiesSlain++;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, distToPlayerFollow);
    }
}
