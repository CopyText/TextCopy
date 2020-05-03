using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using AndroidApp;

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
        base.OnPause();

        input = FindViewById<EditText>(Resource.Id.input);
        clipboardContent = FindViewById<TextView>(Resource.Id.clipboardContent);

        input.TextChanged += Input_TextChanged;
    }

    void Input_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
    {
        TextCopy.Clipboard.SetText(e.Text.ToString());

        clipboardContent.Text = TextCopy.Clipboard.GetText();
    }
}