using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class titel : MonoBehaviour
{
    public float speed = 10.0f;
    public Rigidbody rb;
    public GameObject ca,p;
    public Transform goal;
    public bool wb=true;
    int t,s=500,input,output,w;
    float n=60,pp=0;
    Data data;
    savedata save;
    GameObject x;

    void Start()
    {
        t = 0;
        rb = GetComponent<Rigidbody>();
       
        save=new savedata();

        

        if(!save.chack("savedata")){
            data = new Data();
            for (int i = 0; i < data.input.Length; i++) data.input[i] = 0;
            for (int i = 0; i < data.output.Length; i++) data.output[i] = Random.Range(-1f, 1f);
            for (int i = 0; i < data.w.Length; i++) data.w[i] = Random.Range(-1f, 1f);
            save.Jsave(data,"savedata");
        }else{
            data=save.Jload("savedata");
            if(wb){
                Debug.Log("wwwww");
                //重み変更
                for(int i=0;i<data.w.Length;i++){
                    data.w[i]+=Random.Range(-0.5f,0.5f);
                }
            }
            pp=data.point;
            data.point=0;
        }
        
        input=data.input.Length;
        output=data.output.Length;
        w=data.w.Length;
        
    }

    void Update()
    {
        //Debug.Log(data.point);
        float x = 0,y=0;
        if (t == 0)
        {
            if(Random.Range(0f,1f)<data.move){
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
            
            //Rayの衝突判定
            if (Physics.Raycast(ray, out hit, 10f))
            {
                
                //Debug.Log(hit.collider.gameObject.tag);
                switch (hit.collider.gameObject.tag)
                {
                    case "out":
                        data.input[i] = 0.5f;
                        data.point-=0.01f;
                        break;
                    case "block":
                        data.input[i] = 1.0f;
                        data.point+=0.01f;
                        break;
                    case "item":
                        data.input[i] = 1.5f;
                         data.point+=0.02f;
                        break;
                    default:
                        data.input[i] = 2.0f;
                        break;
                }
                data.input[i+4]=Vector3.Distance(origin,hit.transform.position);
                
                //Debug.Log(data.input[i+4]+" "+i);
            }

            Debug.DrawRay(ray.origin, direction, Color.red, .5f);
        }
        data.point-=0.0001f;
        data.input[data.input.Length-1]=Vector3.Distance(origin,goal.position);
        //Debug.Log(data.input[data.input.Length-1]);

        Network();

        //終了時
        if(n<=0){
        if(data.input[data.input.Length-1]<=5f) data.point+=50f;
        else if(data.input[data.input.Length-1]<=10f) data.point+=10f;
        else if(data.input[data.input.Length-1]<=15f) data.point=1f;

       finsh();
        }
        else n-=0.001f;
        //Debug.Log(n);
    }
   
    void Network()
    {
        int N=1,net;
        float x=0,z=0;
        float[] xx=new float[N],zz=new float[N];
        
        for (int i=0; i<data.input.Length;i++) x += data.input[i] * data.w[i];  
        z = Sigmoid(x);
        for(net=0;net<N;net++){
            xx[net]=0;
            xx[net]+=z*data.w[input+1];
            zz[net]=Sigmoid(xx[net]);
        }
        for (int i = 0; i < data.output.Length; i++)
        {
            data.output[i] = zz[net-1] * data.w[i + data.input.Length + N];
            //Debug.Log(data.output[i]+" "+i);         
        }

    }

    float Sigmoid(float x)
    {
        float z = 1 / (1 + Mathf.Exp(-x));
        return z;
    }

    void finsh (){
        Debug.Log(data.point+" <-point");
        data.move-=0.01f;
        if(pp<data.point || Random.Range(0f,1f)<=data.move) {
            data.move-=0.02f;
            save.Jsave(data,"savedata"); 
            Debug.Log("grow up");
            }
        SceneManager.LoadScene("titel");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "goal")
        {
            data.point+=100f;
           finsh();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "out")
        {
            data.point-=1f;
        }
        else if (collision.gameObject.tag == "block")
        {
            data.point+=1f;
        }
        else if (collision.gameObject.tag == "item")
        {
            data.point+=2f;

        }
    }
}
