using System.Collections.Generic;

namespace Arcstone.Demo.Client.Models
{
    public class ResponseApi<T>
    {
        public ResponseApi()
        {
            ErrorMessage = new List<string>();
        }

        public T Result { set; get; }
        public List<string> ErrorMessage { set; get; }
    }
}