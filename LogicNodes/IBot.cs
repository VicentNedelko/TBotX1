﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gira_com_by.Logic.Nodes
{
    public interface IBot
    {
        Task SendMessageAsync(string message);
    }
}