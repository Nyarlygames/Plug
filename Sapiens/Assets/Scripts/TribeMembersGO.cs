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
    public Vector3 MoveToTarget = Vector3.zero;
    Rigidbody2D TribeBody;
    public Transform TribeTransform;
    public float speed = 1.0f;
    // Use this for initialization
    void Start ()
    {
        TribeTransform = gameObject.GetComponent<Transform>();
        gameObject.AddComponent<Rigidbody2D>();
        TribeBody = gameObject.GetComponent<Rigidbody2D>();
        TribeBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        TribeBody.gravityScale = 0.0f;
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
        if (MoveToTarget != Vector3.zero)
        {
            if (TribeTransform.position != MoveToTarget)
                TribeBody.MovePosition(Vector3.MoveTowards(TribeTransform.position, MoveToTarget, speed * Time.deltaTime));
            else
                MoveToTarget = Vector3.zero;
        }
    }

    public void MoveTo(Vector3 pos)
    {
        MoveToTarget = pos;
    }
}
