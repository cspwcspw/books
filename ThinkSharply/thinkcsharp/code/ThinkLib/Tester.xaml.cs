
//   #define UseConsole

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

// Pete Wentworth, January 2013.   Part of the book "Think Sharply with C#". 

namespace ThinkLib
{
    /// <summary>
    /// Encapsulates a very simple "procedural" test framework.  
    /// There is no setup, teardown, etc. that one usually finds, and just two static methods are provided:
    ///      TestEq tests for two (deep) equal values
    ///      TestFailEq succeeds if an Eq test fails.
    ///      
    /// A non-modal form showing the test results is created and shown on each use when it does not already exist, .  
    /// or was closed by the user.
    /// </summary>
    [System.Diagnostics.DebuggerStepThrough] // Ensure a student doesn't inadvertently step into these innards...
    public partial class Tester
    {
        #region Fields

        static Tester theTester = null;  // This is a singleton class.

        double currFontSize = 10;  


#if UseConsole
        static int passes=0, fails=0;               // Keep counters. 
         bool gotClosed = false;          // Keep track of whether user closed the window.
#else
                int passes, fails;               // Keep counters. 
        bool gotClosed = false;          // Keep track of whether user closed the window.
#endif

        #endregion

        #region Private methods

        private Tester()
        {
            InitializeComponent();
            // Make sure the application terminates if the main window closes.
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            txtTestResults.FontSize =   ThinkLib.Properties.Settings.Default.PreferredFontSize;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            gotClosed = true;
        }


        private void MakeFontBigger_Click(object sender, RoutedEventArgs e)
        {
            changeFontSize(1);
        }

        private void MakeFontSmaller_Click(object sender, RoutedEventArgs e)
        {
            changeFontSize(-1);
        }

        private void changeFontSize(int delta)
        {
            Properties.Settings curr = ThinkLib.Properties.Settings.Default;
            if (delta > 0 && curr.PreferredFontSize >= 50) return;
            if (delta < 0 && curr.PreferredFontSize <= 7) return;
            curr.PreferredFontSize += delta;
            txtTestResults.FontSize = curr.PreferredFontSize;
            curr.Save();
        }

        private static Action emptyDelegate = delegate { };

        private static void flush()
        {
            // Now force the GUI to update and display this latest message.  Without this, a 
            // student who writes an infinite loop gets tricked. The system hangs, but 
            // earler tests that have already run have not managed to get their results
            // flushed to the GUI.
            theTester.Dispatcher.Invoke(emptyDelegate, System.Windows.Threading.DispatcherPriority.SystemIdle);
        }

#if UseConsole
        private static void showOutcome(bool passed, string outcome)
        {   // The first time this static method is called it creates the window.

            // Update the test results
            Console.WriteLine(outcome);
            if (passed)
            {
                Tester.passes++;
            }
            else
            {
                Tester.fails++;
            }
            Console.Title = string.Format("ThinkLib Tester: passed {0};  failed {1}",
                   Tester.passes, Tester.fails);
       //     flush();  // Update May 2013.  Make sure the message shows up now.
        }
#else

        private static void showOutcome(bool passed, string outcome)
        {   // The first time this static method is called it creates the window.
            if (theTester == null)
            {
                theTester = new Tester();
                theTester.Show();
            }

            if (theTester.gotClosed)  // Update May 2013: instead of returning, just reopen the window.
            {
                theTester = new Tester();
                theTester.Show();
            }
            // Update the test results
            theTester.txtTestResults.AppendText(outcome);
            theTester.txtTestResults.AppendText("\n");

            // Scroll the textbox if necessary
            theTester.txtTestResults.ScrollToEnd();



            if (passed)
            {
                theTester.passes++;
            }
            else
            {
                theTester.fails++;
            }
            theTester.lblCounter.Content = string.Format("Passed {0};  failed {1}",
                   theTester.passes, theTester.fails);
            flush();  // Update May 2013.  Make sure the message shows up now.
        }
#endif

        // 31 Oct 2015.  Factor out the AbsTolerance and give it a small initial value
        //               to make the interface easier for students.

        /// <summary>
        /// Get or set an absolute tolerance for equality comparisons that use floating point numbers.
        /// The default is a small value, i.e. 0.001, so any two numbers closer than this are 
        /// considered equal.
        /// </summary>
        public static double AbsTolerance { get; set; }

        static Tester() { AbsTolerance = 0.01; }

        static private string doubleEq(double actual, double expected)
        {
                double absDiff = Math.Abs(expected - actual);
                if (absDiff <= AbsTolerance) return null;  // pass the test 
                return string.Format("Got {0}, expected {1}.", actual, expected);
        }

        static private bool isNumericType<T>(T x)
        {
            var to_test = typeof(T);
            if (to_test == typeof(float)) return true;
            if (to_test == typeof(int)) return true;
            if (to_test == typeof(double)) return true;
            if (to_test == typeof(short)) return true;
            if (to_test == typeof(long)) return true;
            if (to_test == typeof(uint)) return true;
            if (to_test == typeof(ushort)) return true;
            if (to_test == typeof(ulong)) return true;
            if (to_test == typeof(byte)) return true;
            if (to_test == typeof(sbyte)) return true;
            return false;
        }

        static bool isList(Type t)
        {
            return (t.FullName.StartsWith("System.Collections.Generic.List"));
        }

        static private string myEq<T, U>(T actual, U expected)  
        {
            // Test as many simple cases as we can (with some type coercion, etc. just for
            // the purposes of supporting the textbook.
            if (actual == null && expected == null) return null;  // passes
            if (actual == null) return string.Format("Got null, expected {0}.", expected);
            if (expected == null) return string.Format("Got {0}, expected null.", actual);

            if (actual.Equals(expected)) return null;  // passes
            var t_type = typeof(T);
            var u_type = typeof(U);

            var t_is_array = t_type.IsArray;
            var u_is_array = u_type.IsArray;
            if (t_is_array && u_is_array)
            {
                // type-safety is for people who don't know they're dealing with arrays.
                return arrayEq((dynamic)actual, (dynamic)expected); 
            }
            else if (t_is_array || u_is_array)
            {
                return String.Format("You can't compare {0}s and {1}s!", typeof(T), typeof(U));
            }

            // Convert List<T> into Array<T>
            if (isList(t_type) && isList(u_type))
            {
                var v1 = ((dynamic)actual).ToArray();
                var v2 = ((dynamic)expected).ToArray();
                return myEq(v1, v2);  
            }

            // from here on in, we're guaranteed to NOT be dealing with arrays.

            var singleType = typeof(float);
            var doubleType = typeof(double);
            if (isNumericType(actual) && isNumericType(expected))
            {
                try
                {
                    return doubleEq((dynamic)actual, (dynamic)expected);  
                }
                catch
                {
                    return String.Format("You can't compare {0}s and {1}s!", typeof(T), typeof(U));
                }
            }
            if (typeof(T) != typeof(U))
            {
                return String.Format("You can't compare {0}s and {1}s!", typeof(T), typeof(U));
            }

            // 24 May 2014. Add extra quotes around strings to make it easier to spot 
            //              problems with leading or trailing space differences.

            if (actual.GetType() == typeof(string))
            {
                return String.Format("Got \"{0}\", expected \"{1}\".", actual, expected);
            }
            else if (actual.GetType() == typeof(char))
            {
                return String.Format("Got '{0}', expected '{1}'.", actual, expected);
            }
            else {
               return String.Format("Got {0}, expected {1}.", actual, expected); // the types will be the same; the type-system sayeth so.
            }
        }

        private static string arrayEq<T,U>(T[] actual, U[] expected)   
        {
            if (actual.Length != expected.Length)
            {
                return String.Format("Got {0}, expected {1}.", StringifyArray(actual), StringifyArray(expected));
            }
            var failmsg = actual.Zip(expected, (a, e) => myEq(a, e)).FirstOrDefault(x => x != null);
            if (failmsg != null)
            {
                return String.Format("\n   Actual:   {0}\n   Expected: {1}\n   {2}", StringifyArray(actual), StringifyArray(expected), failmsg);
            }
            return null;
        }

        private static string StringifyArray<T>(T[] xs)
        {
            var strings = xs
                .Select(x => typeof(T).IsArray ? StringifyArray((dynamic)x) : x == null ? "(null)" : 
                      (myToString(x)));
            return "[" + String.Join(", ", strings) + "]";
        }

        // Wrap strings and chars in appropriate quotes
        private static string myToString(object x)
        {
            if (x.GetType() == typeof(string))  return "\"" + x.ToString() + "\"";
            if (x.GetType() == typeof(char)) return "'" + x.ToString() + "'";
            return x.ToString();
        }

        static private void TestEqInternal<T, U>(Func<T> f, U expected, bool negateTest)   
        {   // This overloading handles the more general objects.

            int lineNum = new System.Diagnostics.StackTrace(true).GetFrame(2).GetFileLineNumber();
            string msg = "";
            try
            {
                msg = myEq(f(), expected);   
                if (negateTest)
                {
                    if (String.IsNullOrEmpty(msg))
                    {
                        showOutcome(false, string.Format("Test should have failed, but passed, line {0}.", lineNum));
                    }
                    else
                    {
                        showOutcome(true, string.Format("Test passed, line {0}.", lineNum));
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(msg))
                    {
                        showOutcome(true, string.Format("Test passed, line {0}.", lineNum));
                    }
                    else
                    {
                        showOutcome(false, string.Format("Test failed, line {0}. {1}", lineNum, msg));
                    }
                }
            }
            catch (Exception e)
            {
                // bleh, need to parse the exception stacktrace... *sigh*.
                var re = new Regex(@"^.*\.(?<method>[a-zA-Z0-9_]+)\(.*\) in .*\\(?<file>[^\\]+):line (?<line>\d+)", RegexOptions.ExplicitCapture | RegexOptions.Multiline);
                var m = re.Match(e.StackTrace);
                string line = m.Success ? m.Groups["line"].Captures[0].Value : "??";
                string file = m.Success ? m.Groups["file"].Captures[0].Value : "(unknown file)";
                string method = m.Success ? m.Groups["method"].Captures[0].Value : "(unknown method)";
                showOutcome(false, String.Format("Test failed, line {0}. {1} error on line {2} ({3}, method {4})", lineNum, e.GetType().Name, line, file, method));
            }
        }

        #endregion

        #region Public TestEq methods callable by clients

        /// <summary>
        /// Test if two objects are (approximately) equal.
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        // can't replace this with TestEqInternal because the stack-frame count will be different if I do.
        static public void TestEq<T, U>(Func<T> actual, U expected)   
        {
            TestEqInternal(actual, expected, false);  
        }

        static public void TestEq(int actual, int expected)
        {
            TestEqInternal(() => actual, expected, false);    
        }
        static public void TestEq(double actual, double expected)
        {
            TestEqInternal(() => actual, expected, false);   
        }
        static public void TestEq(float actual, double expected)
        {
            TestEqInternal(() => (double)actual, expected, false);   
        }
        static public void TestEq(double actual, float expected)
        {
            TestEqInternal(() => actual, (double)expected, false);    
        }
        static public void TestEq(float actual, float expected)
        {
            TestEqInternal(() => (double)actual, (double)expected, false);  
        }
        static public void TestEq(string actual, string expected)
        {
            TestEqInternal(() => actual, expected, false);  
        }
        static public void TestEq(char actual, char expected)
        {
            TestEqInternal(() => actual, expected, false);  
        }
        static public void TestEq(bool actual, bool expected)
        {
            TestEqInternal(() => actual, expected, false);  
        }
        /* 1D-array variants - BEGIN */
        static public void TestEq(int[] actual, int[] expected)
        {
            TestEqInternal(() => actual, expected, false);    
        }
        static public void TestEq(double[] actual, double[] expected)   
        {
            TestEqInternal(() => actual, expected, false);   
        }
        static public void TestEq(float[] actual, double[] expected)  
        {
            TestEqInternal(() => actual.Select(x => (double)x).ToArray(), expected, false); 
        }
        static public void TestEq(double[] actual, float[] expected)  
        {
            TestEqInternal(() => actual, expected.Select(x => (double)x).ToArray(), false);    
        }
        static public void TestEq(float[] actual, float[] expected)   
        {
            TestEqInternal(() => actual.Select(x => (double)x).ToArray(), expected.Select(x => (double)x).ToArray(), false);  
        }
        static public void TestEq(string[] actual, string[] expected)
        {
            TestEqInternal(() => actual, expected, false);   
        }
        static public void TestEq(char[] actual, char[] expected)
        {
            TestEqInternal(() => actual, expected, false);  
        }
        static public void TestEq(bool[] actual, bool[] expected)
        {
            TestEqInternal(() => actual, expected, false);  
        }
        /* 1D-array variants - END */


        /* This is to handle everything else, e.g. 2D arrays, comparing chars to floats, objects, etc. */
        static public void TestEq<T, U>(T actual, U expected)    
        {
            TestEqInternal(() => actual, expected, false); 
        }

        // this is to handle the case where people pass in untyped nulls.
        static public void TestEq<T>(object x, T y) 
        {
            TestEqInternal(() => (T)x, y, false);   
        }
        // nulls causing trouble...?  What a surprise.
        static public void TestEq<T>(T x, object y)   
        {
            TestEqInternal(() => x, (T)y, false);    
        }
        #endregion

        #region Public TestFailEq methods callable by clients

        /// <summary>
        /// Test passes if TestEq would fail on these arguments. 
        /// </summary>
        // can't replace this with TestEqInternal because the stack-frame count will be different if I do.
        static public void TestFailEq<T, U>(Func<T> actual, U expected)   
        {
            TestEqInternal(actual, expected, true);  
        }

        /// <summary>
        /// Test passes if TestEq would fail on these arguments. 
        /// </summary> 
        static public void TestFailEq(int actual, int expected)
        {
            TestEqInternal(() => actual, expected, true);  
        }

        /// <summary>
        /// Test passes if TestEq would fail on these arguments. 
        /// </summary> 
        static public void TestFailEq(double actual, double expected)  
        {
            TestEqInternal(() => actual, expected, true);   
        }
        /// <summary>
        /// Test passes if TestEq would fail on these arguments. 
        /// </summary> 
        static public void TestFailEq(float actual, double expected)  
        {
            TestEqInternal(() => (double)actual, expected, true);  
        }
        /// <summary>
        /// Test passes if TestEq would fail on these arguments. 
        /// </summary> 
        static public void TestFailEq(double actual, float expected)  
        {
            TestEqInternal(() => actual, (double)expected, true);  
        }
        /// <summary>
        /// Test passes if TestEq would fail on these arguments. 
        /// </summary> 
        static public void TestFailEq(float actual, float expected)
        {
            TestEqInternal(() => (double)actual, (double)expected, true);  
        }
        /// <summary>
        /// Test passes if TestEq would fail on these arguments. 
        /// </summary> 
        static public void TestFailEq(string actual, string expected)
        {
            TestEqInternal(() => actual, expected, true);
        }
        /// <summary>
        /// Test passes if TestEq would fail on these arguments. 
        /// </summary> 
        static public void TestFailEq(char actual, char expected)
        {
            TestEqInternal(() => actual, expected, true);
        }
        /// <summary>
        /// Test passes if TestEq would fail on these arguments. 
        /// </summary> 
        static public void TestFailEq(bool actual, bool expected)
        {
            TestEqInternal(() => actual, expected, true);
        }
        /* 1D-array variants - BEGIN */
        /// <summary>
        /// Test passes if TestEq would fail on these arguments. 
        /// </summary> 
        static public void TestFailEq(int[] actual, int[] expected)
        {
            TestEqInternal(() => actual, expected, true);
        }
        /// <summary>
        /// Test passes if TestEq would fail on these arguments. 
        /// </summary> 
        static public void TestFailEq(double[] actual, double[] expected)
        {
            TestEqInternal(() => actual, expected, true);
        }
        /// <summary>
        /// Test passes if TestEq would fail on these arguments. 
        /// </summary> 
        static public void TestFailEq(float[] actual, double[] expected)
        {
            TestEqInternal(() => actual.Select(x => (double)x).ToArray(), expected, true);
        }
        /// <summary>
        /// Test passes if TestEq would fail on these arguments. 
        /// </summary> 
        static public void TestFailEq(double[] actual, float[] expected)
        {
            TestEqInternal(() => actual, expected.Select(x => (double)x).ToArray(), true);
        }
        /// <summary>
        /// Test passes if TestEq would fail on these arguments. 
        /// </summary> 
        static public void TestFailEq(float[] actual, float[] expected)
        {
            TestEqInternal(() => actual.Select(x => (double)x).ToArray(), expected.Select(x => (double)x).ToArray(), true);
        }
        /// <summary>
        /// Test passes if TestEq would fail on these arguments. 
        /// </summary> 
        static public void TestFailEq(string[] actual, string[] expected)
        {
            TestEqInternal(() => actual, expected, true);
        }
        /// <summary>
        /// Test passes if TestEq would fail on these arguments. 
        /// </summary> 
        static public void TestFailEq(char[] actual, char[] expected)
        {
            TestEqInternal(() => actual, expected, true);
        }
        /// <summary>
        /// Test passes if TestEq would fail on these arguments. 
        /// </summary> 
        static public void TestFailEq(bool[] actual, bool[] expected)
        {
            TestEqInternal(() => actual, expected, true);
        }
        /* 1D-array variants - END */


        /* This is to handle everything else, e.g. 2D arrays, comparing chars to floats, objects, etc. */
        /// <summary>
        /// Test passes if TestEq would fail on these arguments. 
        /// </summary> 
        static public void TestFailEq<T, U>(T actual, U expected)
        {
            TestEqInternal(() => actual, expected, true);
        }

        // this is to handle the case where people pass in untyped nulls.
        /// <summary>
        /// Test passes if TestEq would fail on these arguments. 
        /// </summary> 
        static public void TestFailEq<T>(object x, T y)
        {
            TestEqInternal(() => (T)x, y, true);
        }
        // nulls causing trouble...?  What a surprise.
        /// <summary>
        /// Test passes if TestEq would fail on these arguments. 
        /// </summary> 
        static public void TestFailEq<T>(T x, object y)
        {
            TestEqInternal(() => x, (T)y, true);
        }
        #endregion

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            passes = 0;
            fails = 0;
            txtTestResults.Clear();
            txtTestResults.ScrollToEnd();

        }



    }
}
