using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using STORYGAME;
using UnityEngine.XR;

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

    public void Start()
    {
        ChangeState(GAMESTATE.STORYSHOW);
    }


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

        StorySystem.instance.currentStoryModel = tempStoryModel;
        StorySystem.instance.CoShowText();
    }

    public void ChangeState(GAMESTATE temp)     //���� ���� ���� �Լ� �߰�
    {
        currentState = temp;

        if(currentState == GAMESTATE.STORYSHOW)
        {
            StoryShow(currentStoryIndex);       //���丮 ���
        }
    }

    public void ChangeStats(StoryModel.Result result)           //���� ���� ����
    {
        if (result.stats.hpPoint > 0) stats.hpPoint += result.stats.hpPoint;
        if (result.stats.spPoint > 0) stats.spPoint += result.stats.spPoint;

        if (result.stats.currentHpPoint > 0) stats.currentHpPoint += result.stats.currentHpPoint;
        if (result.stats.currentSpPoint > 0) stats.currentSpPoint += result.stats.currentSpPoint;
        if (result.stats.currentXpPoint > 0) stats.currentXpPoint += result.stats.currentXpPoint;

        if (result.stats.strength > 0) stats.strength += result.stats.strength;
        if (result.stats.dexterity > 0) stats.dexterity += result.stats.dexterity;
        if (result.stats.consitiution > 0) stats.consitiution += result.stats.consitiution;
        if (result.stats.wisdom > 0) stats.wisdom += result.stats.wisdom;
        if (result.stats.Intelligence > 0) stats.Intelligence += result.stats.Intelligence;
        if (result.stats.charisma > 0) stats.charisma += result.stats.charisma;
    }

    public void ApplyChoice(StoryModel.Result result)
    {
        switch (result.resultType)
        {
            case StoryModel.Result.ResultType.ChangeHp:
                stats.currentHpPoint += result.value;
                ChangeStats(result);
                break;

            case StoryModel.Result.ResultType.AddExperience:
                stats.currentXpPoint += result.value;
                ChangeStats(result); 
                break;

            case StoryModel.Result.ResultType.GoToNextStory:
                currentStoryIndex = result.value;
                ChangeState(GAMESTATE.STORYSHOW);
                ChangeStats(result);
                break;

            case StoryModel.Result.ResultType.GoToRandomStory:
                RandomStory();
                ChangeState(GAMESTATE.STORYSHOW);
                ChangeStats(result);
                break;
            default:
                Debug.LogError("Unknown type");
                break;
        }

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

