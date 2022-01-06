using Android.App;
using Android.OS;
using Android.Text;
using Android.Widget;
using AndroidApp;
using AndroidX.AppCompat.App;

[Activity(
    Label = "@string/app_name",
    Theme = "@style/AppTheme.NoActionBar",
    MainLauncher = true)]
public class MainActivity : AppCompatActivity
{
    EditText input;
    TextView clipboardContent;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        SetContentView(Resource.Layout.activity_main);

        var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
        SetSupportActionBar(toolbar);
    }

    protected override void OnResume()
    {
        base.OnResume();

        input = FindViewById<EditText>(Resource.Id.input);
        clipboardContent = FindViewById<TextView>(Resource.Id.clipboardContent);

        input.TextChanged += Input_TextChanged;
    }

    void Input_TextChanged(object sender, TextChangedEventArgs e)
    {
        TextCopy.ClipboardService.SetText(e.Text.ToString());

        clipboardContent.Text = TextCopy.ClipboardService.GetText();
    }
}