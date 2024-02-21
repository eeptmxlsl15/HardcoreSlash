using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    public float growthRate; // ũ�Ⱑ �����ϴ� �ӵ�
    public Vector3 initialScale;
    public Vector3 intialPos;
    private void Awake()
    {
        // �ʱ� �������� �����մϴ�.
        initialScale = transform.localScale;
        intialPos = transform.position;
    }

    private void OnEnable()
    {
        // Ȱ��ȭ�� �� �ʱ� �����Ϸ� �����մϴ�.
        transform.localScale = initialScale;
        transform.position = intialPos;
    }

    private void Update()
    {
        // ���� ������Ʈ�� ũ�⸦ �����ɴϴ�.
        Vector3 currentScale = transform.localScale;
        
        // y�� ũ�⸦ ������ŵ�ϴ�.
        currentScale.y += growthRate * Time.deltaTime;

        

        // ���Ʒ��� Ŀ���� ������ Ŀ���� �ӵ���ŭ ���� �̵������༭ ���θ� Ŀ���� ��
        transform.localScale = currentScale;
        transform.position += Vector3.up * growthRate/2 * Time.deltaTime;
    }
}
