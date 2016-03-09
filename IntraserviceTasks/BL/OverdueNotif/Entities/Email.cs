using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.Entities;

namespace IntraserviceTasks.BL.OverdueNotif
{
    public class Email
    {
        public List<Person> Recipients { get; set; }
        public List<Person> CC { get; set; }
        public List<Person> BCC { get; set; }

        public string Subject  { get; set; }
        public string Body  { get; set; }

        public Email()
        {
            Recipients = new List<Person>();
            CC = new List<Person>();
            BCC = new List<Person>();
        }
    }
}
