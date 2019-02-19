using Ninject;
using Ninject.Modules;

namespace StartPageEditor.Helpers
{
  /// <summary>
  /// IocKernel
  /// </summary>
  public static class IocKernel
  {
    private static StandardKernel _kernel;

    /// <summary>
    /// Gets this instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T Get<T>()
    {
      return _kernel.Get<T>();
    }

    /// <summary>
    /// Initializes the specified modules.
    /// </summary>
    /// <param name="modules">The modules.</param>
    public static void Initialize(params INinjectModule[] modules)
    {
      if (_kernel == null)
      {
        _kernel = new StandardKernel(modules);
      }
    }
  }
}
