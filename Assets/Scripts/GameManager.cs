using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Music : IComparable<Music>
{
    public int no;
    public string genre;
    public int playNum;

    public Music(int no, string genre, int playNum)
    {
        this.no = no;
        this.genre = genre;
        this.playNum = playNum;
    }
    public int CompareTo(Music other)
    {
        return this.playNum.CompareTo(other.playNum);
    }
}


public class Solution
{


    #region 예전거
    public int[] solution1(string[] genres, int[] plays)
    {
        List<int> answerList = new List<int>();

        List<Music> musicList = new List<Music>();
        Dictionary<string, List<Music>> dic = new Dictionary<string, List<Music>>();
        for (int i = 0; i < genres.Length; i++)
        {
            musicList.Add(new Music(i, genres[i], plays[i]));
        }
        
        foreach(Music music in musicList) 
        {
            if (!dic.ContainsKey(music.genre))
            {
                dic[music.genre] = new List<Music>();
            }
            dic[music.genre].Add(music);
        }
        //다넣아ㅓㅆ다
        Dictionary<string,int> musicTotal = new Dictionary<string,int>();
        List<string> testName = new List<string>();
        List<int> ints = new List<int>();

        foreach(var musicVal in dic)
        {
            foreach(Music nn in musicVal.Value)
            {
                if (!musicTotal.ContainsKey(nn.genre))
                {
                    musicTotal.Add(nn.genre, 0);
                }
                musicTotal[nn.genre] += nn.playNum; 
            }
            //Debug.Log("합계 : " + musicVal.Key + musicTotal[musicVal.Key]);
        }
        ints = musicTotal.Values.ToList();
        testName = musicTotal.Keys.ToList();
        List<string> names = new List<string>();
        int maxTemp = ints.Count;
        for (int i = 0; i < maxTemp; i++)
        {            
            int maxIndex = ints.IndexOf(ints.Max());
            names.Add(testName[maxIndex]);
            ints[maxIndex] = -1;
        }
        int num = 0;
        //여길 딕셔너리 갯수만큼 돌리기
        for (int i = 0; i< musicTotal.Count; i++)
        {
            dic[names[i]].Sort();
            dic[names[i]].Reverse();
            if (dic[names[i]].Count >= 2)
            {
                if (dic[names[i]][0].no > dic[names[i]][1].no && dic[names[i]][0].playNum == dic[names[i]][1].playNum)
                {
                    answerList.Add(dic[names[i]][1].no);
                    answerList.Add(dic[names[i]][0].no);
                }
                else
                {
                    answerList.Add(dic[names[i]][0].no);
                    answerList.Add(dic[names[i]][1].no);
                }
            }
            else
            {
                answerList.Add(dic[names[i]][0].no);
            }
                      
            num++;
        }    
        int[] answer = answerList.ToArray();
        return answer;
    }
    public int solution(int n, int[] stations, int w)
    {
        if (stations.Length == 0) return n / (2 * w + 1);
        int answer = 0;
        List<int> calNum = new List<int>();
        foreach(int i in stations)
        {
            if(calNum.Count == 0)
            {
                if(i - w > 1)
                {
                    calNum.Add(i - w - 1);
                }
                else
                {
                    calNum.Add(0);
                }                
            }
            else
            {
                int temp = (i - w - 1) - (stations[calNum.Count - 1] + w);
                
                if (temp < 0)
                {
                    calNum.Add(0);
                }
                else
                {
                    
                    calNum.Add(temp);
                }
                
            }
        }
        if(stations[stations.Length - 1] + w < n && calNum.Count != stations.Length + 1)
        {
            calNum.Add(n - (stations[calNum.Count - 1] + w));
        }
        foreach(int i in calNum)
        {
            double result = (double)(i / (w * 2 + 1.0));
            if (result % 1 != 0)
            {
                answer += i / (w * 2 + 1) + 1;
            }
            else
            {
                answer += (int)result;
            }
        }
        return answer;
    }
    #endregion
}
#region
/*
  public int solution(string begin, string target, string[] words)
    {
        int answer = 0;
        char[] tempWords;
        char[] beginWords = begin.ToCharArray();
        char[] targetWords = target.ToCharArray();
        int maxNum = 0;
        int[] isChangeInt = new int[targetWords.Length];
        Dictionary<int, bool> isVisited = new Dictionary<int, bool>();
        for(int i = 0; i< words.Length;i++)
        {
            isVisited.Add(i, false);
        }
        if (!words.Contains(target))
        {
            return 0;
        }
        for (int j = 0; j < targetWords.Length; j++)
        {
            isChangeInt[j] = Math.Abs(targetWords[j] - beginWords[j]);
            if (targetWords[j] - beginWords[j] == 0)
            {
                maxNum++;
            }
        }

        for (int k = 0; k < words.Length; k++)//단어를 하나씩 체크
        {
            int num = 0;
            int check = 0;
            bool isChange = false;


            tempWords = words[k].ToCharArray();//현재 단어를 넣음

            for (int i = 0; i < tempWords.Length; i++)//타겟 단어를 
            {
                if (tempWords[i] == targetWords[i])
                {
                    num++;
                }

                int tempNum = Math.Abs(targetWords[i] - tempWords[i]);
                int tempNum2 = Math.Abs(targetWords[i] - beginWords[i]);
                if (isChangeInt[i] > 0 && isChangeInt[i] > tempNum)
                {
                    check++;
                }
            }
            if(check == 0)
            {
                isVisited[k] = true;
            }
            if (check == 1 && num >= maxNum)
            {
                for (int n = 0; n < tempWords.Length; n++)
                {
                    isChangeInt[n] = Math.Abs(targetWords[n] - tempWords[n]);
                }
                isVisited[k] = true;
                isChange = true;
            }
            else if(check > 1 && k < words.Length -1)
            {
                for(int i = 0; i < words.Length; i++)
                {
                    while (isVisited[i] == true)
                    {
                        i++;
                    }
                    num = 0;
                    check = 0;
                    for (int j = 0; j < tempWords.Length; j++)//타겟 단어를 
                    {
                        if (tempWords[j] == targetWords[j])
                        {
                            num++;
                        }

                        int tempNum = Math.Abs(targetWords[j] - tempWords[j]);
                        int tempNum2 = Math.Abs(targetWords[j] - beginWords[j]);
                        if (isChangeInt[j] > 0 || isChangeInt[j] > tempNum)
                        {
                            check++;
                            break;
                        }
                    }
                    if(check == 1 && num >= maxNum)
                    {
                        maxNum = num;
                        beginWords = tempWords;
                        isVisited[i] = true;
                        answer++;
                        break;
                    }
                }
            }
            if (isChange && num >= maxNum)
            {
                maxNum = num;
                answer++;
                beginWords = tempWords;
            }
            if (beginWords == targetWords)
            {
                return answer;
            }
        }
        return answer;
    }
    public void Swap(string[] temps,int a, int b)
    {
        string temp;
        temp = temps[a];
        temps[a] = temps[b];
        temps[b] = temp;
    }
 */
#endregion

public class GameManager : MonoBehaviour
{
    //string[] answer = new string[] { };
    //int[] A = new int[] { 1,16 };
    //int[] B = new int[] { 100000000 };
    //int[] C = new int[] { 1, 5 };
    //int[] D = new int[] { 1,16 };
    //string[] genres = new string[] { "pop", "pop", "pop", "rap", "rap" };
    int[] plays = new int[] { 45, 50, 40, 60, 70 };

    int[] stickers = new int[] { 14, 6, 5, 11, 3, 9, 2, 10 };
    //int[] stickers = new int[] { 5, 1, 16, 17, 16 };
    
    private void Start()
    {
        Solution sol = new Solution();
       // Debug.Log(sol.solution(stickers));

        //A.GetHashCode();
        //Solution sol = new Solution();
        //Debug.Log(sol.solution(200000000, B, 5));
        //foreach(int a in sol.solution1(genres, plays))
        //{
        //    Debug.Log(a);
        //}
    }
}
