using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    [SerializeField] private GameObject lightObj = null;
    public bool emptyCharge;
    public static playerMovement Instance;
    bool keyPressed;
    private float horizontalInput;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
        horizontalInput = Input.GetAxis("Horizontal");
    }


    private void FixedUpdate()
    {
        if (!GameController.GamePaused)
        {

            GetComponent<Rigidbody>().velocity = new Vector3(horizontalInput * 4, GetComponent<Rigidbody>().velocity.y, 0);

            if (Input.GetKey(KeyCode.Space))
            {

                UIController.Instance.usingCharge = true;
                if (!emptyCharge)
                    lightObj.SetActive(true);
                else
                    lightObj.SetActive(false);
            }
            else
            {

                UIController.Instance.usingCharge = false;
                lightObj.SetActive(false);
            }
        }

       



    }
}
