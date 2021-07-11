using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace HtmlFormCreate
{
    class JsonInfoClass 
    {
        public class Rootobject //copy in clipboard and Edit -> Paste Special -> Paste Json as Classes
        {
            public Item[] items { get; set; }
        }

        public class Item
        {
            public Owner owner { get; set; }
            public bool is_answered { get; set; } 
            public int creation_date { get; set; } 
            public string link { get; set; } 
            public string title { get; set; } 
        }

        public class Owner
        {
            public string display_name { get; set; }
            public string link { get; set; } //may be will need?
        }

    }
}