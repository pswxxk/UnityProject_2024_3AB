using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParabolicTrajectory : MonoBehaviour
{
    public Slider sliderAngle;
    public Slider sliderDirection;
    public Slider sliderPower;
    public LineRenderer lineRenderer;       //Line Renderer ������Ʈ �Ҵ�

    public int resloution = 30;             //������ �׸� �� ����� ���� ����
    public float timeStep = 0.1f;           //�ð� ���� (0.1�ʸ��� ���� ����)

    public Transform launchPoint;           //�߻� ��ġ�� ��Ÿ���� Ʈ������
    public Transform pivotPoint;

    public float launchPower;               //�߻� �ӵ�
    public float launchAngle;               //�߻� �ӵ� (�� ����)
    public float launchDirection;           //�߻� ���� (XZ ��鿡���� ����, �� ����
    public float gravity = -9.8f;           //�߷� ��

    public GameObject projectilePrefabs;    //�߻��� ��ü�� ������

    void Start()
    {
        lineRenderer.positionCount = resloution;            //Line Renderer�� �� ������ ����

        sliderAngle.onValueChanged.AddListener(sliderAngleValue);
        sliderDirection.onValueChanged.AddListener(SliderDirectionValue);
        sliderPower.onValueChanged.AddListener(SliderPowerValue);
    }

    // Update is called once per frame
    void Update()
    {
        RenderTrajectory();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject temp = Instantiate(projectilePrefabs);
            LaunchProjectile(temp);
        }
    }

    void sliderAngleValue(float angle)
    {
        launchAngle = angle;
    }

    void SliderDirectionValue(float angle)
    {
        launchAngle = angle;
    }

    void SliderPowerValue(float power)
    {
        launchPower= power;
    }


    void RenderTrajectory()                                             //������ ����ϰ� Line Renderer�� �����ϴ� �Լ�
    {
        Vector3[] points = new Vector3[resloution];                     //���� ������ ������ �迭

        for(int i = 0; i < resloution; i++)
        {
            float time = i * timeStep;                                  //���� �ð� ���
            points[i] = CalculatePositionAtTime(time);
        }
        lineRenderer.SetPositions(points);                               //Line Renderer�� ����Ʈ ������ �ѱ��
    }

    Vector3 CalculatePositionAtTime(float time)                         //�־��� �ð����� ��ü�� ��ġ�� ����ϴ� �Լ�
    {
        float launchAngleRad = Mathf.Rad2Deg * launchAngle;             //�߻� ������ �������� ��ȯ
        float launchDirectionRad = Mathf.Rad2Deg * launchDirection;     //�߻� ������ �������� ��ȯ

        //�ð� t������ x,y,z ��ǥ ���

        float x = launchPower * time * Mathf.Cos(launchAngleRad) * Mathf.Cos(launchDirectionRad);
        float y = launchPower * time * Mathf.Cos(launchAngleRad) * Mathf.Sin(launchDirectionRad);
        float z = launchPower * time * Mathf.Sin(launchAngleRad) + 0.5f * gravity * time * time;

        return launchPoint.position + new Vector3(x, y, z);
    }

    public void LaunchProjectile(GameObject projectile)
    {
        projectile.transform.position = launchPoint.position;
        projectile.transform.rotation = launchPoint.rotation;
        projectile.transform.SetParent(null);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if(rb != null)
        {
            rb.isKinematic = false;

            //�߻� ������ ������ �������� ��ȯ
            float launchAngleRad = Mathf.Rad2Deg * launchAngle;             //�߻� ������ �������� ��ȯ
            float launchDirectionRad = Mathf.Rad2Deg * launchDirection;     //�߻� ������ �������� ��ȯ

            float initialVelocityX = launchPower * Mathf.Cos(launchAngleRad) * Mathf.Cos(launchDirectionRad);
            float initialVelocityY = launchPower * Mathf.Cos(launchAngleRad) * Mathf.Sin(launchDirectionRad);
            float initialVelocityZ = launchPower * Mathf.Sin(launchAngleRad);

            Vector3 initialVelocity = new Vector3(initialVelocityX, initialVelocityY, initialVelocityZ);

            rb.velocity = initialVelocity;
        }
    }
}
