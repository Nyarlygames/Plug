using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PanelTriggerInfo : MonoBehaviour, IPointerExitHandler
{
    public ObjectSave obj;
    InputField AssignList;
    InputField RemoveList;
    GameManager GM;
    GameObject RemovePanel;
    GameObject AssignPanel;
    Text Status;
    Text Status_List;
    Text UI_TriggerName;
    Text UI_TriggerStatus;
    Text UI_TriggerCapacity;
    Text UI_TriggerCurrent;
    Text UI_TriggerResource;
    // Use this for initialization
    void Start ()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameObject.Find("UI_TriggerAssign_B").GetComponent<Button>().onClick.AddListener(AssignMemberClick);
        GameObject.Find("UI_TriggerRemove_B").GetComponent<Button>().onClick.AddListener(RemoveMemberClick);
        AssignList = GameObject.Find("UI_TriggerAssignName").GetComponent<InputField>();
        RemoveList = GameObject.Find("UI_TriggerRemoveName").GetComponent<InputField>();
        RemovePanel = GameObject.Find("UI_TriggerInfoRemovePanel");
        AssignPanel = GameObject.Find("UI_TriggerInfoAssignPanel");
        Status = GameObject.Find("UI_TriggerStatus").GetComponent<Text>();
        Status_List = GameObject.Find("UI_TriggerList").GetComponent<Text>();
        UI_TriggerName = GameObject.Find("UI_TriggerName").GetComponent<Text>();
        UI_TriggerCapacity = GameObject.Find("UI_TriggerCapacity").GetComponent<Text>();
        UI_TriggerCurrent = GameObject.Find("UI_TriggerCurrent").GetComponent<Text>();
        UI_TriggerResource = GameObject.Find("UI_TriggerResource").GetComponent<Text>();

        AssignPanel.SetActive(false);
        RemovePanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        EventSave curE = GM.EM.events.Find(es => es.obj.id == obj.id);
        if (curE != null)
        {
            UI_TriggerName.text = "Activity : " + curE.obj.modifiers["activity"];
            if ((AssignPanel.activeSelf == false) && (RemovePanel.activeSelf == false))
            {
                AssignPanel.SetActive(true);
                RemovePanel.SetActive(true);
            }
            UI_TriggerCapacity.text = "Stock : " + curE.obj.modifiers["capacity"];
            UI_TriggerCurrent.text = "Daily gain : " + curE.obj.modifiers["extract_daily"]; // do formula with exp on gather depending on chars
            UI_TriggerResource.text = "Resource type : " + curE.obj.modifiers["resource_type"];

            if (curE.cs.Count>0)
            {
                Status_List.text = "";
                Status.text = "Used.";
                foreach (CharacterSave cs in curE.cs) {
                    Status_List.text += "{" + cs.name + "} ";
                }
            }
            else
            {
                Status.text = "Available.";
                Status_List.text = "";
            }
        }
    }

    void RemoveMemberClick()
    {
        EventSave curE = GM.EM.events.Find(es => es.obj.id == obj.id);
        if (curE != null)
        {
            if (GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.members.Find(cs => cs.name == RemoveList.text) != null)
            {
                if (curE.cs.Contains(GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.members.Find(cs => cs.name == RemoveList.text)))
                    curE.cs.Remove(GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.members.Find(cs => cs.name == RemoveList.text));
            }
        }
    }
    void AssignMemberClick()
    {
        EventSave curE = GM.EM.events.Find(es => es.obj.id == obj.id);
        if (curE != null)
        {
            if (GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.members.Find(cs => cs.name == AssignList.text) != null)
            {
                if (!curE.cs.Contains(GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.members.Find(cs => cs.name == AssignList.text)))
                {
                    curE.cs.Add(GM.TribeGO.GetComponent<TribeGO>().tribeCurrent.members.Find(cs => cs.name == AssignList.text));
                }
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
            Destroy(gameObject);
    }
}
