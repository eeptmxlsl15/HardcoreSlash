using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;//에너미의 콜라이더. 에너미가 죽을 경우 캡슐콜라이더를 비활성화 하기 위한 용도

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;
        //맵 자동 생성. 
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;
       
        switch (transform.tag)
        {
            case "Ground":
                float diffX =playerPos.x - myPos.x;
                float diffY = playerPos.y - myPos.y;
                float dirX = diffX < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;
                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                else
                {
                    transform.Translate(Vector3.right * dirX * 40);
                    transform.Translate(Vector3.up * dirY * 40);
                }

                break;
            case "Enemy":
                if (coll.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 rand = new Vector3(Random.Range(-3,3), Random.Range(-3, 3),0);
                    transform.Translate(rand+ dist * 2);//플레이어 진행 방향에서 생김
                }
                break;
        }
    }
}
