using Redbridge.DependencyInjection;
using Redbridge.Forms;
using System.Linq;
using System;
using System.Reflection;
using Redbridge.Configuration;
using Redbridge.Diagnostics;
using System.Threading;
using Redbridge.Forms.Markup;
using System.Collections.Generic;
using Redbridge.Forms.Navigation;

public abstract class ContainerConfiguration
{
    private ILogger _logger;
    public readonly IApplicationSettingsRepository ApplicationSettings;
    private readonly Lazy<IContainer> _container;
    private List<Assembly> _assemblies = new List<Assembly>();

    protected ContainerConfiguration()
    {
        _container = new Lazy<IContainer>(GetContainer, LazyThreadSafetyMode.PublicationOnly); // Trial for resolving the 'Value' is null
    }

    public IContainer Container { get { return _container.Value; } }

    protected IContainer GetContainer()
    {
        var container = CreateContainer();
        OnRegisterLogger(container);
        OnRegisterApplicationSettings(container);
        OnRegisterTypes(container);
        return container;
    }

    protected void AddAssembly(Assembly assembly)
    {
        if (assembly == null) { throw new ArgumentNullException(nameof(assembly)); }
        _assemblies.Add(assembly);
    }

    public ILogger Logger => _logger;

    protected abstract ILogger CreateLogger();
    protected abstract IApplicationSettingsRepository CreateAppSettingsRepository();

    protected abstract IContainer CreateContainer();

    protected virtual void OnRegisterLogger(IContainer container)
    {
        _logger = CreateLogger();
        container.RegisterInstance(_logger);
    }

    protected virtual void OnRegisterApplicationSettings(IContainer container)
    {
        container.RegisterInstance(CreateAppSettingsRepository());
    }

    protected virtual void OnRegisterTypes(IContainer container)
    {
        container.RegisterInstance(container);
        container.RegisterType<IViewModelFactory, ViewModelFactory>(LifeTime.Container);
        container.RegisterType<IViewFactory, ViewFactory>(LifeTime.Container);
        container.RegisterType<ICurrentPageService, XamarinAppCurrentPageService>(LifeTime.Container);
        container.RegisterType<INavigationService, NavigationService>(LifeTime.Container);
        container.RegisterType<IAlertController, AlertController>(LifeTime.Container);
        container.RegisterType<IIconResourceMapper, DefaultIconMapper>(LifeTime.Container);
        container.RegisterType<IActionSheetController, ActionSheetController>(LifeTime.Container);
        OnAutoRegister(container);
        OnRegisterCells(container);
        OnRegisterViews(container);
    }

    public void OnAutoRegister(IContainer container)
    {
        OnAutoRegisterViews(container);
        OnAutoRegisterCellFactories(container);
    }

    protected virtual TableCellRegistrationConfiguration CreateTableCellRegistration()
    {
        return new TableCellRegistrationConfiguration();
    }

    protected virtual void OnRegisterCells(IContainer container)
    {
        var registration = CreateTableCellRegistration();
        registration.Configure(container);
    }

    protected virtual void OnRegisterViews(IContainer container)
    {
        OnAutoRegisterViews(container);
    }

    protected void OnAutoRegisterViews(IContainer container)
    {
        var assembly = Assembly.GetExecutingAssembly();
        RegisterAssemblyViews(container, assembly);
        _assemblies.ForEach(a => RegisterAssemblyViews(container, a));
    }

    protected void OnAutoRegisterCellFactories(IContainer container)
    {
        var assembly = Assembly.GetExecutingAssembly();
        RegisterCellFactories(container, assembly);
        _assemblies.ForEach(a => RegisterCellFactories(container, a));
    }

    private void RegisterAssemblyViews (IContainer container, Assembly assembly)
    {
        var viewModelTypes = assembly.GetTypes().Where(t => t.GetCustomAttributes<ViewAttribute>().Any()).Select(t => new { Attribute = t.GetCustomAttributes<ViewAttribute>().First(), Type = t });

        foreach (var viewModelType in viewModelTypes)
            container.RegisterType<IView>(viewModelType.Attribute.ViewType, viewModelType.Type.Name);
    }

    private void RegisterCellFactories (IContainer container, Assembly assembly)
    {
        var cellFactoryTypes = assembly.GetTypes().Where(t => t.GetCustomAttributes<CellFactoryAttribute>().Any()).Select(t => new { Attribute = t.GetCustomAttributes<CellFactoryAttribute>().First(), Type = t });

        foreach (var cellFactoryType in cellFactoryTypes)
            container.RegisterType<ICellFactory>(cellFactoryType.Attribute.FactoryType, cellFactoryType.Attribute.Name);
    }

    protected void RegisterView<TPage, TViewModel>(LifeTime lifetime = LifeTime.Transient)
	    where TPage: IView
	    where TViewModel: IViewModel
    {
        Container.RegisterType<IView, TPage>(typeof(TViewModel).Name, lifetime);
    }

    protected void RegisterTableView<TViewModel> (LifeTime lifetime = LifeTime.Transient)
        where TViewModel : ITableViewModel
    {
        Container.RegisterType<IView, TableViewPage>(typeof(TViewModel).Name, lifetime);
    }
}