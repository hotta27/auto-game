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
    int t,s=500,input,output,w,wi=0;
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
                for(int i=0;i<data.w.Length;i++)
                    data.w[i]+=Random.Range(-1f,1f);
                
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
        float x=0,y=0;
        if (t == 0)
        {            
            x = data.output[0] * speed;
            y = data.output[1] * speed;
            Debug.Log(x+","+y);
             rb.AddForce(x, y, 0);
            t = s;
        }t--;
       
        ca.transform.position = new Vector3(p.transform.position.x, p.transform.position.y + 1, -10);
        data.input[4]=p.transform.position.x;
        data.input[5]=p.transform.position.y;

        origin = this.transform.position; // 原点

        data.input[data.input.Length-1]=Vector3.Distance(origin,goal.position);
        data.input[input-2]=Random.Range(-1f,1f);
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
        float[] z=new float[8],num=new float[10];
        int [] M={6,4,2};
        int N=M.Length;
       
        for(int i=0;i<8;i++)
            z[i]=inout(data.input);

    for(int j=0;j<N;j++){
        for(int i=0;i<M[j];i++)
            z[i]=inout(z);
        System.Array.Resize(ref z, M[j]);
        
    }


        wi=0;
        for(int i=0;i<output;i++)
            data.output[i]=z[i]*data.w[output-i];
    }

    float inout(float[] x){
        float z=0f,a;
        for(int i=0;i<x.Length;i++){
        z+=x[i]*data.w[wi+i];
        }
        if(z>data.w[wi+x.Length-1]) a = 1f;
        else a = 0f;
        wi+=x.Length+1;
       
        return a;

    }

    float Sigmoid(float[] x,int wi){
        float z=0;
        for(int i=0;i<x.Length;i++){
        z+=x[i]*data.w[wi+i];
        }
        
        return z = 1 / (1 + Mathf.Exp(-z));
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
