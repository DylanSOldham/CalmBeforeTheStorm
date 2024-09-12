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

    public GameObject ship;

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

    public RectTransform cannonShotBlackBar;
    public RectTransform cannonShotOrangeBar;

    public RectTransform shipHPBlackBar;
    public RectTransform shipHPGreenBar;


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
            barrels -= 1;
            upgradeCannon = true;
            cannonUpgradeHolder.sprite = Resources.Load<Sprite>("Images/HolderGold");
            cannonKeyBind.SetActive(false);
            cannonCostText.SetActive(false);
            cannonBarrelImage.SetActive(false);
            ShipMovement script = ship.GetComponent<ShipMovement>();
            script.WaitBetweenShots = 1f;
            float newWidth = 100f;

            Vector2 size = cannonShotBlackBar.sizeDelta;
            size.x = newWidth;
            cannonShotBlackBar.sizeDelta = size;

            Vector2 size2 = cannonShotOrangeBar.sizeDelta;
            size2.x = newWidth;
            cannonShotOrangeBar.sizeDelta = size;
        }



        if (Input.GetKeyDown(KeyCode.H) && (upgradeHealth == false) && (barrels>=1))
        {
            barrels -= 1;
            upgradeHealth = true;
            healthUpgradeHolder.sprite = Resources.Load<Sprite>("Images/HolderGold");
            healthKeyBind.SetActive(false);
            healthCostText.SetActive(false);
            healthBarrelImage.SetActive(false);
            float newWidth = 500f;

            Vector2 size = shipHPBlackBar.sizeDelta;
            size.x = newWidth;
            shipHPBlackBar.sizeDelta = size;

            Vector2 size2 = shipHPGreenBar.sizeDelta;
            size2.x = newWidth;
            shipHPGreenBar.sizeDelta = size;
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
