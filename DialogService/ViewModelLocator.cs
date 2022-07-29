using System;
using Autofac;

namespace DialogService
{
    public class ViewModelLocator
    {
        private ILifetimeScope? _scope;

        public void SetLifetimeScope(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public MainViewModel MainViewModel => Resolve<MainViewModel>();

        private T Resolve<T>() where T : notnull
        {
            if (_scope == null) throw new NullReferenceException();
            return _scope.Resolve<T>();
        }
    }
}