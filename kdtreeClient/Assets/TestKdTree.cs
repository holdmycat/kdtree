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

    Queue _mQueue = new Queue();

    public CubeBehaviour _mOrigCubeBeha;

    // Start is called before the first frame update
    void Start()
    {
        
        for(var i = 0; i < _mArrayData.Length; i++)
        {
            var tmp = Instantiate(_mOrigCubeBeha.gameObject, _mArrayData[i], Quaternion.identity);
            _mQueue.Enqueue(tmp.GetComponent<CubeBehaviour>());
        }

    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0,0,150,150), "kd item"))
        {
            CubeBehaviour tmp = _mQueue.Dequeue() as CubeBehaviour;
            _mKdList.Add(tmp);
        }
    }

}
