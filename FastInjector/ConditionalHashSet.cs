using System;
using System.Collections.Generic;
using System.Linq;

namespace FastInjector
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ConditionalHashSet<T> where T: class
    {
        private const int ShrinkStepCount = 100;// 每当计数器累积计数100时， 则清理一下无用的对象
        private static readonly Predicate<WeakReference> IsDead = reference => !reference.IsAlive;
        /// <summary>
        ///保存所有元素的弱引用
        /// </summary>
        private readonly Dictionary<int, List<WeakReference>> dictionary = new Dictionary<int, List<WeakReference>>();

        private int shrinkCount = 0;// 清理计数器
        /// <summary>
        /// 添加一个元素
        /// </summary>
        /// <param name="item"></param>
        internal void Add(T item)
        {
            Requires.IsNotNull(item, nameof(item));
            lock(this.dictionary)
            {
                if(this.GetWeakReferenceOrNull(item) == null)// 如果dictionary中不存在dictionary
                {
                    var weakReference = new WeakReference(item);
                    int key = weakReference.Target.GetHashCode();
                    List<WeakReference> bucket;
                    if (!this.dictionary.TryGetValue(key, out bucket))// 如果dictionary中不存在此bucket, 则新增
                    {
                        this.dictionary[key] = bucket = new List<WeakReference>(capacity: 1);
                    }

                    bucket.Add(weakReference);// 否则添加到已有的bucket中
                }
            }
        }

        internal void Remove(T item)
        {
            Requires.IsNotNull(item, nameof(item));
            lock (this.dictionary)
            {
                WeakReference reference = this.GetWeakReferenceOrNull(item);
                if (reference!= null)
                {
                    reference.Target = null;
                }

                if ((++this.shrinkCount % ShrinkStepCount) == 0)
                {
                    this.RemoveDeadItems();
                }
            }
        }

        /// <summary>
        /// 获取set中所有有效的对象
        /// </summary>
        /// <returns></returns>
        internal T[] GetLivingItems()
        {
            lock(this.dictionary)
            {
                var producers =
                    from pair in this.dictionary
                    from reference in pair.Value
                    let target = reference.Target
                    where !object.ReferenceEquals(target, null)
                    select (T)target;

                return producers.ToArray();
            }
        }


        /// <summary>
        /// 在dictionary中查找是否存在item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private WeakReference GetWeakReferenceOrNull(T item)
        {
            List<WeakReference> bucket;
            if (this.dictionary.TryGetValue(item.GetHashCode(), out bucket))
            {
                foreach (var reference in bucket)
                {
                    if (object.ReferenceEquals(item, reference.Target))
                    {
                        return reference;
                    }
                }
            }

            return null;
        }

        // 移除所有已经失效的引用
        private void RemoveDeadItems()
        {
            foreach (var key in this.dictionary.Keys.ToArray())
            {
                var bucket = this.dictionary[key];
                bucket.RemoveAll(IsDead);

                // Remove empty buckets
                if (bucket.Count == 0)
                {
                    this.dictionary.Remove(key);
                }
            }
        }


    }
}
