using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt_Scripts : MonoBehaviour
{
    public GameObject John;
    private float LastShoot;
    public GameObject BulletPrefabs;

    private int health = 3;

    private void Update() {
        if (John == null) {
            //Si John desaparecen saldra de este fragmento
            return;
        }
        //Hubicamos la direcion de John con el del enemigo
        Vector3 direction = John.transform.position - transform.position;
        // si es positivo el enemig mira al lado derecho siempre mirando a john
        if (direction.x >= 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        //Al lado derecho en caso contrario siempre mirando a john
        else
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }

        float distance =Mathf.Abs(John.transform.position.x - transform.position.x) ;

        if (distance < 1.0f && Time.time > LastShoot + 2.0f) {
            Shoot();
            LastShoot = Time.time;
        }
    }

    public void Shoot()
    {
        Vector3 direction;

        if (transform.localScale.x == 1.0f)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }

        // Instanciamos la bala
        GameObject bullet = Instantiate(BulletPrefabs, transform.position + direction * 0.1f, Quaternion.identity);

        // Corregimos el error en GetComponent
        bullet.GetComponent<BulletControler>().setDireccion(direction);
    }

    public void Hit()
    {
        health = health - 1;
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }


}
