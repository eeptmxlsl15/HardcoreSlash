using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;//프리펩을 보관하는 변수

    List<GameObject>[] pools;//풀 담당을 하는 리스트들

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length]; //리스트이기 때문에 new를 써야함. 풀을 담는 배열 초기화

        for(int index = 0; index < prefabs.Length; index++)
        {
            pools[index] = new List<GameObject>();//배열안에 리스트도 초기화

        }

    }
    public GameObject Get(int index)//비어있는 오브젝트를 반환하는 함수
    {
        GameObject select = null;

        //선택한 풀에 놀고있는 게임 오브젝트 접근
        //발견하면  select 할당
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)// 만약 활성화되지않았다면
            {
                select = item;
                select.SetActive(true);// 활성화
                break;

            }
        }

        //모두 쓰고있다면 생성해서 select에 할당
        if (select == null)
        {
            select = Instantiate(prefabs[index], transform);// 오브젝트를 복사하는 함수. 원본 , 자기 자신에게 넣음
            pools[index].Add(select);//pools에 등록


        }
        return select;

    }

    


}
