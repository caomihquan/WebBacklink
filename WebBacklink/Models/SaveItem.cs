using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBacklink.Models
{
    [Serializable]
    public class SaveItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}