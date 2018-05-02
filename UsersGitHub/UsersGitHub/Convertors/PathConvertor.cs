﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace UsersGitHub.Convertors
{
    class PathConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ImageSource.FromFile((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
