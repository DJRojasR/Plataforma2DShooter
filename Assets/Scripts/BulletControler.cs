using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{
    // Componente Rigidbody2D para manejar la f�sica del personaje
    private Rigidbody2D Rigidbody2D;
   
    public float speed;

    private Vector2 Direction;

    public AudioClip Sound;

    void Start()
    {
        // Obtiene el componente Rigidbody2D del objeto al que est� adjunto el script
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
    }


    private void FixedUpdate()
    {
        // Aplica velocidad en el eje X para mover al personaje, manteniendo la velocidad en Y
        
        Rigidbody2D.velocity = Direction * speed;
    }

   public void setDireccion(Vector2 direction) { 
       Direction = direction;
   }

    public void DestroyBullet() { 
        Destroy(gameObject);
    }

    //Se recomienda para cuando detente el objecto a colisionar no lo empuje
    public void OnTriggerEnter2D(Collider2D collision) {
        JohnMovement john = collision.GetComponent<JohnMovement>();
        Grunt_Scripts grunt = collision.GetComponent<Grunt_Scripts>();
        if (john != null)
        {
            john.Hit();
        }
        if (grunt != null)
        {
            grunt.Hit();
        }
        DestroyBullet();
    }
    /* Utilizamos triger para que la bala no este empuzando al enemigo
    private void OnCollisionEnter2D(Collision2D collision) {
        JohnMovement john = collision.collider.GetComponent<JohnMovement>();
        Grunt_Scripts grunt = collision.collider.GetComponent<Grunt_Scripts>();
        if (john != null)
        {
            john.Hit();
        }
        if (grunt != null) { 
            grunt.Hit();
        }
        DestroyBullet();
    }
    */
}
