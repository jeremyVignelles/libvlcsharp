﻿using LibVLCSharp.Shared;
using System;
using System.Windows;
using System.Windows.Controls;

namespace LibVLCSharp.WPF.Sample
{
    public partial class Controls : UserControl
    {
        readonly Example1 parent;
        LibVLC _libVLC;
        MediaPlayer _mediaPlayer;

        public Controls(Example1 Parent)
        {
            parent = Parent;

            InitializeComponent();

            // we need the VideoView to be fully loaded before setting a MediaPlayer on it.
            parent.VideoView.Loaded += VideoView_Loaded;
            parent.VideoView.Unloaded += VideoView_Unloaded;
            PlayButton.Click += PlayButton_Click;
            StopButton.Click += StopButton_Click;
        }

        private void VideoView_Loaded(object sender, RoutedEventArgs e)
        {
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);

            parent.VideoView.MediaPlayer = _mediaPlayer;
        }

        private void VideoView_Unloaded(object sender, RoutedEventArgs e)
        {
            _mediaPlayer.Dispose();
            _libVLC.Dispose();
        }

        void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (parent.VideoView.MediaPlayer.IsPlaying)
            {
                parent.VideoView.MediaPlayer.Stop();
            }
        }

        void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (!parent.VideoView.MediaPlayer.IsPlaying)
            {
                var uri = new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4");
                parent.VideoView.MediaPlayer.Play(new Media(_libVLC, uri));
            }
        }
    }
}
