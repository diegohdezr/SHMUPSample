using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//for loading and reloading of scenes

public class Main : MonoBehaviour
{
    static public Main Singleton;

    [Header("Set in inspector")]
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;
    public float enemyDefaultPadding = 1.5f;

    private BoundsCheck bndCheck;

    private void Awake()
    {
        Singleton = this;
        //set bndCheck to reference the boundscheck component on this gameobject
        bndCheck = GetComponent<BoundsCheck>();
        //invoke spawnEnemy() once (in 2 seconds, based on default values)
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //-------------------------------------------------------------------custom methods
    public void SpawnEnemy()
    {
        //pick a random enemy prefab to instantiate
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject GO = Instantiate<GameObject>(prefabEnemies[ndx]);
        //position the enemy above the screen with a random x position
        float enemyPadding = enemyDefaultPadding;
        if (GO.GetComponent<BoundsCheck>() != null) 
        {
            enemyPadding = Mathf.Abs(GO.GetComponent<BoundsCheck>().radius);
        }

        //set the intial position for the spawned enemy
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth + enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        GO.transform.position = pos;
        //invoke SpawnEnemy again
        Invoke("SpawnEnemy",1f/enemySpawnPerSecond);
    }
}
