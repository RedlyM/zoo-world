using System.Collections.Generic;
using UnityEngine;

namespace ZooWorld.Runtime.Utils
{
    public class Pool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly Stack<T> _stack = new();

        public Pool(T prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
        }

        public T Get()
        {
            T item = _stack.Count > 0
                ? _stack.Pop()
                : Object.Instantiate(_prefab, _parent);

            item.gameObject.SetActive(true);
            return item;
        }

        public void Return(T item)
        {
            item.gameObject.SetActive(false);
            _stack.Push(item);
        }

        public void Clear()
        {
            while (_stack.Count > 0)
            {
                var obj = _stack.Pop();
                Object.Destroy(obj.gameObject);
            }
        }

        public int Count => _stack.Count;
    }
}