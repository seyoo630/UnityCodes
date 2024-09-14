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

    // �� ��� �̹����� �ʺ� ������ �迭
    float[] backgroundWidths;

    // Start is called before the first frame update
    void Start()
    {
        // ī�޶� ũ�� ���
        yScreenHalfSize = Camera.main.orthographicSize;
        xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;

        // �� ��� �̹����� �ʺ� �迭 �ʱ�ȭ
        backgroundWidths = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            // ��� �̹����� SpriteRenderer���� �ʺ� ����Ͽ� �迭�� ����
            backgroundWidths[i] = backgrounds[i].GetComponent<SpriteRenderer>().bounds.size.x;
        }

        // ���� �� ��ġ ���� (ù ��° ����� ��������)
        leftPosX = -(xScreenHalfSize + (backgroundWidths[0] / 2));  // ù ��° ��� �̹����� �ʺ�� ���
        rightPosX = backgroundWidths[0] * backgrounds.Length;  // ù ��° ��� �̹����� �� ���� ���
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
                // ����� ���� ������ ������ �̵� (�� ��� �̹����� �ʺ� ��������)
                Vector3 nextPos = backgrounds[i].position;
                nextPos.x += backgrounds.Length * backgroundWidths[i];  // �� ��� �̹����� �ʺ� ���
                backgrounds[i].position = nextPos;
            }
        }
    }
}
