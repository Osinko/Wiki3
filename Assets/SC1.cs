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

        ChrSet ch = new ChrSet(8, 8,'.');
        ch.PutChr('a', 2, 2);
        ch.PutChr('c', 3, 2);
        ch.PutChr('b', 4, 3);

        ch.RectCopy(2, 2, 3, 2, 2, 5);

        ClassSaveText(folder, @"\test3.txt", ch.ToStringArray());

        //string[] txtTree = TextTree(tree);
        //ClassSaveText(folder, @"\test2.txt", tree.list);
        //ClassSaveText(folder, @"\test3.txt", txtTree);
    }



    class ChrSet
    {
        int column, row=0;
        char initChr;
        char verticalLine;
        char horizontalLine;
        char forkedDownNode;
        char forkedRightNode;
        char cornerNode;
        char[] chr;

        //ctor
        public ChrSet(int column,int row,
            char initChr=' ',
            char verticalLine='│',
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

        public void Initialize() {
            for (int i = 0; i < chr.Length; i++)
            {
                chr[i] = this.initChr;
            }
        }

        public void PutChr(char c,int x, int y) {
            chr[x + (y * row)] = c;
        }

        public char GetChr(int x, int y) {
            return chr[x + (y * row)];
        }

        //横線、縦線を引く
        public void RuledLine(int x, int y,int length,bool vertical=true) {


        }

        public void PutCorner(int x, int y)
        {
            chr[x + (y * row)] = cornerNode;
        }


        //矩形コピー　正常終了でtrueを返す
        public bool RectCopy(
            int x, int y, int width, int height,
            int clnX, int clnY,
            bool init=false)
        {
            //枠内チェック
            if ((x + width) > column || (y+height)>row) return false;
            if ((clnX + width) > column || (clnY+height) > row) return false;

            char[] temp = new char[width*height];

            //コピー元を取る
            for (int k=0,i = 0; i < height; i++)
            {
                for (int j = x; j < x+width; j++)
                {
                    temp[k++] = GetChr(j,y+i);
                    if (init) {
                        PutChr(initChr, j, y + i ); //コピー元を初期化する
                    }
                }
            }

            //コピー先を書き換え
            for (int k=0,i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    PutChr(temp[k++],clnX+j,clnY+i);
                }
            }
            return true;
        }

        //string配列に変換出力
        public string[] ToStringArray() {

            string[] str = new string[row];
            char[] temp = new char[column];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    temp[j] = chr[(i*column)+j];
                }
                str[i] = new string(temp);
            }

            return str;
        }

    }

private string[] TextTree(TreeWord tree)
    {
        List<KeyWord> treeList = new List<KeyWord>(tree.list);      //リストの複製

        ChrSet chrset=new ChrSet(tree.maxLevel * 2, tree.maxRow);

        //TextTreeLoop(treeList[0], chrset);


        return null;


    }

    //利用するtreeListを壊しながらテキストを作成する
    private void TextTreeLoop(KeyWord currentKeyword, char[] chr)
    {
        int column, row = 0;
        int maxColumn, maxRow;

        while (currentKeyword.child.Count != 0)
        {
            currentKeyword = currentKeyword.child[0];
        }

        print("!");
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
                child = null
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

        public override string ToString()
        {
            return string.Format("level:{0} name:{1} rest:{2}", level, name, rest);
            //return string.Format("level:{0} name:{1} rest:{2} root:{3} child:{4}",level,name,rest,root,child);
        }
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