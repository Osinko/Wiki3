using UnityEngine;
using System.Collections;

public class SC2 : MonoBehaviour
{

    void Start()
    {
        Sampling(6, 100000);
    }

    private void Sampling(int dice, int sampling)
    {
        int length = 10;
        int total = 0;

        for (int j = 0; j < sampling; j++)
        {
            int forward = 0;
            for (int i = 0; i < length; i++)
            {
                forward += TossDice(dice);
            }
            total += forward;
        }
        print((float)total / (float)sampling);
    }

    public int TossDice(int dice)
    {
        return Random.Range(1, dice + 1);
    }

}
