using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class upgradeSystem : MonoBehaviour
{
    private bool upgradeCannon = false;
    private bool upgradeHealth = false;

    public int barrels = 0;
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
        //set barrels
        barrelNum.text = barrels.ToString();
        checkAfford();

        if (Input.GetKeyDown(KeyCode.G) && (upgradeCannon == false) && (barrels>=1))
        {
            upgradeCannon = true;
            cannonUpgradeHolder.sprite = Resources.Load<Sprite>("Images/HolderGold");
            cannonKeyBind.SetActive(false);
            cannonCostText.SetActive(false);
            cannonBarrelImage.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.H) && (upgradeHealth == false) && (barrels>=1))
        {
            upgradeHealth = true;
            healthUpgradeHolder.sprite = Resources.Load<Sprite>("Images/HolderGold");
            healthKeyBind.SetActive(false);
            healthCostText.SetActive(false);
            healthBarrelImage.SetActive(false);
        }

    }

    public void checkAfford()
    {
        //cannon Upgrade
        if(barrels >= 1 && (upgradeCannon == false))
        {
            cannonUpgradeHolder.sprite = Resources.Load<Sprite>("Images/HolderGrayAbleToBuy");
            cannonKeyBind.SetActive(true);
        }
        else if(upgradeCannon == false)
        {
            cannonUpgradeHolder.sprite = Resources.Load<Sprite>("Images/HolderGray");
            cannonKeyBind.SetActive(false);
        }
        
        //health Upgrade
        if(barrels >= 1 && (upgradeHealth == false))
        {
            healthUpgradeHolder.sprite = Resources.Load<Sprite>("Images/HolderGrayAbleToBuy");
            healthKeyBind.SetActive(true);
        }
        else if(upgradeHealth == false)
        {
            healthUpgradeHolder.sprite = Resources.Load<Sprite>("Images/HolderGray");
            healthKeyBind.SetActive(false);
        }

    }
}
