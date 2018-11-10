using LibVLCSharp.Forms.Platforms.Gtk;
using LibVLCSharp.Forms.Shared;
using LibVLCSharp.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

[assembly: ExportRenderer(typeof(LibVLCSharp.Forms.Shared.VideoView), typeof(VideoViewRenderer))]
namespace LibVLCSharp.Forms.Platforms.Gtk
{
    public class VideoViewRenderer : ViewRenderer<LibVLCSharp.Forms.Shared.VideoView, LibVLCSharp.Gtk.VideoView>
    {
        LibVLCSharp.Gtk.VideoView _videoView;

        protected override void OnElementChanged(ElementChangedEventArgs<VideoView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                _videoView = new LibVLCSharp.Gtk.VideoView();
                SetNativeControl(_videoView);
            }

            if (e.OldElement != null)
            {
                e.OldElement.MediaPlayerChanged -= OnMediaPlayerChanged;
            }

            if (e.NewElement != null)
            {
                e.NewElement.MediaPlayerChanged += OnMediaPlayerChanged;
            }
        }

        private void OnMediaPlayerChanged(object sender, MediaPlayerChangedEventArgs e)
        {
            if (Control == null)
            {
                return;
            }
            Control.MediaPlayer = e.NewMediaPlayer;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _videoView?.Dispose();
        }
    }
}