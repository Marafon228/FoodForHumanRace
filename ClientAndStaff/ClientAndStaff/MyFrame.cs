using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ClientAndStaff
{
    public class MyFrame : Frame
    {
        public INavigation CurrentNavigation { get; private set; }

        public MyFrame(INavigation navigation)
        {
            CurrentNavigation = navigation;
        }

    }
}
