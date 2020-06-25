..  Copyright (C) Peter Wentworth under a Creative Commons BY-NC-SA Licence.
    See the fine print at http://creativecommons.org/licenses/by-nc-sa/3.0/ 

ThinkLib.Tester Documentation
=============================
     
The textbook makes extensive use of a lightweight unit tester that we provide.
It exists purely to support the textbook examples.  There are other more complex
and comprehensive test frameworks that should be used in more advanced situations.     
     
There is just one class method:

.. sourcecode:: csharp 

      static public void TestEq(object actual, object expected);
      
Calling the method will check whether ``actual`` is equal (in a type-lenient sense) to ``expected``.  TestEq
can currently cope with value types (int, bool, char, float, double, Key (the type for keyboard key presses), 
Point, etc.), and strings.

For numbers, it will do some lenient type-conversion behind your back, so a test will consider
3.0 and 3 to be equal.  

TestEq can also cope with some fairly simple cases of lists or arrays being equal to each other: only 
one-dimensional lists and arrays are catered for at this time. 

There is a class property called AbsTolerance:

.. sourcecode:: csharp 

      static public double AbsTolerance { get; set; }

It has a default small value (0.01). 
This is intended for floating point comparisons where exact equality testing is not safe because of possible 
rounding inaccuracies.   AbsTolerance can be set to some other value.
