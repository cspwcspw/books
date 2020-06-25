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

namespace SMSMessageExercise
{

    public partial class SMS_GUI : Window
    {

        SMSStore theInbox;

        public SMS_GUI()
        {
            InitializeComponent();
            theInbox = new SMSStore();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            theInbox.AddSMS(txtSender.Text, ttxMessage1.Text);
        }

        private void btnReadMsg_Click(object sender, RoutedEventArgs e)
        {
            int num = Convert.ToInt32(txtMessageNum.Text);
            SMSMessage msg = theInbox.GetMessage(num);
            string title = string.Format("#{0} From {1}  received {2}.", num, msg.OtherNumber, msg.TimeStamp);
            msg.MarkAsRead();
            MessageBox.Show(msg.MessageText, title);

        }

        private void btnGetStatus_Click(object sender, RoutedEventArgs e)
        {
            List<int> unreadMessages = theInbox.GetUnreadIndexes();
            int count = theInbox.GetCount();

            txtResult.Text = string.Format("{0} messages, {1} unread.\n", count, unreadMessages.Count);
            for (int msgNum=count-1; msgNum>=0; msgNum--) 
            {  
                SMSMessage msg = theInbox.GetMessage(msgNum);
                string txt = msg.MessageText;
                if (txt.Length > 18)
                {
                    txt = txt.Substring(0, 15) + "...";
                }
                string header = string.Format("{0} #{1,3}: From {2,13}: {3:18}\n",
                    unreadMessages.Contains(msgNum) ?  "*": " ",
                    msgNum, msg.OtherNumber, txt);
                    txtResult.AppendText(header);
            }
            txtResult.ScrollToEnd();
        }

    }
}
