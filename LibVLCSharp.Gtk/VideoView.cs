using System;
using System.Runtime.InteropServices;
using Gdk;
using Gtk;
using LibVLCSharp.Shared;
using Object = Gtk.Object;

namespace LibVLCSharp.GTK
{
    public class VideoView : DrawingArea, IVideoView
    {
        private MediaPlayer _mediaPlayer;

        public VideoView(MediaPlayer mediaPlayer = null)
        {
            this._mediaPlayer = mediaPlayer;
            Color black = Color.Zero;
            Color.Parse("black", ref black);
            this.ModifyBg(StateType.Normal, black);

            this.Realized += (s, e) => { this.Attach(); };
        }

        public MediaPlayer MediaPlayer
        {
            get
            {
                return this._mediaPlayer;
            }
            set
            {
                if (Object.ReferenceEquals(this._mediaPlayer, value))
                {
                    return;
                }

                this.Detach();
                this._mediaPlayer = value;
                this.Attach();
            }
        }

        void Attach()
        {
            if (!this.IsRealized || this._mediaPlayer == null)
            {
                return;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                MediaPlayer.Hwnd = NativeReferences.gdk_win32_drawable_get_handle(this.GdkWindow.Handle);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                MediaPlayer.XWindow = NativeReferences.gdk_x11_drawable_get_xid(this.GdkWindow.Handle);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                MediaPlayer.NsObject = NativeReferences.gdk_quartz_window_get_nsview(GdkWindow.Handle);
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
        }

        void Detach()
        {
            if (!this.IsRealized || this._mediaPlayer == null)
            {
                return;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                MediaPlayer.Hwnd = IntPtr.Zero;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                MediaPlayer.XWindow = 0;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                MediaPlayer.NsObject = IntPtr.Zero;
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
        }

        public override void Dispose()
        {
            Detach();
            base.Dispose();
        }
    }
}
