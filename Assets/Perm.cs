using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Perm : MonoBehaviour
{
    void Start()
    {
        int length = 3;
        char[] set = { 'A', 'B', 'C', 'D','E' };

        //List<string> strList = PRoot(set, length);
        List<string> strList = CRoot(set, length);

        //全順列を網羅してファイル出力
        string folder = Application.dataPath;                   //これだけでunityの実行ファイルがあるフォルダがわかる
        SaveText(folder, @"\順列.txt", strList.ToArray());

        //文字列の中に"HHTT"がある行を抜き出す
        var queary = strList.AsQueryable().Where(item => item.Contains("HHTT"));
        var queList = queary.ToList();
        queList.Add("検索前総数" + strList.Count().ToString());
        queList.Add("カウント数" + (queList.Count-1).ToString());
        SaveText(folder, @"\HHTT抜き出し.txt", queList.ToArray());
    }

    //基底部
    List<string> CRoot(char[] set, int length)
    {
        List<string> strList = new List<string>();
        int level = 0;

        for (int i = 0; i < set.Length; i++)
        {
            char[] dat = new char[length];
            dat[0] = set[i];
            PLoop(dat, level, set, strList);
        }

        return strList;
    }










    //基底部
    List<string> PRoot(char[] set, int length)
    {
        List<string> strList = new List<string>();
        int level = 0;

        for (int i = 0; i < set.Length; i++)
        {
            char[] dat = new char[length];
            dat[0] = set[i];
            PLoop(dat, level, set, strList);
        }

        return strList;
    }

    //帰納部
    void PLoop(char[] dat, int level, char[] set, List<string> strList)
    {
        level++;
        if (level < dat.Length)     //帰納関数の終了条件
        {
            for (int i = 0; i < set.Length; i++)
            {
                char[] cloneDat = (char[])dat.Clone();  //オブジェクトを複製する
                cloneDat[level] = set[i];
                PLoop(cloneDat, level, set, strList);    //帰納関数には複製された参照値が渡される為、呼び出し元側の値が書き換えられることは無い
            }
        }
        else {
            strList.Add(new string(dat));
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
}
