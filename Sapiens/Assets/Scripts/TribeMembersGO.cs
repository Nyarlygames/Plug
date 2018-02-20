using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TribeMembersGO : MonoBehaviour {

    public GameObject radius;
    public GameObject fire;
    public Sprite[] firesprites;
    int firespritespeed = 5;
    int init = 0;
    ParticleSystem ps;
    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {
       if (firesprites != null)
        {
            if (init == 0)
            {
                GameObject FireParticles = new GameObject("FireParticles");
                ps = FireParticles.AddComponent<ParticleSystem>();

                ParticleSystemRenderer psr = FireParticles.GetComponent<ParticleSystemRenderer>();
                psr.material = Resources.Load<Material>("Play/Materials/smoke_mat");
              //  psr.trailMaterial = Resources.Load<Material>("Play/Materials/smoke_mat");
                ParticleSystem.MainModule main = ps.main;
                main.startSize = 0.05f;
                main.startSpeed = 0.1f;
                main.startSizeY = 0.5f;
                main.startLifetime = 2.0f;
                ParticleSystem.ShapeModule pss = ps.shape;
                pss.rotation = Vector3.up;
                pss.scale = new Vector3(0.5f, 1.0f, 1.0f);
                pss.arc = 10.0f;
                pss.radius = 1.0f;
                pss.angle = 0.0f;
                init = 1;
                FireParticles.GetComponent<Transform>().position = fire.GetComponent<Transform>().position;
                FireParticles.GetComponent<Transform>().SetParent(fire.GetComponent<Transform>());
                FireParticles.GetComponent<Transform>().Rotate(new Vector3(0.0f, 0.0f, 90.0f));
            }
            fire.GetComponent<SpriteRenderer>().sprite = firesprites[(int) (Time.time * firespritespeed % firesprites.Length)];
        }
        ps.Emit(2);
    }
}
