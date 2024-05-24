using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using STORYGAME;

#if UNITY_EDITOR
[CustomEditor(typeof(GameSystem))]
public class GameSysteEdiot : Editor       //에디터를 상속받는 클래스 생성
{
        public override void OnInspectorGUI()           //유니티의 인스펙터 함수를 재정의
        {
            base.OnInspectorGUI();                      //유니티 인스펙터 함수 동작을 같이 한다. (Base)

            GameSystem gameSystem = (GameSystem)target;

            //Reset Story Models 버튼 생성
            if (GUILayout.Button("Reset Story Models"))
            {
                gameSystem.ResetStroyModles();
            }
        }
}
#endif
public class GameSystem : MonoBehaviour
{
        public static GameSystem instance;

        private void Awake()
        {
            instance = this;
        }

        public enum GAMESTATE 
        { 
            STORYSHOW,
            WAITSELECT,
            STORYEND
        }

        public Stats stats;
        public GAMESTATE currentState;
        public int currentStoryIndex = 1;
        public StoryModel[] storyModels;
        

#if UNITY_EDITOR
        [ContextMenu("Reset Story Models")]
        public void ResetStroyModles()
        {
            storyModels = Resources.LoadAll<StoryModel>(""); //Resources 폴더 아래 모든 StoryModel을 불러 오기 
        }
#endif

    public void StoryShow(int number)
    {
        StoryModel tempStoryModel = FindStoryModel(number);

        //StorySystem.Instance.currentStoryModel = tempStoryModel;
        //StroySystem.Instance.CoShowText();
    }

    StoryModel FindStoryModel(int number)
    {
        StoryModel tempStoryModels = null;
        for (int i = 0; i < storyModels.Length; i++)
        {
            if (storyModels[i].storyNumber == number)
            {
                tempStoryModels = storyModels[i];
                break;
            }
        }
        return tempStoryModels;
    }

    StoryModel RandomStory()
    {
        StoryModel tempStoryModels = null;

        List<StoryModel>storyModelList = new List<StoryModel>();

        for (int i = 0; i < storyModels.Length; i++)
        {
            if (storyModels[i].storyType == StoryModel.STORYTYPE.MAIN)
            {
                storyModelList.Add(storyModels[i]);
            }
        }
        tempStoryModels = storyModelList[Random.Range(0, storyModelList.Count)]; //리스트에서 랜덤으로 하나 선택
        currentStoryIndex = tempStoryModels.storyNumber;
        return tempStoryModels;
    }
}

