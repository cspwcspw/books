..  Copyright (C) Peter Wentworth, Yusuf Motara, under a Creative Commons BY-NC-SA Licence.
    See the fine print at http://creativecommons.org/licenses/by-nc-sa/3.0/ 
    

.. non-breaking space, courtesy of http://stackoverflow.com/questions/11830242/non-breaking-space
.. |_| unicode:: 0xA0
   :trim:


Basic C# programming theory
===========================

Types
~~~~~

Before you can write your first program, there is some theory that you must know.  The first thing to know is that according to C#, everything has a **type**.  For example, *2* is an *integer*.  We can say that the **type** of *2* is *integer*.  

We are going to use five of the C# **primitive** types, described below:

`int`
   This is any whole number between approximately -2,000,000,000 and 2,000,000,000.
`bool`
   This is either true or false.
`double`
   This is any number with a decimal point in it.
`char`
   This is any single character, digit, or punctuation mark.  In C#, a char is always enclosed in single-quotation ( ' ) marks.
`string`
   This is zero or more characters, digits, and/or punctuation marks.  In C#, a string is always enclosed in quotation ( " ) marks.

.. IMPORTANT::
   We will be referring to these five types very often, and using one or more of them in *every single program* that we write!  Therefore, become very familiar with them.  When you hear the word `string` or `bool`, for example, you should immediately understand the Computer Science meaning of that term; and whenever you see any data, you should immediately understand what its type is.

+----------------------+------------------------------------------------------------------------------------------------+
| Type                 | Examples                                                                                       |
+======================+================================================================================================+
| `int`                | - 576                                                                                          |
|                      | - -873                                                                                         |
|                      | - 0                                                                                            |
+----------------------+------------------------------------------------------------------------------------------------+
| `bool`               | - true                                                                                         |
|                      | - false                                                                                        |
+----------------------+------------------------------------------------------------------------------------------------+
| `double`             | - 7.51954                                                                                      |
|                      | - 985.0                                                                                        |
|                      | - -33.614                                                                                      |
+----------------------+------------------------------------------------------------------------------------------------+
| `char`               | - '8'                                                                                          |
|                      | - 'z'                                                                                          |
|                      | - ','                                                                                          |
|                      | - ' '                                                                                          |
+----------------------+------------------------------------------------------------------------------------------------+
| `string`             | - "Pi, as an irrational number, cannot be represented accurately by any simple fraction."      |
|                      | - "z"                                                                                          |
|                      | - ""                                                                                           |
|                      | - " "                                                                                          |
+----------------------+------------------------------------------------------------------------------------------------+

.. Try to make the "art" side of this clear in lectures.  For example, why should you choose a "char" instead of a "string" to represent your grade?  Because it is the most "natural" type, I guess.  And why not just toss everything into a string?

.. admonition:: Exercises
   :class: exercise


   What is the type of:

   1. 89.9 ______________________________________
   2. false ______________________________________
   3. 'p' ______________________________________
   4. -0.0 ______________________________________
   5. 11 ______________________________________
   6. '0' ______________________________________
   7. 0 ______________________________________
   8. "0" ______________________________________
   9. true ______________________________________
   10. "z" ______________________________________
   11. "775.21" ______________________________________

   Which type would you use to represent:

   1. Your name? ______________________________________
   2. Your age in years? ______________________________________
   3. Whether you are alive? ______________________________________
   4. Your grade (A, B, C, D, or E) for an assignment? ______________________________________
   5. The length of a line in centimetres? ______________________________________
   6. The number of pages in a book? ______________________________________
   7. The exact price of a bottle of milk? ______________________________________
   8. Whether you have eaten anything in the last 6 hours? ______________________________________
   9. The second letter of your first name? ______________________________________
   10. The number of grains of sand in a jar? ______________________________________
   11. The square root of 2? ______________________________________
   12. Your reasons for reading this book? ______________________________________
   13. The colour of your eyes? ______________________________________

Working with time
~~~~~~~~~~~~~~~~~

.. sidebar:: There Is No "Right" Program

   There are an infinite number of ways to write any particular program, just as there are an 
   infinite number of ways to paint a picture of a person.  The solutions that your instructors 
   give you are just the way that **they** would write the program.  Study them, and learn from them, 
   but understand that their solutions are not the only way (or even the best way) to write a program.

The second (and last) bit of theory to know before we begin writing programs is that programming 
instructions are executed **sequentially** [#]_, one after the other.  This means that the 
order in which you write programming statements matters.  If you get the order incorrect, 
the program will do the incorrect thing.


The order in which statements could be executed is called the **control flow** of the program.  
Control flow consists of multiple **paths**, each of which could be taken during program execution.  
One of the most important skills of a programmer is the ability to "see" the control flow of a 
program [#]_.  The best way to get better at "seeing" control flow is to read other people's programs, 
try to work out what they do *without executing them*, and then execute them to see if your 
reasoning was correct (or, if your reasoning was incorrect, where and why it was incorrect).

There are many techniques and tools that programmers have created to help themselves to visualise 
control flow: flowcharts, trace tables, call graphs, sequence diagrams, and so on.  We may cover 
some of these in class.  If you don't find the standard tools for visualising control flow to be 
useful, you must invent your own notation or visualisation for understanding control flow.

.. [#] This is a slight simplification.  You will learn about non-sequential control flows in your second or third year.

.. [#] This is not strictly true for all programming languages, but it is true for the vast majority of mainstream programming languages.

A first program
---------------

.. sidebar:: Syntax

   **Syntax** is the collection of rules that define what a programming language looks like.  
   Each programming language has its own syntax, just as each human language has its own grammar and spelling rules.

With that, you are ready to begin your first program!

.. comment: use WPF, and introduce the { and } which show "blocks".

::

   int a;
   int b;
   int c;

This simply says that an `int` called `a` exists, an `int` called `b` exists, and an `int` called `c` exists.  
Notice that each statement ends with a `;` (**semicolon**) character.  A shorthand, equivalent way of saying exactly the same thing is

.. comment: ask in class, what are the values of `a` and `b` and `c`?

::

   int a, b, c;

`a`, `b`, and `c` are **variables** (or **identifiers**, or **symbols**).  A variable has a name and a type.  It holds a value.  
The value of a variable can be changed, but its name and type cannot.  The programming instructions above are called 
variable **definitions**, since they define that something exists.  
Identifiers should start with a letter and contain no spaces or punctuation.

.. IMPORTANT::
   A **variable** in Computer Science is not the same as a **variable** in Statistics, or a **variable** in Mathematics.  
   Understand the specific Computer Science meaning, and do not confuse it with your other subjects!

Merely saying that something exists doesn't make for an exciting program.  Let's try this program instead:

::

   int a, b, c;
   a = 5;
   b = 10;
   c = a + b;

.. sidebar:: Why not one way to do things?

   Programming languages are designed for humans as well as computers, and language designers have tried to 
   make languages that let you express yourself in the way that you find to be most natural.  Some people 
   like writing very short, very concise code.  Others like to write many lines, believing that this 
   leads to clearer, more readable code.  Most people find a balance between very-concise and very-verbose.  
   It doesn't matter which style you prefer to write in.  What matters is that you are able to *read* programs 
   written in *any* style.

Let's understand what it does.  First, we define the `a`, `b`, and `c` variables.  
Then we assign the value `5` to `a`.  Then we give the value `10` to `b`.  Lastly, we 
assign the value `15` to `c`.  These statements, of the form `X = Y;`, are 
called **assignment statements**.  Assignment statements are used to change the 
value of a variable.  All assignment statements have a right-hand-side 
(on the right of the `=` symbol) and a left-hand-side (on the left of the `=` symbol).  
There is always a variable name on the left-hand-side.  An assignment statement executes 
the right-hand-side first, and then assigns the resulting value to the variable on the 
left-hand-side.  This is why the variable `c` is given the value `15`, rather than the value `a + b`.

.. comment: stress in class that RHS-evaluated first!

.. admonition:: Remember that control flow matters ...

   Programs do their work one statement at a time.  So we would be wrong if we looked at
   the program above and tried to think about all four statements at the same time.
   Each statement in a program changes something from how it was before.  (We say the
   program has a *state*, and statements change the state of the program.)  
   So the first key skill we'll need is to realize that statements are executed in a 
   specific order, top-to-bottom, and that each one makes a change to the state.  It
   is a bit like baking a cake from a recipe --- you need to do things one step at a 
   time, in some definite time order.    



We can tighten this code up a bit, by combining definitions and assignment statements, and reduce it from 4 lines to 3 lines in length.

::
   
   int a = 5;
   int b = 10;
   int c = a + b;

And if we'd like to, we can use the shorthand form of definitions to reduce it to a single line:

::

   int a = 5, b = 10, c = a + b;


.. admonition:: Putting things together

   If you're not yet convinced that Computer Science isn't Mathematics, think about this: in Mathematics, there are common equivalents for `int`, `double`, and `bool` types.  There are no common equivalents for `char` and `string` types.  That might be because Computer Science is a more practical and pragmatic discipline, and one of the common things that people want to be able to do is read and write text.  So it makes sense for Computer Science to have `char` and `string`, and it makes sense for Mathematics to not use those types.  It also makes sense for programming languages, such as C#, to use those types in a non-mathematical way.  For example, if you wrote this:

   ::

      string a = "super";
      string b = "man";
      string c = a + b;

   That would be perfectly OK in C# (and `c` would end up with the value `superman`).  
   In Computer Science, `+` commonly means *put these things together*.  If you put two 
   `int`\s together, you get the sum of the `int`\s.  If you put two `string`\s together, 
   you get the combined value of both `string`\s.  This makes absolutely no sense, 
   mathematically.  But this is Computer Science -- not Mathematics!


.. admonition:: Exercises
   :class: exercise

   There is something wrong with each of these lines of code.  For each line, write a sentence describing what is wrong.
   
   1. in a;

   |
   

   2. string a b;

   |


   3. bool a

   |


   4. int a, string b;

   |
 

   5. var a;

   |
 

   6. int 3d;

   |

 
   7. int a = 3.0;

   |

 

   8. double a, b = 5.0, c = "6.0";

   |

  

   9. var a = 9, b = 8;
   
   |

 

   10. char zoology = "";

   |

 

   11. char biology = "E";

   |

 

   12. bool botany = True;

   |
 

   13. INT CHEMISTRY = 100;

   |

 

   14. bool able, able;

   |

 

   15. int articulate = 8; bool ampersand = articulate;

   |
 

   16. string o'brien = "Connor";

   |

  

   What is the type of `x`?

   1. var x = "Yes"; _______________
   2. var x = 213; _______________
   3. var x = 0.0; _______________
   4. var x = 'y'; _______________
   5. var x = "y"; _______________
   6. var x = true; _______________
   7. var x = 5 + 3; _______________
   8. var x = "false"; _______________
   9. var x = 3.2/5.1; _______________
   10. var x = false; _______________
   11. var x = "20/5"; _______________

.. Demonstrate, in class, the changing of a variable.  Ask what the values are, to show them the effects of sequential execution.
   Also, show an expression with brackets.  One of the purposes of brackets in C# is to group terms, 
   just as in mathematics.  Another of the purposes is for syntax, e.g. with if-statements.


.. Ask in class:
   - Why do we need to say `int a`?  Why not just `int 5`?
   - What does the `=` do?  Why is it not like the `=` in mathematics?
   - Why is there a semicolon at the end of each line?
   - What happens if we shuffle the order of the statements around?
   - Can I just substitute in the numbers?  Do I need `a` and `b`?
   - Can I substitute in just one number?  Can I mix variables and literals?
   - What happens if we try to say `a = "moo"`?

Basic Arithmetic
----------------

There are five arithmetic **operators** that are commonly used in C#.  The first four should be familiar to you from school:

- `X + Y` adds X and Y
- `X - Y` subtracts Y from X
- `X * Y` multiplies X and Y
- `X / Y` divides X by Y

The last operator is the **modulus** operator:

- `X % Y` gives the remainder that would result from dividing X by Y

The first time you did division at school, you probably expressed your answer as "A remainder B".  For example, you would say 
that 7 divided by 4 was "1 remainder 3", or that 24 divided by 6 was "4 remainder 0".  The modulus operation gives you 
the "remainder" part of the answer; so 7 |_| % |_| 4 |_| gives |_| 3, and 24 |_| % |_| 6 |_| gives |_| 0.  The sign of the 
answer is the same as the sign of the dividend (so, for example, -7 |_| % |_| 4 |_| gives |_| -3).

.. sidebar:: Negative numbers and spacing

   You usually don't need to put spaces between the parts of an arithmetic expression: 4-5+3 is the same 
   thing as 4 |_| - |_| 5 |_| + |_| 3.  The only exception to this is when you have a negative term 
   in your expression.  C# doesn't understand the arithmetic expression ``7--4``, but it does 
   understand ``7 - -4``, so be sure to put a space before negative numbers.

You can group arithmetic terms using brackets, just as you would in ordinary mathematics.  
The usual precedence of operations (brackets, division & multiplication, addition & subtraction) 
applies to arithmetic expressions in C#.  When operators have the same precedence, arithmetic 
expressions are evaluated from left to right.

Arithmetic that is done exclusively with `int`\s *always* results in integer answers.  
This means that 7 |_| / |_| 4 |_| = |_| 1, and not 1.75 or 2.  Arithmetic that is done 
exclusively with `double`\s results in `double` answers.  This means that 7.0 |_| / |_| 4.0 |_| = |_| 1.75.  
Arithmetic that mixes `double`\s and `int`\s results in `double` answers.  
This means that 7.0 |_| / |_| 4 |_| = |_| 1.75, and 7 |_| / |_| 4.0 |_| = |_| 1.75.

.. admonition:: Exercises
   :class: exercise

   What is the answer to these arithmetic expressions?  When the answer is a `double`, 
   be sure to show a decimal point in your answer.

   1. 6 * 4 - 1 ________
   2. 6 * (4 - 1) ________
   3. 3.2 * 2.0 ________
   4. 3.2 * 2 ________
   5. (3 * (5 - 2) / 4) + 1 ________
   6. ((5 - 2) / 4 * 3) + 1 ________
   7. 1 + 1.0 ________
   8. (-(-2) + (-2 * -2 - 4 * 3 * 2)) / 2 * 3 ________ 
   9. (-(-2) + (-2 * -2 - 4.0 * 3 * 2)) / 2 * 3 ________ 
   10. 8 - -8 ________
   11. (10 * 7) % 3 ________
   12. (5 % 2) + (71553 % 2) ________
   13. (19 % 4) + (-72 % 10) ________ 

Debugging
---------

Programming is a complex process, and because programs are written by human beings, 
they often contain errors. Some of these errors are simple, like messing up the grammar 
of the programming language, and the compiler can tell you there's a problem and 
(usually) also tell you where it is, so you can fix it.  These kinds of simple 
errors are called **syntax** errors.  **Syntax** refers to the structure of a 
program and the rules about that structure. Mis-spelling a word in a programming 
language, or putting it in the wrong order, or not including necessary punctuation, 
or including too much punctuation, or not capitalising a word that should be 
capitalised (or vice versa!) are all examples of syntax errors.

During the first few weeks of your programming career, you will probably spend a lot of time tracking down syntax errors. As you gain experience, you will make fewer syntax errors and find them faster.  After a few weeks, you won't make any syntax errors at all!  This is just like the process of learning a new spoken language.  While you are learning, you will make silly errors that a native speaker wouldn't make.  But after a while, you don't make any errors.  And, just like learning a new spoken language, the more you practise, the better you'll get ... and if you don't practice enough, you'll keep making mistakes.  So practice: it really does make a big difference.

The more difficult errors are **semantic** errors, where you've given the computer a valid sequence of instructions, but the instructions don't lead to the result that you think they lead to.  You'll run into plenty of these, and when you do you'll need to understand exactly what you're asking the computer to do, so that you can figure out where the flaw in your reasoning is.

**Runtime** errors, which cause a program to stop running, occur because of **semantic** errors.  Usually, you've made the error of assuming something that you shouldn't have assumed!  These errors are also called **exceptions** because they indicate that something exceptional (and bad) has happened.

Programming errors are called **bugs** and the process of tracking them down and 
correcting them is called **debugging**.  It is useful to distinguish between types 
of bugs in order to find the cause of the error and fix it more quickly.  
Here is how you can distinguish between types of bugs:

   1. Do you get errors when you *compile* the program?  If so, you have a **syntax** error.

   2. Does the program fail to do what you expect it to do?  If so, you have a **semantic** error.

   3. Does the program stop running before you expect it to?  If so, you have a **runtime** error which is caused by a **semantic** error.  You need to fix the semantic error so that the runtime error will go away.

What if ...?
------------

For our second program, let's make a simple password program.  If you give it the right password, 
it will tell you some secret message.  If you don't give it the right password, it'll tell you to go away.

.. sourcecode:: csharp
       :linenos:

       string password = Password.Text;
       bool passwordIsOK = password == "top sekrit";
       if (passwordIsOK) 
       {
          Result.Content = "My secret message is: Hi, how are you?";
       } 
       else 
       {
          Result.Content = "Go away.  You don't know the password.";
       }

|
       
       
.. in class, show a shorter version too:
.. .. sidebar:: A Shorter Version
.. 
..    This code does the same thing, but is a bit shorter.
.. 
..    ::
..       
..       if (Password.Text == "top sekrit") {
..          Result.Content = "My secret message is: Hi, how are you?";
..       } else {
..          Result.Content = "Go away.  You don't know the password.";
..       }

The first line is easy: a combined definition and assignment.  The second line is where 
we check whether the password is correct or not.  We do this using the `==`, which 
compares two things for equality.  If the things are equal, the expression is `true`; 
otherwise, the expression is `false`.  There are a few other symbol-combinations 
(called **operators**) which are useful for comparing things, and it's useful to 
know what they are when you want to make your own conditions:

+--------------+-----------------------------------------+
| X == Y       | true if X is equal to Y                 |
+--------------+-----------------------------------------+
| X >= Y       | true if X is greater than or equal to Y |
+--------------+-----------------------------------------+
| X <= Y       | true if X is less than or equal to Y    |
+--------------+-----------------------------------------+
| X != Y       | true if X is not equal to Y             |
+--------------+-----------------------------------------+
| X > Y        | true if X is greater than Y             |
+--------------+-----------------------------------------+
| X < Y        | true if X is less than Y                |
+--------------+-----------------------------------------+

`X` and `Y` may be any expressions, as long as the expressions have the same type.  
Memorise these operators.  They will be used in almost every program that you will write.

Then we come to an **if-statement**.  An if-statement is a way of doing something only 
if some expression (which is called the **condition** of the if-statement) is `true`.  
In this case, if `passwordIsOK` is `true`, then `Result.Content` will be given the value 
`My secret message is: Hi, how are you?`.  If it's not true, then `Go away.  You don't 
know the password.` will be assigned to `Result.Content`.

Conditions *must* result in a `bool` value.  If your condition results in a `string`, 
`int`, `double` or anything except a `bool` value, your program will not compile.

The `else` part of an if-statement is optional.  This means that if you only wanted to do 
something when the password was correct, you could have written

::

   if (Password.Text == "top sekrit") {
      Result.Content = "My secret message is: Hi, how are you?";
   }


.. important:: In class, you will already have seen brackets being used to group terms, 
   just as you would do in Mathematics.  In the if-statement, brackets are used to tell 
   C# where the condition starts and ends.  Within the brackets, you can have other brackets 
   if you want to group terms.

.. admonition:: Exercises
   :class: exercise

   What is the answer to these expressions?

   1. 5 < 3 __________
   2. 5 > 3 __________
   3. 6 <= 6 __________
   4. 6 >= 6 __________
   5. 6 < 6 __________
   6. 6 == 6 __________
   7. 6 != 6 __________
   8. 8 != 6 __________
   9. 7 >= 6 __________
   10. 5 - 3 > 2 + 1 __________
   11. 3 - 5 < 1 - 2 __________
   12. "apple" != "orange" __________
   13. "banana" <= "candy" __________
   14. 3.14 * 2 > 6 __________

   There's something wrong with each of the following pieces of code.  For each piece of code, write a sentence describing what is wrong.

   ::

      if 5 > 3 {
         Result.Content = "Something";
      }
   
   |

   |

   ::

      if (5 <> 3) {
         Result.Content = "Something";
      }

   |

   |

   ::

      if ("true") {
         Result.Content = "Something";
      }

   |

   |

   ::

      if () {
         Result.Content = "Something";
      }

   |

   |

   ::

      If (5 + 3 < 7) {
         Result.Content = "Something";
      }

   |

   |

   ::

      if (5 + 3 < 7) {
         Result.Content = "Something";

   |

   |

   ::

      if {
         Result.Content = "Something";
      }

   |

   |

   ::

      if (5 - 7 > 3) 
         Result.Content = "Something";
      }

   |

   |
 