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
    public GameObject beanPrefab;       // 普通豆子
    public GameObject powerBeanPrefab;  // 能量豆
    public GameObject cherryPrefab;     // 樱桃（Bonus）

    private int[,] levelMap;

    // Start is called before the first frame update
    void Start()
    {
        LoadCSV();  // 加载 CSV 文件
        GenerateLevel();  // 根据 CSV 数据生成关卡
    }

    // 加载 CSV 文件
    void LoadCSV()
    {
        // 从 Resources 文件夹加载 CSV 文件，不带文件扩展名
        TextAsset csvFile = Resources.Load<TextAsset>("PacManLevelMap");  
        if (csvFile != null)
        {
            // 使用能够处理不同换行符的拆分方法
            string[] lines = csvFile.text.Split(new[] { "\r\n", "\n", "\r" }, System.StringSplitOptions.None);

            List<string[]> validLines = new List<string[]>();  // 用于存储有效的数值行

            foreach (string line in lines)
            {
                // 去除行首尾的空格和控制字符
                string trimmedLine = line.Trim();

                // 如果行为空或不以数字开头，跳过该行
                if (string.IsNullOrWhiteSpace(trimmedLine) || !char.IsDigit(trimmedLine[0]))
                {
                    continue;
                }

                // 将修剪后的行拆分为数值部分
                string[] values = trimmedLine.Split(',');

                // 修剪每个值并添加到列表中
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Trim();
                }

                validLines.Add(values);
            }

            int rowCount = validLines.Count;

            // 检查是否有有效的行
            if (rowCount == 0)
            {
                Debug.LogError("CSV 文件中没有找到有效的数据行！");
                return;
            }

            int colCount = validLines[0].Length;

            // 验证每行的列数是否一致
            for (int i = 1; i < rowCount; i++)
            {
                if (validLines[i].Length != colCount)
                {
                    Debug.LogError($"第 {i + 1} 行的列数 ({validLines[i].Length}) 与预期的列数 ({colCount}) 不一致。");
                    return;
                }
            }

            levelMap = new int[rowCount, colCount];

            // 将有效的数值行存入 levelMap
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    // 去除值首尾的空格和控制字符
                    string value = validLines[i][j].Trim();

                    int parsedValue;
                    if (int.TryParse(value, out parsedValue))
                    {
                        levelMap[i, j] = parsedValue;
                    }
                    else
                    {
                        Debug.LogError($"在第 {i + 1} 行，第 {j + 1} 列，无法解析整数：'{value}'");
                        // 根据需要处理错误，例如设置为默认值
                        levelMap[i, j] = 0; // 或者您选择的默认值
                    }
                }
            }
        }
        else
        {
            Debug.LogError("在 Resources 文件夹中未找到 CSV 文件！");
        }
    }

    // 根据 CSV 文件内容生成关卡
    void GenerateLevel()
    {
        if (levelMap == null)
        {
            Debug.LogError("关卡地图未初始化！");
            return;
        }

        for (int i = 0; i < levelMap.GetLength(0); i++)
        {
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {
                int tileType = levelMap[i, j];
                Vector3 position = new Vector3(j, -i, 0);  // 确定每个物体在场景中的位置

                // 根据 tileType 的值生成不同的物体
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
                        Instantiate(beanPrefab, position, Quaternion.identity);  // 普通豆子
                        break;
                    case 6:
                        Instantiate(powerBeanPrefab, position, Quaternion.identity);  // 能量豆
                        break;
                    case 7:
                        Instantiate(tWallPrefab, position, Quaternion.identity);
                        break;
                    case 8:
                        Instantiate(cherryPrefab, position, Quaternion.identity);  // 樱桃
                        break;
                    default:
                        // 对于未知的 tileType，可以选择输出警告或忽略
                        break;
                }
            }
        }
    }
}
