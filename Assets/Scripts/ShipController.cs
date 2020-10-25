using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    float rotationSpeed = 100.0f;
    public float thrustForce = 1f;
 
    public AudioClip crash;
    public AudioClip shoot;
 
    public GameObject bullet;
    public GameObject gun; //it sets the initial position for bullets
 
    private GameController gameController;
    private Animator animator;
 
    void Start(){
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent <GameController>();
        animator = GetComponent<Animator>();
    }
 
    void Update () {
 
        // Rotate the ship if necessary
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
 
        // Thrust the ship if necessary
        if(Input.GetAxis("Vertical") > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * thrustForce);
            animator.SetTrigger("Thrusting");
        }   
        //animator.ResetTrigger("Thrusting");
 
        //Control to shoot
        if (Input.GetKeyDown("space"))
            ShootBullet ();
 
    }
 
    void OnTriggerEnter2D(Collider2D c){
 
        // Anything except a bullet is an asteroid
        if (c.gameObject.tag != "Bullet") {
 
            AudioSource.PlayClipAtPoint(crash, Camera.main.transform.position);
 
            // Move the ship to the centre of the screen
            transform.position = new Vector3 (0, 0, 0); 
 
            // Remove all velocity from the ship
            GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
 
            gameController.DecrementLives();
        }
    }
 
    void ShootBullet()
    {
        // Spawns the bullet
        //GameObject newBullet = Instantiate(bullet, new Vector3(transform.position.x,transform.position.y + 0.43f, 0), transform.rotation, transform);
        Instantiate(bullet, gun.transform.position, transform.rotation, transform);
        //Debug.Log(GetComponent<Renderer>().bounds.extents.y);
 
        // Play the shooting sound
        AudioSource.PlayClipAtPoint(shoot, Camera.main.transform.position);
    }
}
