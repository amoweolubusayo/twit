namespace Twit.Core.Utils
{
    public class GenericResponse<T>
    {
      
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public GenericResponse()
        {
        }

        public GenericResponse(bool status, string message)
        {
            Status = status;
            Message = message;
        }

        public GenericResponse(bool status, T data)
        {
            Status = status;
            Data = data;
        }

        public GenericResponse(bool status, string message, T data)
        {
            Status  = status;
            Message = message;
            Data = data;
        }
    }

     public class GenericResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public GenericResponse()
        {
        }

        public GenericResponse(bool status, string message)
        {
            Status = status;
            Message = message;
        }
    }

    
}