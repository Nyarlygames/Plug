using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TribePanelScript : MonoBehaviour {

    ControlsScript Controls;
    TribeScript Tribe;
    Text Tribe_Unity;
    Text Tribe_Rank;
    Text Tribe_Adults;
    Text Tribe_Youngs;
    Text Tribe_Generations;
    Text Tribe_Food;
    Text Tribe_Water;
    Text Tribe_Confort;
    Text Tribe_Social;
    Text Tribe_Tools;
    Text Tribe_Crafts;
    Text Tribe_Herbs;
    Text Tribe_Speed;

    Text Tribe_Gather;
    Text Tribe_Fish;
    Text Tribe_Hunt;
    Text Tribe_Cook;
    Text Tribe_Source;
    Text Tribe_Manage;
    Text Tribe_Mentor;
    Text Tribe_Sage;
    Text Tribe_Shaman;
    Text Tribe_Skin;
    Text Tribe_Wood;
    Text Tribe_Stone;
    Text Tribe_Protect;
    Text Tribe_Leader;
    Text Tribe_Scout;
    Text Tribe_Rest;
    Text Tribe_Pregnant;


    void Start()
    {
        GameObject.Find("SwitchChar").GetComponent<Button>().onClick.AddListener(SwitchTribe_Click);
        Controls = GameObject.Find("Controls").GetComponent<ControlsScript>();
        Tribe = GameObject.Find("Tribe").GetComponent<TribeScript>();

        Tribe_Unity = GameObject.Find("Tribe_Unity").GetComponent<Text>();
        Tribe_Rank = GameObject.Find("Tribe_Rank").GetComponent<Text>();
        Tribe_Adults = GameObject.Find("Tribe_Adults").GetComponent<Text>();
        Tribe_Youngs = GameObject.Find("Tribe_Youngs").GetComponent<Text>();
        Tribe_Generations = GameObject.Find("Tribe_Generations").GetComponent<Text>();
        Tribe_Food = GameObject.Find("Tribe_Food").GetComponent<Text>();
        Tribe_Water = GameObject.Find("Tribe_Water").GetComponent<Text>();
        Tribe_Confort = GameObject.Find("Tribe_Confort").GetComponent<Text>();
        Tribe_Social = GameObject.Find("Tribe_Social").GetComponent<Text>();
        Tribe_Tools = GameObject.Find("Tribe_Tools").GetComponent<Text>();
        Tribe_Crafts = GameObject.Find("Tribe_Crafts").GetComponent<Text>();
        Tribe_Herbs = GameObject.Find("Tribe_Herbs").GetComponent<Text>();
        Tribe_Speed = GameObject.Find("Tribe_Speed").GetComponent<Text>();

        Tribe_Gather = GameObject.Find("Tribe_Gather").GetComponent<Text>();
        Tribe_Fish = GameObject.Find("Tribe_Fish").GetComponent<Text>();
        Tribe_Hunt = GameObject.Find("Tribe_Hunt").GetComponent<Text>();
        Tribe_Cook = GameObject.Find("Tribe_Cook").GetComponent<Text>();
        Tribe_Source = GameObject.Find("Tribe_Source").GetComponent<Text>();
        Tribe_Manage = GameObject.Find("Tribe_Manage").GetComponent<Text>();
        Tribe_Mentor = GameObject.Find("Tribe_Mentor").GetComponent<Text>();
        Tribe_Sage = GameObject.Find("Tribe_Sage").GetComponent<Text>();
        Tribe_Shaman = GameObject.Find("Tribe_Shaman").GetComponent<Text>();
        Tribe_Skin = GameObject.Find("Tribe_Skin").GetComponent<Text>();
        Tribe_Wood = GameObject.Find("Tribe_Wood").GetComponent<Text>();
        Tribe_Stone = GameObject.Find("Tribe_Stone").GetComponent<Text>();
        Tribe_Protect = GameObject.Find("Tribe_Protect").GetComponent<Text>();
        Tribe_Leader = GameObject.Find("Tribe_Leader").GetComponent<Text>();
        Tribe_Scout = GameObject.Find("Tribe_Scout").GetComponent<Text>();
        Tribe_Rest = GameObject.Find("Tribe_Rest").GetComponent<Text>();
        Tribe_Pregnant = GameObject.Find("Tribe_Pregnant").GetComponent<Text>();

    }

    void SwitchTribe_Click()
    {
        Controls.char_panel = true;
        Controls.tribe_panel = false;
        SceneManager.LoadSceneAsync("CharacterPanel", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("TribePanel");

    }

    void Update () {


        Tribe_Unity.text = "Unity : " + Tribe.TrbUnity;
        Tribe_Rank.text = "Rank : " + Tribe.TrbRank;
        Tribe_Adults.text = "Adults : " + Tribe.TrbAdults;
        Tribe_Youngs.text = "Youngs : " + Tribe.TrbYoungs;
        Tribe_Generations.text = "Generations : " + Tribe.TrbGenerations;
        Tribe_Food.text = "Food : " + Tribe.TrbFood;
        Tribe_Water.text = "Water : " + Tribe.TrbWater;
        Tribe_Confort.text = "Confort : " + Tribe.TrbConfort;
        Tribe_Social.text = "Social : " + Tribe.TrbSocial;
        Tribe_Tools.text = "Tools : " + Tribe.TrbTools;
        Tribe_Crafts.text = "Crafts : " + Tribe.TrbCrafts;
        Tribe_Herbs.text = "Herbs : " + Tribe.TrbHerbs;
        Tribe_Speed.text = "Speed : " + Tribe.TrbSpeed;

        Tribe_Gather.text = "Gather : ";
        GetCharNames(Tribe_Gather, Tribe.TrbGather);
        Tribe_Fish.text = "Fish : ";
        GetCharNames(Tribe_Fish, Tribe.TrbFish);
        Tribe_Hunt.text = "Hunt : ";
        GetCharNames(Tribe_Hunt, Tribe.TrbHunt);
        Tribe_Cook.text = "Cook : ";
        GetCharNames(Tribe_Cook, Tribe.TrbCook);
        Tribe_Source.text = "Source : ";
        GetCharNames(Tribe_Source, Tribe.TrbSource);
        Tribe_Manage.text = "Manage : ";
        GetCharNames(Tribe_Manage, Tribe.TrbManage);
        Tribe_Mentor.text = "Mentor : ";
        GetCharNames(Tribe_Mentor, Tribe.TrbMentor);
        Tribe_Sage.text = "Sage : ";
        GetCharNames(Tribe_Sage, Tribe.TrbSage);
        Tribe_Shaman.text = "Shaman : ";
        GetCharNames(Tribe_Shaman, Tribe.TrbShaman);
        Tribe_Skin.text = "Skin : ";
        GetCharNames(Tribe_Skin, Tribe.TrbSkin);
        Tribe_Wood.text = "Wood : ";
        GetCharNames(Tribe_Wood, Tribe.TrbWood);
        Tribe_Stone.text = "Stone : ";
        GetCharNames(Tribe_Stone, Tribe.TrbStone);
        Tribe_Protect.text = "Protect : ";
        GetCharNames(Tribe_Protect, Tribe.TrbProtect);
        Tribe_Leader.text = "Leader : {" + Tribe.TrbLeader.GetComponent<CharacterScript>().pname + "}";
        Tribe_Scout.text = "Scout : ";
        GetCharNames(Tribe_Scout, Tribe.TrbScout);
        Tribe_Rest.text = "Rest : ";
        GetCharNames(Tribe_Rest, Tribe.TrbRest);
        Tribe_Pregnant.text = "Pregnant : ";
        GetCharNames(Tribe_Pregnant, Tribe.TrbPregnant);




    }

    void GetCharNames(Text work, List<GameObject> workers)
    {
        if (workers.Count == 0)
        {
            work.text += "None.";
        }
        else
        {
            foreach (GameObject worker in workers)
            {
                work.text += "{" + worker.GetComponent<CharacterScript>().pname + "} ";
            }
        }
    }
}