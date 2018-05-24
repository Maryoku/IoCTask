using IocExample.Classes;

namespace IocExample
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    var logger = new ConsoleLogger();
        //    var sqlConnectionFactory = new SqlConnectionFactory("SQL Connection", logger);
        //    var createUserHandler = new CreateUserHandler(new UserService(new QueryExecutor(sqlConnectionFactory), new CommandExecutor(sqlConnectionFactory), new CacheService(logger, new RestClient("API KEY"))), logger);

        //    createUserHandler.Handle();
        //}

        //static void Main(string[] args)
        //{
        //    IKernel kernel = new StandardKernel();

        //    kernel.Bind<ILogger>().To<ConsoleLogger>();
        //    kernel.Bind<UserService>().To<UserService>();
        //    kernel.Bind<QueryExecutor>().To<QueryExecutor>();
        //    kernel.Bind<CommandExecutor>().To<CommandExecutor>();
        //    kernel.Bind<CacheService>().To<CacheService>();

        //    kernel.Bind<RestClient>()
        //        .ToConstructor(k => new RestClient("API_KEY"));

        //    kernel.Bind<IConnectionFactory>()
        //        .ToConstructor(k => new SqlConnectionFactory("SQL Connection", k.Inject<ILogger>()))
        //        .InSingletonScope();

        //    kernel.Bind<CreateUserHandler>().To<CreateUserHandler>();

        //    var createUserHandler = kernel.Get<CreateUserHandler>();

        //    createUserHandler.Handle();
        //}

        static void Main(string[] args)
        {
            Kernel kernel = new Kernel();

            kernel.BindTypeToType(typeof(ILogger), typeof(ConsoleLogger));
            kernel.BindObjectToType(typeof(RestClient), new RestClient("API_KEY"));
            kernel.BindTypeToType(typeof(CacheService), typeof(CacheService));

            kernel.BindObjectToType(typeof(IConnectionFactory), new SqlConnectionFactory("SQL Connection", kernel.GetObject<ILogger>()));

            kernel.BindTypeToType(typeof(CommandExecutor), typeof(CommandExecutor));
            kernel.BindTypeToType(typeof(QueryExecutor), typeof(QueryExecutor));
            kernel.BindTypeToType(typeof(UserService), typeof(UserService));

            kernel.BindTypeToType(typeof(CreateUserHandler), typeof(CreateUserHandler));

            var createUserHandler = kernel.GetObject<CreateUserHandler>();

            createUserHandler.Handle();
        }
    }
}
