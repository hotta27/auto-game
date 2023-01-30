using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nn : MonoBehaviour
{
    int[,] data=new int[4,20];
    int[] set = { 8, 16, 32, 64 },max= {0,0,0,0};
    int x;
    bool sw=false;
    public Text t,xt;
    public InputField ff;
    public GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        
        for(int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                if (j == 0) data[i,j] = set[Random.Range(0, 4)];
                else data[i,j] = 0;
            }
        }
        t.text = "";
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                if (data[i, j] != 0)
                { t.text += data[i, j].ToString() + " "; }
                if (max[i] <= 11) sw = true;
            }
            t.text += "\n";
        }
        
        x = Random.Range(0, 4);
        xt.text = data[x,max[x]].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
        
            
        
    }

   public void ak()
    {

        int ix = int.Parse(ff.text);
        if (data[ix, max[ix]] == set[x])
        {
            data[ix, max[ix]] *= 2;
        }
        else
        {
            data[ix, max[ix] + 1] = set[x];
            max[ix] += 1;
        }
        if (max[ix] > 0) 
          if (data[ix, max[ix]] == data[ix, max[ix] - 1]){
            data[ix, max[ix]-1] *= 2;
            data[ix, max[ix]] = 0;
                max[ix] -= 1;
          }
        print(max[ix]);
        if (max[ix] >= 10) go.SetActive(true);

        t.text = "";
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j <11; j++)
            {
                if (data[i, j] != 0)
                { t.text += data[i, j].ToString() + " "; max[i] = j; }
            }
            t.text += "\n";
        }
      
        x = Random.Range(0, 4);
        xt.text = set[x].ToString();
    }
}
