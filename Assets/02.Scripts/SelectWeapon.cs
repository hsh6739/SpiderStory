using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectWeapon : MonoBehaviour
{
    public ItemData itemData; // ScriptableObject
    //public ItemType itemNum;
    //public int weaponNum;

    public Image iconImage;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI weaponDescText;
    public TextMeshProUGUI priceText;
    public GameObject buyImg;

    public int price;
    //public int level;


    private void Awake()
    {
        iconImage = transform.GetChild(0).GetComponent<Image>();
        levelText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        weaponNameText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        weaponDescText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        priceText = transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        buyImg = transform.GetChild(5).gameObject;
    }

    void Start()
    {
        if (itemData.itemType == ItemType.HealPack)
            levelText.text = "사용 횟수 : " + itemData.level;
        else
            levelText.text = "Lv " + itemData.level;

        price = 10;
    }


    public void init()
    {
        buyImg = transform.GetChild(5).gameObject;
        buyImg.SetActive(false);
        transform.GetComponent<Button>().interactable = true;
        price = 10 + (itemData.level * 10);
        priceText.text = "가격 : " + price + "G";


    }

    public void ClickButton()
    {
        price = 10 + (itemData.level * 10);

        if (GameManager.Instance.goldCount < price)
        {
            Debug.Log("Not Enough Money !!");
            return;
        }

        if (itemData.level == itemData.maxLevel)
        {
            Debug.Log("itemData.level == itemData.maxLevel !!");
            return;
        }

        // 구매 
        GameManager.Instance.goldCount -= price;

        // 아이템 레벨 증가
        itemData.level++;
        price = 10 + (itemData.level * 10);

        buyImg.SetActive(true); // 이미지 불투명하게
        transform.GetComponent<Button>().interactable = false;
        levelText.text = "Lv " + itemData.level;
        priceText.text = "가격 : "+ price +"G";

        switch (itemData.itemType)
        {
            // weapon
            case ItemType.WheelSaw:
            case ItemType.Rifle:
            case ItemType.SniperRifle:
            case ItemType.BallString:
            case ItemType.Web:
                if (itemData.level == 1)
                    GameManager.Instance.player.GetComponent<Player>().Weapons[(int)itemData.itemType].gameObject.SetActive(true);
                else if (itemData.level == itemData.maxLevel)
                    CheckMaxLevel();
                else
                    GameManager.Instance.player.GetComponent<Player>().Weapons[(int)itemData.itemType].GetComponent<Weapon>().LevelUp();
                break;

            case ItemType.SpeedUp:
                if (itemData.level == itemData.maxLevel)
                    CheckMaxLevel();
                else
                    GameManager.Instance.player.GetComponent<Player>().speed = itemData.damages[itemData.level];
                break;

            case ItemType.HealPack:
                GameManager.Instance.health += itemData.counts[0];
                if (GameManager.Instance.health > 4)
                    GameManager.Instance.health = 4;
                GameManager.Instance.player.GetComponent<Player>().UpdatePlayerEyes();
                levelText.text = "사용 횟수 : " + itemData.level;
                break;

            case ItemType.PickupRangeUp:
                if (itemData.level == itemData.maxLevel)
                    CheckMaxLevel();
                else
                    GameManager.Instance.player.GetComponent<Player>().goldCollider.radius = itemData.scales[itemData.level];
                break;

            default:
                break;
        }


    }// ClickButton()


    void CheckMaxLevel()
    {
        itemData.level = itemData.maxLevel;
        levelText.text = "Lv " + itemData.level;
        GameManager.Instance.goldCount += 10;
        levelText.text = "MAX !!";

    }

}
