using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ĳ���͸� ����ٴϰ� �ϴ� ��ũ��Ʈ. ü�¹� ui�� ���
/// </summary>
public class Follow : MonoBehaviour
{
    RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
        //���� ��ǥ�� ��ũ�� ��ǥ�� �Ű��� �̰� ������ ī�޶���ǥ�� ���Ե�

            
    }
}
