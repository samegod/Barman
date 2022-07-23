using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] int dotsNumber;
    [SerializeField] GameObject dotsParent;
    [SerializeField] GameObject dotsPrefab;
    [SerializeField] float dotSpacing;
    public float k;

    Vector3 pos;
    float timeStamp;
    Transform[] dotsList;

    private void Start()
    {
        Hide();

        PrepareDots();
    }

    void PrepareDots()
    {
        dotsList = new Transform[dotsNumber];

        for (int i = 0; i < dotsNumber; i++)
        {
            dotsList[i] = Instantiate(dotsPrefab, null).transform;
            dotsList[i].parent = dotsParent.transform;

            if (i > 0)
            {
                Vector3 scale = new Vector3(dotsList[i - 1].transform.localScale.x - 0.01f, dotsList[i - 1].transform.localScale.y - 0.01f, 1);
                dotsList[i].transform.localScale = scale;
            }
        }
    }

    public void UpdateDots(Vector3 ballPos, Vector2 forceApplied)
    {
        timeStamp = dotSpacing;

        for (int i = 0; i < dotsNumber; i++)
        {
            pos.x = (ballPos.x + forceApplied.x / k * timeStamp);
            pos.y = ballPos.y;
            pos.z = ballPos.z;

            dotsList[i].position = pos;
            timeStamp += dotSpacing;
        }
    }

    public void Show()
    {
        dotsParent.SetActive(true);
    }

    public void Hide()
    {
        dotsParent.SetActive(false);
    }
}
