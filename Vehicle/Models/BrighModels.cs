namespace Vehicle.Models
{
    public class BrighModels
    {
    }

    public class EmailModel
    {
        public string Email { get; set; }
    }

    public class ConfirmModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }

    public class ChangePasswordModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RecoverMailModel
    {
        public string MessageContent { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
    }
}
