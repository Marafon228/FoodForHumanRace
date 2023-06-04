﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace ClientAndStaff.Models
{
    public class AddOrder
    {
        public string LoginUser { get; set; }
        public string Description { get; set; }
        public ManuProduct[] ManuProducts { get; set; }
    }

    public class ManuProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; } = 1;

    }
}
