using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Data
{
    public float[] input=new float[4], output=new float[2], w=new float[6];
    public float point=0f;
    
}

public class titel : MonoBehaviour
{
    public float speed = 10.0f;
    public Rigidbody rb;
    public GameObject ca,p;
    public Transform goal;
    int t,s=500;
    float n=60;
    Data data;
  

    void Start()
    {
        t = 0;
        rb = GetComponent<Rigidbody>();
       
        data = new Data();
        for (int i = 0; i < data.input.Length; i++) data.input[i] = 0;
        for (int i = 0; i < data.output.Length; i++) data.output[i] = Random.Range(-1f, 1f);
        for (int i = 0; i < data.w.Length; i++) data.w[i] = Random.Range(-1f, 1f);
        


    }

    void Update()
    {
        float x = 0,y=0;
        if (t == 0)
        {
            if(Random.Range(0f,1f)<0.4f){
             x = Random.Range(-1f, 1f) * speed;
             y = Random.Range(-1f, 1f) * speed;
            }else{
            x=data.output[0] * speed;
            y=data.output[1] * speed;
            }

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
                        data.point-=1f;
                        break;
                    case "block":
                        data.input[i] = 1.0f;
                        data.point+=1f;
                        break;
                    case "item":
                        data.input[i] = 1.5f;
                        data.point+=2f;
                        break;
                    default:
                        data.input[i] = 2.0f;
                        break;
                }
            }

            Debug.DrawRay(ray.origin, direction, Color.red, .5f);
        }
        Network();

        //報酬
        if(n<=0){
        float goallen=Vector3.Distance(origin,goal.position);
        Debug.Log(goallen);
        if(goallen<=5f) data.point+=3f;
        else if(goallen<=10f) data.point+=2f;
        else if(goallen<=15f) data.point=1f;
        SceneManager.LoadScene("titel");
        }
        else n-=0.001f;
        Debug.Log(n);
    }
   
    void Network()
    {
        float x=0,z=0,num=0;

        for (int i=0; i<data.input.Length;i++) x += data.input[i] * data.w[i];  
        z = Sigmoid(x);
        for (int i = 0; i < data.output.Length; i++)
        {
            data.output[i] = z * data.w[i + data.input.Length];
            //Debug.Log(data.output[i]+" "+i);         
        }

    }

    float Sigmoid(float x)
    {
        float z = 1 / (1 + Mathf.Exp(-x));
        return z;
    }
}
