..  Copyright (C) Peter Wentworth under a Creative Commons BY-NC-SA Licence.
    See the fine print at http://creativecommons.org/licenses/by-nc-sa/3.0/ 
     

    
..
   max code col = 92, will import to Acrobat XI without wrapping, images 720px
    
      
     
Foreword  
========

Thanks
------

This is an Open Source textbook

That means that anyone can use it non-commercially, or modify it, extend it,
translate it into other languages, or even make a movie of its exciting plot.  However, any use must 
stick to the legal fine-print at http://creativecommons.org/licenses/by-nc-sa/3.0/

The first chapter is an extract from a tutorial by Yusuf Motara.  Find the full
tutorial at http://www.ict.ru.ac.za/resources/way_of_the_program/.  
He based some of it on an earlier version of a Python book at
http://openbookproject.net/thinkcs/python/english3e/, and that was
based on yet an earlier version by different authors.   
And even though Yusuf wrote most of the text in this chapter,
it was subsequently modified by the current author, 
Peter Wentworth, who bears ultimate responsibility for 
any errors, omissions, or mistakes...

    

Why?
---- 

As an educator, the book tries to do a few things that are
different from many of the usual approaches.  

I'm trying to move gently towards the idea of Computation-as-Interaction,
rather than Computation-as-Calculation.  These days
we interact with computational artefacts through GUIs.  Notions
of events and responses are more prevalent than console-based
top-to-bottom calculation.  Many of our students have never seen
a command-line program, and perhaps some never will.  
Lynn Andrea Stein's *Rethinking CS101* project has been
influential, see http://www.cs101.org/ 

In recent times the theory of *threshold concepts* and *troublesome concepts*
has become a fashionable and useful lens for understanding why students
have difficulty transitioning to newer ways of thinking about a subject.
Although there probably won't be widespread agreement soon about what exactly
the threshold concepts are for our students, the book does isolate and emphasize
some concepts like state, flow of control, recursion, representation, data
transformation, mental chunking, and so on.  If you've not encountered this
idea yet, see http://crpit.com/confpapers/CRPITV95Rountree.pdf or
looking for Threshold Concepts on Google will do the trick. 

I like visual examples.  Some of our students, sadly, often seem very
weak in the cognitive areas of what Piaget theory refers to
as "Formal Operations" and the ability to abstract, manipulate, reason
about, and control the internal, non-visible state of the program.  This 
means they're more comfortable with more concrete approaches.

So where possible, I prefer the state of the computation to be explicitly visible.  
To this end I use a Logo-like turtle, and I use the graphical controls that are
available in Windows Presentation Foundation quite heavily.   And when we
first tackle recursion, I go for turtle-based fractal curves because of 
this explicit visibility of the internal unfolding of the algorithm.

Our literature abounds too with studies that show that students who are
left to infer and construct their own internal cognitive models of computation
get it wrong much of the time.  So they end up with non-viable models 
of even the most fundamental ideas, like how assignment works.  
(Non-viable means their conceptualization may work in some situations, but
it gives wrong answers in other cases.)  So I put a strong emphasis
on debugging, single-stepping, inspecting the internal state of the computation
as it runs, etc. in the hope that we can build accurate internal mental models. 

Ask yourself or your teaching peers to take their best guess as to what percentage of 
students typically come out of a year-long programming course with a viable understanding 
of value assignment, or with a viable understanding of reference assignment.  
See how that compares with the study at http://dl.acm.org/citation.cfm?id=1227481 
 
And then, of course, we must face the question: "Do we do objects 
right up front, early, or do we delay them while we try to build
the algorithmic and coding skills in a procedural way?"  I advocate
early *exposure* to objects, terminology, and use-cases, but late 
*synthesis* of objects.  A GUI-builder and some turtle examples seem ideal for
this.  We can work with objects from a library, and build familiarity with
the OOP concepts like instances, properties, internal state, lifetimes, events, inheritance
and so on way before we get into "Now synthesize your own objects".  On the
same theme, I have a distaste for introductory approaches that use very trivial
(or minimalist) classes.  Nor do I like OOP examples that give rise to just a single 
instance.  Here I find students struggle to separate the idea of the class as a factory,
and the instances that it produces.

So from the first time students see the Button type, this book will have at 
least two instances, or at least two turtles.  When we do inheritance,
I prefer inheriting from something that already has significant complexity
and functionality.  (In this book we inherit the Turtle type and give it
some extra and some overriding behaviour.)

There is also a tendency in the book to often introduce concepts "informally"
the first time, just because we need it for a compelling current example.
Then later we'll hit the chapters where we revisit and cover the concept more fully.
So you might bump your head against a *while* loop in Chapter 7, and 
some arrays in Chapter 8, even though the chapters on loops and arrays only
come later in the book.  I hope this provides an early motivating context for 
the construct or for the feature.  It also seems useful to be able to tell
students "Sure, we haven't done that.  
So go see what you can find out about it on your own."


The  Tips_ appendix in this book also explains some of my thinking around
teaching Computer Science: I like to cover the appendix material explicitly
with my students. 

.. _Tips: ./tips.html


Enjoy the book.

Peter Wentworth,  January 2015.



 
