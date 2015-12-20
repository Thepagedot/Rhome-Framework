﻿using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thepagedot.Rhome.Demo.Shared.Services;
using Thepagedot.Rhome.Demo.Shared.ViewModels;
using Thepagedot.Rhome.Demo.UWP.Services;

namespace Thepagedot.Rhome.Demo.UWP
{
    public class Bootstrapper
    {
        public Bootstrapper()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IResourceService, ResourceService>();
            SimpleIoc.Default.Register<ISettingsService, SettingsService>();

            SimpleIoc.Default.Register<HomeControlService>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<RoomViewModel>();
        }

        public HomeControlService HomeControlService { get { return SimpleIoc.Default.GetInstance<HomeControlService>(); } }

        public MainViewModel MainViewModel { get { return SimpleIoc.Default.GetInstance<MainViewModel>(); }}
        public RoomViewModel RoomViewModel { get { return SimpleIoc.Default.GetInstance<RoomViewModel>(); }}
    }
}