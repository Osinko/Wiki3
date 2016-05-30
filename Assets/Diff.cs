using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

public class Diff : MonoBehaviour
{

    void Start()
    {
        int length = 10;    //分割数
        double a1 = 0;      //初項
        double h = 1d;    //変化

        Diffs[] dif = new Diffs[length];

        for (int i = 0; i < length; i++)
        {
            dif[i].x = a1 + (double)i * h;      //xの値
            dif[i].fb = f(dif[i].x + h);        //f(x+h)
            dif[i].fa = f(dif[i].x);            //f(x)
            dif[i].function_diff = dif[i].fb - dif[i].fa;   //区間差を求める
        }

        //区間速度を求める
        for (int j = 1; j < length; j++)
        {
            dif[j].floor_diff = (dif[j].function_diff - dif[j - 1].function_diff);
            dif[j].speed = (dif[j].floor_diff/h)/h;
        }

        //foreach (var item in dif)
        //{
        //    print(item.FormatString());
        //}

        string folder = Application.dataPath;       //これだけでunityの実行ファイルがあるフォルダがわかる
        DiffSaveCsv(folder, @"\test5.csv", dif);
        DiffWikiSaveText(folder, @"\test5.txt", dif);
    }

    double f(double x)
    {
        return Math.Pow(x, 2);
    }

    public struct Diffs
    {
        public double x;                //x
        public double fb;               //f(x+h)
        public double fa;               //f(x)
        public double function_diff;    //関数差
        public double floor_diff;       //階差
        public double speed;            //階差速度

        //オーバーライド
        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5}", x, fb, fa, function_diff,floor_diff, speed);
        }

        //csv用ヘッダー
        public static string HeaderString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5}", "x", "f(x+h)", "f(x)", "f(x+h)-f(x)", "階差","階差/h^2=速度");
        }

        //プレビュー用表示
        public string FormatString()
        {
            return string.Format("x={0,-9},f(x+h)={1,-9},f(x)={2,-9},f(x+h)-f(x)={3,-9},FloorDiff={4,-9},Speed={5,-9}", x, fb, fa, function_diff,floor_diff, speed);
        }

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

    //classローダー
    public string[] LoadText(string fileFolder, string filename)
    {
        List<string> strList = new List<string>();
        string line = "";
        using (StreamReader sr = new StreamReader(fileFolder + filename, System.Text.Encoding.GetEncoding("shift_jis")))
        {
            while ((line = sr.ReadLine()) != null)
            {
                strList.Add(line);
            }
        }
        return strList.ToArray();
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

    //Diffs形式用CSVセーバー
    private void DiffSaveCsv(string folder, string name, Diffs[] data)
    {
        List<string> strList = new List<string>();

        strList.Add(Diffs.HeaderString());

        foreach (var item in data)
        {
            strList.Add(item.ToString());
        }
        SaveText(folder, name, strList.ToArray());
    }

    //Diffs形式用PukiWiki表形式セーバー
    private void DiffWikiSaveText(string folder, string name, Diffs[] data)
    {
        List<string> strList = new List<string>();

        //wiki用置換
        strList.Add("|" + Diffs.HeaderString().Replace(',', '|') + "|");

        foreach (var item in data)
        {
            strList.Add("|" + item.ToString().Replace(',', '|') + "|");
        }

        SaveText(folder, name, strList.ToArray());
    }

}
