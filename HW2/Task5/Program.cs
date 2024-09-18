namespace Task5
{
    public class ListNode
    {
        public int Val;
        public ListNode? Next;

        public ListNode(int val = 0, ListNode? next = null)
        {
            Val = val;
            Next = next;
        }
    }

    public class Solution
    {
        public ListNode? GetIntersectionNode(ListNode? headA, ListNode? headB)
        {
            if (headA == null || headB == null)
            {
                return null;
            }

            ListNode? pointerA = headA;
            ListNode? pointerB = headB;

            while (pointerA != pointerB)
            {
                pointerA = (pointerA == null) ? headB : pointerA.Next;
                pointerB = (pointerB == null) ? headA : pointerB.Next;
            }

            return pointerA;
        }
    }

    public class Program
    {
        public static ListNode? CreateList(params int[] values)
        {
            if (values.Length == 0) return null;

            ListNode head = new ListNode(values[0]);
            ListNode current = head;

            for (int i = 1; i < values.Length; i++)
            {
                current.Next = new ListNode(values[i]);
                current = current.Next;
            }

            return head;
        }

        public static void Main()
        {
            Solution solution = new Solution();

            // один список пустой
            ListNode? headA = null;
            // 1 -> 2 -> 3
            ListNode? headB = CreateList(1, 2, 3);
            ListNode? intersection = solution.GetIntersectionNode(headA, headB);
            PrintIntersection(intersection);

            // оба списка пусты
            headA = null;
            headB = null;
            intersection = solution.GetIntersectionNode(headA, headB);
            PrintIntersection(intersection);

            // списки пересекаются 
            // 1 -> 2 -> 3 -> 4
            ListNode node4 = new ListNode(4);
            ListNode node3 = new ListNode(3, node4);
            ListNode node2 = new ListNode(2, node3);
            ListNode node1 = new ListNode(1, node2);
            // 5 -> 6 -> 3 -> 4 (пересечение начинается с 3)
            ListNode node5 = new ListNode(5);
            ListNode node6 = new ListNode(6, node3);
            node5.Next = node6;
            intersection = solution.GetIntersectionNode(node1, node5);
            PrintIntersection(intersection);

            // списки разной длины с пересечением
            // 1 -> 9 -> 1 -> 2 -> 4
            ListNode? node1A = CreateList(1, 9, 1);
            node1A.Next.Next.Next = node2;
            // 3 -> 2 -> 4 (пересечение начинается с 2)
            ListNode node3B = new ListNode(3, node2);
            intersection = solution.GetIntersectionNode(node1A, node3B);
            PrintIntersection(intersection);

            // двойное пересечение
            // 7 -> 8 -> 2 -> 4
            ListNode node8 = new ListNode(8, node2);
            ListNode node7 = new ListNode(7, node8);
            // 6 -> 5 -> 8 -> 2 -> 4 
            ListNode? node5B = new ListNode(5, node8);
            ListNode? node6B = new ListNode(6, node5B);

            intersection = solution.GetIntersectionNode(node7, node6B);
            PrintIntersection(intersection);
        }

        public static void PrintIntersection(ListNode? intersection)
        {
            if (intersection != null)
                Console.WriteLine("Пересечение в узле: " + intersection.Val);
            else
                Console.WriteLine("Пересечения нет.");
        }
    }
}