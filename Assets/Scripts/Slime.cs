using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] Transform player;              // プレイヤーを Inspector で登録

    [SerializeField] float moveSpeed = 2.0f;        // 左右移動速度
    [SerializeField] float moveDuration = 1f;       // 移動を続ける時間
    [SerializeField] float stopDuration = 0.5f;     // 停止時間

    Rigidbody2D rb;

    async UniTask Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 非同期でプレイヤー方向へ移動
        MoveTowardPlayerAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    /// <summary>プレイヤー方向へ一定時間水平移動。</summary>
    async UniTask MoveTowardPlayerAsync(CancellationToken ct)
    {
        while (ct.IsCancellationRequested == false)
        {
            // プレイヤーの位置に応じて移動方向を決定
            float speed;
            if(player.position.x > transform.position.x)
            {
                speed = moveSpeed;
            }
            else
            {
                speed = -moveSpeed;
            }

            // 移動
            float rest = moveDuration;
            while(rest > 0f)
            {
                rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
                await UniTask.DelayFrame(1, cancellationToken: ct);
                rest -= Time.deltaTime;
            }

            // 停止
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            await UniTask.Delay(TimeSpan.FromSeconds(stopDuration), cancellationToken: ct);
        }
    }
}
