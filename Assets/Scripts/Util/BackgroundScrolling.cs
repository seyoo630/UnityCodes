using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public float speed;
    public Transform[] backgrounds;

    float leftPosX = 0f;
    float rightPosX = 0f;
    float xScreenHalfSize;
    float yScreenHalfSize;
    // Start is called before the first frame update
    void Start()
    {
        // ī�޶� ũ�� ���
        yScreenHalfSize = Camera.main.orthographicSize;
        xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;

        // ��� �̹����� �ʺ� ��� (backgrounds �迭 �� ù ��° ����� SpriteRenderer�� �ʺ� ���)
        float backgroundWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;

        // ȭ�鿡�� �������� ����� ���ذ� ����
        leftPosX = -(xScreenHalfSize + (backgroundWidth / 2));  // ī�޶� �ʺ񺸴� ��� �̹��� �� �ʺ�ŭ �� �̵�
    }



    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // ���� ����� ��ġ�� �������� �̵�
            backgrounds[i].transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);

            // ����� ȭ���� ���� ���� �����
            if (backgrounds[i].position.x < leftPosX)
            {
                // ����� ���� ������ ������ �̵���Ŵ (��� �̹����� x ���� 19�� ��������)
                Vector3 nextPos = backgrounds[i].position;
                nextPos.x += backgrounds.Length * 19;  // backgrounds.Length�� ����� ����
                backgrounds[i].position = nextPos;
            }
        }
    }

}