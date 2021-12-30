﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDtoRecipe.Lib
{
    internal class PagePart
    {
        public int Index { get; set; }
        public string Content { get; set; }

        public PagePart(int index)
        {
            this.Index = index;
        }
    }
}