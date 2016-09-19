using UnityEngine;
using System.Collections;

public class circle : MonoBehaviour
{
    int[] X = { 1, 2, 3, 4, 5, 6, };    //番号の集合X　ここであえて視覚化しているがこれは使わない。プログラムでは0から始まる配列番号（自然数）があるからそちらを利用する
    char[] Y = { 'A', 'B', 'C', };      //料理の集合Y　A=シューマイ、B=棒棒鶏、C=酢豚

    void Start()
    {
        //なっとくする群環体P54のプログラムコードによる再現。配列番号に合わせて1引算しています
        //print( f(sigma(1-1)) );
        //print( f(sigma(2-1)) );
        //print( sigma(sigma(sigma(1-1))) );  //σ^3

        print( Sigma(1-1) );  //σ^3
    }

    //σ:X→X
    //番号の回転関数σ（X上の置換）P53参照
    private int Sigma(int x)
    {
        int[] shift = { 5, 0, 1, 2, 3, 4, };
        return shift[x];
    }

    private int Tau(int x)
    {
        int[] shift = { 5, 4, 3, 2, 1, 0};
        return shift[x];
    }


    //f:X→Y
    //番号の集合Xから料理の集合Yへの関数　（意味や型の変換とも言える。この場合、番号から料理へと意味が変わっている）P52参照
    private char f(int x)
    {
        int[] y = { 0, 1, 1, 0, 0, 2, };    //Yの集合から回転テーブルに並んだ料理を表すABBAACにしている
        return Y[y[x]];
    }
}
