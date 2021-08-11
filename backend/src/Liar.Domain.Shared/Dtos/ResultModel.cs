namespace Liar.Domain.Shared.Dtos
{
    public class ResultModel
    {
        public int code { get; set; }
        public bool success { get; set; }
        public object result { get; set; }
        public string message { get; set; }
    }
}
