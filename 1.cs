using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures2
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
            NewChild.Parent = ParentNode;
            if (ParentNode.Children == null)
                ParentNode.Children = new List<SimpleTreeNode<T>>();
            ParentNode.Children.Add(NewChild);
        }

        public void DeleteNode(SimpleTreeNode<T> NodeToDelete)
        {
            if (NodeToDelete.Parent == null) return;
            NodeToDelete.Parent.Children.Remove(NodeToDelete);
            NodeToDelete.Parent = null;
        }

        public List<SimpleTreeNode<T>> GetAllNodes()
        {
            return BreadthSearch().ToList();
        }

        public List<SimpleTreeNode<T>> FindNodesByValue(T val)
        {
            return BreadthSearch()
                .Where(e => Comparer<T>.Default.Compare(e.NodeValue, val) == 0)
                .ToList();
        }

        public void MoveNode(SimpleTreeNode<T> OriginalNode, SimpleTreeNode<T> NewParent)
        {
            if (OriginalNode == NewParent) return;
            var node = OriginalNode;
            OriginalNode.Parent.Children.Remove(OriginalNode);
            AddChild(NewParent, node);
        }

        public int Count()
        {
            return BreadthSearch().Count();
        }

        public int LeafCount()
        {
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
                    if (e.Children != null && e.Children.Count > 0) 
                        queue.Enqueue(e);
                    yield return e;
                }
            }
        }
    }
}