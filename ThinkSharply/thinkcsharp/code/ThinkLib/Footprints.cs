using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

// Pete Wentworth, January 2013.   Part of the book "Think Sharply with C#". 

namespace ThinkLib
{
    /// <summary>
    /// This encapsulates a single footprint from a turtle.
    /// </summary>
    [System.Diagnostics.DebuggerStepThrough] // Ensure a student doesn't inadvertently step into these innards...
    public class Footprint
    {
        /// <summary>
        /// Gets the position of the footprint.
        /// </summary>
        public Point Position
        {
            get 
            {
                Point pt = theUIElem.RenderTransform.Transform(new Point(0, 0));
                return pt;
            }
        }

        /// <summary>
        /// Gets the time when the footprint was created.  We might want to know how old a footprint is.
        /// </summary>
        public DateTime Created { get; private set; }
 

        /// <summary>
        /// Gets the UIElement associated with this footprint.
        /// </summary>
        public UIElement theUIElem { get; private set; }

        internal Footprint(UIElement uie) 
        {
            Created = DateTime.Now;
            this.theUIElem = uie;
        }

        /// <summary>
        /// Moves the footprint by this amount.  The Position of the footprint will change. 
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public void Offset(double dx, double dy)
        {
            TransformGroup tg = theUIElem.RenderTransform as TransformGroup;
            tg.Children.Add(new TranslateTransform(dx, dy));

        }
    }
    
    /// <summary>
    /// Manages a collection of footprints, ensuring that additions or deletions from the list also 
    /// add or delete the UI component of the footprint from the playground.   Any methods that
    /// work on Lists will also work here. 
    /// </summary>
    [System.Diagnostics.DebuggerStepThrough] // Ensure a student doesn't inadvertently step into these innards...
    public class FootprintCollection : IList<Footprint>
    {
        private List<Footprint> theFootprints;
        Canvas playground;

        /// <summary>
        /// Manages a list of Footprints, ensuring that additions or deletions from the list also 
        /// add or delete the UI component of the Footprint from the playground.
        /// </summary>
        /// <param name="playground">The Playground on which the footprints are displayed.</param>
        public FootprintCollection(Canvas playground)
        {
            this.playground = playground;
            theFootprints = new List<Footprint>();
        }

        /// <summary>
        /// Searches for the specific Footprint and returns the index of the first occurrence in the collection.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(Footprint item)
        {
            return theFootprints.IndexOf(item);
        }

        /// <summary>
        /// Inserts a Footprint into the collection at the specified index position.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, Footprint item)
        {
            theFootprints.Insert(index, item);
            playground.Children.Add(item.theUIElem);
        }

        /// <summary>
        /// Removes the item at the specified index position.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            Footprint item = theFootprints[index];
            playground.Children.Remove(item.theUIElem);
            theFootprints.RemoveAt(index);
        }

        /// <summary>
        /// Gets or sets the Footprint at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Footprint this[int index]
        {
            get
            {
                return theFootprints[index];
            }
            set
            {
                if (theFootprints[index] != null)
                {
                    playground.Children.Remove(theFootprints[index].theUIElem);
                }
                theFootprints[index] = value;
                playground.Children.Add(value.theUIElem);
            }
        }

        /// <summary>
        /// Add a new footprint to the end of the collection.
        /// </summary>
        /// <param name="item"></param>
        public void Add(Footprint item)
        {
            theFootprints.Add(item);
            playground.Children.Add(item.theUIElem);
        }

        /// <summary>
        /// Determines whether the item is in the collection of footprints.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(Footprint item)
        {
            return theFootprints.Contains(item);
        }

        /// <summary>
        /// Copy a collection to an array.
        /// </summary>
        /// <param name="array">The array to copy the items to.</param>
        /// <param name="arrayIndex">The index at which copying is to start.</param>
        public void CopyTo(Footprint[] array, int arrayIndex)
        {
            theFootprints.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of Footprints in the collection.
        /// </summary>
        public int Count
        {
            get { return theFootprints.Count; }
        }

        /// <summary>
        /// Returns false
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes the specified Footprint from the collection.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(Footprint item)
        {
            bool b = theFootprints.Remove(item);
            playground.Children.Remove(item.theUIElem);
            return b;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection of footprints.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Footprint> GetEnumerator()
        {
            return theFootprints.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection of footprints.
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return theFootprints.GetEnumerator();
        }

        /// <summary>
        /// Removes all footprints from the collection.
        /// </summary>
        public void Clear()
        {
            foreach (var el in theFootprints)
            {
                playground.Children.Remove(el.theUIElem);
            }
            theFootprints.Clear();
        }
    }
}
 
