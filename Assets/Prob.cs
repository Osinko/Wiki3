using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Prob : MonoBehaviour
{

    void Start()
    {
        //intの範囲的に9999で止めている。https://msdn.microsoft.com/ja-jp/library/exx3b86w.aspx?f=255&MSPPError=-2147217396
        //？？　unityは値のオーバーフローをすると実行が無警告で止まる　？？？　試しに99999を追加すると無警告ノーコンソールで画面が固まる
        //この場合のクラッシュの特徴としてunity上のインターフェイスの▷ボタン（再生ボタン）を押して処理を中断できなくなる。ちょっと憶えておくとデバックの時困らないかも
        //int[] h = { 4, 9, 16, 25, 36, 49, 64, 81, 99, 999, 9999, };
        int[] h = { 9, };

        foreach (int n in h)
        {
            int count = 0;
            int D_count = 0;

            for (int b = -n; b < n + 1; b++)
            {
                for (int c = -n; c < n + 1; c++)
                {
                    count++;
                    if ((b * b - 4 * c) < 0) D_count++;
                }
            }
            print(string.Format("方程式の総数={0}  虚根の場合={1}  虚根の確率={2}", count, D_count, (float)D_count / (float)count));
        }
    }
}
