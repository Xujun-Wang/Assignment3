using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Assignment3.Utility;

namespace Assignment3.Tests
{
    public class LinkedListADTTest
    {
        LinkedListADT list = new LinkedListADT();

        [SetUp]
        public void Setup()
        {
        }

        // [X.W.] Testing isEmpty() method, empty
        [Test, Order(1)]
        [TestCase(true)]
        public void IsEmptyTest_Empty(bool isEmpty)
        {
            bool expected = isEmpty;
            Assert.That(expected,Is.EqualTo(list.IsEmpty()));
        }

        // [X.W.] Testing isEmpty() method, not empty
        [Test, Order(2)]
        [TestCase(false)]
        public void IsEmptyTest_NotEmpty(bool isEmpty)
        {
            // [X.W.] Create a new user and add it to the list to test this method
            User user = new User(1, "Joe Blow", "jblow@gmail.com", "password");
            list.AddLast(user);

            bool expected = isEmpty;
            Assert.That(expected, Is.EqualTo(list.IsEmpty()));
        }

        // [X.W.] Testing AddLast() method
        [Test, Order(3)]        
        public void AddLastTest()
        {             
            list.AddLast(new User(2, "Joe Schmoe", "joe.schmoe@outlook.com", "abcdef"));

            Assert.That(2, Is.EqualTo(list.Size));
        }

        // [X.W.] Testing AddFirst() method
        [Test, Order(4)]
        public void AddFirstTest()
        {
            list.AddFirst(new User(3, "Colonel Sanders", "chickenlover1890@gmail.com", "kfc5555"));
            list.AddFirst(new User(4, "Ronald McDonald", "burgers4life63@outlook.com", "mcdonalds999"));

            Assert.That(4, Is.EqualTo(list.Size));
        }

        // [X.W.] Testing Add() method, no exceptions
        [Test, Order(5)]
        public void AddTest() 
        {
            // [X.W.] Testing the normal case
            User user1 = new User(5, "Michael Jordan", "michaeljordan@nba.com", "thegoat");
            list.Add(user1, 2);
            User expected =user1;
            Assert.That(expected.Name, Is.EqualTo(list.GetValue(2).Name));

            // [X.W.] Testing the edge case, adding at the beginning
            User user2 = new User(6, "Larry Bird", "larrybird@nba.com", "toughguy");
            list.Add(user2, 0);
            expected = user2;
            Assert.That(expected.Name, Is.EqualTo(list.GetValue(0).Name));

            // [X.W.] Testing the edge case, adding at the end
            User user3 = new User(7, "Magic Johnson", "magicjohnson@nba.com", "magicguy");
            list.Add(user3, 6);
            expected = user3;
            Assert.That(expected.Name, Is.EqualTo(list.GetValue(6).Name));
        }

        // [X.W.] Testing Add() method, with exceptions
        [Test, Order(6)]
        public void AddTestException()
        {
            // [X.W.] Testing IndexOutOfRangeException 
            User user1 = new User(8, "Ray Allen", "rayallen@nba.com", "ashooter");
            Assert.Throws<IndexOutOfRangeException>(() => list.Add(user1, -1));

            // [X.W.] Testing IndexOutOfRangeException 
            Assert.Throws<IndexOutOfRangeException>(() => list.Add(user1, 100));

        }

        // [X.W.] Testing Replace() method, no exceptions
        [Test, Order(7)]
        public void ReplaceTest()
        {
            User user1 = new User(8, "Ray Allen", "rayallen@nba.com", "ashooter");
            list.Replace(user1, 3);
            User expected = user1;
            Assert.That(expected.Name, Is.EqualTo(list.GetValue(3).Name));
        }

        // [X.W.] Testing Replace() method, with exceptions
        [Test, Order(8)]
        public void ReplaceTestExceptions()
        {
            User user1 = new User(8, "Ray Allen", "rayallen@nba.com", "ashooter");
            Assert.Throws<IndexOutOfRangeException>(() => list.Replace(user1, -1));
            Assert.Throws<IndexOutOfRangeException>(() => list.Replace(user1, 100));
        }

        // [X.W.] Testing an item is deleted from beginning of list.
        [Test, Order(9)]
        public void RemoveFirstTest()
        {
            list.RemoveFirst();
            Assert.That(6, Is.EqualTo(list.Size));
        }

        // [X.W.] Testing an item is deleted from beginning of list, with exceptions
        [Test, Order(10)]
        public void RemoveFirstTestException()
        {
            // [X.W.] Clear the list first
            list.Clear();
            // [X.W.] Testing CannotRemoveException
            Assert.Throws<Exception>(() => list.RemoveFirst());
        }

        // [X.W.] An item is deleted from end of list, with exceptions
        [Test, Order(11)]
        public void RemoveLastTestException()
        {
            // [X.W.] The list is already cleared
            // [X.W.] Testing CannotRemoveException
            Assert.Throws<Exception>(() => list.RemoveLast());
        }

        // [X.W.] An item is deleted from end of list
        [Test, Order(12)]
        public void RemoveLastTest()
        {
            // [X.W.] Recreate the list
            list.AddLast(new User(1, "Joe Blow", "jblow@gmail.com", "password"));
            list.AddLast(new User(2, "Joe Schmoe", "joe.schmoe@outlook.com", "abcdef"));
            list.AddLast(new User(3, "Colonel Sanders", "chickenlover1890@gmail.com", "kfc5555"));
            list.AddLast(new User(4, "Ronald McDonald", "burgers4life63@outlook.com", "mcdonalds999"));

            list.RemoveLast();
            Assert.That(3, Is.EqualTo(list.Size));
        }

        // [X.W.] An item is deleted from middle of list.
        [Test, Order(13)]
        public void Remove()
        { 
            list.Remove(1);
            Assert.That(2, Is.EqualTo(list.Size));
        }

        // [X.W.] An item is deleted from middle of list, with exception.
        [Test, Order(14)]
        public void RemoveException()
        {
            // [X.W.] Testing IndexOutOfRangeException
            Assert.Throws<IndexOutOfRangeException>(() => list.Remove(-1));
            Assert.Throws<IndexOutOfRangeException>(() => list.Remove(100));
        }

        // [X.W.] Testing An existing item is found and retrieved, by index.
        [Test, Order(15)]
        public void GetValueTest()
        {
            list.Clear(); // [X.W.] Clear the list first
            // [X.W.] Recreate the list
            list.AddLast(new User(1, "Joe Blow", "jblow@gmail.com", "password"));
            list.AddLast(new User(2, "Joe Schmoe", "joe.schmoe@outlook.com", "abcdef"));

            User user3 = new User(3, "Colonel Sanders", "chickenlover1890@gmail.com", "kfc5555");
            list.AddLast(user3);

            list.AddLast(new User(4, "Ronald McDonald", "burgers4life63@outlook.com", "mcdonalds999"));

            Assert.That(user3.Name, Is.EqualTo(list.GetValue(2).Name));
        }

        // [X.W.] Testing An existing item is found and retrieved, by index，with exceptions.
        [Test, Order(16)]
        public void GetValueTestException()
        {
            // [X.W.] Testing IndexOutOfRangeException
            Assert.Throws<IndexOutOfRangeException>(() => list.GetValue(-1));
            Assert.Throws<IndexOutOfRangeException>(() => list.GetValue(100));
        }

        // [X.W.] Testing for getting an existing item's index.
        [Test, Order(17)]
        public void IndexOfTest()
        {
            list.Clear(); // [X.W.] Clear the list first
            // [X.W.] Recreate the list
            list.AddLast(new User(1, "Joe Blow", "jblow@gmail.com", "password"));
            list.AddLast(new User(2, "Joe Schmoe", "joe.schmoe@outlook.com", "abcdef"));

            User user3 = new User(3, "Colonel Sanders", "chickenlover1890@gmail.com", "kfc5555");
            list.AddLast(user3);

            list.AddLast(new User(4, "Ronald McDonald", "burgers4life63@outlook.com", "mcdonalds999")); 

            Assert.That(2,Is.EqualTo(list.IndexOf(user3)));
        }

        // [X.W.] Testing Reverse the order of the nodes in the linked list.
        [Test, Order(18)]
        public void ReverseTest()
        {
            // [X.W.] Already have the list first
            list.Reverse();
            // [X.W.] Check the data
            Assert.That("Ronald McDonald", Is.EqualTo(list.GetValue(0).Name));
            Assert.That("Colonel Sanders", Is.EqualTo(list.GetValue(1).Name));
            Assert.That("Joe Schmoe", Is.EqualTo(list.GetValue(2).Name));
            Assert.That("Joe Blow", Is.EqualTo(list.GetValue(3).Name));
        }

        // [X.W.] Testing Reverse the order of the nodes in the linked list, with an empty list.
        [Test, Order(19)]
        public void ReverseTestExeption()
        {
            list.Clear(); // [X.W.] Clear the list first

            Assert.Throws<Exception>(() => list.Reverse());
        }

        // [X.W.] Testing ToArray[].
        [Test, Order(20)]
        public void ToArrayTest()
        {
            // [X.W.] Recreate the list
            list.AddLast(new User(1, "Joe Blow", "jblow@gmail.com", "password"));
            list.AddLast(new User(2, "Joe Schmoe", "joe.schmoe@outlook.com", "abcdef"));
            list.AddLast(new User(3, "Colonel Sanders", "chickenlover1890@gmail.com", "kfc5555"));
            list.AddLast(new User(4, "Ronald McDonald", "burgers4life63@outlook.com", "mcdonalds999"));

            Node[] userArray = list.ToArray();
            Assert.That(userArray[2].Name, Is.EqualTo(list.GetValue(2).Name));
        }

        // [X.W.] Testing Join.
        [Test, Order(21)]
        public void JoinTest()
        {
            // [X.W.] Recreate 2 new lists
            LinkedListADT list1 = new LinkedListADT();
            list1.AddLast(new User(1, "Joe Blow", "jblow@gmail.com", "password"));
            list1.AddLast(new User(2, "Joe Schmoe", "joe.schmoe@outlook.com", "abcdef"));

            LinkedListADT list2 = new LinkedListADT();
            list2.AddLast(new User(3, "Colonel Sanders", "chickenlover1890@gmail.com", "kfc5555"));
            list2.AddLast(new User(4, "Ronald McDonald", "burgers4life63@outlook.com", "mcdonalds999"));

            // [X.W.] Join the two lists
            list1.Join(list2);
            // [X.W.] Check the data
            Assert.That("Colonel Sanders", Is.EqualTo(list1.GetValue(2).Name));

        }

    }
}
