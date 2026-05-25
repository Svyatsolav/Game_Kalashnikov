using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LogScript : MonoBehaviour
{
    [SerializeField] GameObject logPan;
    [SerializeField] public List<LogData> items = new List<LogData>();
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform grid;
    [SerializeField] Text logHelpText;
    
    [Header("InfoPan")]
    [SerializeField] GameObject infoPan;
    [SerializeField] Image _icon;
    [SerializeField] Text _name;
    [SerializeField] Text _desc;
    [SerializeField] GameObject closeButton;
    public static LogScript instance;
    void Start()
    {
        instance = this;
    }
    void Update()
    {
        if(logPan.activeInHierarchy == false) logHelpText.text = "(Tab) открыть журнал";
        else logHelpText.text = "(Tab) закрыть журнал";
    }
    public void LogPanelActive()
    {
        if(logPan.activeInHierarchy == false)
        {
            logPan.SetActive(true);
            UpdateUI();
        }
        else logPan.SetActive(false); InfoPanOff();
    }
    public void AddItem(LogData item)
    {
        if(!items.Contains(item)) items.Add(item);
        UpdateUI();
    }
    public void UpdateUI()
    {
        foreach (Transform child in grid) Destroy(child.gameObject);

        foreach (var item in items)
        {
            GameObject logItem = Instantiate(itemPrefab, grid);

            logItem.name = item.itemName; 
            
            EventTrigger trigger = logItem.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => {
            PointerEventData pointerData = (PointerEventData)data;
            if (pointerData.button == PointerEventData.InputButton.Right) InfoPanOn(item);});
            trigger.triggers.Add(entry);

            EventTrigger triggerCB = closeButton.GetComponent<EventTrigger>();
            EventTrigger.Entry entryCB = new EventTrigger.Entry();
            entryCB.eventID = EventTriggerType.PointerClick;
            entryCB.callback.AddListener((data) => {
            PointerEventData pointerData = (PointerEventData)data;
            if (pointerData.button == PointerEventData.InputButton.Right) InfoPanOff();});
            triggerCB.triggers.Add(entryCB);

            Text name = logItem.transform.Find("Name")?.GetComponent<Text>();
            name.text = item.itemName;

            Image icon = logItem.transform.Find("Icon")?.GetComponent<Image>();
            icon.sprite = item.icon;
        }
    }
    public void InfoPanOn(LogData item)
    {
        infoPan.SetActive(true);
        InfoPanUI(item);
    }
    public void InfoPanOff() => infoPan.SetActive(false);
    public void InfoPanUI(LogData item)
    {
        _icon.sprite = item.icon;
        _name.text = item.itemName;
        _desc.text = item.description;
    }
}