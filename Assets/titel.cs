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
    int nomber;

    void Start()
    {
        t = 0;
        rb = GetComponent<Rigidbody>();

        data = new Data();
        for (int i = 0; i < data.input.Length; i++) data.input[i] = 0;
        for (int i = 0; i < data.output.Length; i++) data.output[i] = Random.Range(-1f, 1f);
        for (int i = 0; i < data.w.Length; i++) data.w[i] = Random.Range(-1f, 1f);
        nomber = Random.Range(0, 5);


    }

    void Update()
    {
        float x = 0,y=0;
        if (t == 0)
        {
            //outputを2次元にする。x,yを出力にする
            x = Random.Range(-1f, 1f) * speed;
            y = Random.Range(-1f, 1f) * speed;

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
                //Debug.Log(hit.collider.gameObject.tag);
                switch (hit.collider.gameObject.tag)
                {
                    case "out":
                        data.input[i] = 0.5f;
                        break;
                    case "block":
                        data.input[i] = 1.0f;
                        break;
                    case "item":
                        data.input[i] = 1.5f;
                        break;
                    default:
                        data.input[i] = 2.0f;
                        break;
                }
            }

            Debug.DrawRay(ray.origin, direction, Color.red, .5f);
        }
        Network();
    }
   
    void Network()
    {
        float x=0,z=0,num=0;

        for (int i=0; i<data.input.Length;i++) x += data.input[i] * data.w[i];  
        z = Sigmoid(x);
        for (int i = 0; i < data.output.Length; i++)
        {
            data.output[i] = z * data.w[i + data.input.Length];
            Debug.Log(data.output[i]+" "+i);

            if (i == 0)
            {
                num = data.output[0];
                nomber = 0;
            }
            else if (num < data.output[i])
            {
                num = data.output[i];
                nomber = i;
            }
        }

    }

    float Sigmoid(float x)
    {
        float z = 1 / (1 + Mathf.Exp(-x));
        return z;
    }
}
