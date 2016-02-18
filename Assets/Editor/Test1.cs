using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(SC1))]
public class Test1 : Editor
{
    string word;
    string preWord;

    void OnSceneGUI()
    {
    }

    private void show(SC1 sc1)
    {
        GUIStyle style = new GUIStyle();
        style.richText = true;
        style.contentOffset = new Vector2(-50, -8);
        Handles.color = Color.white;
        Handles.Label(sc1.transform.position, "<color=white>" + word + "</color>", style);
        Handles.DrawDottedLine(Vector3.zero, new Vector3(1, 1, 1), 5);
    }

    //再帰処理的に樹形図の樹を作る
    private List<KeyWord> Respown(KeyWord keyword, List<KeyWord> list)
    {
        if (keyword.rest.Length == 0) return list;

        List<KeyWord> childList = new List<KeyWord>();

        for (int i = 0; i < keyword.rest.Length; i++)
        {
            KeyWord key = new KeyWord
            {
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
            Respown(item, list);
        }

        keyword.child = childList;
        return list;
    }

    public class KeyWord
    {
        public string name;
        public string rest;         //残りのワード（記憶）
        public KeyWord root;        //親の枝
        public List<KeyWord> child; //子の枝
    }


}

