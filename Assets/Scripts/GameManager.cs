using UnityEngine;
using System.IO;
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    //public string savePath;
    public UserData data;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    //private void Start()
    //{
    //    savePath = Path.Combine(Application.persistentDataPath, "UserData.json"); //OS에 맞게 자동으로 경로 설정
    //    LoadUserData();
    //}
    //public void SaveUserData(UserData data)
    //{
    //    string json = JsonUtility.ToJson(data, true); // JSON 변환
    //    File.WriteAllText(savePath, json); // 파일로 저장
    //    Debug.Log($"데이터 저장 완료: {savePath}");
    //}

    //public void LoadUserData()
    //{
    //    if (IsSuccesLoad() == true)
    //    {
    //        string json = File.ReadAllText(savePath); // 파일에서 읽기
    //        UserData data = JsonUtility.FromJson<UserData>(json);
    //        Debug.Log($"데이터 로드 완료: {data.userName}, 잔액: {data.balance}, 현금: {data.cash}");
    //    }
    //    else
    //    {
    //        Debug.Log("저장된 데이터가 없음.");
    //    }
    //}

    //public bool IsSuccesLoad()
    //{
    //    if (File.Exists(savePath) && data != null && data.userName != "") return true;
    //    else return false;
    //}
    public void SaveUserData()
    {
        PlayerPrefs.SetString("userName", data.userName);
        PlayerPrefs.SetInt("cash", data.cash);
        PlayerPrefs.SetInt("balance", data.balance);
        PlayerPrefs.Save();
    }

    public void LoadUserData()
    {
        if (data != null || PlayerPrefs.HasKey("userName") || PlayerPrefs.HasKey("cash") || PlayerPrefs.HasKey("balance"))
        {
            GameManager.Instance.data.userName = PlayerPrefs.GetString("userName", "염예찬");
            GameManager.Instance.data.cash = PlayerPrefs.GetInt("cash", 100000);
            GameManager.Instance.data.balance = PlayerPrefs.GetInt("balance", 50000);
            //Refresh();
            //Debug.Log("PlayerPrefs 반영 완료");
        }
        else
            GameManager.Instance.data = new UserData("염예찬", 100000, 50000);
    }
}
