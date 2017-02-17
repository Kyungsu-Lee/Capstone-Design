using UnityEngine;
using System.Collections;
using SimpleJSON;

public class DataMgr : MonoBehaviour
{
    //싱글턴 인스턴스 선언
    public static DataMgr instance = null;

    //MySQL데이터 베이스 사용을 위해 부여된 고유번호
    private const string seqNo = "7777";
    //점수 저장 PHP주소
   // private string urlSave = "http://ec2-52-78-85-116.ap-northeast-2.compute.amazonaws.com/save_score.php";
    private string urlSave = "poiylmasi.iptime.org:3000";
    //랭킹 정보를 요청하기 위한 php주소
    private string urlScoreList = "http://ec2-52-78-85-116.ap-northeast-2.compute.amazonaws.com/get_score_list.php";


    void Awake()
    {
        //싱글턴 인스턴스 할당
        instance = this;

    }

    //점수저장을 위한 코루틴 함수
    public IEnumerator SaveScore(string user_name, int killCount)
    {
        //POST방식으로 인자를 전달하기 위한 FORM선언
        WWWForm form = new WWWForm();
        //전달할 파라미터 설정
        //  form.AddField("user_name", user_name);
        //   form.AddField("kill_count", killCount);
        //    form.AddField("seq_no", seqNo);
        form.AddField("well", 123);

        //url호출
        var www = new WWW(urlSave, form);

        //완료시점까지 대기
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.text);
        }
        else
        {
            Debug.Log("Error : " + www.error);
        }

        //점수저장후 랭킹정보 요청을 위한 코루틴 함수 호출
     //   StartCoroutine(this.GetScoreList());

    }

    //랭킹정보 요청하는 코루틴 함수
    public IEnumerator GetScoreList()
    {
        //post방식으로 인자를 전달하기 위한 form선언
        WWWForm form = new WWWForm();
        form.AddField("seq_no", seqNo);

        //url 호출
        var www = new WWW(urlScoreList, form);
        //완료시점까지 대기
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.text);
            //점수 표시 함수 호출
            DispScoreList(www.text);
        }
        else
        {
            Debug.Log("Error : " + www.error);
        }

    }
    //JSON 파일을 파싱한 후 점수를 표시하는 함수
    void DispScoreList(string strJsonData)
    {
        //JSON파일 파싱
        var N = JSON.Parse(strJsonData);

        //JSON오브젝트의 배열만큼 순회
        for (int i = 0; i < N.Count; i++)
        {
            int ranking = N[i]["ranking"].AsInt;
            string userName = N[i]["user_name"].ToString();
            int killCount = N[i]["kill_count"].AsInt;
            //결과값을 콘솔뷰에 표시
            Debug.Log(ranking.ToString() + userName + killCount.ToString());
        }
    }
}