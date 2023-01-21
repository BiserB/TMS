namespace TMS.Core.Entities
{
    public class UserTask
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }
    }
}
