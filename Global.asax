<%@ Application Language="C#" %>

<script RunAt="server">


    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup


        Hangfire.JobStorage.Current = new Hangfire.SqlServer.SqlServerStorage(ConfigurationManager.AppSettings["hangfireConnect"].ToString());
        Hangfire.GlobalJobFilters.Filters.Add(new LogEverythingAttribute());

        var server = new Hangfire.BackgroundJobServer();

        Hangfire.Common.JobHelper.SetSerializerSettings(new Newtonsoft.Json.JsonSerializerSettings
        {
            TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All
        });
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
