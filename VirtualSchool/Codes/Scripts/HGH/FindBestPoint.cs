using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyGame;

//寻找拼图碎片最佳吸附点
public class FindBestPoint : ViewController, IFindBestPointFunc
{
    private int mAdsorbentIndex;
    //寻找最佳吸附点
    public Vector2 FindPoint(Transform[] p_tf, Transform p_fragment)
    {
        mAdsorbentIndex = 1;
        Vector2 BestPoint = p_tf[0].position;
        float minDistance = Distance(p_tf[0].position, p_fragment.position);
        for (int i = 1; i < p_tf.Length; i++)
        {
            var distance = Distance(p_tf[i].position, p_fragment.position);
            if (distance <= minDistance)
            {
                minDistance = distance;
                BestPoint = p_tf[i].position;
                mAdsorbentIndex = i + 1;
            }
        }
        return BestPoint;
    }

    public int GetAdsorbentIndex()
    {
        return mAdsorbentIndex;
    }

    //计算两点距离
    private float Distance(Vector2 p_vec1, Vector2 p_vec2)
    {
        float x1 = p_vec1.x;
        float x2 = p_vec2.x;
        float y1 = p_vec1.y;
        float y2 = p_vec2.y;
        float x = Mathf.Abs(x1 - x2);
        float y = Mathf.Abs(y1 - y2);
        return Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
    }
}

public interface IFindBestPointFunc
{
    public Vector2 FindPoint(Transform[] p_points, Transform p_fragment);
    public int GetAdsorbentIndex();
}

