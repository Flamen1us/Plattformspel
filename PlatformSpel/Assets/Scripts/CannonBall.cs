using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float lifeTime = 5f;
    Collider2D col;

    private void Start()
    {
        col = GetComponent<CircleCollider2D>();
        Destroy(gameObject, lifeTime);
    }
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (col.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            FindFirstObjectByType<GameSession>().ProcessPlayerDamage(3);
        }
        Destroy(gameObject);
    }
}
