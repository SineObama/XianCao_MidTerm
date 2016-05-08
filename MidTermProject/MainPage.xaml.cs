﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MidTermProject.ViewModels;
using MidTermProject.Models;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.ApplicationModel;
using Windows.Storage.Streams;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MidTermProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ItemViewModel vm;

        public MainPage()
        {
            this.InitializeComponent();
            Network.SYSUEncryptSupporter.init();
            vm = ItemViewModel.instance;
            table.ItemsSource = vm.week.column;
            oneday.ItemsSource = vm.day.row;
        }

        private void get_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GetPage), "");
        }

        async void dtm_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            //Models.TodoItem i = ViewModels.TodoItemViewModel.getInstance().SharedItem;
            DataPackage data = args.Request.Data;
            //data.Properties.Title = i.title;
            //data.SetText(i.description);
            DataRequestDeferral getFile = args.Request.GetDeferral();
            StorageFile file = await Package.Current.InstalledLocation.GetFileAsync("Assets\\background.jpg");
            data.Properties.Thumbnail = RandomAccessStreamReference.CreateFromFile(file);
            data.SetBitmap(RandomAccessStreamReference.CreateFromFile(file));
            getFile.Complete();
        }

        private void previous_Click(object sender, RoutedEventArgs e)
        {
            vm.showPreviousDay();
            oneday.ItemsSource = vm.day.row;
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            vm.showNextDay();
            oneday.ItemsSource = vm.day.row;
        }
    }

    class MyGridView : GridView
    {
        protected override void PrepareContainerForItemOverride(Windows.UI.Xaml.DependencyObject element, object item)
        {
            try
            {  // todo 错误处理
                TableRow _item = item as TableRow;
                if (_item == null)
                    throw new NullReferenceException("internal error");
                //if (_item.span != 0)
                    element.SetValue(VariableSizedWrapGrid.RowSpanProperty, (int)_item.span);
                //element.SetValue(BorderBrushProperty, );
                //SolidColorBrush.;
                //Windows.UI.Xaml.Media.Brush.;
                //Brush a = Background;
                //a.SetValue(ColorProperty, Windows.UI.Colors.Blue);
                base.PrepareContainerForItemOverride(element, item);
            }
            catch(Exception e) { App.debugMessage(e.Message); }
        }

    }
}
