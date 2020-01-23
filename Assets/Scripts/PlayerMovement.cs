using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float baseSpeed = 1f;
    public float speedModifier = 1f;

    private float inputHorizontal;
    private float inputVertical;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");

        Vector3 force = new Vector3(inputHorizontal, -inputVertical, 0f).normalized;

        rb.velocity = (force * (baseSpeed * speedModifier));
    }
}
