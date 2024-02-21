using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    public float growthRate; // 크기가 증가하는 속도
    public Vector3 initialScale;
    public Vector3 intialPos;
    private void Awake()
    {
        // 초기 스케일을 저장합니다.
        initialScale = transform.localScale;
        intialPos = transform.position;
    }

    private void OnEnable()
    {
        // 활성화될 때 초기 스케일로 설정합니다.
        transform.localScale = initialScale;
        transform.position = intialPos;
    }

    private void Update()
    {
        // 현재 오브젝트의 크기를 가져옵니다.
        Vector3 currentScale = transform.localScale;
        
        // y축 크기를 증가시킵니다.
        currentScale.y += growthRate * Time.deltaTime;

        

        // 위아래로 커지기 때문에 커지는 속도만큼 위로 이동시켜줘서 위로만 커지게 함
        transform.localScale = currentScale;
        transform.position += Vector3.up * growthRate/2 * Time.deltaTime;
    }
}
