using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures21
{
    public class SimpleTreeNode<T>
    {
        public T NodeValue;
        public SimpleTreeNode<T> Parent;
        public List<SimpleTreeNode<T>> Children;

        public SimpleTreeNode(T val, SimpleTreeNode<T> parent)
        {
            NodeValue = val;
            Parent = parent;
            Children = null;
        }

        public bool HasChildren
        {
            get { return Children != null && Children.Count > 0; }
        }
    }

    public class SimpleTree<T>
    {
        public SimpleTreeNode<T> Root;

        public SimpleTree(SimpleTreeNode<T> root)
        {
            Root = root;
        }

        public void AddChild(SimpleTreeNode<T> ParentNode, SimpleTreeNode<T> NewChild)
        {
            if (Root == null) return;
            NewChild.Parent = ParentNode;
            if (ParentNode.Children == null)
                ParentNode.Children = new List<SimpleTreeNode<T>>();
            ParentNode.Children.Add(NewChild);
        }

        public void DeleteNode(SimpleTreeNode<T> NodeToDelete)
        {
            if (Root == null) return;
            if (NodeToDelete.Parent == null) return;
            NodeToDelete.Parent.Children.Remove(NodeToDelete);
            NodeToDelete.Parent = null;
        }

        public List<SimpleTreeNode<T>> GetAllNodes()
        {
            if (Root == null) return null;
            return BreadthSearch()
                .Concat(new List<SimpleTreeNode<T>>() { Root })
                .ToList();
        }

        public List<SimpleTreeNode<T>> FindNodesByValue(T val)
        {
            if (Root == null) return null;
            var results = BreadthSearch()
                .Where(e => Comparer<T>.Default.Compare(e.NodeValue, val) == 0)
                .ToList();
            if (Comparer<T>.Default.Compare(Root.NodeValue, val) == 0)
                results.Add(Root);
            return results;
        }

        public void MoveNode(SimpleTreeNode<T> OriginalNode, SimpleTreeNode<T> NewParent)
        {
            if (Root == null) return;
            if (OriginalNode == NewParent) return;
            var node = OriginalNode;
            OriginalNode.Parent.Children.Remove(OriginalNode);
            AddChild(NewParent, node);
        }

        public int Count()
        {
            if (Root == null) return 0;
            return BreadthSearch().Count() + 1;
        }

        public int LeafCount()
        {
            if (Root == null) return 0;
            if (!Root.HasChildren) return 1;
            return BreadthSearch()
                .Where(e => !e.HasChildren)
                .Count();
        }

        IEnumerable<SimpleTreeNode<T>> BreadthSearch()
        {
            var queue = new Queue<SimpleTreeNode<T>>();
            if (Root is object && Root.Children != null) queue.Enqueue(Root);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                foreach (var e in node.Children)
                {
                    if (e.HasChildren) queue.Enqueue(e);
                    yield return e;
                }
            }
        }
    }
}