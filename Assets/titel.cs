using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titel : MonoBehaviour
{
    public float speed = 10.0f;
    public Rigidbody rb;
    public GameObject ca,p;
    int t,s=500;

    void Start()
    {
        t = 0;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float x = 0,z=0;
        if (t == 0)
        {
             x = Random.Range(-1f, 1f) * speed;
             z = Random.Range(-1f, 1f) * speed;
            
            t = s;
        }t--;
        rb.AddForce(x, z, 0);
        ca.transform.position = new Vector3(p.transform.position.x, p.transform.position.y + 1, -10);

    }
    public void play()
    {
        SceneManager.LoadScene("game");
    }

}
