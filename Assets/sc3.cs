using UnityEngine;
using System.Collections;

public class sc3 : MonoBehaviour {
	
	void Start () {
        int sampling = 100000;
        int total=0;
        float result = 0;
        for (int i = 0; i < sampling; i++)
        {
            total+= TrialAttack(0.85f);
        }
        result = (float)total / (float)sampling;    //実験から得た期待値の算出
        print(result);
    }

    //攻撃試行。無限回数攻撃。攻撃失敗の時点で、それまでの成功回数を返す
    int TrialAttack(float successs)
    {
        int i = 0;
        while (true)
        {
            i++;
            if (Attack(successs) == false) return i;
        }
    }

    //攻撃判定。成功確率以内なら真を返す
    public bool Attack(float successs)
    {
        if (Random.value < successs) return true;
        return false;
    }
}
