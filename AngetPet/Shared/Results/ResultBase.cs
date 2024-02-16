namespace AngetPet.Shared.Helpers
{
    public class ResultBase<T>
    {
        public T? Resource { get; set; }
        public string? Message { get; set; }
        public bool Success { get; set; }
        public bool Redirect { get; set; }

        private ResultBase(T resouce)
        {
            Resource = resouce;
            Success = true;
            Redirect = false;
        }

        public static ResultBase<T> COMPLET_RESULT(T resource)
        {
            return new ResultBase<T>(resource);
        }

        private ResultBase(string message)
        {
            Message = message;
            Success = false;
            Redirect = false;
        }

        private ResultBase()
        {
            Success = false;
            Redirect = true;
        }

        public static ResultBase<T> CREATE_CATCH(string e)
        {
            return new ResultBase<T>("Ocurrio un error durante el proceso de creación: " + e);
        }

        public static ResultBase<T> UPDATE_CATCH(string e)
        {
            return new ResultBase<T>("Ocurrio un error durante el proceso de actualización: " + e);
        }

        public static ResultBase<T> REMOVE_CATCH(string e)
        {
            return new ResultBase<T>("Ocurrio un error durante el proceso de eliminación: " + e);
        }

        public static ResultBase<T> NOT_FOUND(string e)
        {
            return new ResultBase<T>(e);
        }

        public static ResultBase<T> NOT_VALID(string e)
        {
            return new ResultBase<T>(e);
        }

        public static ResultBase<T> NOT_AUTHENTICATION()
        {
            return new ResultBase<T>();
        }
    }
}
