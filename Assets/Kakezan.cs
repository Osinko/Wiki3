using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Kakezan : MonoBehaviour {

	void Start () {

        List<string> strList = new List<string>();

        strList.Add("123456");  
        strList.Add("654321");  
        strList.Add("132456");  
        strList.Add("132456");
        strList.Add("123456");
        strList.Add("123456");

        print(strList.Count);

        IEnumerable<string> strListDist = strList.Distinct();

        print(strListDist.Count());

        foreach (var item in strListDist)
        {
            print(item);
        }

    }

}
