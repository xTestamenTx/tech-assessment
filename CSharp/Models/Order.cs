﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CSharp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Total { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }
        public virtual List<Item> Items { get;set;}
    }
}
