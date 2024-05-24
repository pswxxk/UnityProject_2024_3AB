using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using STORYGAME;

#if UNITY_EDITOR
[CustomEditor(typeof(GameSystem))]
public class GameSysteEdiot : Editor       //�����͸� ��ӹ޴� Ŭ���� ����
{
        public override void OnInspectorGUI()           //����Ƽ�� �ν����� �Լ��� ������
        {
            base.OnInspectorGUI();                      //����Ƽ �ν����� �Լ� ������ ���� �Ѵ�. (Base)

            GameSystem gameSystem = (GameSystem)target;

            //Reset Story Models ��ư ����
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
            storyModels = Resources.LoadAll<StoryModel>(""); //Resources ���� �Ʒ� ��� StoryModel�� �ҷ� ���� 
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
        tempStoryModels = storyModelList[Random.Range(0, storyModelList.Count)]; //����Ʈ���� �������� �ϳ� ����
        currentStoryIndex = tempStoryModels.storyNumber;
        return tempStoryModels;
    }
}

