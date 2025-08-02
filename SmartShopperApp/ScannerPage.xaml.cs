using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace SmartShopperApp;

public partial class ScannerPage : ContentPage
{
    public ScannerPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (DeviceHelper.IsSimulator())
        {
            resultLabel.Text = "Camera not available in simulator.\nType a barcode or click Simulate.";
            FakeScanButton.IsVisible = true;
        }
        else
        {
            // Dynamically create the camera view
            var cameraView = new CameraBarcodeReaderView
            {
                IsDetecting = true,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };

            cameraView.BarcodesDetected += OnBarcodesDetected;

            // Add as first child in the grid
            (Content as Grid)?.Children.Insert(0, cameraView);

            FakeScanButton.IsVisible = false;
        }
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

    private async void OnFakeScanClicked(object sender, EventArgs e)
    {
        string fakeBarcode = await DisplayPromptAsync(
            "Simulate Scan",
            "Enter a barcode value to simulate:",
            initialValue: "1234567890123");

        if (!string.IsNullOrWhiteSpace(fakeBarcode))
        {
            resultLabel.Text = $"Simulated Scan: {fakeBarcode}";
        }
    }
} // 👈 make sure this closing brace is theress