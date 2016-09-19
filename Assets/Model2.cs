using UnityEngine;
using System.Collections;

public class Model2 : MonoBehaviour
{

    void Start()
    {
        //置換関数化
        Rotate f = new Rotate("ABCABC");                                //f:X→Y
        Rotate iota = new Rotate(new int[] { 0, 1, 2, 3, 4, 5 });       //ι：恒等置換
        Rotate sigma = new Rotate(new int[] { 5, 0, 1, 2, 3, 4 });      //σ：回転
        Rotate sigma2 = new Rotate(new int[] { 4, 5, 0, 1, 2, 3 });     //σ^2：回転
        Rotate sigma3 = new Rotate(new int[] { 3, 4, 5, 0, 1, 2 });
        Rotate sigma4 = new Rotate(new int[] { 2, 3, 4, 5, 0, 1 });
        Rotate sigma5 = new Rotate(new int[] { 1, 2, 3, 4, 5, 0 });
        Rotate tau = new Rotate(new int[] { 5, 4, 3, 2, 1, 0 });        //τ：左右反転

        print(sigma * tau);
        print(tau * sigma5);
        //以下のように計算機として使う
        //print(sigma * sigma * sigma);
        //print(sigma * sigma * sigma * tau);
        //print(tau * sigma * sigma);
        //print(sigma * sigma * sigma * sigma * tau);
        //print(f * tau * sigma2 == f * sigma4 * tau);
    }

    //回転をモデル化した置換群R
    class Rotate
    {
        public string X;
        public int[] shift;

        public Rotate(int[] shift)
        {
            this.X = "012345";
            this.shift = shift;
        }

        public Rotate(string X)
        {
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
            Rotate iota = new Rotate(new int[] { 0, 1, 2, 3, 4, 5 });
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
}
