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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.InteropServices;

 

namespace DirectoryLister
{
 
    public partial class DirectoryLister_GUI : Window
    {
        public DirectoryLister_GUI()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            lblHeading.Content = String.Format("Folder listing of {0}\n", txtPath.Text);
            txtResult.Clear();
            showFilesIn(txtPath.Text, txtPattern.Text);
          //  txtResult.AppendText(String.Format("*{0}\n", System.IO.Path.GetFileName(txtPath.Text)));
          //  showFilesAsTree(txtPath.Text, txtPattern.Text, "| ");
        }


         static string ResolveShortcut(string filePath)
        {
  
            //https://astoundingprogramming.wordpress.com/2012/12/17/how-to-get-the-target-of-a-windows-shortcut-c/
            // IWshRuntimeLibrary is in the COM library "Windows Script Host Object Model"
            IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();

            try
            {
                IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(filePath);
            //     bool tp = shortcut.TargetPath.Contains("jam10");
 
         //       string fn = shortcut.FullName;
             //   string wd = shortcut.WorkingDirectory;
                //if (tp)
                //{
                //    shortcut.WorkingDirectory = wd.Replace("%epw%", "C:\\EPW");
                //}
                return shortcut.TargetPath;
            }
            catch (Exception)
            {
               
             
                // A COMException is thrown if the file is not a valid shortcut (.lnk) file 
                return null;
            }
        }

         private string GetShortcutTarget(string file)
         {   // https://astoundingprogramming.wordpress.com/2012/12/17/how-to-get-the-target-of-a-windows-shortcut-c/ 
             // This is not part of the textbook, just a quick and dirty I needed.
             try
             {
                 if (System.IO.Path.GetExtension(file).ToLower() != ".lnk")
                 {
                     throw new Exception("Supplied file must be a .LNK file");
                 }

                 FileStream fileStream = File.Open(file, FileMode.Open, FileAccess.Read);
                 using (System.IO.BinaryReader fileReader = new BinaryReader(fileStream))
                 {
                     fileStream.Seek(0x14, SeekOrigin.Begin);     // Seek to flags
                     uint flags = fileReader.ReadUInt32();        // Read flags
                     if ((flags & 1) == 1)
                     {                      // Bit 1 set means we have to
                         // skip the shell item ID list
                         fileStream.Seek(0x4c, SeekOrigin.Begin); // Seek to the end of the header
                         uint offset = fileReader.ReadUInt16();   // Read the length of the Shell item ID list
                         fileStream.Seek(offset, SeekOrigin.Current); // Seek past it (to the file locator info)
                     }

                     long fileInfoStartsAt = fileStream.Position; // Store the offset where the file info
                     // structure begins
                     uint totalStructLength = fileReader.ReadUInt32(); // read the length of the whole struct
                     fileStream.Seek(0xc, SeekOrigin.Current); // seek to offset to base pathname
                     uint fileOffset = fileReader.ReadUInt32(); // read offset to base pathname
                     // the offset is from the beginning of the file info struct (fileInfoStartsAt)
                     fileStream.Seek((fileInfoStartsAt + fileOffset), SeekOrigin.Begin); // Seek to beginning of
                     // base pathname (target)
                     long pathLength = (totalStructLength + fileInfoStartsAt) - fileStream.Position - 2; // read
                     // the base pathname. I don't need the 2 terminating nulls.
                     char[] linkTarget = fileReader.ReadChars((int)pathLength); // should be unicode safe
                     var link = new string(linkTarget);
                     char fc = link[0];


                     int begin = link.IndexOf("\0\0");
                     if (begin > -1)
                     {
                         int end = link.IndexOf("\\\\", begin + 2) + 2;
                         end = link.IndexOf('\0', end) + 1;

                         string firstPart = link.Substring(0, begin);
                         string secondPart = link.Substring(end);

                         return firstPart + secondPart;
                     }
                     else
                     {
                         return link;
                     }
                 }
             }
             catch
             {
                 return "";
             }
         }

        private void showFilesIn(string path, string pattern)
        {   
            foreach (string filename in Directory.GetFiles(path, pattern))
            {

                string target = GetShortcutTarget(filename);
             //   string target = ResolveShortcut(filename);
                if (!string.IsNullOrEmpty(target))
                {
                    txtResult.AppendText(String.Format("{0} --> {1}\n", filename, target));
                }
            }

            foreach (string foldername in Directory.GetDirectories(path))
            {
                showFilesIn(foldername, pattern);
            }         
        }

        private void showFilesAsTree(string path, string pattern, string prefix)
        {
            foreach (string filename in Directory.GetFiles(path, pattern))
            {
                txtResult.AppendText(String.Format("{0}-{1}\n", prefix,  System.IO.Path.GetFileName(filename)));
            }

            foreach (string foldername in Directory.GetDirectories(path))
            {
                txtResult.AppendText(String.Format("{0}*{1}\n", prefix, System.IO.Path.GetFileName(foldername)));
                showFilesAsTree(foldername, pattern, prefix + "| ");
            }
        }
    }
}
