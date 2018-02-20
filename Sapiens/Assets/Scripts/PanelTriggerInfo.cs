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
        TribeGO tribe = GM.TribeGO.GetComponent<TribeGO>();
        if (curE != null)
        {
            CharacterSave removechar = tribe.tribeCurrent.members.Find(cs => cs.name == RemoveList.text);
            if (removechar != null)
            {
                if (curE.cs.Contains(removechar))
                {
                    if (curE.obj.modifiers.ContainsKey("activity"))
                    {
                        switch (curE.obj.modifiers["activity"])
                        {
                            case "gather":
                                if (tribe.tribeCurrent.gatherers.Contains(removechar) && (removechar.available == false)) // available check not necessary, in case multiple assignments go back here later
                                {
                                    removechar.available = true;
                                    curE.cs.Remove(removechar);
                                    tribe.tribeCurrent.gatherers.Remove(removechar);
                                }
                                break;
                            case "fish":
                                if (tribe.tribeCurrent.fishers.Contains(removechar) && (removechar.available == false)) // available check not necessary, in case multiple assignments go back here later
                                {
                                    removechar.available = true;
                                    curE.cs.Remove(removechar);
                                    tribe.tribeCurrent.fishers.Remove(removechar);
                                }
                                break;
                            case "hunt":
                                if (tribe.tribeCurrent.hunters.Contains(removechar) && (removechar.available == false)) // available check not necessary, in case multiple assignments go back here later
                                {
                                    removechar.available = true;
                                    curE.cs.Remove(removechar);
                                    tribe.tribeCurrent.hunters.Remove(removechar);
                                }
                                break;
                            case "source":
                                if (tribe.tribeCurrent.sourcers.Contains(removechar) && (removechar.available == false)) // available check not necessary, in case multiple assignments go back here later
                                {
                                    removechar.available = true;
                                    curE.cs.Remove(removechar);
                                    tribe.tribeCurrent.sourcers.Remove(removechar);
                                }
                                break;
                        }
                    }
                } // else no char to remove
            } // else no char with that name
        } // no event associated (no char assigned to it)
    }
    void AssignMemberClick()
    {
        EventSave curE = GM.EM.events.Find(es => es.obj.id == obj.id);
        TribeGO tribe = GM.TribeGO.GetComponent<TribeGO>();
        if (curE != null)
        {
            CharacterSave addedchar = tribe.tribeCurrent.members.Find(cs => cs.name == AssignList.text);
            if (addedchar != null)
            {
                if (!curE.cs.Contains(addedchar))
                {
                    if (curE.obj.modifiers.ContainsKey("activity"))
                    {
                        switch (curE.obj.modifiers["activity"])
                        {
                            case "gather":
                                if (!tribe.tribeCurrent.gatherers.Contains(addedchar) && (addedchar.available == true))
                                {
                                    addedchar.available = false;
                                    tribe.tribeCurrent.gatherers.Add(addedchar);
                                    curE.cs.Add(addedchar);
                                }
                                break;
                            case "fish":
                                if (!tribe.tribeCurrent.fishers.Contains(addedchar) && (addedchar.available == true))
                                {
                                    addedchar.available = false;
                                    tribe.tribeCurrent.fishers.Add(addedchar);
                                    curE.cs.Add(addedchar);
                                }
                                break;
                            case "hunt":
                                if (!tribe.tribeCurrent.hunters.Contains(addedchar) && (addedchar.available == true))
                                {
                                    addedchar.available = false;
                                    tribe.tribeCurrent.hunters.Add(addedchar);
                                    curE.cs.Add(addedchar);
                                }
                                break;
                            case "source":
                                if (!tribe.tribeCurrent.sourcers.Contains(addedchar) && (addedchar.available == true))
                                {
                                    addedchar.available = false;
                                    tribe.tribeCurrent.sourcers.Add(addedchar);
                                    curE.cs.Add(addedchar);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
            Destroy(gameObject);
    }
}
