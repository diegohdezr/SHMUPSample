using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f;//speed in m/s
    public float fireRate = 0.3f; //seconds/shot(unused)
    public float health = 10;
    public int score = 100;//points earned for destroying this

    private BoundsCheck bndCheck;

    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (bndCheck != null && bndCheck.offDown) 
        {
            //we are off the bottom, so we destroy this GO
            Destroy(gameObject);
        }
    }

    public virtual void Move() 
    {

        Vector3 tempPos =  pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    ///////////////////////////////////////------------------------Properties
    public Vector3 pos 
    {
        get
        {
            return (this.transform.position);
        }
        set 
        {
            this.transform.position = value;
        }
    }
}
