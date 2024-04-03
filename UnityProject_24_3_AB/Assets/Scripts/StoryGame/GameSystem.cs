using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

namespace STORYGAME
{

#if UNITY_EDITOR                                //전처리기 유니티 에디터에서만 동작
    [CustomEditor(typeof(GameSystem))]
    public class GameSysteEditor : Editor       //에디터를 상속받는 클래스 생성
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
        public static GameSystem instance;              //간단한 싱글톤 화

        private void Awake()
        {
            instance = this;
        }

        public enum GAMESTATE
        {
            STORYSHOW,
            WAITSELECT,
            STORYEND,
            BATTLEMODE,
            BATTLEDONE,
            SHOPMODE,
            ENDMODE,
        }

        public GAMESTATE currentState;
        public StroyTableObject[] storyModels;
        public int currentStoryInex = 1;

#if UNITY_EDITOR
        [ContextMenu("Reset Story Models")]
        public void ResetStroyModles()
        {
            storyModels = Resources.LoadAll<StroyTableObject>(""); //Resources 폴더 아래 모든 StoryModel을 불러 오기 
        }
#endif
    }
}