using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
public class Rocket : MonoBehaviour {

    public Transform target;
    public float movementSpeed = 5.0f;
    public float rotateSpeed = 5.0f;
    public int pointsWhenDestroyed;
    public GameObject simpleExplosionPrefab;

    private Rigidbody2D rb;
    private float rotateAmount;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        rotateAmount = 0f;
        if (GameManager.Instance.IsPlayerAlive())
        {
            Vector2 direction = (Vector2)target.position - rb.position;
            direction.Normalize();
            rotateAmount = -Vector3.Cross(direction, transform.up).z;
        }
        else
        {
            Destroy(gameObject, 20);
        }
    }

    private void FixedUpdate()
    {
        rb.rotation += rotateAmount * (rotateSpeed * 78f) * Time.fixedDeltaTime;
        rb.velocity = movementSpeed * transform.up * 144f * Time.fixedDeltaTime;
        //120 : 1.2 = x : 1
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var explosion = Instantiate(simpleExplosionPrefab, transform.position, Quaternion.identity);

        if (collision.tag.Equals("Rocket"))
        {
            if (GameManager.Instance.IsPlayerAlive()) { 
                var controller = explosion.GetComponent<ExplosionController>();
                controller.ShowPoints(pointsWhenDestroyed);
                GameManager.Instance.AddPoints(pointsWhenDestroyed);
            }
        }
        Destroy(gameObject);
    }
}
