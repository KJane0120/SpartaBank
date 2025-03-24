using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public UserData data;

    public static readonly string Name = "/Name";
    public static readonly string PW = "/Password";
    public static readonly string Cash = "/Cash";
    public static readonly string Balance = "/Balance";

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

    public void SaveUserData(string ID, string pw, string name, int cash, int balance)
    {
        PlayerPrefs.SetString(ID + PW, pw);
        PlayerPrefs.SetString(ID + Name, name);
        PlayerPrefs.SetInt(ID + Cash, cash);
        PlayerPrefs.SetInt(ID + Balance, balance);

        PlayerPrefs.Save();

        UserData newdata = new UserData(ID, pw, name, cash, balance);
    }

    public void LoadUserData(string ID)
    {
        if (PlayerPrefs.HasKey(ID + PW))
        {
            GameManager.Instance.data.ID = ID; //얘 추가 안 해줘서 계정생성 시 데이터와 뱅크때 데이터가 따로 놀고 있었음
            GameManager.Instance.data.PW = PlayerPrefs.GetString(ID + PW, "null");
            GameManager.Instance.data.Name = PlayerPrefs.GetString(ID + Name, "null");
            GameManager.Instance.data.cash = PlayerPrefs.GetInt(ID + Cash, 0);
            GameManager.Instance.data.balance = PlayerPrefs.GetInt(ID + Balance, 0);
        }
        else
            GameManager.Instance.data = new UserData("null", "null", "null", 0, 0);
    }
}
