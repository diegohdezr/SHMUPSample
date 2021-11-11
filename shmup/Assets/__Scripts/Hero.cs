using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero HeroSingleton;

    [Header("Set in Inspector")]
    //these fields control the movement of the ship
    public float speed = 30;
    public float RollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;

    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLevel = 1;
    //This variable will hold a reference to the last triggering GO
    private GameObject lastTriggeredGO = null;

    public void Awake()
    {
        if (HeroSingleton == null)
        {
            HeroSingleton = this;
        }
        else 
        {
            Debug.LogError("Hero.Awake() -- Attempted to assign second HeroSingleton");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //pull information from the input class
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        //change transform.position based on the axes
        Vector3 position = transform.position;
        position.x += xAxis * speed * Time.deltaTime;
        position.y += yAxis * speed * Time.deltaTime;
        transform.position = position;

        //rotate the ship to make it feel more dynamic
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * RollMult, 0);
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            TempFire();
        }
    }

    void TempFire() 
    {
        GameObject projGo = Instantiate<GameObject>(projectilePrefab);
        projGo.transform.position = transform.position;
        Rigidbody rigidB = projGo.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.up * projectileSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        //Debug.Log("Triggered: " + go.name);

        //Make sure we are not triggering the same game object
        if (go == lastTriggeredGO) {
            return; //if we collide with the same GO we exit this func
        }
        lastTriggeredGO = go; //if we collide with a new GO we make it the last collided with

        if (go.tag == "Enemy")//if the GO is an Enemy 
        {
            shieldLevel--;//modify our shield level
            Destroy(go);//destroy yhe enemy
        }
        else
        {
            Debug.Log("what we hit was not an Enemy: "+ go.name);

        }

    }

    public float shieldLevel 
    {
        get 
        {
            return _shieldLevel;
        }
        set 
        {
            _shieldLevel = Mathf.Min(value, 4);
            //if the shield is going to be set to be less than zero
            if (value < 0) 
            {
                Destroy(this.gameObject);
                Main.Singleton.DelayedRestart(gameRestartDelay);
            }

            
        }
    }
}
