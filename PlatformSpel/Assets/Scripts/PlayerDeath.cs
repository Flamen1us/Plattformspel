using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class PlayerDeath : MonoBehaviour
{
    Collider2D body;
    Collider2D feets;
    bool isTakingDamage = false;
    //public ScoreSystem system;
    private void Start()
    {
        body = GetComponent<PolygonCollider2D>();
        feets = GetComponent<BoxCollider2D>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (feets.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            Destroy(other.gameObject);
            FindAnyObjectByType<GameSession>().UpdateScore(1);
        }
        else if (body.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            FindFirstObjectByType<GameSession>().ProcessPlayerDamage(1);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isTakingDamage && feets.IsTouchingLayers(LayerMask.GetMask("Spikes")) || body.IsTouchingLayers(LayerMask.GetMask("Spikes")))
        {
            StartCoroutine(ApplyDamageOverTime());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTakingDamage && feets.IsTouchingLayers(LayerMask.GetMask("Lava")) || body.IsTouchingLayers(LayerMask.GetMask("Lava")))
        {
            StartCoroutine(ApplyDamageOverTime());
        }
    }
    private IEnumerator ApplyDamageOverTime()
    {
        isTakingDamage = true;
        if (feets.IsTouchingLayers(LayerMask.GetMask("Lava")) || body.IsTouchingLayers(LayerMask.GetMask("Lava")))
        {
            while (feets.IsTouchingLayers(LayerMask.GetMask("Lava")) || body.IsTouchingLayers(LayerMask.GetMask("Lava")))
            {
                FindFirstObjectByType<GameSession>().ProcessPlayerDamage(4);
                yield return new WaitForSeconds(4f); // Wait 2 seconds before applying damage again
            }
        }
        else if (feets.IsTouchingLayers(LayerMask.GetMask("Spikes")) || body.IsTouchingLayers(LayerMask.GetMask("Spikes")))
        {
            while (feets.IsTouchingLayers(LayerMask.GetMask("Spikes")) || body.IsTouchingLayers(LayerMask.GetMask("Spikes")))
            {
                FindFirstObjectByType<GameSession>().ProcessPlayerDamage(2);
                yield return new WaitForSeconds(2f); // Wait 2 seconds before applying damage again
            }
        }
        isTakingDamage = false;
    }
}
