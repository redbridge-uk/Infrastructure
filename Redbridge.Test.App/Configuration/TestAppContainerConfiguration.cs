namespace Redbridge.Test.App.Configuration
{
    public abstract class TestAppContainerConfiguration : ContainerConfiguration
    {
        protected TestAppContainerConfiguration ()
        {
            AddAssembly(typeof(TestAppContainerConfiguration).Assembly);
        }
    }
}
