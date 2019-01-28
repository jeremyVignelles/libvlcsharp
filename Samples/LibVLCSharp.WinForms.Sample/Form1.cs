using LibVLCSharp.Shared;
using System;
using System.IO;
using System.Windows.Forms;

namespace LibVLCSharp.WinForms.Sample
{
    public partial class Form1 : Form
    {
        LibVLC _libVLC;
        VideoView _videoView;
        MediaPlayer _mp;

        public Form1()
        {
            if (!DesignMode)
            {
                Core.Initialize(Directory.GetParent(typeof(LibVLC).Assembly.Location).Parent.FullName);
            }

            _videoView = new VideoView();
            ((System.ComponentModel.ISupportInitialize)_videoView).BeginInit();
            SuspendLayout();

            _libVLC = new LibVLC();
            _mp = new MediaPlayer(_libVLC);
            Load += Form1_Load;

            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Text = "LibVLCSharp.WinForms";
            _videoView.Dock = DockStyle.Fill;
            Controls.Add(_videoView);
            _videoView.MediaPlayer = _mp;

            ((System.ComponentModel.ISupportInitialize)_videoView).EndInit();
            ResumeLayout(false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _mp.Play(new Media(_libVLC, "http://www.quirksmode.org/html5/videos/big_buck_bunny.mp4", Media.FromType.FromLocation));
        }
    }
}