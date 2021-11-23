using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    [Header("setup in inspector : Enemy_2")]
    //evaluamos como la onda senosoidal afecta el movimiento del enemigo
    public float sinEccentricity = 0.6f;
    public float lifeTime = 10;

    [Header("set dinamically")]
    public Vector3 point0;
    public Vector3 point1;
    public float birthTime;
    // Start is called before the first frame update
    void Start()
    {
        point0 = Vector3.zero;
        point0.x = -bndCheck.camWidth - bndCheck.radius;
        point0.y = Random.Range(-bndCheck.camHeight, bndCheck.camHeight);

        point1 = Vector3.zero;
        point1.x = -bndCheck.camWidth + bndCheck.radius;
        point1.y = Random.Range(-bndCheck.camHeight, bndCheck.camHeight);

        //set probabilioty to change sides
        if (Random.value > 0.5f) 
        {
            point0.x *= -1;
            point1.x *= -1;
        }
        birthTime = Time.time;
    }

    public override void Move()
    {
        float u = (Time.time - birthTime) / lifeTime;
        if (u > 1) 
        {
            Destroy(this.gameObject);
            return;
        }
        u = u + sinEccentricity * (Mathf.Sin(u * Mathf.PI * 2));
        pos = (1 - u) * point0 + u * point1;
        
    }
}
