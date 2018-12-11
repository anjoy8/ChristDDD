namespace Christ3D.Application.EventSourcedNormalizers
{
    /// <summary>
    /// Student 历史记录模型
    /// 用事件溯源
    /// </summary>
    public class StudentHistoryData
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BirthDate { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }
}