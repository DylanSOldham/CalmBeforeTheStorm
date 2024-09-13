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

    public int repairCost = 1;
    public int cannonUpgradeCost = 5;
    public int healthUpgradeCost = 5;

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

    public TextMeshProUGUI cannonCostTextText;
    public TextMeshProUGUI healthCostTextText;
    public TextMeshProUGUI repairCostTextText;

    public GameObject cannonBarrelImage;
    public GameObject healthBarrelImage;
    public GameObject repairBarrelImage;

    public RectTransform cannonShotBlackBar;
    public RectTransform cannonShotOrangeBar;

    public RectTransform shipHPBlackBar;
    public RectTransform shipHPGreenBar;

    ShipMovement script;

    // Start is called before the first frame update
    void Start()
    {
        cannonCostTextText.text = cannonUpgradeCost.ToString();
        healthCostTextText.text = healthUpgradeCost.ToString();
        repairCostTextText.text = repairCostText.ToString();
        script = ship.GetComponent<ShipMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
        barrelNum.text = barrels.ToString();
        checkAfford();

        if (Input.GetKeyDown(KeyCode.G) && (upgradeCannon == false) && (barrels>=cannonUpgradeCost))
        {
            barrels -= cannonUpgradeCost;
            upgradeCannon = true;
            cannonUpgradeHolder.sprite = Resources.Load<Sprite>("Images/HolderGold");
            cannonKeyBind.SetActive(false);
            cannonCostText.SetActive(false);
            cannonBarrelImage.SetActive(false);
            //ShipMovement script = ship.GetComponent<ShipMovement>();
            script.WaitBetweenShots = 1f;
            float newWidth = 100f;

            Vector2 size = cannonShotBlackBar.sizeDelta;
            size.x = newWidth;
            cannonShotBlackBar.sizeDelta = size;

            Vector2 size2 = cannonShotOrangeBar.sizeDelta;
            size2.x = newWidth;
            cannonShotOrangeBar.sizeDelta = size;
        }

        if (Input.GetKeyDown(KeyCode.H) && (upgradeHealth == false) && (barrels>=healthUpgradeCost))
        {
            barrels -= healthUpgradeCost;
            upgradeHealth = true;
            healthUpgradeHolder.sprite = Resources.Load<Sprite>("Images/HolderGold");
            healthKeyBind.SetActive(false);
            healthCostText.SetActive(false);
            healthBarrelImage.SetActive(false);
            //ShipMovement script = ship.GetComponent<ShipMovement>();
            script.maxHp = 200f;
            script.currentHp += 100f;
            float newWidth = 500f;

            Vector2 size = shipHPBlackBar.sizeDelta;
            size.x = newWidth;
            shipHPBlackBar.sizeDelta = size;

            Vector2 size2 = shipHPGreenBar.sizeDelta;
            size2.x = newWidth;
            shipHPGreenBar.sizeDelta = size;
        }

        //ShipMovement script = ship.GetComponent<ShipMovement>();
        if(Input.GetKeyDown(KeyCode.J) && barrels >= repairCost && (script.currentHp < script.maxHp))
        {
            barrels -= repairCost;
            script.currentHp = script.maxHp;
            repairCost *= 2;
            Debug.Log($"{repairCost}");
            repairCostTextText.text = repairCost.ToString();
        }



    }

    public void checkAfford()
    {
        //cannon Upgrade
        if(barrels >= cannonUpgradeCost && (upgradeCannon == false))
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
        if(barrels >= healthUpgradeCost && (upgradeHealth == false))
        {
            healthUpgradeHolder.sprite = Resources.Load<Sprite>("Images/HolderGrayAbleToBuy");
            healthKeyBind.SetActive(true);
        }
        else if(upgradeHealth == false)
        {
            healthUpgradeHolder.sprite = Resources.Load<Sprite>("Images/HolderGray");
            healthKeyBind.SetActive(false);
        }

        //ShipMovement script = ship.GetComponent<ShipMovement>();
        if(script.currentHp < script.maxHp && barrels >= repairCost)
        {
            repairHolder.sprite = Resources.Load<Sprite>("Images/HolderGrayAbleToBuy");
            repairKeyBind.SetActive(true);
            repairCostText.SetActive(true);
            repairCostTextText.text = repairCost.ToString();
            repairBarrelImage.SetActive(true);
        }
        else if(script.currentHp < script.maxHp && barrels < repairCost)
        {
            repairHolder.sprite = Resources.Load<Sprite>("Images/HolderGray");
            repairKeyBind.SetActive(false);
            repairCostText.SetActive(true);
            repairCostTextText.text = repairCost.ToString();
            repairBarrelImage.SetActive(true);
        }
        else if(script.currentHp == script.maxHp)
        {
            repairHolder.sprite = Resources.Load<Sprite>("Images/HolderGold");
            repairKeyBind.SetActive(false);
            repairCostText.SetActive(false);
            repairBarrelImage.SetActive(false);
        }

    }
}
