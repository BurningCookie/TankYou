using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    [SerializeField] private GameObject upgradeIcon;
    [SerializeField] private List<Sprite> upgradeImages = new List<Sprite>();
    [SerializeField] private List<string> upgradeNames = new List<string>();

    private TankController tankController;

    private List<GameObject> currentlyActiveIcons = new List<GameObject>();
    private int notPurchasedUprages = 0;

    private void Start()
    {
        ScoreManager.levelup.AddListener(NewUpgrades);
        tankController = GameObject.FindGameObjectWithTag("Player").GetComponent<TankController>();
    }
    private void Update()
    {
        if (notPurchasedUprages != 0)
        {
            if (Input.GetKeyDown("1"))
            {
                Debug.Log("Upgraded "  + currentlyActiveIcons[0].transform.Find("UpgradeNameText").GetComponent<Text>().text);
                UpgradePurchased(currentlyActiveIcons[0].transform.Find("UpgradeNameText").GetComponent<Text>().text);
            }
            if (Input.GetKeyDown("2"))
            {
                Debug.Log("Upgraded " + currentlyActiveIcons[1].transform.Find("UpgradeNameText").GetComponent<Text>().text);
                UpgradePurchased(currentlyActiveIcons[1].transform.Find("UpgradeNameText").GetComponent<Text>().text);
            }
            if (Input.GetKeyDown("3"))
            {
                Debug.Log("Upgraded " + currentlyActiveIcons[2].transform.Find("UpgradeNameText").GetComponent<Text>().text);
                UpgradePurchased(currentlyActiveIcons[2].transform.Find("UpgradeNameText").GetComponent<Text>().text);
            }
        }
    }

    void NewUpgrades()
    {
        if (notPurchasedUprages == 0)
        {
            ShowUpgrades();
        }
        notPurchasedUprages++;
    }
    void ShowUpgrades()
    {
        int rnd = Random.Range(0, upgradeNames.Count);
        for (int i = 0; i < 3; i++)
        {
            if (rnd < upgradeNames.Count - 1)
            {
                rnd++;
            }
            else
            {
                rnd = 0;
            }
            GameObject upgrade = Instantiate(upgradeIcon);
            upgrade.transform.Find("UpgradeNameText").GetComponent<Text>().text = upgradeNames[rnd];
            upgrade.transform.Find("keyText").GetComponent<Text>().text = "Press " + (i + 1).ToString();
            upgrade.GetComponent<Image>().sprite = upgradeImages[rnd];
            upgrade.transform.SetParent(transform);
            currentlyActiveIcons.Add(upgrade);
        }
    }



    void UpgradePurchased(string upgrade)
    {
        switch (upgrade)
        {
            case "Topspeed":
                tankController.maxSpeed += 20;
                break;
            case "Rotation":
                tankController.rotationSpeed += 20;
                break;
            case "Acceleration":
                tankController.acceleration += 10;
                break;
            case "Turret Rotation":
                tankController.turretSpeed += 10;
                break;
            default:
                break;
        }
        if (notPurchasedUprages != 0)
        {
            foreach(GameObject g in currentlyActiveIcons)
            {
                Destroy(g);
            }
            currentlyActiveIcons.Clear();
            notPurchasedUprages -= 1;
            if (notPurchasedUprages > 0)
            {
                ShowUpgrades();
            }
        }
    }
}
