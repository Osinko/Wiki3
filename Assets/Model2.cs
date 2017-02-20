using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Model2 : MonoBehaviour
{
    void Start()
    {
        List<string> strList = Permutation(6, new char[] { 'A', 'B' });
        RotateGropup rg = new RotateGropup();

        List<string> rotList = new List<string>();
        for (int i = 0; i < strList.Count; i++)
        {
            rotList.Add(string.Format("{0} : {1}", strList[i],rg.Where(new Rotate(strList[i]))));
        }

        string folder = Application.dataPath;                   //これだけでunityの実行ファイルがあるフォルダがわかる
        SaveText(folder, @"\置換サーチ結果.txt", rotList.ToArray());
    }

    //順列作成
    public List<string> Permutation(int x, char[] y)
    {

        int[] Z = new int[x+1];
        for (int i = 0; i < Z.Length; i++)
        {
            Z[i] = (int)Mathf.Pow(y.Length, i);     //指数を利用して作成した数列
        }
        int total = Z[Z.Length - 1];


        List<char[]> charList = new List<char[]>();
        for (int i = 0; i < total; i++)
        {
            charList.Add(new char[x]);
        }

        for (int i = 0; i < x; i++)
        {
            int mod = 0;
            char set = y[mod];
            for (int j = 0; j < total; j++)
            {
                //指数の数列と回転置換群との関係をここで利用してる
                if (j % Z[i] == 0)
                {
                    set = y[mod % (y.Length)];
                    mod++;

                }
                charList[j][i] = set;        //二次元配列的なListの使用方法
            }
        }

        List<string> strList = charList.ConvertAll<string>(z => new string(z));     //こういう変換方法もある
        return strList;
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

class RotateGropup
{
    public List<Rotate> rx = new List<Rotate>();

    public RotateGropup()
    {
        rx.Add(new Rotate("ι", new int[] { 0, 1, 2, 3, 4, 5 }));
        rx.Add(new Rotate("σ", new int[] { 5, 0, 1, 2, 3, 4 }));
        rx.Add(new Rotate("σ2", new int[] { 4, 5, 0, 1, 2, 3 }));
        rx.Add(new Rotate("σ3", new int[] { 3, 4, 5, 0, 1, 2 }));
        rx.Add(new Rotate("σ4", new int[] { 2, 3, 4, 5, 0, 1 }));
        rx.Add(new Rotate("σ5", new int[] { 1, 2, 3, 4, 5, 0 }));
        rx.Add(new Rotate("τ", new int[] { 5, 4, 3, 2, 1, 0 }));
        rx.Add(new Rotate("τσ", new int[] { 0, 5, 4, 3, 2, 1 }));
        rx.Add(new Rotate("τσ2", new int[] { 1, 0, 5, 4, 3, 2 }));
        rx.Add(new Rotate("τσ3", new int[] { 2, 1, 0, 5, 4, 3 }));
        rx.Add(new Rotate("τσ4", new int[] { 3, 2, 1, 0, 5, 4 }));
        rx.Add(new Rotate("τσ5", new int[] { 4, 3, 2, 1, 0, 5 }));
    }

    public string Where(Rotate f)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        for (int i = 0; i < rx.Count; i++)
        {
            if (f * rx[0] == f * rx[i]) sb.Append(rx[i].name + " , ");
        }
        return sb.ToString();
    }
}

//回転をモデル化した置換群R
class Rotate
{
    public string name;
    public string X;
    public int[] shift;

    public Rotate(string name, int[] shift)
    {
        this.name = name;
        this.X = "012345";
        this.shift = shift;
    }

    public Rotate(string X)
    {
        this.name = "func";
        this.X = X;
        this.shift = new int[] { 0, 1, 2, 3, 4, 5 };
    }

    //演算子「*」のオーバーロード
    public static Rotate operator *(Rotate left, Rotate right)
    {
        char[] temp = new char[left.X.Length];
        char[] temp2 = new char[left.X.Length];
        for (int i = 0; i < left.shift.Length; i++)
        {
            temp[i] = left.X[left.shift[i]];
        }
        for (int i = 0; i < right.shift.Length; i++)
        {
            temp2[i] = temp[right.shift[i]];
        }

        //あたらしいRotate型を作って返す
        //演算後はιで文字列の配置が変わった物を渡している。これが連続する二項演算の左辺になる
        Rotate iota = new Rotate("iota", new int[] { 0, 1, 2, 3, 4, 5 });
        iota.X = new string(temp2);
        return iota;
    }

    //演算子「==」のオーバーロード
    public static bool operator ==(Rotate left, Rotate right)
    {
        //オブジェクトとして同一かどうか
        if (object.ReferenceEquals(left, right))
        {
            return true;
        }

        //オブジェクトとしてnullかどうか
        if (((object)left == null) || ((object)right == null))
        {
            return false;
        }

        return (left.X == right.X);
    }

    //演算子「==」と共に必ず設定する必要がある
    public static bool operator !=(Rotate left, Rotate right)
    {
        return !(left == right); //これはoperatorの処理で判定している
    }

    //文字表示
    public override string ToString()
    {
        return X;
    }
}
