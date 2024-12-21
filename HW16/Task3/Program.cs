namespace TreeSerialization
{
    public class Node
    {
        public int Value { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }

        public Node(int value, Node? left = null, Node? right = null)
        {
            Value = value;
            Left = left;
            Right = right;
        }

        public static string Serialize(Node? node)
        {
            if (node == null)
            {
                return "#";
            }

            string leftSerialized = Serialize(node.Left);
            string rightSerialized = Serialize(node.Right);

            return $"Node({node.Value},{leftSerialized},{rightSerialized})";
        }

        public static Node? Deserialize(string data)
        {
            if (data == "#")
            {
                return null;
            }

            return ParseNode(data);
        }

        private static Node? ParseNode(string source)
        {
            if (!source.StartsWith("Node(") || !source.EndsWith(")"))
            {
                throw new FormatException("Invalid format for Node serialization.");
            }

            source = source.Substring(5, source.Length - 6);

            int firstComma = source.IndexOf(',');
            if (firstComma == -1)
            {
                throw new FormatException("Invalid format for Node serialization.");
            }

            string valuePart = source[..firstComma].Trim();
            if (!int.TryParse(valuePart, out int value))
            {
                throw new FormatException("Invalid value for Node.");
            }

            string remaining = source[(firstComma + 1)..].Trim();
            (string leftSubtree, string rightSubtree) = SplitSubtrees(remaining);

            Node? leftNode = leftSubtree == "#" ? null : ParseNode(leftSubtree);
            Node? rightNode = rightSubtree == "#" ? null : ParseNode(rightSubtree);

            return new Node(value, leftNode, rightNode);
        }

        private static (string, string) SplitSubtrees(string source)
        {
            int balance = 0, commaIndex = -1;

            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == '(') balance++;
                if (source[i] == ')') balance--;
                if (balance == 0 && source[i] == ',')
                {
                    commaIndex = i;
                    break;
                }
            }

            if (commaIndex == -1)
            {
                throw new FormatException("Failed to split subtrees.");
            }

            string left = source[..commaIndex].Trim();
            string right = source[(commaIndex + 1)..].Trim();

            return (left, right);
        }

        public override string ToString()
        {
            return Serialize(this);
        }
    }


    public static class Program
    {
        public static void Main(string[] args)
        {
            var root = new Node(1, new Node(2, new Node(3), new Node(4)), new Node(5));
            string serialized = Node.Serialize(root);
            Console.WriteLine($"Serialized tree: {serialized}");

            Node? deserialized = Node.Deserialize(serialized);
            string deserializedSerialized = Node.Serialize(deserialized);
            Console.WriteLine($"Deserialized tree: {deserializedSerialized}");
        }
    }
}
