using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 1;

    public float shootSpeed = 10;

    private Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float y = Input.GetAxis("Vertical");
        body.velocity = Vector2.up * y * speed;

        if (Input.GetButtonDown("Jump"))
        {
            GameObject bullet = Spawner.Spawn("Bullet");
            bullet.transform.position = transform.position;
            bullet.GetComponent<ProjectileController>().Fire(Vector2.right);
        }
    }
}
