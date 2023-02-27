using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; // UnityJson���g���ꍇ�ɕK�v
using System.IO; // �t�@�C���������݂ɕK�v


    [Serializable]
public class Data
{
    public int point,good=0,bad=0,sum=0,logpoint=100,slotcount=10,npo=0;
    public DateTime bdate,ndate;
    public string date;
    public bool log = true;
    public string []varname = { "point","sum","logpoint","slotcount" };
}

public class savedata : MonoBehaviour
{
    public void Jsave(Data dataw,string name) { 
        var Json = JsonUtility.ToJson(dataw,true);
        string path = Application.dataPath + "/" + name + ".txt";
        StreamWriter sw = File.CreateText(path);
        sw.Write(Json);
        sw.Close();
    }

    public Data Jload(string name)
    {
        // Assets�t�H���_���烍�[�h
        string path = Application.dataPath + "/" + name + ".txt";
        var json = File.ReadAllText(path);
        Data data = JsonUtility.FromJson<Data>(json);
        return data;
    }

    public bool chack(string name)
    {
        string path = Application.dataPath + "/" + name + ".txt";
        return File.Exists(path);
    }
    /*�g����
    Data m,s;
    void Start()
    {
        m.x =10;
        m.num +=1.2;
        Jsave(m,"test");
        s=Jload("test");
        Debug.Log(s.x + "," + s.num);
    }*/
    /*���ۑ��֐�
    //point
    public static string read() {
        // �ǂݍ���
        string path = Application.dataPath + "/" + "save_point.txt";
        string data = File.ReadAllText(path);
        
        return data;
    }
    static int p;

    public void save(int point) {
        p = point;
        CreateTextFile("save_point.txt", p.ToString());

    }


    //all save
    public void save_num(string name, int x) {
        name += ".txt";
        CreateTextFile(name, x.ToString());
    }

    public string read_num(string name) {
        string path = Application.dataPath + "/" + name +".txt";
        string data = File.ReadAllText(path);

        return data;
    }

    void CreateTextFile(string name, string content)
    {
    string path = Application.dataPath + "/" + name;
    StreamWriter sw = File.CreateText(path);
    sw.Write(content);
    sw.Close();
    }
    */

}