using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] bool isFlying = false;
    [SerializeField] float radius = 2f;

    private Rigidbody2D rb;
    float speed = 5f;

    private const float minSpeed = 1f;
    private const float maxSpeed = 5.5f;
    Transform player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();       
    }

    private void Update()
    {
        UpdateSpeedBasedOnDistance();
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, player.position) > radius)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, player.position, speed * Time.fixedDeltaTime));
        }
    }
    
    private void UpdateSpeedBasedOnDistance()
    {
        if (Vector2.Distance(transform.position, player.position) > radius)
        {
            speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, minSpeed, Time.deltaTime);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
