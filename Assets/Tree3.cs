//当コードの著作：総武ソフトウェア推進所　http://smdn.jp/
//ソース元　http://smdn.jp/programming/tips/polish/
//MITライセンス。著作は元ソースの権利者に帰属
//
//ソース元のコードで一部動かない間違いがあったので修正しています
//理解しやすいように注釈の追加とunity用の改編を行っています

using UnityEngine;
using System.Collections;
using System;

public class Tree3 : MonoBehaviour
{

    void Start()
    {

        //両端を括弧で閉じるのはNG（最下層の括弧は必ず省略する必要がある）
        var root = new Node("(5/(3+2))*4");

        print(string.Format("expression: {0}", root.Expression));

        root.Parse();

        print("reverse polish notation: ");
        root.TraversePostorder();
        print("");

        print("infix notation: ");
        root.TraverseInorder();
        print("");

        print("polish notation: ");
        root.TraversePreorder();
        print("");

        print(string.Format("calculated result: {0}", root.Calculate()));
    }


    //自分自身のクラスの中に自分自身と同じクラス（型）を収める面白い構造をしている
    //再帰関数と絡めて型を運用するとシンプルにデーター設計できる好例となっている
    public class Node
    {
        public string Expression;
        public Node Left = null;
        public Node Right = null;

        public Node(string expression)
        {
            this.Expression = expression;
        }

        //文字列の式をNode型に式木で分解する
        public void Parse()
        {
            //分解は一番優先順位の低い演算子の位置から始まる
            var posOperator = GetOperatorPos(Expression);

            if (posOperator < 0)
            {
                Left = null;
                Right = null;
                return;
            }

            //Nodeクラスのルートと左右の葉に適切にデーターを収納する
            //左右の葉では、それぞれ解析実行して再帰処理し文字列の式を分解していく

            // left-hand side
            Left = new Node(RemoveBracket(this.Expression.Substring(0, posOperator)));
            Left.Parse();

            // right-hand side
            Right = new Node(RemoveBracket(this.Expression.Substring(posOperator + 1)));
            Right.Parse();

            // operator
            this.Expression = this.Expression.Substring(posOperator, 1);
        }

        //文字列の式から外側の括弧を消す処理
        //例："((2*4)+3)"→"(2*4)+3"
        private static string RemoveBracket(string str)
        {
            //両端に括弧が無い場合、そのまま文字列を返す
            if (!(str.StartsWith("(") && str.EndsWith(")")))
                return str;


            //括弧が組になって存在しているかエラーチェック
            var nest = 1;
            for (var i = 1; i < str.Length - 1; i++)
            {
                if (str[i] == '(')
                    nest++;
                else if (str[i] == ')')
                    nest--;

                if (nest == 0)
                    return str;
            }
            if (nest != 1)
                throw new Exception(string.Format("unbalanced bracket: {0}", str)); //数が揃っていなければエラー出力


            //文字列、先頭と末尾文字を抜いて出力
            str = str.Substring(1, str.Length - 2);

            //先頭の括弧をチェックして二重以上の括弧を再帰処理する
            if (str.StartsWith("("))
                return RemoveBracket(str);
            else
                return str;
        }

        //一番、低い優先順位の演算結合子の文字列位置を返す
        private static int GetOperatorPos(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return -1;

            var pos = -1;
            var nest = 0;
            var priority = 0;
            var lowestPriority = 4;

            //文字列の左側から順に処理される
            for (var i = 0; i < expression.Length; i++)
            {
                switch (expression[i])
                {
                    case '=': priority = 1; break;
                    case '+': priority = 2; break;
                    case '-': priority = 2; break;
                    case '*': priority = 3; break;
                    case '/': priority = 3; break;
                    case '(': nest++; continue;
                    case ')': nest--; continue;
                    default: continue;
                }

                //nestが0は括弧が閉じ切っている。ネストの底に位置している事を意味する
                //nestが0以上は括弧内なので記録しない。0以下はエラーを意味するのでこれも記録しない
                //左から順に右に向かって優先順位の低い結合子の位置を探す。同じ優先順位の結合子なら右にあるものほど低い
                //例："5/(3+(2*4))-3"ならば一番優先順位が低い結合子は"-"となる。左項は"5/(3+(2*4))"、右項は"3"となる
                if (nest == 0 && priority <= lowestPriority)
                {
                    lowestPriority = priority;
                    pos = i;
                }
            }

            return pos;
        }

        public void TraversePostorder()
        {
            if (Left != null)
                Left.TraversePostorder();
            if (Right != null)
                Right.TraversePostorder();

            print(Expression);
        }

        public void TraverseInorder()
        {
            if (Left != null && Right != null)
                print("(");

            if (Left != null)
                Left.TraverseInorder();

            print(Expression);

            if (Right != null)
                Right.TraverseInorder();

            if (Left != null && Right != null)
                print(")");
        }

        public void TraversePreorder()
        {
            print(Expression);

            if (Left != null)
                Left.TraversePreorder();
            if (Right != null)
                Right.TraversePreorder();
        }

        //自分自身を必要回複数呼び出す再帰関数
        //ノードは結合子に、サブツリーの末端は左右の葉がnullになることを利用して計算処理している
        public double Calculate()
        {
            //両葉に何か入っているとき処理する
            if (Left != null && Right != null)
            {
                //再帰先を受け取る
                var leftOperand = Left.Calculate();
                var rightOperand = Right.Calculate();

                //ノードの演算結合子を処理
                switch (this.Expression)
                {
                    case "+": return leftOperand + rightOperand;
                    case "-": return leftOperand - rightOperand;
                    case "*": return leftOperand * rightOperand;
                    case "/": return leftOperand / rightOperand;
                    default: return 0.0d;
                }
            }
            else {
                return double.Parse(Expression);    //両葉に何も入ってない時、ここで文字列を数字にしている
            }
        }
    }
}