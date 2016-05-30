using UnityEngine;
using System.Collections;

public class prob2 : MonoBehaviour
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
            float x = Random.Range(0, 6f);  //floatの場合6fギリギリに近い小数点を含む数字、例えば5.98f等の数がランダムで出力されるので１加算しなくていい
            float y = Random.Range(0, 4f);

            if (((2f * x) - (3f * y)) < 0) count++;   //判別式がマイナスの場合、成功と見なしてカウントアップ
        }
        return (float)count / (float)sampling;    //確率を求める
    }
}
