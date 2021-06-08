using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("set in inspector")]
    public float rotationsPersecond = 0.1f;

    [Header("Set Dynamically")]
    public int levelShown = 0;

    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //read the current shield level from the hero singleton
        int currentLevel = Mathf.FloorToInt(Hero.HeroSingleton.shieldLevel);
        //this is different from level shown!
        if (levelShown != currentLevel)
        {
            levelShown = currentLevel;
            //adjust the texture offset to show different shield level
            mat.mainTextureOffset = new Vector2(0.2f * levelShown, 0);
        }
        //rotate the shield a bit everyframe in a time-based way
        float rZ = -(rotationsPersecond * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }
}
