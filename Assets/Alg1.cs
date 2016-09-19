using UnityEngine;
using System.Collections;

public class Alg1 : MonoBehaviour {

	void Start () {
        Gcd(44977,40589);
	}

    //ユークリッドの互除法は再帰関数で記述できる
    //これは関数の入力に対する置換を挟んだ数学的帰納で考えることができる　…→数学的帰納→置換関数→数学的帰納→置換関数→…
    private void Gcd(int m, int n)
    {
        int nn = m % n;
        if (nn != 0) {
            Gcd(n, nn);  //入力m,nをn,nnに置換していると考える　つまり、これは群論で考えると準同型写像Φになる？(まだよくわかってない)
        } else {
            print("最大公約数は "+ n);
        }
    }
}
