using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopScript : MonoBehaviour
{
    [SerializeField] GameObject shopPan;
    [SerializeField] Text moneyText;
    public static int currentSkin;
    public bool[] isBought;
    public bool[] isEquiped;
    public int[] skinsCost;
    public Button[] SkinsBtns;
    public GameObject[] BuyObjs;
    public Text[] SkinsBtnTxts;
    public TextMeshProUGUI helpText;

    void Start()
    {
        currentSkin = PlayerPrefs.GetInt("currSkin");
        if(PlayerPrefs.HasKey("IsBought"))
        {
            string json = PlayerPrefs.GetString("IsBought");
            SerializableBoolArray wrapper = JsonUtility.FromJson<SerializableBoolArray>(json);
            isBought = wrapper.bools;
        }
        if(PlayerPrefs.HasKey("IsEquiped"))
        {
            string json = PlayerPrefs.GetString("IsEquiped");
            SerializableBoolArray1 wrapper = JsonUtility.FromJson<SerializableBoolArray1>(json);
            isEquiped = wrapper.bools;
        }
        UpdateUI();
    }
    public void ShopPan()
    {
        if(shopPan.activeInHierarchy == false) shopPan.SetActive(true);
        else shopPan.SetActive(false);
    }
    void Update()
    {
        moneyText.text = PlayerPrefs.GetInt("money").ToString();

        if(Input.GetKeyDown(KeyCode.G))
        {
            PlayerPrefs.DeleteKey("IsBought");
            PlayerPrefs.DeleteKey("IsEquiped");
            PlayerPrefs.DeleteKey("currSkin");
            PlayerPrefs.DeleteKey("money");
        }
    }
    void UpdateUI()
    {
        for(int i = 0; i < isBought.Length; i++)
        {
            if(isBought[i] == false)
            {
                BuyObjs[i].SetActive(true);
                SkinsBtnTxts[i].gameObject.SetActive(false);
            }
            else if(isBought[i] == true && isEquiped[i] == false)
            {
                BuyObjs[i].SetActive(false);
                SkinsBtnTxts[i].gameObject.SetActive(true);
                SkinsBtnTxts[i].text = "Выбрать";
            }
            else if(isBought[i] == true && isEquiped[i] == true)
            {
                BuyObjs[i].SetActive(false);
                SkinsBtnTxts[i].gameObject.SetActive(true);
                SkinsBtnTxts[i].text = "Выбрано";
            }
        }
    }
    public void BuySkin(int id)
    {
        int currMoney = PlayerPrefs.GetInt("money");
        if(isBought[id] == false)
        {
            if(currMoney >= skinsCost[id])
            {
                currMoney -= skinsCost[id];
                isBought[id] = true;
                HelpText(0);
            }
            else HelpText(1);
        }
        else if(isBought[id] == true)
        {
            currentSkin = id;
            for(int i = 0; i < isBought.Length; i++) isEquiped[i] = false;
            isEquiped[id] = true;
            HelpText(2);
        }
        UpdateUI();
        PlayerPrefs.SetInt("money", currMoney);
        PlayerPrefs.SetInt("currSkin", currentSkin);
        string jsonb = JsonUtility.ToJson(new SerializableBoolArray(isBought));
        PlayerPrefs.SetString("IsBought", jsonb);
        string json = JsonUtility.ToJson(new SerializableBoolArray1(isEquiped));
        PlayerPrefs.SetString("IsEquiped", json);
        PlayerPrefs.Save();
    }
    public void HelpText(int id)
    {
        helpText.gameObject.SetActive(false);
        helpText.gameObject.SetActive(true);
        if(id == 0) helpText.text = $"<color=green>Скин куплен!";
        else if(id == 1) helpText.text = $"<color=red>Недостаточно монет!";
        else if(id == 2) helpText.text = $"<color=white>Скин выбран";
    }

    [System.Serializable]
    private class SerializableBoolArray
    {
        public bool[] bools;
        public SerializableBoolArray(bool[] bools) { this.bools = bools; }
    }
    private class SerializableBoolArray1
    {
        public bool[] bools;
        public SerializableBoolArray1(bool[] bools) { this.bools = bools; }
    }
}