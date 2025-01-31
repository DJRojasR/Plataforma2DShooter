using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    // Componente Rigidbody2D para manejar la física del personaje
    private Rigidbody2D Rigidbody2D;

    // Variable para almacenar la dirección del movimiento horizontal
    private float Horizontal;

    // Fuerza del salto
    public float JumpForce;

    // Velocidad de movimiento
    public float speed;

    // Variable para verificar si el personaje está en el suelo
    private bool Grounded;

    public GameObject BulletPrefabs;

    private float LastShoot;

    private Animator Animator;

    //Vidas del jugador
    private int health = 5;

    void Start()
    {
        // Obtiene el componente Rigidbody2D del objeto al que está adjunto el script
        Rigidbody2D = GetComponent<Rigidbody2D>();
        // Obtiene el componente Animator del objeto
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Actualiza el parámetro "running" en el Animator(si horizon es igual a 0 es false caso contrario es true
        Animator.SetBool("running", Horizontal != 0.0f);

        // Captura la entrada del usuario para el movimiento horizontal (-1 = izquierda(letra = "A"), 1 = derecha(letra = "D") )
        Horizontal = Input.GetAxisRaw("Horizontal");

        //Si presionamos A devuele un valor negativo y el player se movera y mirara hacia la izquierda 
        if (Horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        } //Si presionamos D devuele un valor positivo y el player se movera y mirara hacia la derecha
        else if (Horizontal > 0.0f) {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        /* Dibuja un rayo rojo apuntando hacia abajo partiendo desde el centro del player(Con esto se lograr saber cuando el personaje
        esta tocando el suelo). Ademas para que esto funcion se tiene que desabilitar la opcion "start in collider" que se
        ubica en project setting */
        Debug.DrawRay(transform.position, Vector3.down * 0.4f, Color.red);

        // Verifica si el personaje está en contacto con el suelo usando un Raycast
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.4f))
        {
            //Devuelve true si esta en suelo
            Grounded = true;
        }
        else
        {
            //Si el personaje esta en el aire devuelve false
            Grounded = false;
        }

        // Si se presiona la tecla espacio y el personaje está en el suelo, se ejecuta el salto
        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f) {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void FixedUpdate()
    {
        // Aplica velocidad en el eje X para mover al personaje, manteniendo la velocidad en Y
        Rigidbody2D.velocity = new Vector2(Horizontal * speed, Rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        // Aplica una fuerza hacia arriba para realizar el salto
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
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


    public void Hit() {
        health = health - 1;
        if (health == 0) {
            Destroy(gameObject);
        }
    }


}

