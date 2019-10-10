using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //private = アクセス演算子。他のクラスからメンバメソッドにアクセスできなくさせる
    private GameDirector manager;
    
    //void はメソッド。メソッドとは、意味のある処理ブロックごとに分解して名前を付ける仕組みのこと
    //void = 返値なし
    public void SetGameManagement (GameDirector manager)
    {
        //this =自分自身のインスタンスを指す。メンバー変数とローカル変数が同じ名前の場合、
        //ローカル変数の方が早めに呼ばれてしまうため、メンバー変数を活用する場合は[this]を付ける
        this.manager = manager;
    }
    //OnCollisionEnter = 衝突検知（当たり判定）
    //Collision = OnCollisionEnterの引数の型

    void OnCollisionEnter(Collision other) 
    {
        //this.manager.GetPoint();
        Destroy(gameObject);
    }
}
