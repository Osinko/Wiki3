using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public class SC1 : MonoBehaviour
{
    public string word;
    public float columnWidth;

    public void Start()
    {
        string folder = Application.dataPath;    //これだけでunityの実行ファイルがあるフォルダがわかる
        TreeWord tree = Respown();

        TextTree textTree = new TextTree(tree);
        ClassSaveText(folder, @"\test4.txt", textTree.chrset.ToStringArray());
    }


    public class TextTree
    {
        TreeWord tree;
        public ChrSet chrset;

        //ctor
        public TextTree(TreeWord tree)
        {
            this.tree = tree;
            chrset = new ChrSet(tree.maxLevel * 2, tree.maxRow);

            Controller con = new Controller(tree.list[0]);

        }

        public class Controller {
            Key next;
            Stack <Key> prev;

            //ctor
            public Controller(KeyWord rootKey)
            {
                prev = new Stack<Key>();
                SetKey(rootKey);

                //TODO 再帰初期化が終了して再帰処理を書くところ。設計を形にしていくフェイズ


            }

            private void SetKey(KeyWord key)
            {
                next = new Key(ChrSet.Character.forkedDownNode, key.child[0]);

                for (int i = key.child.Count; i > 0; i--)
                {
                    if (i == key.child.Count)
                    {
                        prev.Push(new Key(ChrSet.Character.cornerNode, key.child[i]));
                    }
                    else
                    {
                        prev.Push(new Key(ChrSet.Character.forkedRightNode, key.child[i]));
                    }
                }
            }
        }

        public class Key
        {
            ChrSet.Character first;
            KeyWord keyWord;

            public Key(ChrSet.Character first,KeyWord keyWord)
            {
                this.first = first;
                this.keyWord = keyWord;
            }
        }
    }


    public class ChrSet
    {
        int column, row = 0;
        char initChr;
        char verticalLine;
        char horizontalLine;
        char forkedDownNode;
        char forkedRightNode;
        char cornerNode;
        char[] chr;

        public enum Character
        {
            forkedDownNode,
            forkedRightNode,
            cornerNode,
            verticalLine,
            horizontalLine,
        }

        public char SetChr(Character sw)
        {
            char c=' ';
            switch (sw)
            {
                case Character.forkedDownNode: c= forkedDownNode; break;
                case Character.forkedRightNode: c = forkedRightNode; break;
                case Character.cornerNode: c = cornerNode; break;
                case Character.horizontalLine: c = horizontalLine; break;
                case Character.verticalLine: c = verticalLine; break;
            }
            return c;
        }

        //ctor
        public ChrSet(int column, int row,
            char initChr = ' ',
            char verticalLine = '│',
            char horizontalLine = '─',
            char forkedDownNode = '┬',
            char forkedRightNode = '├',
            char cornerNode = '└')
        {
            this.column = column;
            this.row = row;
            this.initChr = initChr;
            this.verticalLine = verticalLine;
            this.horizontalLine = horizontalLine;
            this.forkedDownNode = forkedDownNode;
            this.forkedRightNode = forkedRightNode;
            this.cornerNode = cornerNode;

            chr = new char[column * row];

            Initialize();
        }

        public void Initialize()
        {
            for (int i = 0; i < chr.Length; i++)
            {
                chr[i] = this.initChr;
            }
        }

        public bool PutChr(char c, int x, int y)
        {
            if (x > column || y > row) return false;
            chr[x + (y * row)] = c;
            return true;
        }

        public char GetChr(int x, int y)
        {
            return chr[x + (y * row)];
        }

        //横線、縦線を引く
        public bool PutRuledLine(int x, int y, int length, bool vertical = true)
        {
            if (vertical)
            {
                if ((y + length) > row) return false;
                for (int i = 0; i < length; i++)
                {
                    PutChr(verticalLine, x, y + i);
                }
            }
            else
            {
                if ((x + length) > column) return false;
                for (int i = 0; i < length; i++)
                {
                    PutChr(horizontalLine, x + i, y);
                }
            }
            return true;
        }

        public bool PutCorner(int x, int y)
        {
            if (x > column || y > row) return false;
            chr[x + (y * row)] = cornerNode;
            return true;
        }

        public bool PutForkedDown(int x, int y)
        {
            if (x > column || y > row) return false;
            chr[x + (y * row)] = forkedDownNode;
            return true;
        }

        public bool PutForkedRight(int x, int y)
        {
            if (x > column || y > row) return false;
            chr[x + (y * row)] = forkedRightNode;
            return true;
        }

        //矩形コピー　正常終了でtrueを返す
        public bool RectCopy(
            int x, int y, int width, int height,
            int clnX, int clnY,
            bool init = false)
        {
            //枠内チェック
            if ((x + width) > column || (y + height) > row) return false;
            if ((clnX + width) > column || (clnY + height) > row) return false;

            char[] temp = new char[width * height];

            //コピー元を取る
            for (int k = 0, i = 0; i < height; i++)
            {
                for (int j = x; j < x + width; j++)
                {
                    temp[k++] = GetChr(j, y + i);
                    if (init)
                    {
                        PutChr(initChr, j, y + i); //コピー元を初期化する
                    }
                }
            }

            //コピー先を書き換え
            for (int k = 0, i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    PutChr(temp[k++], clnX + j, clnY + i);
                }
            }
            return true;
        }

        //string配列に変換出力
        public string[] ToStringArray()
        {

            string[] str = new string[row];
            char[] temp = new char[column];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    temp[j] = chr[(i * column) + j];
                }
                str[i] = new string(temp);
            }

            return str;
        }

    }







    //階乗計算
    int Factorial(int n)
    {
        if (n == 0) return 1;
        return n * Factorial(n - 1);
    }

    //順列
    private TreeWord Respown()
    {
        TreeWord tree = new TreeWord();
        tree.maxRow = Factorial(word.Length);   //縦列の最大値をここであらかじめ出しておく
        tree.maxLevel = word.Length;            //樹の深さも保存しておく

        //第一ステップ
        KeyWord keyword = new KeyWord { level = 0, name = "root", rest = word, root = null, child = null };
        tree.list.Add(keyword);

        //第二ステップ以降の再帰処理
        RespownLoop(keyword, tree.list, 0);

        return tree;
    }

    public bool RespownLoop(KeyWord keyword, List<KeyWord> list, int lv)
    {
        if (keyword.rest.Length == 0) return false;

        List<KeyWord> childList = new List<KeyWord>();

        lv++;

        for (int i = 0; i < keyword.rest.Length; i++)
        {
            KeyWord key = new KeyWord
            {
                level = lv,
                name = keyword.rest.Substring(i, 1),
                rest = keyword.rest.Remove(i, 1),
                root = keyword,
                child = null,
                traceCount = 0,
                traceRect = { posX = 0, posY = 0, width = 0, height = 0 },
            };

            list.Add(key);
            childList.Add(key);
        }

        foreach (var item in childList)
        {
            RespownLoop(item, list, lv);
        }

        keyword.child = childList;
        return true;
    }


    public class TreeWord
    {
        public List<KeyWord> list;
        public int maxLevel;
        public int maxRow;

        //ctor
        public TreeWord()
        {
            list = new List<KeyWord>();
            maxLevel = 0;
            maxRow = 0;
        }

    }

    public class KeyWord
    {
        public int level;
        public string name;
        public string rest;         //残りのワード（記憶）
        public KeyWord root;        //親の枝
        public List<KeyWord> child; //子の枝
        public int traceCount;
        public IntRect traceRect;
        public bool passed;
        public ChrSet chrParts;

        public override string ToString()
        {
            return string.Format("level:{0} name:{1} rest:{2}", level, name, rest);
            //return string.Format("level:{0} name:{1} rest:{2} root:{3} child:{4}",level,name,rest,root,child);
        }
    }

    public struct IntRect
    {
        public int posX;
        public int posY;
        public int width;
        public int height;
    }


    //資料：StreamWriter クラス (System.IO)
    //http://msdn.microsoft.com/ja-jp/library/system.io.streamwriter(v=vs.110).aspx

    //テキストファイルとしてセーブ
    public void SaveText(string fileFolder, string filename, string[] dataStr)
    {
        using (StreamWriter w = new StreamWriter(fileFolder + filename))
        {
            foreach (var item in dataStr)
            {
                w.WriteLine(item);
            }
        }
    }

    //ローダー
    public string[] LoadText(string fileFolder, string filename)
    {
        List<string> strList = new List<string>();
        string line = "";
        using (StreamReader sr = new StreamReader(fileFolder + filename))
        {
            while ((line = sr.ReadLine()) != null)
            {
                strList.Add(line);
            }
        }
        return strList.ToArray();
    }

    private void ClassSaveText(string folder, string name, IEnumerable data)
    {
        List<string> strList = new List<string>();
        foreach (var item in data)
        {
            strList.Add(item.ToString());
        }
        SaveText(folder, name, strList.ToArray());
    }
}