using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image chargeImage = null;
    [SerializeField] private GameObject emptyImage = null;
    [SerializeField] private float timeOffset = 2.0f;
    [SerializeField] private float timeMod = 4.0f;
    [SerializeField] private TextMeshProUGUI distanceText = null;
    [SerializeField] private GameObject startPanel = null;
    [SerializeField] private GameObject restartPanel = null;


    private float chargeValue = 1f;
    public bool usingCharge;
    public static UIController Instance;


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
        if (!GameController.GamePaused)
        {
            GameController.Distance += Time.deltaTime * timeMod;
            distanceText.text = String.Format("{0:0m}", GameController.Distance);

            if (usingCharge)
            {
                chargeValue = Mathf.Clamp01(chargeValue - (timeOffset * Time.deltaTime));
                chargeImage.fillAmount = chargeValue;

            }
            else
            {
                chargeValue = Mathf.Clamp01(chargeValue + (timeOffset * Time.deltaTime));
                chargeImage.fillAmount = chargeValue;
            }

            if (chargeValue <= 0)
            {

                playerMovement.Instance.emptyCharge = true;
                emptyImage.SetActive(true);
            }
            else
            {
                playerMovement.Instance.emptyCharge = false;
                emptyImage.SetActive(false);
            }
        }
    }
    public void StartGame()
    {
        startPanel.SetActive(false);
        GameController.GamePaused = false;
    }

    public void RestartGame()
    {
        restartPanel.SetActive(false);
        GameController.GamePaused = false;
    }

    public void endGame()
    {
        restartPanel.SetActive(true);
        GameController.GamePaused = true;
        GameController.Distance = 0;
        GameController.EnemyCount = 0;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) 
        {
            enemy.GetComponent<healthComponent>().resetHealth();
            enemy.SetActive(false);

        }
    }


}
