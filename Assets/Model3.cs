using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Model3 : MonoBehaviour
{

    void Start()
    {
        int[] X = { 1, 2, 3,4,5,6 };
        char[] Y = { 'A', 'B' };    //Y要素を自由に変えても正確に順列が生成できます

        int[] Z = new int[X.Length + 1];
        for (int i = 0; i < Z.Length; i++)
        {
            Z[i] = (int)Mathf.Pow(Y.Length, i);     //指数を利用して作成した数列
        }
        int total = Z[Z.Length - 1];


        List<char[]> charList = new List<char[]>();
        for (int i = 0; i < total; i++)
        {
            charList.Add(new char[X.Length]);
        }

        for (int i = 0; i < X.Length; i++)
        {
            int mod = 0;
            char set = Y[mod];
            for (int j = 0; j < total; j++)
            {
                //指数の数列と合同との関係をここで利用してる
                if (j % Z[i] == 0)
                {
                    set = Y[mod % (Y.Length)];
                    mod++;

                }
                charList[j][i] = set;        //二次元配列的なListの使用方法
            }
        }
        List<string> strList = charList.ConvertAll<string>(x => new string(x));

        foreach (var item in strList)
        {
            print(item);
        }

        string folder = Application.dataPath;                   //これだけでunityの実行ファイルがあるフォルダがわかる
        SaveText(folder, @"\W（μ）.txt", strList.ToArray());
    }

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

}
