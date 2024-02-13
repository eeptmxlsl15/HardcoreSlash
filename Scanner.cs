using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;//다수를 검색해야해서 배열
    public Transform nearsetTarget;//가장 가까운 타겟
    public Transform randomTarget;

    void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        //원형 형태로 검색하겠다. 
        //캐스팅 시작 위치, 원의 반지름(검색 범위),캐스팅 방향(쏘는 방향),쏘는 방향의 길이(사거리),대상 레이어
        nearsetTarget = GetNearest(); // 검색한 것들 중 가장 가까운 것.
        randomTarget = GetRandom();
    }

    Transform GetNearest()
    {
        Transform result = null; //임시 반환 값
        float diff = 100;

        foreach(RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;// 내 위치
            Vector3 targetPos = target.transform.position;// 타겟의 위치
            float curDiff = Vector3.Distance(myPos, targetPos);

            if (curDiff < diff)//거리가 더 가까운 곳으로 이동
            {
                diff = curDiff;
                result = target.transform;
            }

        }

        return result;

    }
    Transform GetRandom()
    {
        Transform result = null; //임시 반환 값
        
        int randomIndex = Random.Range(0, targets.Length); // 랜덤한 인덱스 선택
        result = targets[randomIndex].transform;


        return result;
    }
}
