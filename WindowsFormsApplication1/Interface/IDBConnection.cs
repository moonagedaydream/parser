﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace InvertedIndex
{
    interface IDBConnection
    {
        bool Open();
        bool Close();
        DataTable ExecuteQuery(string query);
    }
}
