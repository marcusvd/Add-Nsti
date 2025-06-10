using Newtonsoft.Json;

namespace Domain.Entities.GlobalSystem
{
    public class GlobalErrorHandling
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Trace { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }

    
}