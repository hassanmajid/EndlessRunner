using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthComponent : MonoBehaviour
{
    public float maxhealth;
    public float currhealth;
    // Start is called before the first frame update
    void Start()
    {
        currhealth = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currhealth<=0)
        {
            if (gameObject.tag=="Enemy")
            {
                GameController.EnemyCount--;
                currhealth = maxhealth;
                gameObject.SetActive(false);


            }
            
        }
    }

    public void updateHealth(float imt)
    {
        currhealth += imt;
    }

    public void resetHealth()
    {
        currhealth = maxhealth;
    }
}
