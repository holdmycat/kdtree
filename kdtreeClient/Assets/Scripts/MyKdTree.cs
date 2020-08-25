using System.Collections.Generic;
using UnityEngine;

public class MyKdTree<T> where T : Component
{

    private KdNode _mRoot;

    private KdNode _mLast;

    public class KdNode {
        public T component;
        public int level;
        public KdNode left;
        public KdNode right;
        public KdNode next;
    }

    public void Add (T item)
    {
        _add(new KdNode() { component = item});
    }

    private void _add (KdNode node)
    {

        _mCount++;
        node.level = 0;
        node.next = node.left = node.right = null;

        if (null != _mLast)
            _mLast.next = node;
        _mLast = node;

        //find parent
        var parent = _findParent(node.component.transform.position);

        //set root
        if(null == parent && null == _mRoot)
        {
            _mRoot = node;
            _mRoot.component.GetComponent<CubeBehaviour>().SetParent();
            _mLast = _mRoot;
            return;
        }
     
      

        //get parent
        var parentValue = _getSplitValue(parent);

        //get node
        var nodeValue = _getSplitValue(parent.level, node.component.transform.position);

        node.level = parent.level + 1;

        var cube = parent.component.GetComponent<CubeBehaviour>();


        if (nodeValue < parentValue)
        {
            parent.left = node;
           
            cube.SetLeft(node.component.transform.position);
        }
        else
        {
            parent.right = node;
          
            cube.SetRight(node.component.transform.position);
        }
    }

    private KdNode _findParent(Vector3 pos)
    {
        var current = _mRoot;
        var parent = _mRoot;
        while(null != current)
        {
            var parentValue = _getSplitValue(current);
            var nodeValue = _getSplitValue(current.level, pos);
            parent = current;
            if(nodeValue < parentValue)
            {
                current = current.left;
            }
            else
            {
                current = current.right;
            }
        }
        return parent;
    }

    private float _getSplitValue(KdNode item)
    {
        return _getSplitValue(item.level, item.component.transform.position);
    }

    private float _getSplitValue(int level, Vector3 position)
    {
        return (level % 3 == 0) ? position.x : (level % 3 == 1) ? position.y : position.z;
    }

    private int _mCount;
    public int Count => (_mCount);

    float CalDistance (Vector3 a, Vector3 b)
    {
        return ((a.x-b.x)*(a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z));
    }

    public CubeBehaviour FindClosest (Vector3 pos)
    {
        float _nearestDis = float.MaxValue;
        KdNode _nearestNode = null;
        var current = _mRoot;

        while (null != current)
        {
            var dis = CalDistance(current.component.transform.position, pos);
            if (dis < _nearestDis)
            {
                _nearestDis = dis;
                _nearestNode = current;
            }

            var curValue = _getSplitValue(current);
            var nodeValue = _getSplitValue(current.level, pos);
            if(nodeValue < curValue)
            {
                current = current.left;
            }
            else
            {
                current = current.right;
            }

        }

        if (null == _nearestNode)
            return null;

        return _nearestNode.component as CubeBehaviour;
    }


    public void Clear ()
    {
        _mRoot = null;
        _mCount = 0;
        _mLast = null;
    }

    public List<T> ToList()
    {
        var current = _mRoot;
        List<T> tmpList = new List<T>();
        tmpList.Add(current.component);
        while (current.next != null)
        {
            current = current.next;
            tmpList.Add(current.component);
        }


        return tmpList;

    }

}
