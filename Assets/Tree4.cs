using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System;

public class Tree4 : MonoBehaviour
{

    void Start()
    {
        int hit = 10;   //検出する値

        //Numbers();
        //TestMethod1();
        TestMethod2();

    }

    private void TestMethod2()
    {
        print( CharCheck("(***)*(***)", new char[] { '(', ')' }));
        print(new string(MixString("(***)*(***)", new char[] { '(', ')' }, "1+2+5+6+8+",true).ToArray()));

    }

    private  void TestMethod1()
    {
        Node tes = new Node("(-5/2)/(-3/-4)");
        tes.Parse();
        print(tes.Calculate());

        string tesA = "ABCDEF";
        string tesB = "abc";

        print(Zip(tesA, tesB, false));
        print(+1 + -13);
        //print(new string (MixString("(***)****", new char[] { '(', ')' }, "1*2+3/4").ToArray()));
        print(CharCheck("(***)****", new char[] { '(', ')' }));
        print(CharCheck('=', new char[] { '(', ')' }));
        print(CharCheck('(', new char[] { '(', ')' }));


        string naked = "a①b②c③d";
        print(naked.Replace('①', '*'));

    }

    private void Numbers()
    {

        int length = 3;
        char[] set = { '+', '-', '/', '*' };
        char[] throughChar = { '(', ')' };
        string[] operatorList = Root(set, length).ToArray();  //演算結合子の順列リスト

        string naked = "a①b②c③d";

        //括弧がある式の順列リスト
        string[] nestList = {
            "(a①b)②c③d",
            "a①(b②c)③d",
            "a①b②(c③d)",
            "(a①b)②(c③d)",
            "a①(b②(c③d))",
            "a①((b②c)③d)",
            "((a①b)②c)③d",
            "(a①(b②c))③d",
        };

        for (int i = 0; i < 10000; i++)
        {
            bool flag = false;
            string mass = null;
        }

    }

    public string MixString(string wildCode, char[] throughChar, string source, bool continuation = false)
    {
        int wildCardLength = CharCheck(wildCode, throughChar);
        int wordLength = wildCode.Length - wildCardLength;
        int tempLength = 0;

        if (continuation)
        {
            tempLength=source.Length + wildCardLength; //sourceの文字数優先
        }
        else
        {
            tempLength = wildCode.Length;   //wildCodeの文字数優先
        }

        char[] temp = new char[tempLength];

        int wildCodePos = 0;
        int sourcePos = 0;

        for (int i = 0; i < temp.Length; i++)
        {
            if (i >= wildCode.Length) {
                temp[i]= source[sourcePos++];
            }
            else {
                if (CharCheck(wildCode[i], throughChar))
                {
                    temp[i] = wildCode[i];
                }
                else
                {
                    if (sourcePos <= source.Length - 1)
                    {
                        temp[i] = source[sourcePos++];
                    }
                    else
                    {
                        if (continuation)
                        {
                            temp[i] = wildCode[i];
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        return new string( temp);
    }

    //文字列内に該当するキャラクタグループが現れる回数を出力
    public static int CharCheck(string code, char[] checkChar)
    {
        int count = 0;
        for (int i = 0; i < code.Length; i++)
        {
            for (int j = 0; j < checkChar.Length; j++)
            {
                if (code[i] == checkChar[j])
                {
                    count++;
                    break;
                }
            }
        }
        return count;
    }

    //文字内に該当するキャラクタグループが存在した場合true
    public static bool CharCheck(char code, char[] checkChar)
    {
        bool check = false;
        for (int j = 0; j < checkChar.Length; j++)
        {
            if (code == checkChar[j])
            {
                check = true;
                break;
            }
        }
        return check;
    }


    //ふたつの文字列を交互に混合（.NET4以下用）
    private static string Zip(string odd, string even, bool ending = false)
    {

        int length;
        bool evenLong = false;

        if (even.Length <= odd.Length)
        {
            length = even.Length;   //文字数が少ない方を基準に。同数の場合はevenLongフラグはfalse
        }
        else {
            length = odd.Length;
            evenLong = true;
        }

        int j = 0, evenPos = 0, oddPos = 0;
        char[] temp = new char[length * 2];

        for (int i = 0; i < length; i++)
        {
            temp[j++] = odd[oddPos++];
            temp[j++] = even[evenPos++];
        }

        if (ending)
        {
            if (evenLong)
            {
                return new string(temp) + even.Substring(length);
            }
            else
            {
                return new string(temp) + odd.Substring(length);
            }
        }
        return new string(temp);
    }

    //基底部
    List<string> Root(char[] set, int length)
    {
        List<string> strList = new List<string>();
        int level = 0;

        for (int i = 0; i < set.Length; i++)
        {
            char[] dat = new char[length];
            dat[0] = set[i];
            Loop(dat, level, set, strList);
        }

        return strList;
    }

    //帰納部
    void Loop(char[] dat, int level, char[] set, List<string> strList)
    {
        level++;
        if (level < dat.Length)     //帰納関数の終了条件
        {
            for (int i = 0; i < set.Length; i++)
            {
                char[] cloneDat = (char[])dat.Clone();  //オブジェクトを複製する
                cloneDat[level] = set[i];
                Loop(cloneDat, level, set, strList);    //帰納関数には複製された参照値が渡される為、呼び出し元側の値が書き換えられることは無い
            }
        }
        else {
            strList.Add(new string(dat));
        }

    }




























    //資料：StreamWriter クラス (System.IO)
    //http://msdn.microsoft.com/ja-jp/library/system.io.streamwriter(v=vs.110).aspx

    //テキストファイルとしてセーブ
    public void SaveText(string fileFolder, string filename, string[] dataStr)
    {
        using (StreamWriter w = new StreamWriter(fileFolder + filename, false, System.Text.Encoding.GetEncoding("shift_jis")))
        {
            foreach (var item in dataStr)
            {
                w.WriteLine(item);
            }
        }
    }




    public class Node
    {
        public string Expression;
        public Node Left = null;
        public Node Right = null;

        public Node(string expression)
        {
            this.Expression = expression;
        }

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
                    //正負符号の処理の追加
                    //expression最初の１文字目が+-の場合は無視。以降、一文字前に+や-等の四則演算結合子がある場合は無視
                    case '+': if (i == 0 || (expression[i - 1] == '+' || expression[i - 1] == '-' || expression[i - 1] == '*' || expression[i - 1] == '/')) { continue; } else { priority = 2; } break;
                    case '-': if (i == 0 || (expression[i - 1] == '+' || expression[i - 1] == '-' || expression[i - 1] == '*' || expression[i - 1] == '/')) { continue; } else { priority = 2; } break;
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

        //文字列の式から外側の括弧を消す処理
        //例："((2*4)+3)"→"(2*4)+3"
        private static string RemoveBracket(string str)
        {
            //両端に括弧が無い場合、そのまま文字列を返す
            if (!(str.StartsWith("(") && str.EndsWith(")")))
                return str;

            //文字列、先頭と末尾文字を抜いて出力
            str = str.Substring(1, str.Length - 2);

            //先頭の括弧をチェックして二重以上の括弧を再帰処理する
            if (str.StartsWith("("))
                return RemoveBracket(str);
            else
                return str;
        }

        //自分自身を必要回複数呼び出す再帰関数
        //ノードは結合子に、サブツリーの末端は左右の葉がnullになることを利用して計算処理している
        //括弧の前の正負符号演算子には非対応。例："(5-2)--(3*-4)"
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
