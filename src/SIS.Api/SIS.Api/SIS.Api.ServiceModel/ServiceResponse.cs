
namespace SIS.Api.ServiceModel
{
    public class CommandResponse
    {
        public Status Status { get; set; }
        public string ErrorMessage { get; set; }
    }
    public enum Status { OK = 1, Error = 2 }
}