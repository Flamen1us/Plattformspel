using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public GameObject cannonBall; 
    public Transform firePoint;         
    public float shootInterval = 4f;
    Rigidbody2D rb;
    float speed = 300f;

    private void Start()
    {
        rb = cannonBall.GetComponent<Rigidbody2D>();
        StartCoroutine(ShootCannon());
        
    }

    private IEnumerator ShootCannon()
    {
        while (true)
        {
            GameObject ball =  Instantiate(cannonBall, firePoint.position, firePoint.rotation);
            ball.GetComponent<Rigidbody2D>().AddForce(transform.right*speed);
            yield return new WaitForSeconds(shootInterval);
        }
    }
}
