using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Entities;
using IntraserviceTasks.Res;

namespace IntraserviceTasks.BL.OverdueNotif
{
    class EmailGenerator
    {
        private NotificationsByUserAndBusinessUnit _notification;
        private Email _email;

        public EmailGenerator(NotificationsByUserAndBusinessUnit notification)
        {
            _notification = notification;
        }

        public Email CreateEmail()
        {
            _email = new Email();

            AppendRecipient();

            AppendCopyRecipient();

            BuildSubject();

            BuildBody();

            return _email;
        }

        private void AppendRecipient()
        {
            Guid approvalPersonGuid = ExternalSystems.CrmUserRepository.Get()
                                            .GetGuidByLogin(_notification.UserLogin);

            Person approvalPerson = new Person()
            {
                CrmGuid = approvalPersonGuid,
                Login = _notification.UserLogin
            };
            _email.Recipients.Add(approvalPerson);
        }

        private void AppendCopyRecipient()
        {
            List<Guid> ccGuids = new List<Guid>();
            if (_notification.BusinessUnit == Entities.Enums.BusinessUnit.AIiIT)
            {
                ccGuids = ConstantsOverdueNotifier.AIiIT_CC_ADDRESSATS_GUIDS;
            }
            else if (_notification.BusinessUnit == Entities.Enums.BusinessUnit.Atrinity)
            {
                ccGuids = ConstantsOverdueNotifier.ATRINITY_CC_ADDRESSATS_GUIDS;
            }

            List<Person> ccPersons = ccGuids.Select(o => new Person { CrmGuid = o }).ToList();
            _email.CC.AddRange(ccPersons);
        }

        private void BuildSubject()
        {
            TryToGetAddressatName();
            _email.Subject = _addressatName + ". Требуется согласование договоров";
        }

        private string _addressatName;

        private void TryToGetAddressatName()
        {
            string login = _notification.UserLogin;

            try
            {
                CrmUserRepository userRep = ExternalSystems.CrmUserRepository.Get();
                Guid userGuid = userRep.GetGuidByLogin(login);
                Person person = userRep.GetByGuid(userGuid);

                if (string.IsNullOrEmpty(person.Name))
                    _addressatName = login;

                _addressatName = person.Name;
            }
            catch
            {
                _addressatName = login;
            }
        }

        private void BuildBody()
        {
            _email.Body = new BodyGenerator(this).CreateBody();
        }

        class BodyGenerator
        {
            private EmailGenerator _parent;
            private StringBuilder _message;

            public BodyGenerator(EmailGenerator parent)
            {
                _parent = parent;
                _message = new StringBuilder();
            }

            public string CreateBody()
            {
                AppendHeader();
                AppendBodyTop();
                AppendTableHeader();

                foreach (var taskNotif in _parent._notification.Notifications)
                    AppendNotifRow(taskNotif);

                AppendFooter();

                return _message.ToString();
            }

            private void AppendHeader()
            {
                _message.Append("<html>");
                _message.Append("<head>");
                _message.Append("<style type=\"text/css\">");
                _message.Append("body { font-family: Tahoma, Arial, Helvetica, sans-serif; font-size: 10pt; color: #333333;}");
                _message.Append(@"table { font-family: Tahoma, Arial, Helvetica, sans-serif; font-size: 10pt; color: #333333; table-layout: fixed;}");
                _message.Append(@" table.tblTasks td {
                                       border-width: 1px;
                                       border-bottom: 1px solid gray;                                                                                                         
                                       font-family: Tahoma;
                                       font-size: 12px;
                                       padding: 2px 7px 2px 10px;   
                                   }
                                   table.tblTasks thead td {
                                       border-width: 1px;
                                       padding: 1px;
                                       border-bottom: 1px solid gray;                                                                       
                                       border-top: 1px solid gray;  
                                       font-weight: bold;
                                       text-align:center;    
                                       font-family: Tahoma;
                                       font-size: 12px;                                                                                      
                                   }
                                   .leftBorder
                                   {
                                       border-left: 1px solid gray;  
                                   }
                                   .rightBorder
                                   {
                                       border-right: 1px solid gray;  
                                   }
                                   .rightPadding
                                   {
                                       text-align:right;
                                   }
                                    .cellNumber 
                                    {
                                        /*width: 75px;*/
                                    }
                                    .cellName 
                                    {
                                       /* width: 300px;*/
                                    }
                                    .cellImportant
                                    {
                                        /*width: 100px;*/
                                        color: red;
                                    }
                                    .cellOverdue
                                    {
                                        /*width: 100px;*/
                                    }
                                    .cellValue
                                    {
                                        padding-left: 10px;
                                    }
                                    ");

                _message.Append(".style1 { color: #666666; font-family: Tahoma, Arial, Helvetica, sans-serif; font-size: 8pt; }");
                _message.Append("</style>");
                _message.Append("</head>");

            }

            private void AppendBodyTop()
            {
                _message.Append("<body bgcolor=\"#F9F9F9\" text=\"#333333\" link = \"#003399\" vlink = \"#003399\" alink = \"#FF0000\">");
                _message.Append("<p>" + _parent._addressatName + ", здравствуйте!</p>");
                _message.Append("<p>Требуется Ваше согласование по указанным ниже договорам:</p>");
            }

            private void AppendTableHeader()
            {
                _message.Append(@"
                                <table class='tblTasks' style='table-layout: fixed; display: inline;' cellspacing='0' cellpadding='3'>
                                <thead>
                                    <tr>
                                        <td style='width: 75px; border-left: 1px solid gray;'>№ заявки</td>
                                        <td style='width: 300px; border-left: 1px solid gray;'>Название</td>
                                        <td style='width: 100px; border-left: 1px solid gray;'>Тип</td>
                                        <td style='width: 140px; border-left: 1px solid gray;'>Дата отправки</td>
                                        <td style='width: 140px; border-left: 1px solid gray;'>Срок</td>
                                        <td style='width: 100px; border-left: 1px solid gray;' class='rightBorder'>Просрочено на</td>
                                    </tr>
                                </thead>
                                <tbody>");
            }

            private void AppendNotifRow(TaskNotification taskNotif)
            {
                _message.Append("<tr>");
                _message.AppendFormat("<td style='width: 75px; border-left: 1px solid gray;'>{0}</td>", taskNotif.TaskId.ToString());
                _message.AppendFormat("<td style='width: 300px; border-left: 1px solid gray;'><a href='{0}'>{1}</a></td>", taskNotif.TaskUrl, taskNotif.Name);
                string taskTypeImportant = taskNotif.TaskTypeIsImportant ? " class='cellImportant' " : "";
                _message.AppendFormat("<td style='width: 100px; border-left: 1px solid gray;' {0}>{1}</td>", taskTypeImportant, taskNotif.TaskType);
                _message.AppendFormat("<td style='width: 140px; border-left: 1px solid gray;'>{0}</td>", taskNotif.StartOfApproval);
                _message.AppendFormat("<td style='width: 140px; border-left: 1px solid gray;' class='cellImportant'>{0}</td>", taskNotif.Deadline);
                _message.AppendFormat("<td style='width: 100px; border-left: 1px solid gray;' class='rightBorder'>{0}</td>", taskNotif.OverdueTime);
                _message.Append("</tr>");
            }

            private void AppendFooter()
            {
                _message.Append("</tbody></table>");

                string urlToCrm = ExternalSystems.CrmSystem.Get().GetSystemUrl();
                _message.Append("<BR>_______________________________________________________________________");
                _message.AppendFormat("<BR><span class=\"style1\"><i>Автоматическая служба системы поддержки продаж «<a href=\"{0}/\">Altus</a>».</i></span><BR>", urlToCrm);
                _message.Append("<span class=\"style1\"><i>Внимание! Данное письмо было отправлено автоматически. Пожалуйста, не отвечайте на него.</i></span>");

                _message.Append("</body>");
                _message.Append("</html>");
            }
        }
    }
}
