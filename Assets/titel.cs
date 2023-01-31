using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titel : MonoBehaviour
{
    public float speed = 10.0f;
    public Rigidbody rb;
    public GameObject ca,p;
    public Transform target;
    int t,s=500;
    LineRenderer linerend;
    


    void Start()
    {
        t = 0;
        rb = GetComponent<Rigidbody>();

     
    
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

      Vector3 origin = target.position; // 原点
      Vector3 direction = new Vector3(1, 0, 0); // X軸方向を表すベクトル
      Ray ray = new Ray(origin:origin, direction:direction); // Rayを生成
     
      RaycastHit hit;
      if (Physics.Raycast(ray,out hit,10.0f))
        {
            Debug.Log(hit.collider.gameObject.tag);
        }

      //Lineを描画する関数
      DrawRayLine(ray.origin, ray.direction *10);

    }
    
     private void DrawRayLine(Vector3 start, Vector3 direction)
    {
      //LineRendererコンポーネントの取得
      linerend = this.GetComponent<LineRenderer>();

      //線の太さを設定
      linerend.startWidth = 0.04f;
      linerend.endWidth = 0.04f;

      //始点, 終点を設定し, 描画
      linerend.SetPosition(0, start);
      linerend.SetPosition(1, start + direction);

      
    }

}
