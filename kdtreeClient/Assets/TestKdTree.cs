using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKdTree : MonoBehaviour
{
    
    Vector2[] _mArrayData = new Vector2[] {
        new Vector2(-3,6),
        new Vector2(-6,2),
        new Vector2(0,5),
        new Vector2(1,-2),
        new Vector2(7,-4),
        new Vector2(5,3)
    };

    KdTree<CubeBehaviour> _mKdList = new KdTree<CubeBehaviour>();

    MyKdTree<CubeBehaviour> _mMyKdTree = new MyKdTree<CubeBehaviour>();

    Queue _mQueue = new Queue();

    public CubeBehaviour _mOrigCubeBeha;

    public GameObject _mLocalSphere;

    // Start is called before the first frame update
    void Start()
    {
        
        for(var i = 0; i < _mArrayData.Length; i++)
        {
            var tmp = Instantiate(_mOrigCubeBeha.gameObject, _mArrayData[i], Quaternion.identity);
            _mQueue.Enqueue(tmp.GetComponent<CubeBehaviour>());
        }

    }

    GameObject _mSphere;
    private void OnGUI()
    {
        LeftKdTree();

        RightMyKdTree();
    }

    void LeftKdTree ()
    {
        if (GUI.Button(new Rect(0, 0, 150, 150), "kd item"))
        {
            if (_mQueue.Count == 0)
            {
                Debug.LogWarning("Queue is empty");
                return;
            }
            CubeBehaviour tmp = _mQueue.Dequeue() as CubeBehaviour;
            _mKdList.Add(tmp);
        }
        else if (GUI.Button(new Rect(0, 200, 150, 150), "load item"))
        {
            if (null != _mSphere || _mArrayData.Length != _mKdList.Count)
            {
                Debug.LogWarning("Sphere Alrealdy loaded or kd tree is not finished");
                return;
            }
            var tmp = new Vector3(Random.Range(-10, 10), Random.Range(-4, 4f), 0f);
            _mSphere = Instantiate(_mLocalSphere, tmp, Quaternion.identity);
        }
        else if (GUI.Button(new Rect(0, 400, 150, 150), "match item"))
        {

            if (null == _mSphere)
            {
                Debug.LogWarning("please load Sphere first");
                return;
            }
            float dis = 0f;

            var item = _mKdList.FindClosest(_mSphere.transform.position, out dis);
            item.SetChoosen();
        }
        else if (GUI.Button(new Rect(0, 600, 150, 150), "reset"))
        {
            if (null != _mSphere)
            {
                Destroy(_mSphere);
            }
            _mSphere = null;


            var list = _mKdList.ToList();

            foreach (var item in list)
            {
                item.ResetItem();
                _mQueue.Enqueue(item);
            }
            _mKdList.Clear();
        }
    }

    void RightMyKdTree()
    {
        if (GUI.Button(new Rect(Screen.width - 150, 0, 150, 150), "mykd item"))
        {
            if (_mQueue.Count == 0)
            {
                Debug.LogWarning("Queue is empty");
                return;
            }
            CubeBehaviour tmp = _mQueue.Dequeue() as CubeBehaviour;
            _mMyKdTree.Add(tmp);
        }
        else if (GUI.Button(new Rect(Screen.width - 150, 200, 150, 150), "load item"))
        {
            if (null != _mSphere || _mArrayData.Length != _mMyKdTree.Count)
            {
                Debug.LogWarning("Sphere Alrealdy loaded or kd tree is not finished");
                return;
            }
            var tmp = new Vector3(Random.Range(-10, 10), Random.Range(-4, 4f), 0f);
            _mSphere = Instantiate(_mLocalSphere, tmp, Quaternion.identity);
        }
        else if (GUI.Button(new Rect(Screen.width - 150, 400, 150, 150), "match item"))
        {

            if (null == _mSphere)
            {
                Debug.LogWarning("please load Sphere first");
                return;
            }
            var item = _mMyKdTree.FindClosest(_mSphere.transform.position);
            item.SetChoosen();
        }
        else if (GUI.Button(new Rect(Screen.width - 150, 600, 150, 150), "reset"))
        {
            if (null != _mSphere)
            {
                Destroy(_mSphere);
            }
            _mSphere = null;


            var list = _mMyKdTree.ToList();

            foreach (var item in list)
            {
                item.ResetItem();
                _mQueue.Enqueue(item);
            }
            _mMyKdTree.Clear();
        }
    }

}
