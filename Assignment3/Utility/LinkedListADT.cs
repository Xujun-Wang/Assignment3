using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

// [X.W.] This class is the SLL
namespace Assignment3.Utility
{
    [Serializable]
    public class LinkedListADT : ILinkedListADT
    {
        // [X.W.] Setting up the linked list
        public Node Head { get; set; }
        public int Size { get; set; }

        public LinkedListADT()
        {
            Head = null;
            Size = 0;
        }

        // [X.W.] Checks if the list is empty, returns True if it is empty
        public bool IsEmpty()
        {
            if (Head == null)
                return true;
            else
                return false;
        }

        // [X.W.] Clears the list.
        public void Clear()
        {
            Head = null;
            Size = 0;
        }

        // [X.W.] Appends data to the list.
        public void AddLast(User value)
        {
            // [X.W.] Create a new node            
            Node newUser = new Node(value);
            // [X.W.] If the list is empty, set the head to the new node
            if (Head == null)
            {
                Head = newUser;
            }
            else
            {
                // [X.W.] Traverse to the end of the list
                Node current = Head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                // [X.W.] Set the new node after the last node
                current.Next = newUser;
            }
            Size++;
        }

        // [X.W.] Prepends data to the list.
        public void AddFirst(User value)
        {
            // [X.W.] Create a new node
            Node newUser = new Node(value);

            // [X.W.] Set the new node before the current head
            if (Head == null)
            {
                Head = newUser;
            }
            else
            {
                newUser.Next = Head;
                Head = newUser;
            }
            Size++;
        }

        // [X.W.] Adds a new element at a specific position.
        public void Add(User value, int index)
        {
            // [X.W.] Create a new node
            Node newUser = new Node(value);

            // [X.W.] Set the new node at the specified position
            if (index < 0 || index > Size)
            {
                throw new IndexOutOfRangeException("Wrong! Index out of range");
            }
            else if (index == 0)
            {
                AddFirst(value);
            }
            else if (index == Size)
            {
                AddLast(value);
            }
            else
            {
                // [X.W.] Traverse to the index
                Node current = Head;
                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next;
                }

                newUser.Next = current.Next;
                current.Next = newUser;
                Size++;
            }
        }

        // [X.W.] Replaces the value  at index.
        public void Replace(User value, int index)
        {
            // [X.W.] Create a new node
            Node newUser = new Node(value);

            // [X.W.] Check if the index is valid
            if (index < 0 || index >= Size)
            {
                throw new IndexOutOfRangeException("Wrong! Index out of range");
            }
            else if (index == 0)
            {
                Node current = Head;
                newUser.Next = current.Next; // [X.W.] Link the new node to the next node
                current.Next = null; // [X.W.] Dislink the current node
                Head = newUser; // Replace the head with the new node
            }
            else
            {
                // [X.W.] Traverse to the index
                Node current = Head;
                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next;
                }
                // [X.W.] Replaceing
                newUser.Next = current.Next.Next;
                current.Next = newUser;
            }
        }

        // [X.W.] Counts the list.
        public int Count()
        {
            return Size;
        }

        // [X.W.] Removes first element from list
        public void RemoveFirst()
        {
            // [X.W.] Check if the list is empty
            if (Head == null)
            {
                throw new Exception("Cannot remove from an empty list.");
            }
            else
            {
                Head = Head.Next; // [X.W.] Set the head to the next node
                Size--;
            }
        }

        // [X.W.] Removes last element from list
        public void RemoveLast()
        {
            // [X.W.] Check if the list is empty
            if (Head == null)
            {
                throw new Exception("Cannot remove from an empty list.");
            }
            // [X.W.] Check if the node is the last one
            else if (Head.Next == null)
            {
                Head = null; // [X.W.] Set the head to null
                Size = 0;
            }
            else
            {
                // [X.W.] Traverse to the second last node
                Node current = Head;
                while (current.Next.Next != null)
                {
                    current = current.Next;
                }
                current.Next = null; // [X.W.] Remove the last node
                Size--;
            }
        }

        // [X.W.] Removes a node at a specific index
        public void Remove(int index)
        {
            // [X.W.] Check if the index is valid
            if (index < 0 || index >= Size)
            {
                throw new IndexOutOfRangeException("Wrong! Index out of range");
            }
            else if (index == 0)
            {
                RemoveFirst();
            }
            else if (index == Size)
            {
                RemoveLast();
            }
            else
            {
                // [X.W.] Traverse to the node before the specified index
                Node current = Head;
                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next;
                }
                // [X.W.] Remove the node at the specified index
                current.Next = current.Next.Next;
                Size--;
            }
        }

        // [X.W.] Gets the value at the specified index
        public Node GetValue(int index)
        {
            // [X.W.] Check if the index is valid
            if (index < 0 || index >= Size)
            {
                throw new IndexOutOfRangeException("Wrong! Index out of range");
            }
            else
            {
                // [X.W.] Traverse to the specified index
                Node current = Head;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }
                return current;
            }
        }

        // [X.W.] Gets the first index of element containing value
        public int IndexOf(User value)
        {
            // [X.W.] Traverse the list to find the index of the value
            Node current = Head;
            for (int i = 0; i < Size; i++)
            {
                if (current.Name == value.Name)
                {
                    return i;
                }
                current = current.Next;
            }
            return -1; // [X.W.] Return -1 if not found
        }

        // [X.W.] Go through nodes and check if one has value
        public bool Contains(User value)
        {
            // [X.W.] Traverse the list to find the value
            Node current = Head;
            while (current != null)
            {
                if (current.Equals(value))
                {
                    return true; // [X.W.] Return true if found
                }
                current = current.Next;
            }
            return false; // [X.W.] Return false if not found
        }

        // [X.W.] Reverse the order of the nodes in the linked list.
        public void Reverse()
        {
            Node prev = null;
            Node current = Head;
            Node next = null;
            if (Head == null)
            {
                throw new Exception("Cannot reverse an empty list.");
            }
            while (current != null)
            {
                // [X.W.] Store the next node
                next = current.Next;
                // [X.W.] Reverse the current node's pointer
                current.Next = prev;
                // [X.W.] Move pointers one position ahead
                prev = current;
                current = next;
            }
            // [X.W.] Update the head to be the last node
            Head = prev;

        }

        // [X.W.] Copy the values of the linked list nodes into an array.
        public Node[] ToArray()
        {
            // [X.W.] Create an array of the same size as the linked list
            Node[] array = new Node[Size];
            Node current = Head;
            // [X.W.] Traverse the linked list and copy values to the array
            for (int i = 0; i < Size; i++)
            {
                array[i] = current;
                current = current.Next;
            }
            return array;
        }

        // [X.W.] Join two or more linked lists together to create a single linked list.
        public void Join(LinkedListADT newList)
        {
            // [X.W.] Check if the new list is empty
            if (newList.IsEmpty())
            {
                throw new Exception("Wrong, Cannot join an empty list.");
            }
            if (this.IsEmpty())
            {
                // [X.W.] If the current list is empty, set the head to the new list's head
                Head = newList.Head;
                Size = newList.Size; // [X.W.] Update the size of the current list
            }
            else
            {
                // [X.W.] Traverse to the end of the current list
                Node current = Head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                // [X.W.] Set the next node of the last node to the head of the new list
                current.Next = newList.Head;
                Size += newList.Size; // [X.W.] Update the size of the current list
            }
        }

    }
}
