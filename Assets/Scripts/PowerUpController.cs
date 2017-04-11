using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour {

    public int speed = 10;

    Rigidbody2D _body;
    public Rigidbody2D body
    {
        get
        {
            if (_body == null)
            {
                _body = GetComponent<Rigidbody2D>();
            }
            return _body;
        }
    }

    IEnumerator FloatLeft()
    {
        body.velocity = Vector2.left * speed;
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }

	private void OnEnable()
    {
        StartCoroutine("FLoatLeft");
    }

}
