using UnityEngine;
using System.Collections;

public class Circle2 : MonoBehaviour
{

    void Start()
    {
        char[] X = { '1', '2', '3', '4', '5', '6' };
        char[] Y = Sigma(X, 2);
        print(new string(Y));
    }

    public char[] Sigma(char[] x, int pow = 0)
    {
        int[] shift0 = { 0, 1, 2, 3, 4, 5, };
        int[] shift1 = { 5, 0, 1, 2, 3, 4, };
        int[] shift2 = { 4, 5, 0, 1, 2, 3, };
        int[] shift3 = { 3, 4, 5, 0, 1, 2, };
        int[] shift4 = { 2, 3, 4, 5, 0, 1, };
        int[] shift5 = { 1, 2, 3, 4, 5, 0, };

        char[] temp = new char[6];
        for (int i = 0; i < 6; i++)
        {
            temp[i] = x[shift0[i]];
        }
        return temp;
    }

    public char[] Tau(char[] x)
    {
        int[] shift = { 5, 4, 3, 2, 1, 0 };
        char[] temp = new char[6];

        for (int i = 0; i < 6; i++)
        {
            temp[i] = x[shift[i]];
        }
        return temp;
    }

}
