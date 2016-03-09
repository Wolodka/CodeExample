using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.BL.OverdueNotif;
using IntraserviceTasks.CrmAPI.Constants;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Res;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using NLog;

namespace IntraserviceTasks.CrmAPI.CrmMail
{
    public class CrmMailSender : EmailSender
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public void Send(Email email)
        {
            Entity crmEmailEnitity = new Entity("email");

            // В отношении
            crmEmailEnitity.Attributes = new AttributeCollection();

            // Тема
            crmEmailEnitity.Attributes.Add("subject", email.Subject);
            // Содержание
            crmEmailEnitity.Attributes.Add("description", email.Body);

            Guid postId = GetPostId();

            // От кого будет отправляться уведомление
            EntityCollection sender = new EntityCollection();
            Entity from = new Entity("activityparty");
            from.Attributes["partyid"] = new EntityReference("systemuser", postId);
            sender.Entities.Add(from);

            crmEmailEnitity.Attributes["from"] = sender;

            // Кому будет отправляться
            if (email.Recipients.Count > 0)
                crmEmailEnitity.Attributes["to"] = GetRecipients(email.Recipients.Select(o=>o.CrmGuid).ToList());

            //cc
            EntityCollection ccTo = GetRecipients(email.CC.Select(o => o.CrmGuid).ToList());
            crmEmailEnitity.Attributes["cc"] = ccTo;

            //bcc
            crmEmailEnitity.Attributes["bcc"] = GetRecipients(
                new List<Guid> { ConstantsOverdueNotifier.KURAEV_USER_CRM_GUID });

            //create email
            var crmProxy = CrmService.GetProxy();
            Guid emailId = crmProxy.Create(crmEmailEnitity);

            //send email
            /*OrganizationRequest request = new OrganizationRequest() { RequestName = "SendEmail" };
            request["EmailId"] = emailId;
            request["TrackingToken"] = string.Empty;
            request["IssueSend"] = true;

            crmProxy.Execute(request);*/
        }

        private EntityCollection GetRecipients(List<Guid> userGuids)
        {
            List<Entity> recipients = new List<Entity>();

            foreach (Guid userId in userGuids)
            {
                Entity activity = new Entity("activityparty");
                activity.Attributes["partyid"] = new EntityReference("systemuser", userId);
                recipients.Add(activity);
            }

            return new EntityCollection(recipients);
        }

        private EntityCollection GetRecipientTeam(Guid teamGuid)
        {
            string fetchString = ""
                + "<fetch distinct=\"true\" mapping=\"logical\" output-format=\"xml-platform\" version=\"1.0\">"
                + "<entity name=\"systemuser\">"
                + "<attribute name=\"fullname\"/>"
                + "<attribute name=\"businessunitid\"/>"
                + "<attribute name=\"title\"/>"
                + "<attribute name=\"address1_telephone1\"/>"
                + "<attribute name=\"systemuserid\"/>"
                + "<order descending=\"false\" attribute=\"fullname\"/>"
                + "<link-entity name=\"teammembership\" intersect=\"true\" visible=\"false\" to=\"systemuserid\" from=\"systemuserid\">"
                + "<link-entity name=\"team\" to=\"teamid\" from=\"teamid\" alias=\"aa\">"
                + "<filter type=\"and\">"
                + "<condition attribute=\"teamid\" value=\"{" + teamGuid.ToString() + "}\" operator=\"eq\"/>"
                + "</filter>"
                + "</link-entity>"
                + "</link-entity>"
                + "</entity>"
                + "</fetch>";

            var crmService = CrmService.GetProxy();
            var crmTeamMembers = crmService.RetrieveMultiple(new FetchExpression(fetchString));

            EntityCollection recipients = GetRecipients(crmTeamMembers.Entities.Select(o=>o.Id).ToList());
            
            return recipients;
        }

        /// <summary>
        /// Получить идентификатор почтовой службы
        /// </summary>
        /// <param name="crmService">CRM-сервис</param>        
        public static Guid GetPostId()
        {
            QueryByAttribute qeParametr = new QueryByAttribute("ac_parametr");
            qeParametr.AddAttributeValue("ac_parametrtypecode", 9); // 9 - тип "Почтовая служба рассылки"
            qeParametr.ColumnSet = new ColumnSet("ac_value");

            var crmProxy = CrmService.GetProxy();
            var parametrs = crmProxy.RetrieveMultiple(qeParametr).Entities;
            foreach (var currentParament in parametrs)
            {
                if (currentParament.Contains("ac_value"))
                    return new Guid((string)currentParament["ac_value"]);
            }

            return Guid.Empty;
        }

    }
}
