using System.Collections.Generic;
using System.Windows.Forms;

namespace DN_UNPSA_TOOLKIT
{
	public class TreeViewUtils
	{
		public static List<TreeNode> FlattenBreath(TreeView tree)
		{
			List<TreeNode> list = new List<TreeNode>();
			Queue<TreeNode> queue = new Queue<TreeNode>();
			foreach (TreeNode node in tree.Nodes)
			{
				queue.Enqueue(node);
			}
			while (queue.Count > 0)
			{
				TreeNode treeNode = queue.Dequeue();
				if (treeNode == null)
				{
					continue;
				}
				list.Add(treeNode);
				if (treeNode.Nodes == null || treeNode.Nodes.Count <= 0)
				{
					continue;
				}
				foreach (TreeNode node2 in treeNode.Nodes)
				{
					queue.Enqueue(node2);
				}
			}
			return list;
		}

		public static List<TreeNode> FlattenDepth(TreeView tree)
		{
			List<TreeNode> list = new List<TreeNode>();
			Stack<TreeNode> stack = new Stack<TreeNode>();
			foreach (TreeNode node in tree.Nodes)
			{
				stack.Push(node);
			}
			while (stack.Count > 0)
			{
				TreeNode treeNode = stack.Pop();
				if (treeNode == null)
				{
					continue;
				}
				list.Add(treeNode);
				if (treeNode.Nodes == null || treeNode.Nodes.Count <= 0)
				{
					continue;
				}
				foreach (TreeNode node2 in treeNode.Nodes)
				{
					stack.Push(node2);
				}
			}
			return list;
		}
	}
}
