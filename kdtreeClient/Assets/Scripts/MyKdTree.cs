using System.Collections.Generic;
using UnityEngine;

public class MyKdTree<T> where T : Component
{

    private KdNode _mRoot;

    public class KdNode {
        public T component;
        public int level;
        public KdNode left;
        public KdNode right;
    }

    public void Add (T item)
    {
        _add(new KdNode() { component = item});
    }

    private void _add (KdNode node)
    {

        _mCount++;
        node.level = 0;
        node.left = node.right = null;

        //find parent
        var parent = _findParent(node.component.transform.position);

        //set root
        if(null == parent && null == _mRoot)
        {
            _mRoot = node;
            _mRoot.component.GetComponent<CubeBehaviour>().SetParent();
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


    public void Clear ()
    {
        _mRoot = null;
        _mCount = 0;
    }

    //public List<T> ToList()
    //{
     
    //}

}
