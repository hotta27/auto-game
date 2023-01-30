using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class script : MonoBehaviour
{

    public float speed = 10.0f;
    public Rigidbody rb;
    public GameObject ca,p,over,goal,re,teach,ke;
    public int hp = 10;
    public Text h,pp,k;
    bool g = true,f=true;
    int point = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
        rb = GetComponent<Rigidbody>();
        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;
        
        ca.transform.position = new Vector3(p.transform.position.x, p.transform.position.y+1,-10);

        if (hp <= 0)
        {
            if (f == true)
            {
                ke.SetActive(true);
                over.SetActive(true);
                re.SetActive(true);
                int kk = hp + point;
                k.text = "����\nHP:" + hp.ToString() + "\npoint:" + point.ToString() + "\n------------\n����:" + kk.ToString();
                f = false;
            }
        }
        else
        {
            rb.AddForce(x, z, 0);
            if (Input.GetKey(KeyCode.Space))            
                    rb.velocity = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.Return))
            teach.SetActive(false);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "out")
        {
            hp--;
            h.text = "HP:" + hp.ToString();
        }
        else if (collision.gameObject.tag == "block")
        {
            point += 1;
            pp.text = "point:" + point.ToString();
        }
        else if (collision.gameObject.tag == "item")
        {
            point += 10;
            pp.text = "point:" + point.ToString();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "goal")
        {
            point += 100;
            goal.SetActive(true);
            re.SetActive(true);
            ke.SetActive(true);
            pp.text = "point:" + point.ToString();
            int kk = hp + point;
            k.text = "����\nHP:" + hp.ToString() + "\npoint:" + point.ToString() + "\n------------\n����:" + kk.ToString();
        }
    }

    public void restart()
    {
        SceneManager.LoadScene("titel");
    }
    //void OnMouseDrag()
    //{
    //    //Cube�̈ʒu�����[���h���W����X�N���[�����W�ɕϊ����āAobjectPoint�Ɋi�[
    //    Vector3 objectPoint
    //        = Camera.main.WorldToScreenPoint(transform.position);

    //    //Cube�̌��݈ʒu(�}�E�X�ʒu)���ApointScreen�Ɋi�[
    //    Vector3 pointScreen
    //        = new Vector3(Input.mousePosition.x,
    //                      Input.mousePosition.y,
    //                      objectPoint.z);

    //    //Cube�̌��݈ʒu���A�X�N���[�����W���烏�[���h���W�ɕϊ����āApointWorld�Ɋi�[
    //    Vector3 pointWorld = Camera.main.ScreenToWorldPoint(pointScreen);
    //    pointWorld.z = transform.position.z;

    //    //Cube�̈ʒu���ApointWorld�ɂ���
    //    transform.position = pointWorld;
    //}
}
