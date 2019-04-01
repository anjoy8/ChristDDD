using System;
using System.Collections.Generic;
using System.Linq;
using Christ3D.Domain.Core.Events;
using Newtonsoft.Json;

namespace Christ3D.Application.EventSourcedNormalizers
{
    /// <summary>
    /// 事件溯源规范化
    /// </summary>
    public class StudentHistory
    {
        public static IList<StudentHistoryData> HistoryData { get; set; }

        // 将数据从事件源中获取到list中
        public static IList<StudentHistoryData> ToJavaScriptStudentHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<StudentHistoryData>();
            StudentHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<StudentHistoryData>();
            var last = new StudentHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new StudentHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name
                        ? ""
                        : change.Name,
                    Email = string.IsNullOrWhiteSpace(change.Email) || change.Email == last.Email
                        ? ""
                        : change.Email,
                    Phone = string.IsNullOrWhiteSpace(change.Phone) || change.Phone == last.Phone
                        ? ""
                        : change.Phone,
                    BirthDate = string.IsNullOrWhiteSpace(change.BirthDate) || change.BirthDate == last.BirthDate
                        ? ""
                        : change.BirthDate.Substring(0,10),
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    When = change.When,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        /// <summary>
        /// 将事件源进行反序列化
        /// </summary>
        /// <param name="storedEvents"></param>
        private static void StudentHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var slot = new StudentHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "StudentRegisteredEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.BirthDate = values["BirthDate"];
                        slot.Email = values["Email"];
                        slot.Phone = values["Phone"];
                        slot.Name = values["Name"];
                        slot.Action = "Registered";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "StudentUpdatedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.BirthDate = values["BirthDate"];
                        slot.Email = values["Email"];
                        slot.Phone = values["Phone"];
                        slot.Name = values["Name"];
                        slot.Action = "Updated";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "StudentRemovedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Action = "Removed";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                }
                HistoryData.Add(slot);
            }
        }
    }
}