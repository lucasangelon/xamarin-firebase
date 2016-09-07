using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Xamarin.Forms;

namespace firebase_test
{
	public partial class FirebaseChat : ContentPage
	{
		private IFirebaseClient firebaseClient = new FirebaseClient(new FirebaseConfig
		{
			AuthSecret = "EmgJFyoXiTxyCLru1mjzmAhix3DdjavF4FLRgnkl",
			BasePath = "https://xamarin-firebase.firebaseio.com"
		});

		private ObservableCollection<string> messages = new ObservableCollection<string>();

		public FirebaseChat()
		{
			InitializeComponent();
			Title = "Message";
			messagesView.ItemsSource = messages;
			ListenChatStream();
		}

		void SendMessage(object sender, EventArgs args)
		{
			AddMessage();
		}

		async void AddMessage()
		{
			var message = new Message(content.Text);
			await firebaseClient.PushAsync("messages/push", message);
			content.Text = "";

		}

		async void ListenChatStream()
		{
			await firebaseClient.OnAsync("messages", (sender, args, content) =>
			{
				messages.Add(args.Data);
			});
		}

		public class Message
		{
			public string body;

			public Message(string body)
			{
				this.body = body;
			}
		}
	}
}

