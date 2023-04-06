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
    public string dataname="savedata";
    int t,s=500,input,output,w;
    float n=30,pp=0;
    Data data;
    savedata save;
    GameObject xnot;
    Vector3 origin;

    

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
            data=save.Jload(dataname);
            if(wb){
                //Debug.Log("wwwww");
                //重み変更
                for(int i=0;i<data.w.Length;i++){
                    float a=(100f-data.goalcount)*0.01f;
                    if(a<0)a=1;
                    data.w[i]+=Random.Range(-0.5f*a,0.5f*a);
                }
            }
            pp=data.point;
            data.point=0;
        }
        
        input=data.input.Length;
        output=data.output.Length;
        w=data.w.Length;

            float x = Random.Range(-1f, 1f) *speed;
            float y = Random.Range(-1f, 1f) *speed;
            rb.AddForce(x, y, 0);
    }

    void Update()
    {
        //Debug.Log(data.point);
        float x = 0,y=0;
        if (t == 0)
        {
            
            x = data.output[0] * speed;
            y = data.output[1] * speed;
        
        
            t = s;
        }t--;
        rb.AddForce(x, y, 0);
        ca.transform.position = new Vector3(p.transform.position.x, p.transform.position.y + 1, -10);


        origin = this.transform.position; // 原点

        //  int vx=10, vy=0;
        //  for (int i = 0; i < 4; i++)
        //  {

        //      switch (i)
        //      {
        //          case 1:
        //              vx = -10;
        //              break;
        //          case 2:
        //              vx = 0;
        //              vy = 10;
        //              break;
        //          case 3:
        //              vy = -10;
        //              break;
        //      }
        //      Vector3 direction = new Vector3(vx, vy, 0); // X軸方向を表すベクトル
        //      Ray ray = new Ray(origin: origin, direction: direction); // Rayを生成
        //      RaycastHit hit;

        //      //Rayの衝突判定
        //      if (Physics.Raycast(ray, out hit, 10f))
        //      {

        //          //Debug.Log(hit.collider.gameObject.tag);


        //          //Debug.Log(data.input[i+4]+" "+i);
        //      }

        //      Debug.DrawRay(ray.origin, direction, Color.red, .5f);
        //  }
        //data.point-=0.0001f;
        data.input[data.input.Length-1]=Vector3.Distance(origin,goal.position);
        //Debug.Log(data.input[data.input.Length-1]);

        Network();

        //終了時
        if(n<=0){
        float a=100-Vector3.Distance(origin,goal.position);
        if(a<0)a=0;
        data.point+=a;

        finsh();
        }
        else n-=0.001f;
        //Debug.Log(n);
    }
   
    void Network()
    {
        // int N=1,M=1,j,k=0;
        // float x=0,z=0;
        // float[,] xx=new float[N,M],zz=new float[N,M];
        
        // for (int i=0; i<data.input.Length;i++) x += data.input[i] * data.w[i];  
        // z = Sigmoid(x);
        // for (j = 0; j < N; j++)
        // {
        //     for (k = 0; k < M; k++)
        //     {
        //         xx[j,k] = 0;
        //         xx[j,k] += z * data.w[input + 1];
        //         zz[j,k] = Sigmoid(xx[j,k]);
        //     }
        // }
        // for (int i = 0; i < data.output.Length; i++)
        // {
        //     data.output[i] = zz[j-1,k-1] * data.w[i + data.input.Length + N];
        //     //Debug.Log(data.output[i]+" "+i);         
        // }
        int[] M={1,2,3,4,5,2};
        int N=M.Length,wc=-1;
        float x=0,z=0;
        float[] xx=new float[input],net=new float[input];
         
         //Debug.Log("----------------");
                        
        xx=data.input;                  
        for(int i=0;i<N;i++){
            for(int j=0;j<M[i];j++){                
                    net=xx;
                    System.Array.Resize(ref net, xx.Length);
                    System.Array.Resize(ref xx,M[i]);
                
                    // foreach(float name in net)
                    // {
                    //     Debug.Log(name+" <"+i+" ,"+j+">");
                    // }
                    int k; 
                for(k=0;k<net.Length;k++){
                    wc+=1;
                    xx[j]+=net[k]*data.w[wc];
                    // Debug.Log(i+","+j+","+k+":"+net.Length);
                }
                
                xx[j]=Sigmoid(xx[j]);
                if(i==N-1) data.output[j]=xx[j];
            }




        }
        

    }

    float Sigmoid(float x)
    {
        float z = 1 / (1 + Mathf.Exp(-x));
        return z;
    }

    void finsh (){
        Debug.Log(data.point+" <-point");
        if((pp<=data.point || Random.Range(0f,1f)<=data.move)&&wb) {
            data.move-=0.01f;
            save.Jsave(data,"savedata"); 
            Debug.Log("grow up");
            }
        SceneManager.LoadScene("titel");
    }

    void OnTriggerEnter(Collider other)
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "out")
        {
            data.point-=1f;
        }
        else if (collision.gameObject.tag == "block")
        {
            data.point+=2f;
        }
        else if (collision.gameObject.tag == "item")
        {
            data.point+=4f;

        }else if (collision.gameObject.tag == "goal")
        {
            Debug.Log("goal");
            data.point+=200f;
            data.goalcount += 1;
            save.Jsave(data,"goal"+data.goalcount);
            finsh();
        }
    }
    void OnTriggerStay(Collider other)
    {
        int i = 0;
        switch (other.gameObject.tag)
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
            case "goal":
                data.input[i] = 2.5f;
                break;
            default:
                data.input[i] = 2.0f;
                break;
        }
        data.input[1] = other.transform.position.x;
        data.input[2] = other.transform.position.y;
        //Debug.Log(data.input[1] + " "+"\n"+ data.input[2]+other.gameObject.tag);
        data.input[3] = Vector3.Distance(origin, other.transform.position);
    }
}
