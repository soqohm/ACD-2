using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class BSTNode<T>
    {
        public int NodeKey; // ключ узла
        public T NodeValue; // значение в узле
        public BSTNode<T> Parent; // родитель или null для корня
        public BSTNode<T> LeftChild; // левый потомок
        public BSTNode<T> RightChild; // правый потомок	

        public BSTNode(int key, T val, BSTNode<T> parent)
        {
            NodeKey = key;
            NodeValue = val;
            Parent = parent;
            LeftChild = null;
            RightChild = null;
        }
    }

    public class BSTFind<T>
    {
        // null если в дереве вообще нету узлов
        public BSTNode<T> Node;

        // true если узел найден
        public bool NodeHasKey;

        // true, если родительскому узлу надо добавить новый левым
        public bool ToLeft;

        public BSTFind() { Node = null; }
    }

    public class BST<T>
    {
        BSTNode<T> Root; // корень дерева, или null

        public BST(BSTNode<T> node)
        {
            Root = node;
        }

        public BSTFind<T> FindNodeByKey(int key)
        {
            if (Root == null) return null;
            var node = Root;
            var search = new BSTFind<T>();

            while (search.NodeHasKey == true || search.NodeHasKey == false)
            {
                if (key == node.NodeKey)
                {
                    search.Node = node;
                    search.NodeHasKey = true;
                    return search;
                }

                if (key < node.NodeKey)
                {
                    if (node.LeftChild != null)
                        node = node.LeftChild;
                    else
                    {
                        search.Node = node;
                        search.NodeHasKey = false;
                        search.ToLeft = true;
                        return search;
                    }
                }
                else
                {
                    if (node.RightChild != null)
                        node = node.RightChild;
                    else
                    {
                        search.Node = node;
                        search.NodeHasKey = false;
                        search.ToLeft = false;
                        return search;
                    }
                }
            }
            return search;
        }

        public bool AddKeyValue(int key, T val)
        {
            if (Root == null) 
                Root = new BSTNode<T>(key, val, null);
            else
            {
                var node = FindNodeByKey(key);
                if (node.NodeHasKey == false)
                {
                    var newNode = new BSTNode<T>(key, val, node.Node);
                    if (node.ToLeft == true)
                    {
                        node.Node.LeftChild = newNode;
                        newNode.Parent = node.Node;
                    }
                    else
                    {
                        node.Node.RightChild = newNode;
                        newNode.Parent = node.Node;
                    }
                }
                else return false;
            }
            return true;
        }

        public BSTNode<T> FinMinMax(BSTNode<T> FromNode, bool FindMax)
        {
            var node = FromNode;

            if (node != null && Root != null)
            {
                if (FindNodeByKey(node.NodeKey).NodeHasKey)
                {
                    if (FindMax == false)
                        while (node.LeftChild != null)
                            node = node.LeftChild;
                    else
                        while (node.RightChild != null)
                            node = node.RightChild;

                    return node;
                }
            }
            return null;
        }

        public bool DeleteNodeByKey(int key)
        {
            var foundNode = FindNodeByKey(key);
            var Node = foundNode.Node;
            BSTNode<T> successorNode;

            if (foundNode.NodeHasKey == true)
            {
                if (Node.LeftChild != null && Node.RightChild != null)
                {
                    if (Node == Root)
                    {
                        successorNode = FinMinMax(Node.RightChild, false);
                        if (GetAllNodes(successorNode).Count == 1)
                        {
                            Node.RightChild.Parent = null;
                            Node.LeftChild.Parent = successorNode;
                            successorNode.LeftChild = Node.LeftChild;
                            Root = Node.RightChild;
                        }
                        else
                        {
                            if (successorNode != Node.RightChild)
                            {
                                successorNode.Parent.LeftChild = null;
                                successorNode.Parent = null;

                                Root.LeftChild.Parent = successorNode;
                                successorNode.LeftChild = Root.LeftChild;

                                Root.RightChild.Parent = successorNode;
                                successorNode.RightChild = Root.RightChild;
                                Root = successorNode;
                            }
                            else
                            {
                                Root.LeftChild.Parent = successorNode;
                                successorNode.LeftChild = Root.LeftChild;

                                Root.RightChild.Parent = null;
                                Root = successorNode;
                            }
                        }
                    }
                    else
                    {
                        successorNode = FinMinMax(Node.RightChild, false);

                        if (successorNode != Node.RightChild)
                        {
                            successorNode.Parent.LeftChild = null;
                            successorNode.Parent = Node.Parent;

                            if (Node.Parent.LeftChild == Node)
                            {
                                Node.Parent.LeftChild = successorNode;
                            }
                            else
                                Node.Parent.RightChild = successorNode;

                            if (Node.LeftChild != null)
                            {
                                Node.LeftChild.Parent = successorNode;
                                successorNode.LeftChild = Node.LeftChild;
                            }
                            if (Node.RightChild != null)
                            {
                                Node.RightChild.Parent = successorNode;
                                successorNode.RightChild = Node.RightChild;
                            }
                        }
                        else
                        {
                            successorNode.Parent = Node.Parent;

                            if (Node.Parent.LeftChild == Node)
                            {
                                Node.Parent.LeftChild = successorNode;
                            }
                            else
                                Node.Parent.RightChild = successorNode;

                            successorNode.LeftChild = Node.LeftChild;
                            Node.LeftChild.Parent = successorNode;
                        }
                    }
                }
                else
                {
                    if (Node == Root)
                    {
                        if (GetAllNodes(Root).Count == 1)
                            Root = null;
                        else
                        {
                            if (Node.LeftChild != null)
                            {
                                Node.LeftChild.Parent = null;
                                Root = Node.LeftChild;
                            }
                            else
                            {
                                Node.RightChild.Parent = null;
                                Root = Node.RightChild;
                            }
                        }
                    }
                    else
                    {
                        if (Node.LeftChild == null && Node.RightChild == null)
                        {
                            if (Node.Parent.LeftChild == Node)
                                Node.Parent.LeftChild = null;
                            else
                                Node.Parent.RightChild = null;
                            Node.Parent = null;
                        }

                        else if (Node.LeftChild != null)
                        {
                            Node.LeftChild.Parent = Node.Parent;

                            if (Node.Parent.LeftChild == Node)
                                Node.Parent.LeftChild = Node.LeftChild;
                            else
                                Node.Parent.RightChild = Node.RightChild;
                        }
                        else
                        {
                            Node.RightChild.Parent = Node.Parent;

                            if (Node.Parent.LeftChild == Node)
                                Node.Parent.LeftChild = Node.LeftChild;
                            else
                                Node.Parent.RightChild = Node.RightChild;
                        }
                    }
                }
                return true;
            }
            else return false;
        }

        public int Count()
        {
            if (Root != null) return GetAllNodes(Root).Count;
            return 0;
        }

        public List<BSTNode<T>> GetAllNodes(BSTNode<T> Root)
        {
            var nodes = new List<BSTNode<T>> { Root };

            if (Root.LeftChild != null)
                nodes.AddRange(GetAllNodes(Root.LeftChild));
            if (Root.RightChild != null)
                nodes.AddRange(GetAllNodes(Root.RightChild));

            return nodes;
        }

        public List<BSTNode<T>> WideAllNodes()
        {
            var wideList = new List<BSTNode<T>>();
            var nodesQueue = new Queue<BSTNode<T>>();
            var node = Root;
            nodesQueue.Enqueue(Root);

            if (Root != null)
            {
                while (nodesQueue.Count > 0)
                {
                    node = nodesQueue.Dequeue();
                    wideList.Add(node);

                    if (node.LeftChild != null)
                        nodesQueue.Enqueue(node.LeftChild);
                    if (node.RightChild != null)
                        nodesQueue.Enqueue(node.RightChild);
                }
                return wideList;
            }
            return null;
        }

        public List<BSTNode<T>> DeepAllNodes(int Order)
        {
            return DeepTraversing(Root, Order);
        }

        public List<BSTNode<T>> DeepTraversing(BSTNode<T> FromNode, int Order)
        {
            var deepList = new List<BSTNode<T>>();
            var node = FromNode;

            if (node != null)
            {
                switch (Order)
                {
                    case 0:
                        {
                            deepList.AddRange(DeepTraversing(node.LeftChild, Order));
                            deepList.Add(node);
                            deepList.AddRange(DeepTraversing(node.RightChild, Order));

                            break;
                        }

                    case 1:
                        {
                            deepList.AddRange(DeepTraversing(node.LeftChild, Order));
                            deepList.AddRange(DeepTraversing(node.RightChild, Order));
                            deepList.Add(node);

                            break;
                        }

                    case 2:
                        {
                            deepList.Add(node);
                            deepList.AddRange(DeepTraversing(node.LeftChild, Order));
                            deepList.AddRange(DeepTraversing(node.RightChild, Order));
                            break;
                        }

                    default:
                        return null;
                }
            }
            return deepList;
        }
    }
}