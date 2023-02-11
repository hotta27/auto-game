using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Data
{
    public float[] input=new float[4], output=new float[5], w=new float[9];
    
}

public class titel : MonoBehaviour
{
    public float speed = 10.0f;
    public Rigidbody rb;
    public GameObject ca,p;
    public Transform target;
    int t,s=500;
    Data data;

    void Start()
    {
        t = 0;
        rb = GetComponent<Rigidbody>();
        
        data = new Data();
        foreach (int i in data.input) data.input[i] = Random.Range(-1f, 1f);
        foreach (int i in data.output) data.output[i] = Random.Range(-1f, 1f);
        foreach (int i in data.w) data.w[i] = Random.Range(-1f, 1f);
    }

    void Update()
    {
        float x = 0,y=0;
        if (t == 0)
        {
             x =Random.Range(-1f, 1f) * speed;
             y =Random.Range(-1f, 1f) * speed;
            
            t = s;
        }t--;
        rb.AddForce(x, y, 0);
        ca.transform.position = new Vector3(p.transform.position.x, p.transform.position.y + 1, -10);

      
      Vector3 origin = this.transform.position; // 原点

        int vx=10, vy=0;
        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 1:
                    vx = -10;
                    break;
                case 2:
                    vx = 0;
                    vy = 10;
                    break;
                case 3:
                    vy = -10;
                    break;
            }
            Vector3 direction = new Vector3(vx, vy, 0); // X軸方向を表すベクトル
            Ray ray = new Ray(origin: origin, direction: direction); // Rayを生成
            RaycastHit hit;
            float len = direction.x;
            if (i > 1) len = direction.y;

            if (Physics.Raycast(ray, out hit, len))
            {
                Debug.Log(hit.collider.gameObject.tag);
            }

            Debug.DrawRay(ray.origin, direction, Color.red, .5f);
        }
    }
   
    void Network()
    {
        float x,z;
        foreach(int i in data.input) x = data.input[i] * data.w[i];
        z = Sigmoid(x);
        
    }

    float Sigmoid(float x)
    {
        float z = 1 / (1 + Mathf.Exp(-x));
        return z;
    }
}
