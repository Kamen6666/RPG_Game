using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MovingShot : MonoBehaviour
{
    public Image plane;
    public Text GameName;
    public Text Log;
    public Button newGame;
    public Button LoadGame;
    public GameObject bg;
    private Vector3 StartPoint;
    private Vector3 StartRotation;
    private Vector3 EndPoint;
    private Vector3 EndRotation;
    private bool isOK = true;

    void Awake()
    {
        StartPoint = new Vector3(-10f, 115f, 186f);
        StartRotation = new Vector3(30f, 88f, 0f);

        EndPoint = new Vector3(126f, 44f, 190f);
        EndRotation = new Vector3(22f, 90f, 0f);

        GameName.gameObject.SetActive(false);
        Log.gameObject.SetActive(false);

        newGame.gameObject.SetActive(false);
        LoadGame.gameObject.SetActive(false);

        isOK = false;
    }
    void Start()
    {
        //获取初始位置
        transform.position = StartPoint;
        //获取初始角度
        transform.eulerAngles = StartRotation;
        //遮布初始化
        plane.color = new Color(1, 1, 1, 0);
        //启动协成
        StartCoroutine(MovingCamera());
    }
    void Update()
    {
        if (Input.anyKeyDown && isOK)
        {
            //防止连续按键 开关
            isOK = false;
            //停止协成
            StopCoroutine(MovingCamera());
            //模式选择协成
            if(!isfinish)
                StartCoroutine(Select());
        }
    }
    bool isfinish = false;
    //摄像头运境协成
    IEnumerator MovingCamera()
    {
        //等候玩家加载游戏
        yield return new WaitForSeconds(0.5f);
        //运境
        while (plane.gameObject.activeSelf)
        {
            transform.position = Vector3.Lerp(transform.position, EndPoint, Time.deltaTime * 1f);
            plane.color = new Color(1, 1, 1, (1 - Time.time / 6f));
            if (plane.color.a < 0.2f)
                plane.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.02f);
        }
        float a = 0f;
        //显示提示 标题
        GameName.gameObject.SetActive(true);
        Log.gameObject.SetActive(true);
        //提示标签呼吸效果
        while (true)
        {
            a += Time.deltaTime;
            if (a > 1f)
                a -= 1f;
            Log.color = new Color(1, 1, 1, a);
            yield return new WaitForSeconds(0.02f);
            isOK = true;
        }
    }
    //显示按钮
    IEnumerator Select()
    {
        isfinish = true;
        //题目离开
        while (GameName.gameObject.activeSelf)
        {
            if (GameName.rectTransform.localPosition.y > 200f)
                GameName.gameObject.SetActive(false);
            GameName.rectTransform.localPosition += new Vector3(0, 10f, 0);
            yield return new WaitForSeconds(0.02f);
        }
        //标签关闭
        Log.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);

        //显示按钮 选择
        newGame.gameObject.SetActive(true);

        LoadGame.gameObject.SetActive(true);
        bg.SetActive(true);
        PlayerDataFramework.GetInstance().OpenSQLDataBase("RPG_Game");
        //如果没有存档 按钮变灰色
        if (!PlayerDataFramework.GetInstance().SelectIsHavePlayerName())
            LoadGame.interactable = false;
        StartCoroutine(LoadScene("SelectGame"));

        StopCoroutine(Select());
    }
    void OnApplicationQuit()
    {
        PlayerDataFramework.GetInstance().CloseDataBase();
    }
    AsyncOperation SG;
    AsyncOperation SG2;
    //AsyncOperation LoadGame = SceneManager.LoadSceneAsync("SelectGame");
    //新游戏
    public void CreateNewGame()
    {
        if (SG.progress > 0.8f)
            SG.allowSceneActivation = true;
    }
    //场景预先加载协成
    IEnumerator LoadScene(string name)
    {
        SG = SceneManager.LoadSceneAsync(name);
        SG.allowSceneActivation = false;
        while (!SG.isDone)
        {
            //场景预先加载
            yield return 0;
        }
    } 

    //是否存在游戏记录
    public void IsHaveGame()
    {
        //加载游戏场景
        SceneManager.LoadScene("GameScene");
    }


}
