using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    //SerializeField = privateフィールドをインスペクタに表示する際に付けるおまじない
    [SerializeField]
    private GameObject Enemy;
   //リストの作成
   //リストはなぜ作るのか？
    private List<GameObject> enemyList;

    [SerializeField]
    Text KillCount;

    private const int POINT_VALUE = 1;
    private int point;

    //const = 属性に指定するパラメータや列挙型の定義など、コンパイル時に値が必要な場合にのみ使用する
    private const int MAX_COUNT = 500;
    private int count;
    
    //bool = 変数の型名 trueまたはfalse
    private bool gamePlay;

    // Start is called before the first frame update
    void Start()
    {
        //Random = 乱数を生成する際のクラス
        //System = 名前空間であり、Randomクラスとセットで使われる
        System.Random random = new System.Random();

        //Create Enemy
        enemyList = new List<GameObject>();
        for(int i = 0; i < MAX_COUNT; i++)
        {
            //Instantiate = ゲーム中に表示される主人公や敵キャラクターなどの動的なオブジェクトを生成する関数
            //var = メソッド内のローカル変数を宣言する際に型宣言の代わりに使用する。コンパイラが自動で型を判断してくれる
            //var = 長い型名の代わりに var を使うとコードをスッキリする
            //var = 右辺から型が明らかでない場合、var は推奨されない
            var go = Instantiate(Enemy);
            go.GetComponent<Enemy>().SetGameManager(this);
            if(go == null)
            {
                 Debug.Log("生成できていない");

            }
            else if(go.GetComponent<Enemy>() == null)
            {
                Debug.Log("EnemyがAddされていない");
            }

            //Enemyの出現ポイントの指定
            int positionX = random.Next(-150, 150);
            int positionY = random.Next(0, 150);
            
            go.GetComponent<Transform>().position = new Vector3(
                positionX,positionY,
                go.GetComponent<Transform>().position.z
            );

            enemyList.Add(go);
        }

        ount = MAX_COUNT;
        frame = 0;
        gamePlay = true;

        SetPointText();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetPointText()
    {
        if(gamePlay == false) return;
        this.KillCount.text = point.ToString();
    }

    public void GetPoint()
    {
        if(gamePlay == false) return;
        this.point += POINT_VALUE;
    }
}

