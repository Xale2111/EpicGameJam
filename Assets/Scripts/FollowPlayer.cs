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
    [Tooltip("Vitesse réelle (unités/seconde) à laquelle l'animal parcourt la courbe, indépendante de sa taille.")]
    [SerializeField] private float curveSpeed = 5f;
    [Tooltip("Distance en dessous de laquelle l'animal est considéré 'sur le rail' du huit et le suit exactement.")]
    [SerializeField] private float snapToPathDistance = 1f;

    [Header("Boing Animation")]
    [SerializeField] private AnimationCurve boingCurve;
    [SerializeField] private Transform spriteTransform;
    
    [SerializeField] private float spriteForwardOffsetDegrees = 0f;
    [SerializeField] private float rotationSpeed = 180;

    
    private Rigidbody2D rb;
    float speed = 5f;
    private float infinityTime = 0f;

    private const float maxSpeed = 5.5f;
    private const float fullRunSpeed = 10f;
    private const float flyingSpeed = 25f;
    Transform player;
    
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteTransform = GetComponentInChildren<SpriteRenderer>().transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
            
            spriteTransform.localPosition = new Vector3(0,1,0) * boingCurve.Evaluate(Time.time * speed)/10;
            
            if (transform.position.x > player.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }


        // Debug.Log(rb.linearVelocity)
    }
    
    private void FollowInInfinityPattern()
    {
        float maxAmplitude = Mathf.Max(infinityWidth, infinityHeight, 0.01f);
        float angularSpeed = curveSpeed / maxAmplitude;
 
        infinityTime += Time.fixedDeltaTime * angularSpeed;
 
        float x = infinityWidth * Mathf.Sin(infinityTime);
        float y = infinityHeight * Mathf.Sin(infinityTime) * Mathf.Cos(infinityTime);
 
        Vector2 offset = new Vector2(x, y);
        Vector2 targetPos = (Vector2)player.position + offset;
 
        float distanceToPath = Vector2.Distance(rb.position, targetPos);
        Vector2 previousPos = rb.position;
        Vector2 newPos;
 
        if (distanceToPath > snapToPathDistance)
        {
            newPos = Vector2.MoveTowards(rb.position, targetPos, flyingSpeed * Time.fixedDeltaTime);
        }
        else
        {
            newPos = targetPos;
        }
 
        FaceMovementDirection(newPos - previousPos);
        rb.MovePosition(newPos);
    }
    
    private void FaceMovementDirection(Vector2 moveDelta)
    {
        if (spriteTransform == null || moveDelta.sqrMagnitude < 0.0001f) return;
 
        float targetAngle = Mathf.Atan2(moveDelta.y, moveDelta.x) * Mathf.Rad2Deg + spriteForwardOffsetDegrees;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
 
        spriteTransform.rotation = Quaternion.RotateTowards(
            spriteTransform.rotation,
            targetRotation,
            rotationSpeed * Time.fixedDeltaTime
        );
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shortRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, farRadius);

        if (isFlying && player != null)
        {
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