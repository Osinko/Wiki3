using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class niji_prob : MonoBehaviour
{

    void Start()
    {
        int sampling = 10000;
        int tossCount = 16;
        float headPercentage = 0.5f;
        string hitString = "HHTT";

        char[] coinList = new char[tossCount];

        for (int j = 0; j < 5; j++)
        {
            int successCount = 0;

            for (int i = 0; i < sampling; i++)
            {
                coinList = GetCoinTossTest(coinList, headPercentage);
                if (new string(coinList).Contains(hitString)) successCount++;      //文字列検索してマッチすればtrueを返す
            }
            print((double)successCount / (double)sampling);
        }
    }

    //動作確認用メソッド
    private void SimpleMethod(float headPercentage, string hitString, char[] coinList)
    {
        coinList = GetCoinTossTest(coinList, headPercentage);
        print(new string(coinList));
        print(new string(coinList).Contains(hitString));
    }

    //コイントス試行
    public char[] GetCoinTossTest(char[] coinList, float headPercentage)
    {
        for (int i = 0; i < coinList.Length; i++)
        {
            coinList[i] = TossCoin(headPercentage);
        }
        return coinList;
    }

    //指定した表の確率でコイントスして表が出たら'H'(表:Head)、裏で'T'(裏:tail)を返す
    public char TossCoin(float headPercent)
    {
        if (Random.value < headPercent) return 'H';
        return 'T';
    }

}
