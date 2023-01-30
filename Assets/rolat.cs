using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rolat : MonoBehaviour
{
    public int i=1;
    bool t=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       switch (i) {
            case 1:
                //if(t==true)
                GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 1);
                //else
                //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, -1);
                break;

            case 2:
                //if (t == true)
                GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, -1);
                //else
                //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 1);
                break;
            case 3:
                if(t==true)
                GetComponent<Rigidbody>().AddForce(0, 3, 0);
                else
                GetComponent<Rigidbody>().AddForce(0, -3, 0);
            break;
            case 4:
                if (t == true)
                    GetComponent<Rigidbody>().AddForce(3, 0, 0);
                else
                    GetComponent<Rigidbody>().AddForce(-3, 0, 0);
            break;
            case 5:
                GetComponent<Rigidbody>().angularVelocity = new Vector3(0, -1, 0);
            break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (t == true)
            t = false;
        else
            t = true;
    }
}
