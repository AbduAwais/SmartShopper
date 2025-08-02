using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace SmartShopperApp;

public partial class ScannerPage : ContentPage
{
    public ScannerPage()
    {
        InitializeComponent();
    }

    private void OnBarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            var firstBarcode = e.Results?.FirstOrDefault();
            if (firstBarcode != null)
            {
                resultLabel.Text = $"Scanned: {firstBarcode.Value}";
            }
        });
    }
}