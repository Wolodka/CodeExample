using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntraserviceTasks.Entities.Enums;

namespace IntraserviceTasks.Entities
{
    public class TaskLifeTime : CrmConvertable
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public TaskPriority TaskPrioriry { get; set; }
        public bool IsStandartContract { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public DateTime StartOfApproval { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime ApprovalDeadline { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public BusinessUnit BusinessUnit { get; set; }

        private Person _approvalPerson = new Person();
        public Person ApprovalPerson 
        { 
            get 
            { 
                return _approvalPerson; 
            } 
            set 
            { 
                _approvalPerson = value; 
            } 
        }
        
        public string Version { get; set; }
        
        public Guid CrmGuid { get; set; }
    }
}
