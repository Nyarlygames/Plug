using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class NewGameController : MonoBehaviour {

    public int Points = 20; 
    public int Force = 0;
    public int Endurance = 0;
    public int Constitution = 0;
    public int Mental = 0;
    public int Dexterite = 0;
    public int Precision = 0;
    public int Vitesse = 0;
    public int Perception = 0;
    public int Survie = 0;
    public int Intelligence = 0;
    public int Memoire = 0;
    public int Charisme = 0;
    public int Social = 0;
    public int Langage = 0;
    Text Total_Text;
    Text Force_Text;
    Text Endurance_Text;
    Text Constitution_Text;
    Text Mental_Text;
    Text Dexterite_Text;
    Text Precision_Text;
    Text Vitesse_Text;
    Text Perception_Text;
    Text Survie_Text;
    Text Intelligence_Text;
    Text Memoire_Text;
    Text Charisme_Text;
    Text Social_Text;
    Text Langage_Text;
    
    void Start () {

        Button temp = GameObject.Find("+Force").GetComponent<Button>();
        temp.onClick.AddListener(ForceUpButton_Click);
        temp = GameObject.Find("-Force").GetComponent<Button>();
        temp.onClick.AddListener(ForceDownButton_Click);
        temp = GameObject.Find("+Endurance").GetComponent<Button>();
        temp.onClick.AddListener(EnduranceUpButton_Click);
        temp = GameObject.Find("-Endurance").GetComponent<Button>();
        temp.onClick.AddListener(EnduranceDownButton_Click);
        temp = GameObject.Find("+Constitution").GetComponent<Button>();
        temp.onClick.AddListener(ConstitutionUpButton_Click);
        temp = GameObject.Find("-Constitution").GetComponent<Button>();
        temp.onClick.AddListener(ConstitutionDownButton_Click);
        temp = GameObject.Find("+Mental").GetComponent<Button>();
        temp.onClick.AddListener(MentalUpButton_Click);
        temp = GameObject.Find("-Mental").GetComponent<Button>();
        temp.onClick.AddListener(MentalDownButton_Click);
        temp = GameObject.Find("+Dexterite").GetComponent<Button>();
        temp.onClick.AddListener(DexteriteUpButton_Click);
        temp = GameObject.Find("-Dexterite").GetComponent<Button>();
        temp.onClick.AddListener(DexteriteDownButton_Click);
        temp = GameObject.Find("+Precision").GetComponent<Button>();
        temp.onClick.AddListener(PrecisionUpButton_Click);
        temp = GameObject.Find("-Precision").GetComponent<Button>();
        temp.onClick.AddListener(PrecisionDownButton_Click);
        temp = GameObject.Find("+Vitesse").GetComponent<Button>();
        temp.onClick.AddListener(VitesseUpButton_Click);
        temp = GameObject.Find("-Vitesse").GetComponent<Button>();
        temp.onClick.AddListener(VitesseDownButton_Click);
        temp = GameObject.Find("+Perception").GetComponent<Button>();
        temp.onClick.AddListener(PerceptionUpButton_Click);
        temp = GameObject.Find("-Perception").GetComponent<Button>();
        temp.onClick.AddListener(PerceptionDownButton_Click);
        temp = GameObject.Find("+Survie").GetComponent<Button>();
        temp.onClick.AddListener(SurvieUpButton_Click);
        temp = GameObject.Find("-Survie").GetComponent<Button>();
        temp.onClick.AddListener(SurvieDownButton_Click);
        temp = GameObject.Find("+Intelligence").GetComponent<Button>();
        temp.onClick.AddListener(IntelligenceUpButton_Click);
        temp = GameObject.Find("-Intelligence").GetComponent<Button>();
        temp.onClick.AddListener(IntelligenceDownButton_Click);
        temp = GameObject.Find("+Memoire").GetComponent<Button>();
        temp.onClick.AddListener(MemoireUpButton_Click);
        temp = GameObject.Find("-Memoire").GetComponent<Button>();
        temp.onClick.AddListener(MemoireDownButton_Click);
        temp = GameObject.Find("+Charisme").GetComponent<Button>();
        temp.onClick.AddListener(CharismeUpButton_Click);
        temp = GameObject.Find("-Charisme").GetComponent<Button>();
        temp.onClick.AddListener(CharismeDownButton_Click);
        temp = GameObject.Find("+Social").GetComponent<Button>();
        temp.onClick.AddListener(SocialUpButton_Click);
        temp = GameObject.Find("-Social").GetComponent<Button>();
        temp.onClick.AddListener(SocialDownButton_Click);
         temp = GameObject.Find("+Langage").GetComponent<Button>();
        temp.onClick.AddListener(LangageUpButton_Click);
        temp = GameObject.Find("-Langage").GetComponent<Button>();
        temp.onClick.AddListener(LangageDownButton_Click);
        temp = GameObject.Find("Launch").GetComponent<Button>();
        temp.onClick.AddListener(LaunchButton_Click);


        Total_Text = GameObject.Find("Total").GetComponent<Text>();
        Total_Text.text = Points.ToString();
        Force_Text = GameObject.Find("Force_Int").GetComponent<Text>();
        Force_Text.text = Force.ToString();
        Endurance_Text = GameObject.Find("Endurance_Int").GetComponent<Text>();
        Endurance_Text.text = Endurance.ToString();
        Constitution_Text = GameObject.Find("Constitution_Int").GetComponent<Text>();
        Constitution_Text.text = Constitution.ToString();
        Mental_Text = GameObject.Find("Mental_Int").GetComponent<Text>();
        Mental_Text.text = Mental.ToString();
        Dexterite_Text = GameObject.Find("Dexterite_Int").GetComponent<Text>();
        Dexterite_Text.text = Dexterite.ToString();
        Precision_Text = GameObject.Find("Precision_Int").GetComponent<Text>();
        Precision_Text.text = Precision.ToString();
        Vitesse_Text = GameObject.Find("Vitesse_Int").GetComponent<Text>();
        Vitesse_Text.text = Vitesse.ToString();
        Perception_Text = GameObject.Find("Perception_Int").GetComponent<Text>();
        Perception_Text.text = Perception.ToString();
        Survie_Text = GameObject.Find("Survie_Int").GetComponent<Text>();
        Survie_Text.text = Survie.ToString();
        Intelligence_Text = GameObject.Find("Intelligence_Int").GetComponent<Text>();
        Intelligence_Text.text = Intelligence.ToString();
        Memoire_Text = GameObject.Find("Memoire_Int").GetComponent<Text>();
        Memoire_Text.text = Memoire.ToString();
        Charisme_Text = GameObject.Find("Charisme_Int").GetComponent<Text>();
        Charisme_Text.text = Charisme.ToString();
        Social_Text = GameObject.Find("Social_Int").GetComponent<Text>();
        Social_Text.text = Social.ToString();
        Langage_Text = GameObject.Find("Langage_Int").GetComponent<Text>();
        Langage_Text.text = Langage.ToString();

    }
	
	void Update () {

    }

    void LaunchButton_Click()
    {
        SceneManager.LoadScene("Sapiens", LoadSceneMode.Additive);
    }

    void ForceUpButton_Click()
    {
        if (Points > 0)
        {
            Points--;
            Force++;
            Force_Text.text = Force.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void ForceDownButton_Click()
    {
        if (Force > 0)
        {
            Force--;
            Points++;
            Force_Text.text = Force.ToString();
            Total_Text.text = Points.ToString();
        }

    }

    void EnduranceUpButton_Click()
    {
        if (Points > 0)
        {
            Points--;
            Endurance++;
            Endurance_Text.text = Endurance.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void EnduranceDownButton_Click()
    {
        if (Endurance > 0)
        {
            Endurance--;
            Points++;
            Endurance_Text.text = Endurance.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void ConstitutionUpButton_Click()
    {
        if (Points > 0)
        {
            Points--;
            Constitution++;
            Constitution_Text.text = Constitution.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void ConstitutionDownButton_Click()
    {
        if (Constitution > 0)
        {
            Constitution--;
            Points++;
            Constitution_Text.text = Constitution.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void MentalUpButton_Click()
    {
        if (Points > 0)
        {
            Points--;
            Mental++;
            Mental_Text.text = Mental.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void MentalDownButton_Click()
    {
        if (Mental > 0)
        {
            Mental--;
            Points++;
            Mental_Text.text = Mental.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void DexteriteUpButton_Click()
    {
        if (Points > 0)
        {
            Points--;
            Dexterite++;
            Dexterite_Text.text = Dexterite.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void DexteriteDownButton_Click()
    {
        if (Dexterite > 0)
        {
            Dexterite--;
            Points++;
            Dexterite_Text.text = Dexterite.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void PrecisionUpButton_Click()
    {
        if (Points > 0)
        {
            Points--;
            Precision++;
            Precision_Text.text = Precision.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void PrecisionDownButton_Click()
    {
        if (Precision > 0)
        {
            Precision--;
            Points++;
            Precision_Text.text = Precision.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void VitesseUpButton_Click()
    {
        if (Points > 0)
        {
            Points--;
            Vitesse++;
            Vitesse_Text.text = Vitesse.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void VitesseDownButton_Click()
    {
        if (Vitesse > 0)
        {
            Vitesse--;
            Points++;
            Vitesse_Text.text = Vitesse.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void PerceptionUpButton_Click()
    {
        if (Points > 0)
        {
            Points--;
            Perception++;
            Perception_Text.text = Perception.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void PerceptionDownButton_Click()
    {
        if (Perception > 0)
        {
            Perception--;
            Points++;
            Perception_Text.text = Perception.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void SurvieUpButton_Click()
    {
        if (Points > 0)
        {
            Points--;
            Survie++;
            Survie_Text.text = Survie.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void SurvieDownButton_Click()
    {
        if (Survie > 0)
        {
            Survie--;
            Points++;
            Survie_Text.text = Survie.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void IntelligenceUpButton_Click()
    {
        if (Points > 0)
        {
            Points--;
            Intelligence++;
            Intelligence_Text.text = Intelligence.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void IntelligenceDownButton_Click()
    {
        if (Intelligence > 0)
        {
            Intelligence--;
            Points++;
            Intelligence_Text.text = Intelligence.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void MemoireUpButton_Click()
    {
        if (Points > 0)
        {
            Points--;
            Memoire++;
            Memoire_Text.text = Memoire.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void MemoireDownButton_Click()
    {
        if (Memoire > 0)
        {
            Memoire--;
            Points++;
            Memoire_Text.text = Memoire.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void CharismeUpButton_Click()
    {
        if (Points > 0)
        {
            Points--;
            Charisme++;
            Charisme_Text.text = Charisme.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void CharismeDownButton_Click()
    {
        if (Charisme > 0)
        {
            Charisme--;
            Points++;
            Charisme_Text.text = Charisme.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void SocialUpButton_Click()
    {
        if (Points > 0)
        {
            Points--;
            Social++;
            Social_Text.text = Social.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void SocialDownButton_Click()
    {
        if (Social > 0)
        {
            Social--;
            Points++;
            Social_Text.text = Social.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void LangageUpButton_Click()
    {
        if (Points > 0)
        {
            Points--;
            Langage++;
            Langage_Text.text = Langage.ToString();
            Total_Text.text = Points.ToString();
        }

    }
    void LangageDownButton_Click()
    {
        if (Langage > 0)
        {
            Langage--;
            Points++;
            Langage_Text.text = Langage.ToString();
            Total_Text.text = Points.ToString();
        }

    }
}
