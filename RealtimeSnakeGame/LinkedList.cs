namespace RealtimeSnakeGame
{
    public class LinkedList
    {
        private LinkedNode first = new LinkedNode(null, null, null);
        private int SIZE;

        public LinkedList()
        {
            first.next = first.back = first;
        }

        public void add(int index, object e)
        {
            addBefore(nodeAt(index), e);
        }

        public void add(object e)
        {
            addBefore(first, e);
        }

        public bool contains(object e)
        {
            return indexOf(e) != -1;
        }

        public object get(int index)
        {
            return nodeAt(index).e;
        }

        public int indexOf(object e)
        {
            LinkedNode node = first.next; // เริ่มต้นที่โหนดแรก
            for (int i = 0; i < SIZE; i++)
            {
                if ((e == null && node.e == null) ||
                    (e != null && e.Equals(node.e))) return i; // หากพบค่า ให้ส่งคืน index
                node = node.next; // ไปยังโหนดถัดไป
            }

            return -1; // หากไม่พบ ให้คืนค่า -1 (Header)
        }

        public bool isEmpty()
        {
            return SIZE == 0;
        }

        public void remove(int index)
        {
            removeNode(nodeAt(index));
        }

        public void remove(object e)
        {
            LinkedNode node = first.next;
            while (node != first && !node.e.Equals(e)) node = node.next;
            removeNode(node);
        }

        public void set(int index, object e)
        {
            nodeAt(index).e = e;
        }

        public int size()
        {
            return SIZE;
        }

        private LinkedNode nodeAt(int index) //Get node จาก Index ที่ให้
        {
            LinkedNode node = first; //first = header ใช้ first.next อาจเป็น null ก็ได้
            for (int i = -1; i < index; i++) //i = -1 (Header)
                node = node.next;
            return node;
        }

        private void addBefore(LinkedNode node, object e) //O(1)
        {
            LinkedNode before = node.back;
            LinkedNode here = new LinkedNode(e, before, node); //node = node ถัดจาก node ที่จะมาแทรก
            before.next = node.back = here;
            SIZE++;
        }

        private void removeNode(LinkedNode node)
        {
            LinkedNode before = node.back;
            LinkedNode after = node.next;
            before.next = after;
            after.back = before;
            SIZE--;
        }

        private class LinkedNode
        {
            public LinkedNode back, next;
            public object e;

            public LinkedNode(object e, LinkedNode back, LinkedNode next) //Constructor
            {
                this.e = e;
                this.back = back;
                this.next = next;
            }
        }
    }
}
