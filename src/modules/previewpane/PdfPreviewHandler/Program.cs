// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using System.Globalization;
using System.Windows.Threading;
using Common.UI;
using interop;

namespace Microsoft.PowerToys.PreviewHandler.Pdf
{
    internal static class Program
    {
        private static CancellationTokenSource _tokenSource = new CancellationTokenSource();

        private static PdfPreviewHandlerControl _previewHandlerControl;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();
            if (args != null)
            {
                if (args.Length == 6)
                {
                    string filePath = args[0];
                    int hwnd = Convert.ToInt32(args[1], 16);

                    Rectangle s = default(Rectangle);
                    int left = Convert.ToInt32(args[2], 10);
                    int right = Convert.ToInt32(args[3], 10);
                    int top = Convert.ToInt32(args[4], 10);
                    int bottom = Convert.ToInt32(args[5], 10);

                    _previewHandlerControl = new PdfPreviewHandlerControl();
                    _previewHandlerControl.SetWindow((IntPtr)hwnd, s);
                    _previewHandlerControl.DoPreview(filePath);

                    NativeEventWaiter.WaitForEventLoop(
                        Constants.PdfPreviewResizeEvent(),
                        () =>
                        {
                            Rectangle s = default(Rectangle);
                            _previewHandlerControl.SetRect(s);
                        },
                        Dispatcher.CurrentDispatcher,
                        _tokenSource.Token);
                }
                else
                {
                    MessageBox.Show("Wrong number of args: " + args.Length.ToString(CultureInfo.InvariantCulture));
                }
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.Run();
        }
    }
}
