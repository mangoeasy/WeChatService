namespace WeChatService.Service
{
    public class BaseService
    {
        public readonly WeChatServiceDataContext DbContext;

        public BaseService(WeChatServiceDataContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
