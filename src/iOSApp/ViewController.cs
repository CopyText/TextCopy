using CoreGraphics;
using System;
using UIKit;

namespace iOSApp
{
    public partial class ViewController : UIViewController
    {
        UITextField input;
        UITextView clipboardContent;

        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            clipboardContent = new UITextView(View.Bounds.Inset(5, 40)) { Text = "Type something" };
            input = new UITextField(View.Bounds.Inset(10, 40)) { Placeholder = "Write clipboard content here" };
            input.EditingChanged += Input_EditingChanged;

            Add(clipboardContent);
            Add(input);
        }

        private void Input_EditingChanged(object sender, EventArgs e)
        {
            TextCopy.ClipboardService.SetText(input.Text);
            clipboardContent.Text = TextCopy.ClipboardService.GetText();
        }
    }
}