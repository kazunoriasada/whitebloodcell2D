using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    /// <summary>
    /// 物理剛体
    /// </summary>
    private Rigidbody2D physics = null;

    /// <summary>
    /// 発射方向
    /// </summary>
    [SerializeField]
    private LineRenderer direction = null;

    /// <summary>
    /// 最大付与力量
    /// </summary>
    private const float MaxMagnitude = 2f;

    /// <summary>
    /// 発射方向の力
    /// </summary>
    private Vector3 currentForce = Vector3.zero;

    /// <summary>
    /// メインカメラ
    /// </summary>
    private Camera mainCamera = null;

    /// <summary>
    /// メインカメラ座標
    /// </summary>
    private Transform mainCameraTransform = null;

    /// <summary>
    /// ドラッグ開始点
    /// </summary>
    private Vector3 dragStart = Vector3.zero;

    public void Awake()
    {
        this.physics                = this.GetComponent<Rigidbody2D>();
        this.mainCamera             = Camera.main;
        this.mainCameraTransform    = this.mainCamera.transform;
    }
   public void start()
   {
       //Event TriggerコンポーネントでDrugイベントを取得
       //https://qiita.com/takesuke/items/e3b314aa7fd9111bc17f
       var eventTrigger       = GetComponent<EventTrigger>();
            var beginDragEntry     = new EventTrigger.Entry();
            //PointerDown = 範囲内でボタンを押す(タップ)した時
            beginDragEntry.eventID = EventTriggerType.PointerDown;
            
            /* 
            デリゲート = メソッドを参照するための型
            前提 →デリゲートを作るためだけに、クラスメソッドかインスタンスメソッドを定義するのは面倒で読みづらい
            匿名関数を使えばメソッドを定義せずともインラインに処理を書いてデリゲートを作る事ができる
            匿名関数には2種類あってその1つがラムダ式
            https://qiita.com/RyotaMurohoshi/items/740151bd772889cf07de

            ラムダ式
            匿名関数を使えばインラインでデリゲートの処理を記述し、デリゲートを生成できる
            匿名関数の一つはラムダ式
            もう一つの匿名関数である匿名メソッドよりも簡潔に記述することができる
            C# 3.0以降であれば匿名メソッド式でなく、ラムダ式を使うべき

            */

            //AddListener = UnityAction系のデリゲートを引数にとる
            beginDragEntry.callback.AddListener(data =>
            {
                //OnBeginDrag() = タッチ開始座標を取得
                OnBeginDrag((PointerEventData)data);
            });
           
            eventTrigger.triggers.Add(beginDragEntry);

            //ドラッグ開始
            var dragEntry     = new EventTrigger.Entry();
            //EventTriggerType.Drag = PointerDown後に押しっぱなしのまま移動している時
            dragEntry.eventID = EventTriggerType.Drag;
            
            dragEntry.callback.AddListener(data =>
            {
                //OnDrag() = 現在のタッチ座標 - タッチ開始座標でひっぱりのベクトルを算出
                OnDrag((PointerEventData)data);
            });
            eventTrigger.triggers.Add(dragEntry);

            var endDragEntry = new EventTrigger.Entry();
            endDragEntry.eventID = EventTriggerType.EndDrag;
            endDragEntry.callback.AddListener(data =>
            {
                //OnEndDrag () = ひっぱりのベクトルに応じてキャラのRigidbodyを操作
                OnEndDrag((PointerEventData)data);
            });

            eventTrigger.triggers.Add(endDragEntry);
            
   }
    public void Update() 
    {
        //画面外に出たら最初から
        if(transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    private Vector3 GetMousePosition()
    {
        Vector3 position = Input.mousePosition;
        position.z = this.mainCameraTransform.position.z;
        position = this.mainCamera.ScreenToWorldPoint(position);
        position.z = 0;
        return position;
    }

    
    /* public void OnMouseDown()
    {
        this.dragStart = this.GetMousePosition();

        this.direction.enabled = true;
        this.direction.SetPosition(0, this.physics.position);
        this.direction.SetPosition(1, this.physics.position);
    }

    public void OnMouseDrag()
    {
        var position        = this.GetMousePosition();
        this.currentForce   = position - this.dragStart;

        if (this.currentForce.magnitude > MaxMagnitude * MaxMagnitude)
        {
            this.currentForce *= MaxMagnitude / this.currentForce.magnitude;
        }

        this.direction.SetPosition(0, this.physics.position);
        this.direction.SetPosition(1, this.physics.position + (Vector2) this.currentForce);
    }

    public void OnMouseUp()
    {
        this.direction.enabled = false;
        this.Flip(this.currentForce * 6f);
    }

    public void Flip(Vector3 force)
    {
        this.physics.AddForce(force, ForceMode2D.Impulse);
    }
    */

}
