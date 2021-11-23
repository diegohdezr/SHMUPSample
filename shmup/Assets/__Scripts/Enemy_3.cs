using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3 : Enemy
{

    [Header("set in inspector")]
    public float lifeTime=5;

    [Header("set dinamically")]
    public Vector3[] points;
    public float birthTime;
    // Start is called before the first frame update
    void Start()
    {
        points = new Vector3[3];
        points[0]= pos;
        float xMin = -bndCheck.camWidth + bndCheck.radius;
        float xMax = bndCheck.camWidth - bndCheck.radius;

        Vector3 v;
        v = Vector3.zero;
        v.x = Random.Range(xMin, xMax);
        v.y = -bndCheck.camHeight * Random.Range(2.75f, 2);
        points[1] = v;

        //selecciona un a posicion random al final de lo que ve la camara
        v = Vector3.zero;
        v.y = pos.y;
        v.x = Random.Range(xMin, xMax);
        points[2] = v;

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
        //interpolate the bezier points
        Vector3 point01, point12;
        point01 = (1 - u) * points[0] + u * points[2];
        point12 = (1 - u) * points[1] + u * points[2];
        pos = (1 - u) * point01 + u * point12;
        
    }

}
