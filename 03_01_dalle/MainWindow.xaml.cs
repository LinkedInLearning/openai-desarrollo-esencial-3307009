using Azure.AI.OpenAI;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dalle;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private OpenAIClient client;
    public MainWindow()
    {
        InitializeComponent();
        client = new OpenAIClient(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
        GenerationProgressBar.Visibility = Visibility.Hidden;
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        var options = new ImageGenerationOptions(PromptTextBox.Text);
        GenerationProgressBar.Visibility = Visibility.Visible;
        var response = await client.GetImageGenerationsAsync(options);
        LoadImageFromUrl(response.Value.Data.First().Url);
        GenerationProgressBar.Visibility = Visibility.Hidden;
    }

    private void LoadImageFromUrl(Uri imageUri)
    {
        try
        {
            BitmapImage bitmap = new();
            bitmap.BeginInit();
            bitmap.UriSource = imageUri;
            bitmap.EndInit();

            GeneratedImage.Source = bitmap;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading image: " + ex.Message);
        }
    }
}