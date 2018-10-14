using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

    public float movementSpeed = 5.0f;
    public float rotateSpeed = 5.0f;
    public Joystick joystick;
    public GameObject simpleExplosionPrefab;

    private Rigidbody2D rb;
    private float rotateAmount;
    private float rotate2;
    Vector2 move;
    Vector3 joystickMoveVector;
    float angle;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    void Update()
    {
        if (GameManager.Instance.IsInputEnabled())
        {
            joystickMoveVector = (Vector3.right * joystick.Horizontal + Vector3.up * joystick.Vertical);
            angle = Mathf.Atan2(-joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;

            //rotateAmount = -Input.GetAxis("Horizontal");
            rotate2 = -Input.GetAxisRaw("Horizontal");
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movementSpeed * transform.up * 120f * Time.fixedDeltaTime;
        rb.rotation += rotate2 * (rotateSpeed * 130f) * Time.fixedDeltaTime;
        if (joystickMoveVector != Vector3.zero)
        {
            float rot = Mathf.LerpAngle(rb.rotation, angle, Time.fixedDeltaTime * (rotateSpeed * 1.5f));
            rb.rotation = rot;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Rocket"))
        {
            // Player destroy effect
            // Game over
            Instantiate(simpleExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameManager.Instance.GameOver();
        }
    }
}
