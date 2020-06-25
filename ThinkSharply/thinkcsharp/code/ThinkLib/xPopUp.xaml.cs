using System;

using System.Windows;

// This silly workaround lets me flush partial WPF drawing to the display
// in my turtle library.  The standard way of doing this (by fiddling with the
// dispatcher) doesn't work properly when single-stepping in the debugger: the debugger 
// grabs focus before the WPF windows are drawn.   But if the turtle opens a new
// modal form, (i.e this one), even momentarily, it somehow forces the dispatcher to
// get the owner window fully rendered before the debugger grabs control again.

namespace ThinkLib
{
    [System.Diagnostics.DebuggerStepThrough] // Ensure a student doesn't inadvertently step into these innards...
    public partial class xPopUp : Window
    {
        public xPopUp()
        {
            InitializeComponent();
            this.Opacity = 0.0;  // Make this window transparent to the user. 
        }
        
        protected override void OnContentRendered(EventArgs e)
        {   // As soon as it has been rendered, it's job is done. Close it.
            base.OnContentRendered(e);
            this.Close();
        }
    }
}
