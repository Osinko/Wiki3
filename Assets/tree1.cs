using UnityEngine;
using System.Collections;
using System.Linq.Expressions;
using System;

public class tree1 : MonoBehaviour
{

    void Start()
    {
        print(Sigma(5));

        bool check = true;

        Expression a = Expression.Constant(7);

        Expression body =
            Expression.Add(
                Expression.Constant(5),
                Expression.Multiply(
                    Expression.Constant(7),
                    Expression.Constant(3)
                    )
                );

        Expression body2 =
    Expression.Add(
        Expression.Constant(5),
        Expression.Multiply(
            a,
            Expression.Constant(3)
            )
        );


        Expression<Func<int>> lambda = Expression.Lambda<Func<int>>(body); // () => 5 + 7 * 3
        Func<int> func = lambda.Compile();
        int result = func(); // result == 26

        print(result);

    }

    Func<int, int> Sigma = i =>
    {
        int x = 0;
        for (;;)
        {
            x += i;
            i -= 1;
            if (i <= 0) break;
        }
        return x;
    };

}
