using UnityEngine;
using System.Collections;
using System;

public class Tree2 : MonoBehaviour
{

    void Start()
    {

        Node node = new Node("(a+b)*c");
        node.Parse();
    }

    class Node
    {
        public string Exp;
        public Node Left;
        public Node Right;

        //ctor
        public Node(string exp)
        {
            this.Exp = exp;
        }

        public void Parse()
        {
            int pos = GetOperateorPos(this.Exp);
            print(pos);

            //終端なら左右の項はnull
            if(pos < 0)
            {
                this.Left = null;
                this.Right = null;
                return;
            }

            //左右に文字列を分解
            print(this.Exp.Substring(0, pos));
            print(this.Exp.Substring( pos+1));
            print(this.Exp.Substring( pos,1));





        }

        private string RemoveBracket(string str) {

            //両端が括弧で囲まれて無い場合そのまま返す
            if (!(str.StartsWith("(") && str.EndsWith(")"))) return str;

            return str;

        } 
           

        private int GetOperateorPos(string exp)
        {
            if (string.IsNullOrEmpty(exp)) return -1;

            int pos = -1;
            int nest = 0;
            int priority = 0;
            int lowPriority = 4;    //最高優先順位

            for (int i = 0; i < exp.Length; i++)
            {
                switch (exp[i])
                {
                    case '=': priority = 1; break;
                    case '-': priority = 2; break;
                    case '+': priority = 2; break;
                    case '*': priority = 3; break;
                    case '/': priority = 3; break;
                    case '(': nest++; break;
                    case ')': nest--; break;
                    default: continue;
                }

                if (nest == 0 && priority <= lowPriority) {
                    lowPriority = priority;
                    pos = i;
                }
            }
            return pos;
        }
    }



}
