using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

public class Pow10 : MonoBehaviour {

	void Start () {
        int length = 20;
        float[] bottomList = { 10f, 200f, 1000f, 5000f, 10000f };

        float[] powNumber = GeneratorNumPow(bottomList, length).ToArray<float>();
        string[] str = CSV2Wiki( PowNumToCSV(powNumber, length));

        string folder = Application.dataPath;    //これだけでunityの実行ファイルがあるフォルダがわかる
        ClassSaveText(folder, @"\PowList.txt", str);
    }

    //数列を作成
    private IEnumerable<float> GeneratorNumPow(float[] bottomList,int length)
    {
        for (int j = 0; j < bottomList.Length; j++)
        {
            for (float i = 0; i < length; i++)
            {
                yield return Mathf.Pow(bottomList[j], Mathf.Pow(1f / 2f, i));  //指数計算部
            }
        }
    }

    //CSV形式に
    private string[] PowNumToCSV(float[] powNumber,int length)
    {
        int strLength = powNumber.Length / length;
        string[] str = new string[length];

        for (int j = 0; j < length; j++)
        {
            string temp=powNumber[j].ToString();
            for (int i = 1; i < strLength; i++)
            {
                temp += string.Format(",{0}",powNumber[i * length + j]);
            }
            str[j] = temp;
        }
        return str;
    }

    //csv形式をPukiWiki表形式にする
    private string[] CSV2Wiki(IEnumerable<string> data)
    {
        List<string> strList = new List<string>();

        //wiki用置換
        foreach (var item in data)
        {
            strList.Add("|" + item.ToString().Replace(',', '|') + "|");
        }

        return strList.ToArray();
    }

    //資料：StreamWriter クラス (System.IO)
    //http://msdn.microsoft.com/ja-jp/library/system.io.streamwriter(v=vs.110).aspx

    //テキストファイルとしてセーブ
    public void SaveText(string fileFolder, string filename, string[] dataStr)
    {
        using (StreamWriter w = new StreamWriter(fileFolder + filename, false, System.Text.Encoding.GetEncoding("shift_jis")))
        {
            foreach (var item in dataStr)
            {
                w.WriteLine(item);
            }
        }
    }

    //Classセーバー
    private void ClassSaveText(string folder, string name, IEnumerable data)
    {
        List<string> strList = new List<string>();
        foreach (var item in data)
        {
            strList.Add(item.ToString());
        }
        SaveText(folder, name, strList.ToArray());
    }
}
