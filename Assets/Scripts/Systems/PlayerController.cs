using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int startHealth = 20;
    public CameraMotion cameraMotion;
    public Transform target;
    public Canvas dieCanvas;

    int health = 0;
    bool stopped = false;
    private void Start()
    {
        health = startHealth;
    }
    public void StopMoving()
    {
        stopped = true;
    }
    public void Damage()
    {
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z), transform.forward, out hit, 3, 1);
        if(hit.collider != null)
        if(hit.collider.transform.tag == "Enemy")
        {
            hit.collider.transform.GetComponent<Mutant_AI>().TakeDamage();
        }
    }
    public bool TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
            return false;
        } else
        {
            this.GetComponent<Animator>().SetTrigger("TakeDamage");
            return true;
        }
    }
    public void Die()
    {
        dieCanvas.gameObject.SetActive(true);
        StopMoving();
        this.GetComponent<Animator>().SetTrigger("Die");
        cameraMotion.enabled = false;
        this.enabled = false;
        
    }
    public void UnblockMoving()
    {
        stopped = false;
    }
    public int GetCurrentHP()
    {
        return health;
    }
    void Update()
    {
        if (!stopped) //Проверка на анимации
        {
            if (Input.GetButtonDown("Fire1"))
            {
                this.GetComponent<Animator>().SetTrigger("Attack");
            }

            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            if (x != 0)
            {
                //Ну тут надо бы сделать стрейф, но делать я его не буду, т.к. нет анимаций)
            }
            this.GetComponent<Animator>().SetBool("Run", y > 0);
            this.GetComponent<Animator>().SetBool("Back", y < 0);
            if (y > 0)
            {
                this.GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * 0.1f);
                tick();
            }
            if (y < 0)
            {
                this.GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * -0.1f);
                tick();
            }
        }

    }
    void tick()
    {
        Vector3 direction = target.transform.position - this.transform.position;
        direction.y = 0;

        this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 1f);
        this.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X")* cameraMotion.mouseXSpeedMod, 0));

        cameraMotion.gameObject.GetComponent<CameraMotion>().ResetCamera();

    }
}
