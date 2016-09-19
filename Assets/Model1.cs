using UnityEngine;
using System.Collections;
using System;

public class Model1 : MonoBehaviour {

	void Start () {
        MyModel f = new MyModel("654321");
        MyModel g = new MyModel();
        MyModel h = new MyModel("654321");

        print(f.Equals(g));
        print(f.X.Length);
        print(f.Equals(h));
        print(f==g);
        print(f==h);

    }

    public class MyModel
    {
        public int mod;
        public string X { get; set; }
        public string Y { get; set; }

        public MyModel()
        {
            this.X = "123456";
        }

        public MyModel(string X)
        {
            if (X.Length != 6) X = "123456";
            this.X = X;
        }

        //通常Equalsメソッドをオーバーライドしたときは、GetHashCodeメソッドもオーバーライドします。
        public override int GetHashCode()
        {
            return this.X.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;
            MyModel c = (MyModel)obj;
            return (this.X == c.X);
        }
       
        public static bool operator ==(MyModel c1,MyModel c2)
        {
            //オブジェクトとして同一かどうか
            if (object.ReferenceEquals(c1, c2))
            {
                return true;
            }

            //オブジェクトとしてnullかどうか
            if (((object)c1 == null) || ((object)c2 == null))
            {
                return false;
            }

            return (c1.X== c2.X);
        }

        public static bool operator !=(MyModel c1, MyModel c2)
        {
            return !(c1 == c2); //これはoperatorの処理で判定している
        }
    }
}
