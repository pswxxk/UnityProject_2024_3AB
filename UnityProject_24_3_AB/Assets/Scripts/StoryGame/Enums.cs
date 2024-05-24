using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STORYGAME
{
    public class Enums 
    { 
        public enum StoryType
        {
            MAIN,
            SUB,
            SERIAL
        }


        [System.Serializable]
        public enum EvenType
        {
            NONE,
            GoToBattle = 100,
            CheckSTR = 1000,
        }

        [System.Serializable]
        public enum ResultType
        {
            ADDExpreience,
            GoToNextStory,
            GoToRandomStory,
        }


    }

    [System.Serializable]
    public class Stats 
    {
        public int hpPoint;
        public int spPoint;

        public int currentHpPoint;
        public int currentSpPoint;
        public int currentXpPoint;

        //기본 스텟 설정
        public int strength;        //STR
        public int dexterity;       //DEX
        public int consitiution;    //CON
        public int Intelligence;    //INT
        public int wisdom;          //WIS
        public int charisma;        //CHA


    }


}

