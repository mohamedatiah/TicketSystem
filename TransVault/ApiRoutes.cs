namespace FutureWorkshopTicketSystem
{
    public static class ApiRoutes
    {
        private const string Root = "api";

        // Routes
        public const string ControllerRoute = Root + "/[controller]";
        // public const string Repositories = Root + "/Repositories";
        // public const string Repository = Repositories + "/{repositoryName}";
        public const string Documents = "/Documents";
        public const string IndexedField = "{documentId:int}";
        public const string Page = "/Page";
        public const string DropDownValues = "/DropDownValues";
        public const string TransValultRepository = "/TransValultRepository";
        public const string SearchIndexedField = "Search";
        public const string AddIndexedField = "Add";
        public const string UploadPage = "Upload";




    }

}
