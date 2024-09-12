using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class upgradeSystem : MonoBehaviour
{
    private bool upgradeCannon = false;
    private bool upgradeHealth = false;

    private int barrels = 0;
    public TextMeshProUGUI barrelNum;

    public Image cannonUpgradeHolder;
    public Image healthUpgradeHolder;
    public Image repairHolder;

    public GameObject cannonKeyBind;
    public GameObject healthKeyBind;
    public GameObject repairKeyBind;

    public GameObject cannonCostText;
    public GameObject healthCostText;
    public GameObject repairCostText;

    public GameObject cannonBarrelImage;
    public GameObject healthBarrelImage;
    public GameObject repairBarrelImage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
