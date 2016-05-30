using UnityEngine;
using System.Collections;

public class Niji_rand : MonoBehaviour
{
    void Start()
    {
        int sampling = 1000000;
        for (int i = 0; i < 5; i++)
        {
            print(Test(sampling));
        }
    }

    private float Test(int sampling)
    {
        int count = 0;

        for (int i = 0; i < sampling; i++)
        {
            int b = RandomNum();
            int c = RandomNum();

            if (D(b, c)) count++;   //判別式がマイナスの場合虚根となるのでカウントアップ
        }
        return (float)count / (float)sampling;    //確率を求める
    }

    //判別式でマイナスの値で真を返す
    private bool D(int b, int c)
    {
        return (b * b - 4 * c < 0) ? true : false;
    }

    //-9～9の乱数を返す
    private int RandomNum()
    {
        return Random.Range(-9, 9 + 1);
    }

}

