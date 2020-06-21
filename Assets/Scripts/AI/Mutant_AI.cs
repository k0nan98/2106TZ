using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Mutant_AI : MonoBehaviour
{
    public int startHealth = 1;
    public GameObject Parent;
    public TextMeshPro hpBar;
    int health = 0;
    bool blocked = false;
    Transform target = null;
    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            Die();
        } else
        {
            this.GetComponent<Animator>().SetTrigger("TakeDamage");
        }
    }
    public void Die()
    {
        Destroy(hpBar.gameObject);
        PlayerGUI.AddKill();
        blocked = true;
        this.GetComponent<NavMeshAgent>().Stop();
        this.GetComponent<CapsuleCollider>().enabled = false;
        this.GetComponent<Animator>().SetTrigger("Die");
        Spawner.reSpawnMutant(this.gameObject, Parent);
    }
    public void Attack()
    {
        if (!target.GetComponent<PlayerController>().TakeDamage(1))
        {
            blocked = true;
            this.GetComponent<NavMeshAgent>().Stop();
            this.GetComponent<CapsuleCollider>().enabled = false;
            this.GetComponent<Animator>().SetTrigger("Idle");
        }
    }
    public void BlockInput()
    {
        blocked = true;
    }
    public void UnblockInput()
    {
        blocked = false;
    }

    void Start()
    {
        target = GameObject.Find("Player").transform;
        health = startHealth;

    }
    bool ready = false;
    private void FixedUpdate()
    {
        if(hpBar != null)
        {
            if(Vector3.Distance(this.transform.position, target.position) < 5)
            {
                hpBar.text = "Mutant HP: " + health;
                hpBar.transform.LookAt(Camera.main.transform.position);
            } else hpBar.text = string.Empty;

        }
        if (!blocked) //Проверка на анимации
        {
            this.GetComponent<NavMeshAgent>().SetDestination(target.position);
            if (this.GetComponent<NavMeshAgent>().destination != null && ready)
                if (this.GetComponent<NavMeshAgent>().stoppingDistance >= this.GetComponent<NavMeshAgent>().remainingDistance)
                {
                    GetComponent<Animator>().SetTrigger("Attack");
                }
                else GetComponent<Animator>().SetTrigger("Move");
        }
        ready = true; //Костыль!)
    }
}
