using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gira_com_by.Logic.Nodes.Models
{
    public class Message
    {
        public int Message_id { get; set; }
        public From From { get; set; }
        public Chat Chat { get; set; }
        public int Date { get; set; }
        public string Text { get; set; }
    }
}
