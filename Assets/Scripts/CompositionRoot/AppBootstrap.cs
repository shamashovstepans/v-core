using System;
using Core;
using Core.Features;
using Core.Utils;
using Features;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CompositionRoot
{
    internal class AppBootstrap : MonoBehaviour
    {
        private IDisposable _appScope;

        private void OnEnable()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Install<CoreInstaller>();
            containerBuilder.Install<CoreFeaturesInstaller>();
            containerBuilder.Install<FeaturesInstaller>();
            EntryPointsBuilder.EnsureDispatcherRegistered(containerBuilder);
            _appScope = containerBuilder.Build();
        }

        private void OnDisable()
        {
            _appScope?.Dispose();
            _appScope = null;
        }

        private void OnApplicationQuit()
        {
            _appScope?.Dispose();
            _appScope = null;
        }
    }
}
