using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] bool isFlying = false;
    [SerializeField] float shortRadius = 2f;
    [SerializeField] float farRadius = 20f;

    [Header("Vol - Trajectoire en signe infini")]
    [SerializeField] private float infinityWidth = 3f;
    [SerializeField] private float infinityHeight = 1.5f;
    [Tooltip("Vitesse à laquelle la courbe est parcourue (plus haut = boucle plus rapide).")]
    [SerializeField] private float infinityFrequency = 1f;

    private Rigidbody2D rb;
    float speed = 5f;
    private float infinityTime = 0f;

    private const float maxSpeed = 5.5f;
    private const float fullRunSpeed = 10f;
    private const float flyingSpeed = 12f;
    Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        UpdateSpeedBasedOnDistance();

        if (isFlying)
        {
            FollowInInfinityPattern();
        }
        else if (Vector2.Distance(transform.position, player.position) > shortRadius)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, player.position, speed * Time.fixedDeltaTime));
        }
    }
    
    private void FollowInInfinityPattern()
    {
        infinityTime += Time.fixedDeltaTime * infinityFrequency;

        float x = infinityWidth * Mathf.Sin(infinityTime);
        float y = infinityHeight * Mathf.Sin(infinityTime) * Mathf.Cos(infinityTime);

        Vector2 offset = new Vector2(x, y);
        Vector2 targetPos = (Vector2)player.position + offset;

        rb.MovePosition(Vector2.MoveTowards(rb.position, targetPos, flyingSpeed * Time.fixedDeltaTime));
    }

    private void UpdateSpeedBasedOnDistance()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > farRadius)
        {
            speed = Mathf.Lerp(speed, fullRunSpeed, Time.deltaTime);
        }
        else if (distance > shortRadius && distance < farRadius)
        {
            speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, 0, Time.deltaTime);
        }
    }

    private void GoToHouse()
    {
        //get distance from transform to house
        //if distance is less than 10, go to house
        
        //if (Vector2.Distance(transform.position, player.position) > 10f)
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shortRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, farRadius);

        if (isFlying && player != null)
        {
            // Aperçu de la trajectoire en huit dans l'éditeur
            Gizmos.color = Color.yellow;
            Vector3 prevPoint = player.position;
            int steps = 50;
            for (int i = 1; i <= steps; i++)
            {
                float t = (i / (float)steps) * Mathf.PI * 2f;
                float x = infinityWidth * Mathf.Sin(t);
                float y = infinityHeight * Mathf.Sin(t) * Mathf.Cos(t);
                Vector3 point = player.position + new Vector3(x, y, 0);
                Gizmos.DrawLine(prevPoint, point);
                prevPoint = point;
            }
        }
    }
}