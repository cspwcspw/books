
..  Copyright (C) Peter Wentworth under a Creative Commons BY-NC-SA Licence.
    See the fine print at http://creativecommons.org/licenses/by-nc-sa/3.0/ 
    
.. index:: list, element, item, collection    
    
Lists
=====


Lists and arrays are similar to each other in many
ways; for the moment we can think of a list as a supercharged kind of array with some nice extra features.
We highlight the essential differences as we go along.   Let's just review what we already 
know about arrays, because all this information applies to lists too.

Like an array, a list is an ordered **collection** of values.  We'll see other kinds of collections
later, e.g. a *dictionary*.

The individual values that make up a list (or an array) are called its **elements**, or its **items**. 
(We will use the term `element` or `item` to mean the same thing.)  All the elements in a list
(or array) must be of the same type: so we can have a list (or array) of students, or a list of integers,
or a list of strings, or a list of turtles. 

Like strings and arrays, individual elements in the list can be accessed by *indexing*.  The first
element is always at position 0, the next at position 1, and so on.  This means the whole
collection (the list or the array) is *ordered* in the sense that one string comes before
another in the structure.  (Take note: an *ordered* collection means "they're in a definite known
sequence".  This is not the same as a *sorted* collection, which means they're in ascending
(or perhaps descending) order of values.)


What's the key difference from arrays?
--------------------------------------

Once an array has been created, its length remains unchanged.

A list is a *dynamic* structure: it can grow or shrink as our program runs.  We can easily
add new elements to the existing list, or remove elements.   To do this, (and some other things
that arrays cannot do), a list has a number of convenient methods available.


Defining and initializing lists
-------------------------------

.. sourcecode:: csharp
    :linenos:
    
    List<string> weekdayNames = new List<string>() { "Sun", "Mon", "Tue", "Wed", 
                                                           "Thur", "Fri", "Sat" };
    List<int> daysInMonths = new List<int>() { 31, 28, 31, 30, 31, 30, 
                                               31, 31, 30, 31, 30, 31 };
    List<double> readings = new List<double>();
    List<Button> btns = null;

The first example defines and initializes an array of seven strings. 
The second defines and initializes an array of 12
integers. The elements of a list must all be of the same type.   

The form ``List<T>`` is called a **generic** list.  The **T** between the angle brackets 
is a **type parameter**.  It allows us to substitute any type for **T**.  So in line 1 in the definition above, we've
substituted the type ``string``, and have a list of strings.  Similarly, in line 3, we define
a ``List<int>``.   Both these definitions also have initializers, so those elements are added
to the list.   In line 5, we have an example where we instantiate a list, but it will not
contain any elements at this point.  In line 6 we define a variable that can reference a 
list of Button elements, but at this stage the list has not yet been instantiated, so we
initialize it with the value ``null``;

Lists, like arrays, are reference types: a variable refers to the actual object, which must be
instantiated in the heap.

Notice also a very important difference between lines 5 and 6:  ``readings`` references a list that
has no elements in it.   But ``btns`` has the value ``null`` --- it doesn't reference anything.

.. index:: list traversal, index operator

Accessing elements
------------------

The syntax for accessing the elements of a list is the same as the syntax for
accessing the characters of a string or the elements of an array --- 
the index operator: ``[]``. The expression inside the brackets specifies
the index. Remember that the indices start at 0.  So ``daysInMonths[2]``
has the value 31.

With arrays we have a ``Length`` property that tells us how many elements are
in the array.  With lists, (and all other collections that we'll work with) 
the property is ``Count``.

So let's provide two methods to show how we would sum the elements in a list of int:


.. sourcecode:: csharp
    :linenos:
    
    private int sumList1(List<int> xs)
    {
        int sum = 0;
        for (int i = 0; i < xs.Count; i++)
        {
            sum += xs[i];
        }
        return sum;
    }

    private int sumList2(List<int> xs)
    {
        int sum = 0;
        foreach (int x in xs)
        {
            sum += x;
        }
        return sum;
    }

    private void button1_Click(object sender, RoutedEventArgs e)
    {
        List<int> daysInMonths = new List<int>() { 31, 28, 31, 30, 31, 30,
                                                   31, 31, 30, 31, 30, 31 };
        int v1 = sumList1(daysInMonths);
        int v2 = sumList2(daysInMonths);
        Tester.TestEq(v1, v2);
    }
        
Line 1 demonstrates how we define a list parameter.  Line 6 shows how we index the list.  

The ``foreach`` in line 14 works because a list is an *enumerable* structure.  We say
that the ``foreach`` **enumerates** the items in the collection. 

And lines 25 and 26 show that passing a ``List<int>`` argument to a method is just like passing a string,
an array, or a turtle.

When are lists really special?
------------------------------

Because lists can grow and shrink in size, they're ideal for problems that need that kind
of flexibility.   Let's start with a very simple problem:  Write a method to return all
the prime numbers less than some number N.   

We need to return a list or an array, because there are potentially many such primes.  But because we
don't know how many primes there are going to be when N=1000, we can't allocate a fixed-size array to
hold the answer. So we need a structure that can grow as we discover each new prime.  

.. sourcecode:: csharp
    :linenos:
    
    private List<int> findPrimesLessThan(int N)
    {
        List<int> results = new List<int>();
        for (int i=2; i<N; i++) 
        {
             if (isPrime(i))
             {   
                 results.Add(i);
             }
        }
        return results;
    }

Line 8 is the interesting one.  The ``Add`` method of a list puts the new item at the end of the current 
items.  So the list grows in size.  The example also demonstrates a value-returning method that 
returns a list. 

Converting arrays to lists, and lists to arrays
-----------------------------------------------

If you have an array but need the additional flexibility of a list,
you can pass the array into the list Constructor, or you can add
an array of items to the end of an existing list:

.. sourcecode:: csharp
    :linenos:
    
    string[] semester1 = {"Jan", "Feb", "Mar", "Apr", "May", "June"};
    List<string> xs = new List<string>(semester1);
    string[] semester2 = {"July", "Aug", "Sep", "Oct", "Nov", "Dec"};
    xs.AddRange(semester2);

    MessageBox.Show(string.Format("There are {0} strings in xs", xs.Count));
        
Lines 1 and 3 define and initialize two string arrays. 
In line 2 we construct a new list initialized with all the strings
in ``semester1``.   In line 4, we dynamically add all the second semester's
lines onto the end of ``xs``.

Going the other way --- from a list to an array, is just as easy:

.. sourcecode:: csharp
    :linenos:

    string[] allTheMonths =  xs.ToArray();
 

Other list methods that we'll find useful
-----------------------------------------

.. sourcecode:: csharp
    :linenos:
    
    xs.Sort();
    int i = xs.IndexOf("rotten");
    xs.RemoveAt(i);
    xs.Insert(3, "potato");
    xs.Clear();
    
``Sort`` will put the list into a sorted order.          
        
``IndexOf`` works on lists like it does on strings: we can find the
index of the first occurrence of an element in the list.  If it is not
found, we'll get back -1.  So this is a good way to test whether something
is in a list.   

And ``RemoveAt`` allows us to remove the item at a given position.  The list shrinks,
and all the items after i get shifted to the left.  

At line 4, a new element is squeezed into the list at index position 3.  All the other
items have to move up, so the item originally at 3 now goes to position 4, and so on.

At line 5, the ``Clear`` method empties the list.
  
Cloning lists
-------------

Because a list is a reference type, the warnings we had earlier apply,  When
we assign a list to another list variable, or pass a list as an argument, we
create an alias --- another reference to the same underlying object.   Aliases
are tricky when the referenced object is modified.

If we want to modify a list and also keep a copy of the original, we need to be
able to make a copy of the list itself, not just an alias of its reference. 
This process is sometimes called **cloning**, to avoid the ambiguity of the word copy.

The easiest way to clone a list is to use the constructor pattern we used 
for arrays:

.. sourcecode:: csharp
    :linenos:
    
    List<string> one =  ....;
    ...
    List<string> two = new List<string>(one);
    two.Sort();
        
Now the sort method on list ``two`` won't rearrange the order of the elements in ``one``.
        
Don't fiddle with a collection (list) that is being enumerated
--------------------------------------------------------------

Enumeration of a list (e.g. using ``foreach``) "locks" the
list against changes to its size or contents.  So we cannot add or 
delete elements to a collection, nor can we modify any of the elements in
the collection while the ``foreach`` construct
is busy working its way through the collection:
 
.. sourcecode:: csharp
    :linenos:
    :emphasize-lines: 5
    
    foreach (int d in daysInMonths)
    {
        if (...) 
        {    // Add a 13th month to our year, with 25 days.
             daysInMonths.Add(25); // Will give an error
        }
    }      
        
We can, however, use a ``for`` or ``while`` loop with our
own loop indexes, and we can delete or add elements at specific
positions in the list.   

But you need to be careful: we saw above that if you delete or
insert a new element at, say, index position 5, all the other elements 
previously at positions 6, 7, 8 etc., move up or move down and so they 
change their index positions.  This can make your loops tricky.


For this reason it is often easier to make the loop run backwards 
so we don't have
to work out how the indexes to the left of where we are working
will change as we add or delete items.
        
        
Is the original list (or array) mutated, or do we want a new list?
------------------------------------------------------------------

Sometimes we'll need to create a new list, while leaving
the original list (or array) unchanged. Sometimes we'll be asked
to mutate, or make the changes to the original.  If the changes
are going to be made to the original list, we'll say that the
change is an **in-place update**.

So we should always be clear on which situation we're dealing with.

In this book we sometimes express our requirement as a test case.  let's
consider an example, starting from some unit tests.  We want two methods
that work with ``List<int>``.  The one method should take a list and return
a new list in which all the elements have been doubled:

.. sourcecode:: csharp
    :linenos:
    
    List<int> xs = new List<int>() { 3, 5, 4, 7, 2 };      
    Tester.TestEq(doubleAll_1(xs), new List<int>() { 6, 10, 8, 14, 4 });
    Tester.TestEq(xs, new List<int>() { 3, 5, 4, 7, 2 });

Looking at the test case on line 2 we can see that ``doubleAll_1`` should be a
value-returning method.  The test at line 3 checks that the ``xs`` that we passed
into ``doubleAll_1`` still has the original values that we gave it. 

So a method like this passes the tests:

.. sourcecode:: csharp
    :linenos:
    
    private List<int> doubleAll_1(List<int> xs)
    {
        List<int> result = new List<int>();
        foreach (int x in xs)
        {
            result.Add(2*x);
        }
        return result;
    }
        
Now let's solve an in-place version of the problem that passes this test:

.. sourcecode:: csharp
    :linenos:

    List<int> ys = new List<int>() { 3, 5, 4, 7, 2 };  
    doubleAll_2(ys);
    Tester.TestEq(ys, new List<int>() { 6, 10, 8, 14, 4 });

The method now should be a void method. And the test at line
3 checks that the list that was passed as an argument to our 
method has indeed been mutated.
               
.. sourcecode:: csharp
    :linenos:
    
    private void doubleAll_2(List<int> xs)
    {
        for (int i=0; i < xs.Count; i++) 
        {
            xs[i] *= 2;
        }
    }
        
Notice that we did not create a new list --- we changed the 
elements that were in ``xs``.

When should we use a list, when should we use an array?
-------------------------------------------------------

Some say that the era of the fixed-size array is dead: we should always prefer a list. 

But the historical role of arrays keeps them alive.  For example,
``string.Split()`` and ``File.ReadAllLines(...)`` (which we'll cover soon)
return arrays of strings, not a lists.  
This is because methods like this were part of the earliest versions of C#, before
lists were introduced.  And by time generic lists were introduced, it was too much
trouble to change things.  (Programmers get resentful when their code
breaks because older features are changed or removed in newer 
versions of the language, so *backward compatibility* --- the older code must run
on the newer versions --- is important.)

Definitely use a list if you need a structure in which the number of elements can
change while your program runs.  Or if you need the more powerful methods that a
list provides, then the list should be your choice. 

In other situations either a list or an array should work equally well.  


Glossary
--------

.. glossary::


    alias
        Multiple variables that contain references to the same object.

    clone
        To create a new object that has the same value as an existing object.
        Copying a reference creates an alias but doesn't clone the object.
    
    collection
        A collection of elements is stored in a single structure.  So far we've seen
        arrays and lists.  Both are collections.
        
    dynamic data structure
        A way to organize data that changes it shape or size over time.  
        A List in C# is a dynamic data structure that can expand or 
        contract as we add or delete items.  Arrays, by contrast, are fixed size. 
        
    enumerate
        To visit each element of a collection in turn.  The ``foreach`` loop allows
        us to enumerate a collection.

    element
        One of the values in a list (or other sequence, like an array). The bracket operator
        selects elements of a list.  Also called an *item*.
        
    generic
        General, or able to work with many different types.  The type ``List<T>``  means
        we can have a list of any type T --- we can substitute any specific type for T.

    index
        An integer value that indicates the position of an item in a list.
        Indexes start from 0. 
        
    in-place
        (or in-place update)  A change that is done to the original list (or collection).
        The opposite idea is that we create a new collection while leaving the original unchanged.
        
    item
        See *element*.

    list
        A collection of values, each in a specific position within the list.
 
    list traversal
        The sequential accessing of each element in a list.

    sequence
        Any of the data types that consist of an ordered collection of elements, with
        each element identified by an index.
        
    type parameter
        A type parameter is a place-holder for an actual type in a *generic* type definition.
        In C#, the type parameters occur in angle brackets, e.g. ``List<T>``.  When we specialize
        a generic type we have to provide an actual type (e.g. ``int``, ``string``, ``Turtle``),
        in our definition, e.g.  ``List<string> myFriends;``

        
Exercises
---------

#. Consider this fragment of code: 

   .. sourcecode:: csharp
        :linenos:
        
        List<int> xs = new List<int>();
        List<int> ys = xs; 
        ys.Add(42);
   
   Does this create one or two list instances?  Would 
   would the value of ``xs.Count`` be after executing this code?
   
#. What will be the output of the following program?

   .. sourcecode:: csharp
        :linenos:
    
        string[] us = { "I", "am", "not", "a", "crook" };
        string[] vs = { "I", "am", "not", "a", "crook" };
        Console.WriteLine("Test 1: {0}",  us == vs);
        us = vs;
        Console.WriteLine("Test 2: {0}", us == vs);

   Provide a *detailed* explanation of the results.
   
#. The ``us == vs`` expression doesn't look "into" the list or an array
   when it makes its comparison:   it simply asks
   "are these two references referring to the same object in memory?".  We
   call this kind of test a *shallow equality* test.  By contrast, a *deep
   equality* test asks "do the lists contain the same items in the same order?". 
   
   a) Write a deep equality test for two arrays of string 
      so that these unit tests pass:
      
      .. sourcecode:: csharp
        :linenos:
      
        string[] us = { "I", "am", "not", "a", "crook" };
        string[] vs = { "I", "am", "not", "a", "crook" };
        string[] ws = { "I", "am", "a", "crook" };
        string[] xs = { "I", "am", "not", "a", "cowboy" };
        Tester.TestEq(myEquals(us, vs),  true);
        Tester.TestEq(myEquals(us, ws),  false);
        Tester.TestEq(myEquals(us, xs),  false);
        Tester.TestEq(myEquals(xs, xs),  true);
        
   b) Now do the same for a deep equality test for List<string>.
        
   
#. Write two methods that remove all the odd numbers from a list.
   The first method should build a new list containing only the even elements.
   The second method should do an in-place change to the original list.

   .. sourcecode:: csharp
        :linenos:

        List<int> xs = new List<int>() { 3,5,4,7,2 };  
        Tester.TestEq(removeOdds_1(xs), new List<int>() {4, 2});
        Tester.TestEq(removeOdds_1(new List<int>() {}), new List<int>() {});
        Tester.TestEq(xs, new List<int>() { 3,5,4,7,2 });
        
        removeOdds_2(xs);
        Tester.TestEq(xs, new List<int>() {4, 2});
        
        List<int> ys = new List<int>() { 3, 5, 7, 9, 11, 13, 15};
        removeOdds_2(ys);
        Tester.TestEq(ys, new List<int>() { });
        
#. Write a method ``moveToBack(xs, p)``.  The p'th element of the
   list should "lose its place" and go to the back of the list.  
   If p is out of bounds, no changes are made.  This should be an in-place update.

   .. sourcecode:: csharp
        :linenos:

        List<int> xs = new List<int>() { 30,50,40,70,20 };  
        moveToBack(xs, 2);         // move element at position 2 to the back
        Tester.TestEq(xs, new List<int>() { 30,50,70,20,40 });
        moveToBack(xs, 0);
        moveToBack(xs, -1);
        moveToBack(xs, 4);
        moveToBack(xs, 5);
        moveToBack(xs, 2);
        Tester.TestEq(xs, new List<int>(){ 50,70,40,30,20});
  
#. Re-do the above exercise, this time with fixed-size arrays.  You may not
   use a list for the logic, nor are you allowed to attempt to resize the array.

#. Write a method that deletes any items in a ``List<int>`` that are
   smaller than their immediate predecessor in the original list.
   The list should be mutated: do not build a new list of items.
   Study the tests carefully to make sure you understand
   the requirements.
   
   You should try this problem in two ways and compare the code you get. In the
   first case, work backwards, starting at the last element of the list, and
   deciding if it needs to be deleted.  Then work towards the front of the list.
 
   In the second variation, use a while loop to start at the left and work to the end.
   This is more difficult!
   
   .. sourcecode:: csharp
        :linenos:
        
        List<int> xs = new List<int>() { 12, 16, 14, 14, 16, 18, 11, 9, 12, 4, 2 };       
        deleteSmallerSuccessors(xs);
        Tester.TestEq(xs, new List<int>() { 12, 16, 14, 16, 18, 12 });
        deleteSmallerSuccessors(xs);
        Tester.TestEq(xs, new List<int>() { 12, 16, 16, 18 });

     


