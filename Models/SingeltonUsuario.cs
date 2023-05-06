namespace Final_IngenieriaSoftware.Models
{
    public class SingeltonUsuario
    {
        private static SingeltonUsuario _instance = null;
        private static readonly object _lock = new object();
        public Votante votante { get; set; }

        private SingeltonUsuario()
        {

        }

        public static SingeltonUsuario Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SingeltonUsuario();
                    }
                    return _instance;
                }
            }
        }
        public static void Destroy()
        {
            _instance = null;
        }
    }
}
