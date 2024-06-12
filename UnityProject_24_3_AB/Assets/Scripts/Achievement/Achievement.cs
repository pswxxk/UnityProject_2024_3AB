using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement
{
    public string name;
    public string description;
    public bool isUnlocked;
    public int currentProgress;     //���� ����
    public int goal;                //Ƚ��

    public Achievement(string name, string description, int goal)       //�����ڿ��� �ʱ�ȭ{
    {
        this.name = name;
        this.description = description;
        this.isUnlocked = false;
    }
    public void AddProgress(int amount)
    {
        if (!isUnlocked)
        {
            currentProgress += amount;
            if (currentProgress >= goal)
            {
                isUnlocked = true;
                OnAchievementUnlocked();
            }
        }
    }

    protected virtual void OnAchievementUnlocked()
    {
        Debug.Log($"���� �޼� : {name} ");
    }
}
