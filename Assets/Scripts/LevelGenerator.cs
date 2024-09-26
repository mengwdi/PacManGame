using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelGenerator : MonoBehaviour
{
    public GameObject insideCornerPrefab;
    public GameObject insideWallPrefab;
    public GameObject outsideCornerPrefab;
    public GameObject outsideWallPrefab;
    public GameObject tWallPrefab;
    public GameObject beanPrefab;       // ��ͨ����
    public GameObject powerBeanPrefab;  // ������
    public GameObject cherryPrefab;     // ӣ�ң�Bonus��

    private int[,] levelMap;

    // Start is called before the first frame update
    void Start()
    {
        LoadCSV();  // ���� CSV �ļ�
        GenerateLevel();  // ���� CSV �������ɹؿ�
    }

    // ���� CSV �ļ�
    void LoadCSV()
    {
        // �� Resources �ļ��м��� CSV �ļ��������ļ���չ��
        TextAsset csvFile = Resources.Load<TextAsset>("PacManLevelMap");  
        if (csvFile != null)
        {
            // ʹ���ܹ�����ͬ���з��Ĳ�ַ���
            string[] lines = csvFile.text.Split(new[] { "\r\n", "\n", "\r" }, System.StringSplitOptions.None);

            List<string[]> validLines = new List<string[]>();  // ���ڴ洢��Ч����ֵ��

            foreach (string line in lines)
            {
                // ȥ������β�Ŀո�Ϳ����ַ�
                string trimmedLine = line.Trim();

                // �����Ϊ�ջ������ֿ�ͷ����������
                if (string.IsNullOrWhiteSpace(trimmedLine) || !char.IsDigit(trimmedLine[0]))
                {
                    continue;
                }

                // ���޼�����в��Ϊ��ֵ����
                string[] values = trimmedLine.Split(',');

                // �޼�ÿ��ֵ����ӵ��б���
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Trim();
                }

                validLines.Add(values);
            }

            int rowCount = validLines.Count;

            // ����Ƿ�����Ч����
            if (rowCount == 0)
            {
                Debug.LogError("CSV �ļ���û���ҵ���Ч�������У�");
                return;
            }

            int colCount = validLines[0].Length;

            // ��֤ÿ�е������Ƿ�һ��
            for (int i = 1; i < rowCount; i++)
            {
                if (validLines[i].Length != colCount)
                {
                    Debug.LogError($"�� {i + 1} �е����� ({validLines[i].Length}) ��Ԥ�ڵ����� ({colCount}) ��һ�¡�");
                    return;
                }
            }

            levelMap = new int[rowCount, colCount];

            // ����Ч����ֵ�д��� levelMap
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    // ȥ��ֵ��β�Ŀո�Ϳ����ַ�
                    string value = validLines[i][j].Trim();

                    int parsedValue;
                    if (int.TryParse(value, out parsedValue))
                    {
                        levelMap[i, j] = parsedValue;
                    }
                    else
                    {
                        Debug.LogError($"�ڵ� {i + 1} �У��� {j + 1} �У��޷�����������'{value}'");
                        // ������Ҫ���������������ΪĬ��ֵ
                        levelMap[i, j] = 0; // ������ѡ���Ĭ��ֵ
                    }
                }
            }
        }
        else
        {
            Debug.LogError("�� Resources �ļ�����δ�ҵ� CSV �ļ���");
        }
    }

    // ���� CSV �ļ��������ɹؿ�
    void GenerateLevel()
    {
        if (levelMap == null)
        {
            Debug.LogError("�ؿ���ͼδ��ʼ����");
            return;
        }

        for (int i = 0; i < levelMap.GetLength(0); i++)
        {
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {
                int tileType = levelMap[i, j];
                Vector3 position = new Vector3(j, -i, 0);  // ȷ��ÿ�������ڳ����е�λ��

                // ���� tileType ��ֵ���ɲ�ͬ������
                switch (tileType)
                {
                    case 1:
                        Instantiate(outsideCornerPrefab, position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(outsideWallPrefab, position, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(insideCornerPrefab, position, Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(insideWallPrefab, position, Quaternion.identity);
                        break;
                    case 5:
                        Instantiate(beanPrefab, position, Quaternion.identity);  // ��ͨ����
                        break;
                    case 6:
                        Instantiate(powerBeanPrefab, position, Quaternion.identity);  // ������
                        break;
                    case 7:
                        Instantiate(tWallPrefab, position, Quaternion.identity);
                        break;
                    case 8:
                        Instantiate(cherryPrefab, position, Quaternion.identity);  // ӣ��
                        break;
                    default:
                        // ����δ֪�� tileType������ѡ�������������
                        break;
                }
            }
        }
    }
}
