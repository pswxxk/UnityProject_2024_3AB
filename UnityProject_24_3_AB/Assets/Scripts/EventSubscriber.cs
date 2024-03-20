using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSubscriber : MonoBehaviour
{
    public ExEventChannel eventChannel;

    void OnEventRaised()
    {
        Debug.Log(gameObject.name + " ���� �̺�Ʈ �߻�");
    }

    private void OnEnable()
    {
        eventChannel.OnEventRaised.AddListener(OnEventRaised);
    }

    private void OnDisable()    //��Ȱ��ȭ �� �� �̺�Ʈ ����
    {
        eventChannel.OnEventRaised.RemoveListener(OnEventRaised);
    }
}
