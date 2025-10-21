using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    [Header("洞物体的在编辑器中的父节点")]   
    public GameObject holeGoParent;
    [Header("洞预制体")]
    public GameObject holePrefab;
    [Header("猫预制体")]
    public GameObject catPrefab;
    [Header("行以及列")]
    public int row=3,column=3;
    [Header("洞口生成的初始位置")]
    public Vector2 originalPosition = new Vector2(-2,-2);
    [Header("行以及列的间距")]
    public float intervalX = 2, intervalY = 1;
    [Header("猫出现的频率")]
    public float catFrequency;
    [Header("其他Manager")]
    public SoundManager soundManager;
    public TimeManager timeManager;
    
    
    private int _holeLength = 0;
    private Hole[] _holes;
    private bool _canChangeFrequency;
    private float _changeFrequencyTime;
    void Start()
    {

        _canChangeFrequency = true;
        _holeLength = row * column;
        _holes = new Hole[_holeLength];
        Initial();
        ShowCatByFrequency(1f, 1f);
        _changeFrequencyTime = timeManager.gameTime / 3;
        timeManager.StartCountTime(true);
    }

    /********************
     * 生成地图，将洞和猫的物体存储在一维数组_holes中，其中
     * 把a[m][n]映射到b[k]的公式为:k=i*n+j
     * m、n分别表示二维数组的行数和列数,i为元素所在行,j为元素所在列，同时0<=i<m,0<=j<n。
     ******************/

    private void Initial()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                int index = i * column + j;
                float xPos = originalPosition.x + j * intervalX;
                float yPos = originalPosition.y + i * intervalY;
                GameObject holeGo = Instantiate(holePrefab, new Vector3(xPos,yPos
                    , 0), Quaternion.identity);
                holeGo.name = "hole"+index;
                holeGo.transform.parent = holeGoParent.transform;
                if (holeGo.TryGetComponent(out Hole hole))
                {
                    _holes[index] = hole;
                    hole.xPosition = (int)xPos;
                    hole.yPosition = (int)yPos;
                    GameObject catGo = Instantiate(catPrefab, new Vector3(xPos,yPos
                        , 0), Quaternion.identity);
                    hole.catGameObject = catGo;
                    hole.catGameObject.SetActive(false);
                    hole.catGameObject.name = "cat"+index;
                    hole.catGameObject.transform.parent = holeGo.transform;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeManager.gameTime<0)
        {
            GameOver();
        }

        if (timeManager.gameTime<_changeFrequencyTime&& 
            _canChangeFrequency)
        {
            ShowCatByFrequency(0,catFrequency/3);
            Debug.Log("加速生成");
            _canChangeFrequency = false;
        }
    }

    private void GameOver()
    {
        timeManager.StartCountTime(false);
        CancelInvoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(originalPosition,1f);
    }

    private void ShowCatByFrequency(float delayTime, float frequency)
    {
        CancelInvoke();
        InvokeRepeating(nameof(ShowCat),delayTime,frequency);
    }

    private void ShowCat()
    {
        List<int> readyToShow = new List<int>();
        for (int i = 0; i < _holes.Length; i++)
        {
            if (!_holes[i].catGameObject.activeInHierarchy)
            {
                readyToShow.Add(i);
            }
        }
        int index = Random.Range(0, readyToShow.Count);
        _holes[readyToShow[index]].catGameObject.SetActive(true);
        soundManager.PlayShowCat();
    }


    public void ReStartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
